using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SnackShack.Api;
using SnackShack.Api.Data;
using SnackShack.Model;

namespace SnackShack
{
    internal class App
    {
        #region Private Members
        private string ORDER_REGEX = @"^sandwich(\s*\d{2}:\d{2})?$";
        private IScheduler scheduler;
        private IOrderFactory orderFactory;
        #endregion

        public App(IScheduler scheduler, IOrderFactory orderFactory)
        {
            if (scheduler == null)
                throw new ArgumentNullException(nameof(scheduler));

            if (orderFactory == null)
                throw new ArgumentNullException(nameof(orderFactory));

            this.scheduler = scheduler;
            this.orderFactory = orderFactory;
        }

        public void Run()
        {
            DisplayInitialGreeting();

            OrderLoop();

            DisplayGoodbye();
        }

        private void DisplayInitialGreeting()
        {
            Console.WriteLine("Welcome to the Snack Shack!");
            Console.WriteLine("The best snack joint in 50 miles!");
            Console.WriteLine();
            Console.WriteLine("To order type the following: item [Placed Time]");
            Console.WriteLine("Placed Time Format: 00:00");
            Console.WriteLine();
        }

        private void DisplayGoodbye()
        {
            Console.WriteLine("Thank you for choosing Snack Shack!");
            Console.WriteLine("Have a wonderful day!");
        }

        private void OrderLoop()
        {
            var done = false;
            while (!done)
            {
                var order = GetOrder("What can I get you? ", "I'm sorry! Please repeat your order. ", "Thank you for your order!  I'll get it right away.");

                var placedOrder = this.orderFactory.Get(order.Item, order.Placed);

                if(PlaceOrder(placedOrder))
                    DisplaySchedule(this.scheduler.Create());

                done = GetInput("Can I get you anything else (y/n)? ", BoolValidator, InvertedBoolTransformer);
            }

            Console.WriteLine();
        }

        #region Private Methods
        private bool PlaceOrder(IOrder placedOrder)
        {
            var result = true;
            try
            {
                this.scheduler.Add(placedOrder);
            }
            catch (WaitTimeTooLongException)
            {
                Console.WriteLine("I'm sorry! I can't fulfill your order due to time constraints.");
                Console.WriteLine();
                result = false;
            }

            return result;
        }

        private static void DisplaySchedule(ISchedule schedule)
        {
            Console.WriteLine("Current Schedule:");
            Console.WriteLine();

            foreach (var task in schedule.Tasks)
                Console.WriteLine("{0:mm\\:ss} {1}", task.Start, task.Name);
            Console.WriteLine();
        }

        private (string Item, TimeSpan Placed) GetOrder(string prompt, string invalidMessage, string validMessage)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            (string Item, TimeSpan Placed) order;
            if(!OrderValidator(input))
                order = GetInput(invalidMessage, OrderValidator, OrderTransformer);
            else
                order = OrderTransformer(input);

            Console.WriteLine(validMessage);
            Console.WriteLine();

            return order;
        }

        private T GetInput<T>(string prompt, Func<string, bool> inputValidator, Func<string, T> inputTransformer)
        {
            string input = null;
            var done = false;

            while (!done)
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                done = inputValidator(input);
            }

            return inputTransformer(input);
        }

        /// <summary>
        /// Checks that the provided input is an <see cref="int"/>.
        /// </summary>
        /// <param name="input">The string to check.</param>
        /// <returns><see langword="true"/> if the string is an int, otherwise <see langword="false"/>.</returns>
        private bool IntValidator(string input) => int.TryParse(input, out var result);

        /// <summary>
        /// Transforms the provided input to an <see cref="int"/>.
        /// </summary>
        /// <param name="input">The string to transform.</param>
        /// <returns>An int from the input.</returns>
        private int IntTransformer(string input) => int.Parse(input);

        /// <summary>
        /// Checks that the provided input is an <see cref="bool"/>.
        /// </summary>
        /// <param name="input">The string to check.</param>
        private bool BoolValidator(string input)
        {
            return string.Equals(input, "y", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(input, "n", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Transforms the provided input to an <see cref="bool"/>.
        /// </summary>
        /// <param name="input">The string to transform.</param>
        /// <returns>A bool from the input.</returns>
        private bool BoolTransformer(string input) => string.Equals(input, "y", StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// Transforms the provided input to an <see cref="bool"/>.
        /// </summary>
        /// <param name="input">The string to transform.</param>
        /// <returns>A bool from the input.</returns>
        private bool InvertedBoolTransformer(string input) => string.Equals(input, "n", StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// Checks that the provided input is an <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="input">The string to check.</param>
        /// <returns><see langword="true"/> if the string is an TimeSpan, otherwise <see langword="false"/>.</returns>
        private bool TimeSpanValidator(string input) => TimeSpan.TryParseExact(input, "mm\\:ss", CultureInfo.InvariantCulture, out var result);

        /// <summary>
        /// Transforms the provided input to an <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="input">The string to transform.</param>
        /// <returns>A TimeSpan from the input.</returns>
        private TimeSpan TimeSpanTransformer(string input) => TimeSpan.ParseExact(input, "mm\\:ss", CultureInfo.InvariantCulture);

        private bool OrderValidator(string input) => Regex.IsMatch(input, ORDER_REGEX);
        
        private (string Item, TimeSpan Placed) OrderTransformer(string input)
        {
            var parts = input.Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            if (parts.Length == 1)
                return (parts[0], TimeSpan.Zero);

            return (parts[0], TimeSpanTransformer(parts[1]));
        }
        #endregion
    }
}

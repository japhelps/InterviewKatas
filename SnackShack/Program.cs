using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;
using SnackShack.Commands;
using SnackShack.Model;

namespace SnackShack
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Welcome to the Snack Shack!");
                Console.WriteLine();

                var command = new GetOrdersCommand();

                var orders = command.Execute();

                var scheduler = new Scheduler();

                var schedule = scheduler.Create(orders);

                Console.WriteLine();
                DisplaySchedule(schedule);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }

        #region Private Methods
        #region UI Methods
        private static void DisplaySchedule(ISchedule schedule)
        {
            foreach (var task in schedule.Tasks)
				Console.WriteLine("{0:mm\\:ss} {1}", task.Start, task.Name);
                //Console.WriteLine$"{task.Start:mm\\\:ss} {task.Name}");
        }
        #endregion
        #endregion
    }
}

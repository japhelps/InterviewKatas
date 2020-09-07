using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SnackShack.Api;
using SnackShack.Api.Data;
using SnackShack.Model.Steps;

namespace SnackShack.Model
{
    /// <summary>
    /// Represents a work item scheduling system.
    /// </summary>
    public class Scheduler : IScheduler
    {
        #region Private Members
        private const string FINAL_TASK_NAME = "take a well earned break!";
        private readonly int binCapacity;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of a schedule builder.
        /// </summary>
        /// <param name="binCapacity">The capacity of the bins for scheduling.</param>
        public Scheduler(int binCapacity)
        {
            if (binCapacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(binCapacity), binCapacity, "The bin capacity must be greater than zero.");

            this.binCapacity = binCapacity;
        }
        #endregion

        #region Public Properties
        /// <inheritdoc/>
        public IReadOnlyList<IOrder> Orders => this.OrdersInternal;
        #endregion

        #region Public Methods
        /// <inheritdoc/>
        public ISchedule Create()
        {
            var tasks = new List<Task>();
            var bins = BuildBins(this.OrdersInternal);

            var currentTime = TimeSpan.Zero;
            var builder = new StringBuilder();
            foreach (var bin in bins)
            {
                builder.Clear();
                var placedOrders = bin.Items.Where(x => x.Step is PlaceOrderStep)
                    .GroupBy(x => new { Type = x.Order.Item })
                    .Select(x => new { Type = x.Key.Type, OrderSteps = x.ToList() });
                if (placedOrders.Count() > 0)
                {
                    var description = placedOrders.Select(x => $"{x.OrderSteps.Count} {x.Type} orders placed")
                        .Aggregate((s1, s2) => $"{s1}, {s2}");

                    builder.Append(description);
                }

                var nonOrderSteps = bin.Items.Where(x => !(x.Step is PlaceOrderStep))
                    .Select(x => x.Description);
                if(nonOrderSteps.Count() > 0)
                {
                    if (builder.Length > 0)
                        builder.Append(", ");

                    var description = nonOrderSteps.Aggregate((s1, s2) => $"{s1}, {s2}");
                    builder.Append(description);
                }

                tasks.Add(new Task(builder.ToString(), currentTime));
                currentTime = currentTime.Add(bin.TimeUsed);
            }

            tasks.Add(new Task(FINAL_TASK_NAME, currentTime));
            return new Schedule(tasks);
        }

        ///// <inheritdoc/>
        //public ISchedule Create()
        //{
        //    var tasks = new List<Task>();

        //    var grouped = this.OrdersInternal.GroupBy(x => new { x.Placed, x.Item })
        //        .Select(x => new { Placed = x.Key.Placed, Type = x.Key.Item, Orders = x.ToList() })
        //        .ToList();

        //    var currentTime = TimeSpan.Zero;
        //    var builder = new StringBuilder();
        //    foreach (var order in this.OrdersInternal)
        //    {
        //        while (!order.StepsComplete)
        //        {
        //            builder.Clear();
        //            if (grouped.Any(x => x.Placed == currentTime))
        //            {
        //                var ordersMadeDescription = grouped.Where(x => x.Placed == currentTime)
        //                    .Select(x => $"{x.Orders.Count} {x.Type} orders placed")
        //                    .Aggregate((s1, s2) => $"{s1}, {s2}");
        //                builder.Append(ordersMadeDescription);
        //            }

        //            if (builder.Length > 0)
        //                builder.Append(", ");

        //            var step = order.GetNextStep();

        //            builder.Append(step.Name);
        //            tasks.Add(new Task(builder.ToString(), currentTime));

        //            currentTime = currentTime.Add(step.TimeToComplete);
        //        }
        //    }

        //    tasks.Add(new Task(FINAL_TASK_NAME, currentTime));
        //    return new Schedule(tasks);
        //}

        /// <inheritdoc/>
        public void Add(IOrder order)
        {
            order.Position = GetOrderPosition(order);

            var waitTime = TimeSpan.FromMinutes(5);
            var estimate = MakeEstimate(order);
            if (estimate > waitTime)
                throw new WaitTimeTooLongException($"Unable to complete order in {waitTime:m\\:ss} minutes.  Estimated completion time: {estimate:m\\:ss} minutes.");

            this.OrdersInternal.Add(order);
        }
        #endregion

        #region Private Properties
        private List<IOrder> OrdersInternal { get; } = new List<IOrder>();
        #endregion

        #region Private Methods
        private TimeSpan MakeEstimate(IOrder order)
        {
            var estimateOrders = new List<IOrder>();
            estimateOrders.AddRange(this.OrdersInternal);
            estimateOrders.Add(order);

            var bins = BuildBins(estimateOrders);

            var binListForOrder = bins.SkipWhile(x => !x.Contains(order, order.Steps.First()))
                .TakeUntil(x => x.Contains(order, order.Steps.Last()));

            return binListForOrder.Aggregate(TimeSpan.Zero, (ts, b) => ts.Add(b.TimeUsed));
        }

        private List<Bin> BuildBins(List<IOrder> orders)
        {
            var bins = new List<Bin>();

            var orderList = orders.SelectMany(x => x.Steps, (o, s) => new OrderStep(o, s))
                .OrderBy(x => x.Order.Placed)
                .ThenBy(x => x.Step.Weight)
                .ToList();

            foreach (var order in orderList)
            {
                var added = false;
                foreach (var bin in bins)
                {
                    added = bin.TryAdd(order);
                    if (added)
                        break;
                }

                if(!added)
                {
                    var bin = new Bin(this.binCapacity);
                    bin.TryAdd(order);
                    bins.Add(bin);
                }
            }

            return bins;
        }

        private int GetOrderPosition(IOrder order)
        {
            return this.OrdersInternal.Count(x => x.Item == order.Item) + 1;
        }
        #endregion
    }
}

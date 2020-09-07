using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SnackShack.Api;
using SnackShack.Api.Data;

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
        public Scheduler(int binCapacity)
        {
            if (binCapacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(binCapacity), binCapacity, "The bin capacity must be greater than zero.");

            this.binCapacity = binCapacity;
        }
        #endregion

        #region Public Methods
        /// <inheritdoc/>
        public ISchedule Create(IEnumerable<IOrder> orders)
        {
            var tasks = new List<Task>();

            var grouped = orders.GroupBy(x => new { x.Placed, x.Item })
                .Select(x => new { Placed = x.Key.Placed, Type = x.Key.Item, Orders = x.ToList() })
                .ToList();

            var currentTime = TimeSpan.Zero;
            var builder = new StringBuilder();
            foreach (var order in orders)
            {
                while (!order.StepsComplete)
                {
                    builder.Clear();
                    if (grouped.Any(x => x.Placed == currentTime))
                    {
                        var ordersMadeDescription = grouped.Where(x => x.Placed == currentTime)
                            .Select(x => $"{x.Orders.Count} {x.Type} orders placed")
                            .Aggregate((s1, s2) => $"{s1}, {s2}");
                        builder.Append(ordersMadeDescription);
                    }

                    if (builder.Length > 0)
                        builder.Append(", ");

                    var step = order.GetNextStep();

                    builder.Append(step.Name);
                    tasks.Add(new Task(builder.ToString(), currentTime));

                    currentTime = currentTime.Add(step.TimeToComplete);
                }
            }

            tasks.Add(new Task(FINAL_TASK_NAME, currentTime));
            return new Schedule(tasks);
        }

        /// <inheritdoc/>
        public TimeSpan Add(IOrder order)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Properties
        private List<IOrder> Orders { get; } = new List<IOrder>();
        //private List<Bin> Bins { get; } = new List<Bin>();
        #endregion

        #region Private Methods
        private TimeSpan MakeEstimate(IOrder order)
        {
            throw new NotImplementedException();
        }

        //private List<Bin> EstimateBins(IOrder order)
        //{
        //    var estimateBins = new List<Bin>();
        //    estimateBins.AddRange(this.Bins);

        //    estimateBins = AddOrderToBins(order, estimateBins);


        //}

    //    private List<Bin> AddOrderToBins(IOrder order, List<Bin> bins)
    //    {
    //        while(!order.Item.StepsComplete)
    //        {
    //            var step = order.Item.GetNextStep();

    //            var binPosition = 0;
    //            Bin newBin = null;
    //            foreach (var bin in bins)
    //            {
    //                var result = bin.TryAdd(step);
    //                if (result.Added)
    //                {
    //                    newBin = result.Bin;
    //                    break;
    //                }

    //                binPosition++;
    //            }

    //            if (newBin == null)
    //            {
    //                newBin = new Bin(this.binCapacity, step);
    //                bins.Add(newBin);
    //            }
    //            else
    //            {
    //                bins[binPosition] = newBin;
    //            }
    //        }

    //        return bins;
    //    }
        #endregion
    }
}

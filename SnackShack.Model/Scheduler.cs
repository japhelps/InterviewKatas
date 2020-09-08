using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly int timeSlotCapacity;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of a schedule builder.
        /// </summary>
        /// <param name="timeSlotCapacity">The capacity of the time slots for scheduling.</param>
        public Scheduler(int timeSlotCapacity)
        {
            if (timeSlotCapacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(timeSlotCapacity), timeSlotCapacity, "The time slot capacity must be greater than zero.");

            this.timeSlotCapacity = timeSlotCapacity;
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
            var taskDescriptions = new Dictionary<TimeSpan, string>();
            var ordersPlacedSlots = BuildOrdersPlacedSlots();

            //Add the order task descriptions
            foreach (var ordersPlaced in ordersPlacedSlots)
                taskDescriptions.Add(ordersPlaced.Key, ordersPlaced.Value);

            //Get the work item time slots
            var timeSlots = BuildTimeSlots(this.OrdersInternal, x => !(x.Step is PlaceOrderStep));

            var currentTime = TimeSpan.Zero;
            foreach (var timeSlot in timeSlots)
            {
                foreach (var item in timeSlot.Items)
                {
                    if (taskDescriptions.TryGetValue(currentTime, out string description))
                    {
                        description = $"{description}, {item.Description}";
                        taskDescriptions[currentTime] = description;
                    }
                    else
                        taskDescriptions.Add(currentTime, item.Description);

                    currentTime = currentTime.Add(item.Step.TimeToComplete);
                }
            }

            taskDescriptions.Add(currentTime, FINAL_TASK_NAME);

            var tasks = taskDescriptions.Select(x => new Task(x.Value, x.Key))
                .OrderBy(x => x.Start)
                .ToList();
            return new Schedule(tasks);
        }

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

            var timeSlots = BuildTimeSlots(estimateOrders, x => true);

            var timeSlotListForOrder = timeSlots.SkipWhile(x => !x.Contains(order, order.Steps.First()))
                .TakeUntil(x => x.Contains(order, order.Steps.Last()));

            return timeSlotListForOrder.Aggregate(TimeSpan.Zero, (ts, b) => ts.Add(b.TimeUsed));
        }

        private List<TimeSlot> BuildTimeSlots(List<IOrder> orders, Func<OrderStep, bool> includeStep)
        {
            var timeSlots = new List<TimeSlot>();
            
            var orderList = orders.SelectMany(x => x.Steps, (o, s) => new OrderStep(o, s))
                .Where(x => includeStep(x))
                .OrderBy(x => x.Order.Placed)
                .ThenBy(x => x.Step.Weight)
                .ToList();

            foreach (var orderStep in orderList)
            {
                var added = false;
                foreach (var timeSlot in timeSlots.Where(x => x.Placed >= orderStep.Order.Placed))
                {
                    added = timeSlot.TryAdd(orderStep);
                    if (added)
                        break;
                }

                if(!added)
                {
                    var newTimeSlot = new TimeSlot(this.timeSlotCapacity);
                    newTimeSlot.TryAdd(orderStep);
                    timeSlots.Add(newTimeSlot);
                }
            }

            return timeSlots;
        }

        private Dictionary<TimeSpan, string> BuildOrdersPlacedSlots()
        {
            var placedItemOrders = this.OrdersInternal.GroupBy(x => new { Placed = x.Placed, x.Item })
                .Select(x => new { Placed = x.Key.Placed, Value = $"{x.ToList().Count} {x.Key.Item} order(s) placed" })
                .ToList();

            var placedOrders = new Dictionary<TimeSpan, string>();

            foreach (var itemOrder in placedItemOrders)
            {
                if (placedOrders.TryGetValue(itemOrder.Placed, out string value))
                    placedOrders[itemOrder.Placed] = $"{value}, {itemOrder.Value}";
                else
                    placedOrders.Add(itemOrder.Placed, itemOrder.Value);
            }

            return placedOrders;
        }

        private int GetOrderPosition(IOrder order)
        {
            //The first order
            if (this.OrdersInternal.Count == 0)
                return 1;

            //Placing an order at the end.
            if(order.Placed >= this.OrdersInternal.Where(x => x.Item == order.Item).Max(x => x.Placed))
                return this.OrdersInternal.Count(x => x.Item == order.Item) + 1;
            else //Placing an order in the middle.  Fix all the positions and return the correct position
            {
                foreach (var fixOrder in this.OrdersInternal.Where(x => x.Item == order.Item && x.Placed > order.Placed))
                    fixOrder.Position = fixOrder.Position + 1;

                return this.OrdersInternal.Count(x => x.Item == order.Item && x.Placed <= order.Placed) + 1;
            }
        }
        #endregion
    }
}

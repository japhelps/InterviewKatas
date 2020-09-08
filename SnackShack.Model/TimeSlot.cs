using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Model
{
    /// <summary>
    /// Represents scheduling time slot to hold menu item production steps.
    /// </summary>
    internal class TimeSlot
    {
        #region Private Members
        private List<OrderStep> items;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of a time slot to hold menu item production steps.
        /// </summary>
        /// <param name="capacity">The amount the time slot can hold.</param>
        public TimeSlot(int capacity, TimeSpan start)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Capacity must be greater than zero.");

            this.Capacity = capacity;
            this.items = new List<OrderStep>();
            this.Start = start;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the total capacity of the time slot.
        /// </summary>
        public int Capacity { get; }
        /// <summary>
        /// Gets the current capacity of the items being held.
        /// </summary>
        public int CurrentCapacity => this.items.Sum(x => x.Step.Weight);

        /// <summary>
        /// Gets the amount of capacity remaining in the time slot.
        /// </summary>
        public int CapacityRemaining => this.Capacity - this.CurrentCapacity;

        /// <summary>
        /// Gets the total time used in this time slot.
        /// </summary>
        public TimeSpan TimeUsed => this.items.Select(x => x.Step.TimeToComplete)
            .Aggregate((ts1, ts2) => ts1.Add(ts2));

        /// <summary>
        /// Gets the earliest placed time in the time slot.
        /// </summary>
        public TimeSpan Placed => this.items.Min(x => x.Order.Placed);

        /// <summary>
        /// Gets the start time for the time slot.
        /// </summary>
        public TimeSpan Start { get; }

        /// <summary>
        /// Gets the end time for the time slot.
        /// </summary>
        public TimeSpan End => this.Start.Add(this.TimeUsed);

        /// <summary>
        /// Gets the items in the time slot.
        /// </summary>
        public IReadOnlyList<OrderStep> Items => this.items;
        #endregion

        #region Public Methods
        /// <summary>
        /// Attempts to add the item to the time slot.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>Returns <see langword="true"/> if the item is added to the time slot. Otherwise <see langword="false"/>.</returns>
        public bool TryAdd(OrderStep item)
        {
            if (item.Step.Weight > this.CapacityRemaining)
                return false;

            this.items.Add(item);
            return true;
        }

        /// <summary>
        /// Determines whether a time slot contains a specific order and order step.
        /// </summary>
        /// <param name="order">The order for which to check.</param>
        /// <param name="step">The step for which to check.</param>
        /// <returns><see langword="true"/> if the order and step are found, otherwise <see langword="false"/>.</returns>
        public bool Contains(IOrder order, IStep step) => this.items.Contains(new OrderStep(order,step));
        #endregion
    }
}

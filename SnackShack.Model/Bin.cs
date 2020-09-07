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
    /// Represents scheduling bin to hold menu item production steps.
    /// </summary>
    internal class Bin
    {
        #region Constructors
        /// <summary>
        /// Creates an instance of a bin to hold menu item production steps.
        /// </summary>
        /// <param name="capacity">The amount the bin can hold.</param>
        public Bin(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Capacity must be greater than zero.");

            this.Capacity = capacity;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the total capacity of the bin.
        /// </summary>
        public int Capacity { get; }
        /// <summary>
        /// Gets the current capacity of the items being held.
        /// </summary>
        public int CurrentCapacity => this.Items.Sum(x => x.Step.Weight);

        /// <summary>
        /// Gets the amount of capacity remaining in the bin.
        /// </summary>
        public int CapacityRemaining => this.Capacity - this.Items.Sum(x => x.Step.Weight);

        /// <summary>
        /// Gets the total time used in this bin.
        /// </summary>
        public TimeSpan TimeUsed => this.Items.Select(x => x.Step.TimeToComplete)
            .Aggregate((ts1, ts2) => ts1.Add(ts2));
        #endregion

        #region Public Methods
        /// <summary>
        /// Attempts to add the item to the bin.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>Returns <see langword="true"/> if the item is added to the bin. Otherwise <see langword="false"/>.</returns>
        public bool TryAdd(OrderStep item)
        {
            if (item.Step.Weight > this.CapacityRemaining)
                return false;

            this.Items.Add(item);
            return true;
        }

        /// <summary>
        /// Determines whether a bin contains a specific order and order step.
        /// </summary>
        /// <param name="order">The order for which to check.</param>
        /// <param name="step">The step for which to check.</param>
        /// <returns><see langword="true"/> if the order and step are found, otherwise <see langword="false"/>.</returns>
        public bool Contains(IOrder order, IStep step) => this.Items.Contains(new OrderStep(order, step));
        #endregion

        #region Private Properties
        private List<OrderStep> Items { get; } = new List<OrderStep>();
        #endregion
    }
}

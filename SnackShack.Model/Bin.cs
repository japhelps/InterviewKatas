using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
    public class Bin
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

        /// <summary>
        /// Creates an instance of a bin to hold menu item production steps.
        /// </summary>
        /// <param name="capacity">The amount the bin can hold.</param>
        /// <param name="item">The item to add.</param>
        public Bin(int capacity, IStep item)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Capacity must be greater than zero.");

            this.Capacity = capacity;
            this.Items = this.Items.Add(item);
        }

        /// <summary>
        /// Creates an instance of the bin with the provided capacity and items.
        /// </summary>
        /// <param name="capacity">The amount the bin can hold.</param>
        /// <param name="items">The items in the bin.</param>
        private Bin(int capacity, IEnumerable<IStep> items)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Capacity must be greater than zero.");

            this.Capacity = capacity;
            this.Items = this.Items.AddRange(items);
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
        public int CurrentCapacity => this.Items.Sum(x => x.Weight);

        /// <summary>
        /// Gets the amount of capacity remaining in the bin.
        /// </summary>
        public int CapacityRemaining => this.Capacity - this.Items.Sum(x => x.Weight);
        #endregion

        #region Public Methods
        /// <summary>
        /// Attempts to add the item to the bin.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>Returns <see langword="true"/> if the item is added to the bin. Otherwise <see langword="false"/>.</returns>
        public (bool Added, Bin Bin) TryAdd(IStep item)
        {
            if (item.Weight > this.CapacityRemaining)
                return (false, this);

            this.Items.Add(item);
            return (true, new Bin(this.Capacity, this.Items.Add(item)));
        }
        #endregion

        #region Private Properties
        private ImmutableList<IStep> Items { get; } = ImmutableList<IStep>.Empty;
        #endregion
    }
}

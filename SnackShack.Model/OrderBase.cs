using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Model
{
    /// <summary>
	/// Represents an base class for orders placed by a customer.
	/// </summary>
    public abstract class OrderBase : IOrder
    {
        #region Private Members
        private Queue<IStep> steps;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of a menu item being ordered by a customer.
        /// </summary>
        /// <param name="item">The item being ordered.</param>
        /// <param name="placed">The time the order was placed.</param>
        public OrderBase(TimeSpan placed)
        {
            this.Placed = placed;
            this.steps = BuildSteps();
        }
        #endregion

        #region Public Properties
        /// <inheritdoc/>
        public abstract string Item { get; }
        /// <inheritdoc/>
        public TimeSpan Placed { get; }
        /// <inheritdoc/>
        public bool StepsComplete => this.steps.Count == 0;
        #endregion

        #region Public Methods
        /// <inheritdoc/>
        public IStep GetNextStep() => this.steps.Dequeue();
        #endregion

        #region Protected Methods
        protected abstract Queue<IStep> BuildSteps();
        #endregion
    }
}

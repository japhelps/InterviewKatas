using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api;
using SnackShack.Api.Data;

namespace SnackShack.Model
{
    /// <summary>
    /// Represents a listing of the available items for sale.
    /// </summary>
    public class Inventory : IInventory
    {
        #region Private Members
        private int sandwiches;
        #endregion

        /// <summary>
        /// Creates an instance of a listing of the available items for sale.
        /// </summary>
        /// <param name="sandwiches">The number of sandwiches available for sale.</param>
        public Inventory(int sandwiches)
        {
            if (sandwiches <= 0)
                throw new ArgumentOutOfRangeException(nameof(sandwiches), sandwiches, "The number of sandwichs must be greater than zero.");

            this.sandwiches = sandwiches;
        }

        /// <inheritdoc/>
        public bool HaveItem(IOrder order)
        {
            if(order is SandwichOrder && this.sandwiches > 0)
            {
                this.sandwiches--;
                return true;
            }

            return false;
        }
    }
}

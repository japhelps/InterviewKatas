using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Api
{
    /// <summary>
    /// Represents the properties and methods for managing inventory.
    /// </summary>
    public interface IInventory
    {
        /// <summary>
        /// Returns whether there are enough items in the inventory for the provided order.
        /// </summary>
        /// <param name="order">The order to check.</param>
        /// <returns><see langword="true"/> if there is enough inventory, otherwise <see langword="false"/>.</returns>
        bool HaveItem(IOrder order);
    }
}

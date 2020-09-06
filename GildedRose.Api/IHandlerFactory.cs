using System;
using System.Collections.Generic;
using System.Text;
using GildedRose.Api.Data;

namespace GildedRose.Api
{
	/// <summary>
	/// Represents a factory to retrieve a item handler for an item.
	/// </summary>
	public interface IHandlerFactory
	{
		/// <summary>
		/// Returns the correct quality handler for the provided item.
		/// </summary>
		/// <param name="item">The item for which to retrieve the quality handler.</param>
		/// <returns>The quality handler.</returns>
		IItemHandler Get(IItem item);
	}
}

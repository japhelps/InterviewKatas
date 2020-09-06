using System;
using System.Collections.Generic;
using System.Text;

namespace GildedRose.Api
{
	/// <summary>
	/// Represents the methods and members needed to update a item's quality. 
	/// </summary>
	public interface IItemHandler
	{
		/// <summary>
		/// The method that gets the new value for the provided item's quality.
		/// </summary>
		/// <param name="item">The item on which to calculate the quality.</param>
		/// <returns>The new quality.</returns>
		(int SellIn, int Quality) Update(IItem item);
	}
}

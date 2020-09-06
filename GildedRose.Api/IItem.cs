using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using GildedRose.Api.Data;

namespace GildedRose.Api
{
	/// <summary>
	/// Represents an item sold by the shop.
	/// </summary>
	public interface IItem
	{
		/// <summary>
		/// Gets the name of the item.
		/// </summary>
		string Name { get; }
		/// <summary>
		/// Gets the type of the item.
		/// </summary>
		ItemType Type { get; }
		/// <summary>
		/// Gets the quality of the item.
		/// </summary>
		int Quality { get; }
		/// <summary>
		/// Gets the number of days left in which to sell the item.
		/// </summary>
		int SellIn { get; }
	}
}

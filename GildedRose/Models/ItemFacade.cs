using System;
using System.Collections.Generic;
using System.Text;
using GildedRose.Api;
using GildedRose.Api.Data;

namespace GildedRose.Models
{
	/// <summary>
	/// Represents an instance of an item.
	/// </summary>
	public class ItemFacade : IItem
	{
		#region Constructors
		public ItemFacade(string name, ItemType type, int quality, int sellIn)
		{
			if (name == null)
				throw new ArgumentNullException(nameof(name));

			this.Name = name;
			this.Type = type;
			this.Quality = quality;
			this.SellIn = sellIn;
		}
		#endregion

		#region Public Properties
		public string Name { get; }
		public ItemType Type { get; }
		public int Quality { get; }
		public int SellIn { get; } 
		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using GildedRose.Api;
using GildedRose.Models;

namespace GildedRose.Infrastructure
{
	internal static class ItemConverter
	{
		public static IItem Convert(Item item)
		{
			if (item.Name.Contains("Sulfuras", StringComparison.InvariantCultureIgnoreCase))
				return new ItemFacade(item.Name, Api.Data.ItemType.Legendary, item.Quality, item.SellIn);
			else if (item.Name.Contains("Aged", StringComparison.InvariantCultureIgnoreCase))
				return new ItemFacade(item.Name, Api.Data.ItemType.Aged, item.Quality, item.SellIn);
			else if (item.Name.Contains("Conjured", StringComparison.InvariantCultureIgnoreCase))
				return new ItemFacade(item.Name, Api.Data.ItemType.Conjured, item.Quality, item.SellIn);
			else if (item.Name.Contains("passes", StringComparison.InvariantCultureIgnoreCase))
				return new ItemFacade(item.Name, Api.Data.ItemType.TimeSensitive, item.Quality, item.SellIn);
			else
				return new ItemFacade(item.Name, Api.Data.ItemType.Default, item.Quality, item.SellIn);
		}
	}
}

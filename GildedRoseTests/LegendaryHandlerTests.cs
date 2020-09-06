using System;
using System.Collections.Generic;
using System.Text;
using GildedRose.Api;
using GildedRose.Api.Data;
using GildedRose.Infrastructure.QualityHandlers;
using GildedRose.Models;
using Xunit;

namespace GildedRoseTests
{
	public class LegendaryHandlerTests
	{
		[Theory]
		[InlineData("Legendary - The Bifrost", 50, 0)]
		[InlineData("Legendary - The Bifrost", 1, 50)]
		[InlineData("Legendary - The Bifrost", 150, -10)]
		[InlineData("Legendary - The Bifrost", 3, 5)]
		[InlineData("Legendary - The Bifrost", -10, 20)]
		[InlineData("Legendary - The Bifrost", 5, -1)]
		[InlineData("Legendary - The Bifrost", -50, 1)]
		public void LegendaryQualityDoesNotChange(string name, int quality, int sellIn)
		{
			IItem item = new ItemFacade(name, ItemType.Legendary, quality, sellIn);
			IItemHandler handler = new LegendaryHandler();

			var result = handler.Update(item);

			Assert.Equal(80, result.Quality);
			Assert.Equal(sellIn, result.SellIn);
		}
	}
}

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
	public class ConjuredHandlerTests
	{
		[Fact]
		public void DoesConjuredHandlerDecreaseQuality()
		{
			IItem item = new ItemFacade("Conjured Fiery Greatsword", ItemType.Conjured, 2, 30);
			IItemHandler handler = new ConjuredHandler();

			var result = handler.Update(item);

			Assert.True(result.Quality < item.Quality);
			Assert.Equal(0, result.Quality);
			Assert.Equal(29, result.SellIn);
		}

		[Fact]
		public void DoesConjuredHandlerDecreaseByDouble()
		{
			IItem item = new ItemFacade("Conjured Fiery Greatsword", ItemType.Conjured, 15, -1);
			IItemHandler handler = new ConjuredHandler();

			var result = handler.Update(item);

			Assert.True(result.Quality < item.Quality);
			Assert.Equal(11, result.Quality);
			Assert.Equal(-2, result.SellIn);
		}

		[Fact]
		public void QualityDoesNotGoBelowZero()
		{
			IItem item = new ItemFacade("Conjured Fiery Greatsword", ItemType.Conjured, -10, 30);
			IItemHandler handler = new ConjuredHandler();

			var result = handler.Update(item);

			Assert.True(result.Quality >= 0);
			Assert.Equal(0, result.Quality);
			Assert.Equal(29, result.SellIn);
		}
	}
}

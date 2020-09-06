using System;
using GildedRose;
using GildedRose.Api;
using GildedRose.Api.Data;
using GildedRose.Infrastructure.QualityHandlers;
using GildedRose.Models;
using Xunit;

namespace GildedRoseTests
{
	public class AgedHandlerTests
	{
		[Fact]
		public void DoesAgedHandlerIncreaseQuality()
		{
			IItem item = new ItemFacade("Aged Bottle of Ancient Orrian Wine", ItemType.Aged, 1, 30);
			IItemHandler handler = new AgedHandler();

			var result = handler.Update(item);

			Assert.True(result.Quality > item.Quality);
			Assert.Equal(2, result.Quality);
			Assert.Equal(29, result.SellIn);
		}

		[Fact]
		public void DoesAgedHandlerIncreaseByDouble()
		{
			IItem item = new ItemFacade("Aged Bottle of Ancient Orrian Wine", ItemType.Aged, 1, 0);
			IItemHandler handler = new AgedHandler();

			var result = handler.Update(item);

			Assert.True(result.Quality > item.Quality);
			Assert.Equal(3, result.Quality);
			Assert.Equal(-1, result.SellIn);
		}

		[Fact]
		public void QualityDoesNotGoBelowZero()
		{
			IItem item = new ItemFacade("Aged Bottle of Ancient Orrian Wine", ItemType.Aged, -10, 30);
			IItemHandler handler = new AgedHandler();

			var result = handler.Update(item);

			Assert.True(result.Quality > item.Quality);
			Assert.True(result.Quality >= 0);
			Assert.Equal(0, result.Quality);
			Assert.Equal(29, result.SellIn);
		}

		[Fact]
		public void QualityDoesNotGoAboveFifty()
		{
			IItem item = new ItemFacade("Aged Bottle of Ancient Orrian Wine", ItemType.Aged, 50, 30);
			IItemHandler handler = new AgedHandler();

			var result = handler.Update(item);

			Assert.Equal(item.Quality, result.Quality);
			Assert.Equal(29, result.SellIn);
		}

		[Fact]
		public void QualityDoesNotGoAboveFiftyIfAlreadyAbove()
		{
			IItem item = new ItemFacade("Aged Bottle of Ancient Orrian Wine", ItemType.Aged, 55, 30);
			IItemHandler handler = new AgedHandler();

			var result = handler.Update(item);

			Assert.Equal(50, result.Quality);
			Assert.Equal(29, result.SellIn);
		}
	}
}

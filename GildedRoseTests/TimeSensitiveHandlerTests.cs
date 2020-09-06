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
	public class TimeSensitiveHandlerTests
	{
		[Fact]
		public void DoesTimeSensitiveHandlerIncreaseQuality()
		{
			IItem item = new ItemFacade("Backstage passes to Pentakill", ItemType.TimeSensitive, 1, 30);
			IItemHandler handler = new TimeSensitiveHandler();

			var result = handler.Update(item);

			Assert.True(result.Quality > item.Quality);
			Assert.Equal(2, result.Quality);
			Assert.Equal(29, result.SellIn);
		}

		[Fact]
		public void DoesTimeSensitiveHandlerIncreaseByDouble()
		{
			IItem item = new ItemFacade("Backstage passes to Pentakill", ItemType.TimeSensitive, 1, 11);
			IItemHandler handler = new TimeSensitiveHandler();

			var result = handler.Update(item);

			Assert.True(result.Quality > item.Quality);
			Assert.Equal(3, result.Quality);
			Assert.Equal(10, result.SellIn);
		}

		[Fact]
		public void DoesTimeSensitiveHandlerIncreaseByTriple()
		{
			IItem item = new ItemFacade("Backstage passes to Pentakill", ItemType.TimeSensitive, 1, 6);
			IItemHandler handler = new TimeSensitiveHandler();

			var result = handler.Update(item);

			Assert.True(result.Quality > item.Quality);
			Assert.Equal(4, result.Quality);
			Assert.Equal(5, result.SellIn);
		}

		[Fact]
		public void QualityDoesNotGoBelowZero()
		{
			IItem item = new ItemFacade("Backstage passes to Pentakill", ItemType.TimeSensitive, -10, -1);
			IItemHandler handler = new TimeSensitiveHandler();

			var result = handler.Update(item);

			Assert.True(result.Quality > item.Quality);
			Assert.True(result.Quality >= 0);
			Assert.Equal(0, result.Quality);
			Assert.Equal(-2, result.SellIn);
		}

		[Fact]
		public void QualityDoesNotGoAboveFifty()
		{
			IItem item = new ItemFacade("Backstage passes to Pentakill", ItemType.TimeSensitive, 50, 30);
			IItemHandler handler = new TimeSensitiveHandler();

			var result = handler.Update(item);

			Assert.Equal(item.Quality, result.Quality);
			Assert.Equal(29, result.SellIn);
		}

		[Fact]
		public void QualityDoesNotGoAboveFiftyIfAlreadyAbove()
		{
			IItem item = new ItemFacade("Backstage passes to Pentakill", ItemType.TimeSensitive, 55, 30);
			IItemHandler handler = new TimeSensitiveHandler();

			var result = handler.Update(item);

			Assert.Equal(50, result.Quality);
			Assert.Equal(29, result.SellIn);
		}

		[Fact]
		public void DoesQualityGoToZeroAfterSellInDate()
		{
			IItem item = new ItemFacade("Backstage passes to Pentakill", ItemType.TimeSensitive, 50, 0);
			IItemHandler handler = new TimeSensitiveHandler();

			var result = handler.Update(item);

			Assert.Equal(0, result.Quality);
			Assert.Equal(-1, result.SellIn);
		}
	}
}

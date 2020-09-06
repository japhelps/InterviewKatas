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
	public class DefaultHandlerTests
	{
		[Fact]
		public void DoesDefaultHandlerDecreaseQuality()
		{
			IItem item = new ItemFacade("Berserker's Emblazoned Helm", ItemType.Default, 1, 30);
			IItemHandler handler = new DefaultHandler();

			var result = handler.Update(item);

			Assert.True(result.Quality < item.Quality);
			Assert.Equal(0, result.Quality);
			Assert.Equal(29, result.SellIn);
		}

		[Fact]
		public void DoesDefaultHandlerDecreaseByDouble()
		{
			IItem item = new ItemFacade("Berserker's Emblazoned Helm", ItemType.Default, 15, 0);
			IItemHandler handler = new DefaultHandler();

			var result = handler.Update(item);

			Assert.True(result.Quality < item.Quality);
			Assert.Equal(13, result.Quality);
			Assert.Equal(-1, result.SellIn);
		}

		[Fact]
		public void QualityDoesNotGoBelowZero()
		{
			IItem item = new ItemFacade("Berserker's Emblazoned Helm", ItemType.Default, -10, 30);
			IItemHandler handler = new DefaultHandler();

			var result = handler.Update(item);

			Assert.True(result.Quality >= 0);
			Assert.Equal(0, result.Quality);
			Assert.Equal(29, result.SellIn);
		}
	}
}

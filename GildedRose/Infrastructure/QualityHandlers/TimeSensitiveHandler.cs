using System;
using System.Collections.Generic;
using System.Text;
using GildedRose.Api;

namespace GildedRose.Infrastructure.QualityHandlers
{
	/// <summary>
	/// Represents a quality handler for time sensitive items.
	/// </summary>
	/// <remarks>Time sensitive handler is handled as follows:
	/// 1. The quality increases in quality as it ages.
	/// 2. The quality increases by 2 at 10 days or less.
	/// 3. The quality increases by 3 at 5 days of less.
	/// 4. The quality drops to <see cref="Constants.MinQuality"/> after the sell date.
	/// </remarks>
	public class TimeSensitiveHandler : IItemHandler
	{
		/// <inheritdoc/>
		public (int SellIn, int Quality) Update(IItem item)
		{
			//Changed to calculate the quality by the next day rather than today, since we are selling for tomorrow.
			var sellIn = item.SellIn - 1;
			if (sellIn < 0)
				return (sellIn, Constants.MinQuality);
			
			var changeAmount = 1;
			if (sellIn < 11)
				changeAmount = 2;

			if (sellIn < 6)
				changeAmount = 3;

			var quality = item.Quality + changeAmount;

			if (quality >= Constants.MaxQuality)
				return (sellIn, Constants.MaxQuality);

			if (quality <= Constants.MinQuality)
				return (sellIn, Constants.MinQuality);

			return (sellIn, quality);
		}
	}
}

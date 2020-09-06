using System;
using System.Collections.Generic;
using System.Text;
using GildedRose.Api;

namespace GildedRose.Infrastructure.QualityHandlers
{
	/// <summary>
	/// Represents a quality handler for aging items.
	/// </summary>
	/// <remarks>Aged handler is defined as follows:
	/// 1. The quality increases as it gets older.
	/// 2. The quality increases twice as fast if it is passed the sell by date.
	/// 3. Quality cannot be less then <see cref="Constants.MinQuality"/>.
	/// 4. Quality cannot be more than <see cref="Constants.MaxQuality"/>.
	/// </remarks>
	public class AgedHandler : IItemHandler
	{
		/// <inheritdoc/>
		public (int SellIn, int Quality) Update(IItem item)
		{
			var sellIn = item.SellIn - 1;
			
			var changeAmount = 1;
			if (sellIn < 0)
				changeAmount++;

			int quality = item.Quality + changeAmount;

			if (quality <= Constants.MinQuality)
				quality = Constants.MinQuality;

			if (quality >= Constants.MaxQuality)
				quality = Constants.MaxQuality;

			return (sellIn, quality);
		}
	}
}

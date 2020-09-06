using System;
using System.Collections.Generic;
using System.Text;
using GildedRose.Api;

namespace GildedRose.Infrastructure.QualityHandlers
{
	/// <summary>
	/// Represents the handler for conjured items.
	/// </summary>
	/// <remarks>Conjured handler is defined as follows.
	/// 1. Degrades the same manner as a normal item.
	/// 2. The quality degrades twice as fast as normal items.
	/// </remarks>
	public class ConjuredHandler : IItemHandler
	{
		/// <inheritdoc/>
		public (int SellIn, int Quality) Update(IItem item)
		{
			var changeAmount = 2;
			if (item.SellIn < 0)
				changeAmount = changeAmount * 2;

			int quality = item.Quality - changeAmount;

			if (quality <= Constants.MinQuality)
				quality = Constants.MinQuality;

			if (quality >= Constants.MaxQuality)
				quality = Constants.MaxQuality;

			return (item.SellIn - 1, quality);
		}
	}
}

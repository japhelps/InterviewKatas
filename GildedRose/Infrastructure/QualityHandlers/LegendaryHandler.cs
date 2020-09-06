using System;
using System.Collections.Generic;
using System.Text;
using GildedRose.Api;

namespace GildedRose.Infrastructure.QualityHandlers
{
	/// <summary>
	/// Represents a quality handler for legendary items.
	/// </summary>
	/// <remarks>Legendary handler is defined as follows:
	/// 1. Legendary items have a quality of <see cref="Constants.LegendaryQuality"/>
	/// 2. The quality does not change.
	/// </remarks>
	public class LegendaryHandler : IItemHandler
	{
		///<inheritdoc/>
		public (int SellIn, int Quality) Update(IItem item)
		{
			if (item.Quality != Constants.LegendaryQuality)
				return (item.SellIn, Constants.LegendaryQuality);

			return (item.SellIn, item.Quality);
		}
	}
}

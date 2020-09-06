using System;
using System.Collections.Generic;
using System.Text;
using GildedRose.Api;

namespace GildedRose.Infrastructure.QualityHandlers
{
	/// <summary>
	/// Represents the default quality handler for items.
	/// </summary>
	/// <remarks>Default handler is defined as follows:
	/// 1. Quality goes down by 1 every day
	/// 2. If the Sell by date is passed quality goes down by 2 every day.
	/// 3. Quality cannot be less then <see cref="Constants.MinQuality"/>.
	/// 4. Quality cannot be more than <see cref="Constants.MaxQuality"/>.
	/// </remarks>
	public class DefaultHandler : IItemHandler
	{
		#region Public Methods
		/// <inheritdoc/>
		public (int SellIn, int Quality) Update(IItem item)
		{
			var sellIn = item.SellIn - 1;
			var changeAmount = 1;
			if (sellIn < 0)
				changeAmount++;

			int quality = item.Quality - changeAmount;

			if (quality <= Constants.MinQuality)
				quality = Constants.MinQuality;

			if (quality >= Constants.MaxQuality)
				quality = Constants.MaxQuality;

			return (sellIn, quality);
		} 
		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GildedRose.Api;
using GildedRose.Infrastructure;

namespace GildedRose
{
	public class GildedRoseApp
	{
		#region Private Members
		IList<Item> Items;
		private readonly IHandlerFactory factory;
		#endregion

		public GildedRoseApp(IList<Item> Items, IHandlerFactory factory)
		{
			if (Items == null)
				throw new ArgumentNullException(nameof(Items));

			if (factory == null)
				throw new ArgumentNullException(nameof(factory));

			this.Items = Items;
			this.factory = factory;
		}

		public void UpdateQuality()
		{
			var itemList = this.Items.Select(x => new { Original = x, Facade = ItemConverter.Convert(x) })
					.ToList();

			foreach (var item in itemList)
			{
				var handler = this.factory.Get(item.Facade);
				var changes = handler.Update(item.Facade);

				item.Original.SellIn = changes.SellIn;
				item.Original.Quality = changes.Quality;
			}
		}
	}
}

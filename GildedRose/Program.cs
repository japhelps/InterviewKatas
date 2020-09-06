using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using GildedRose.Api.Data;
using GildedRose.Infrastructure;
using GildedRose.Infrastructure.QualityHandlers;

namespace GildedRose
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("OMGHAI!");

            IList<Item> Items = new List<Item>{
				new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
				new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
				new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
				new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
				new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
				new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
				new Item
				{
					Name = "Backstage passes to a TAFKAL80ETC concert",
					SellIn = 10,
					Quality = 49
				},
				new Item
				{
					Name = "Backstage passes to a TAFKAL80ETC concert",
					SellIn = 5,
					Quality = 49
				},
				// this conjured item does not work properly yet
				new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
			};

            var handlerFactory = new HandlerFactory(new Dictionary<Api.Data.ItemType, Api.IItemHandler>()
            {
                { Api.Data.ItemType.Legendary, new LegendaryHandler() },
                { ItemType.Aged, new AgedHandler() },
                { ItemType.Conjured, new ConjuredHandler() },
                { ItemType.TimeSensitive, new TimeSensitiveHandler() },
                { ItemType.Default, new DefaultHandler() },
            });

            var app = new GildedRoseApp(Items, handlerFactory);

            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- Day " + i + " --------");
                DisplayItems(Items);
                Console.WriteLine("");

				Console.WriteLine("Press any key to continue.");
				Console.ReadKey();
                app.UpdateQuality();
            }
        }

        private static void DisplayItems(IEnumerable<Item> items)
		{
            var maxLength = items.Max(x => x.Name.Length);

			Console.WriteLine($"Name{new string(' ', maxLength + 1 - 4)}Sell In\tQuality");
			foreach (var item in items)
				Console.WriteLine($"{item.Name, -42}{item.SellIn,6}\t{item.Quality,6}");
        }
    }
}
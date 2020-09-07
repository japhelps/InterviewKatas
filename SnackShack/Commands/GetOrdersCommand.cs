using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;
using SnackShack.Model;

namespace SnackShack.Commands
{
	internal class GetOrdersCommand : CommandBase<IEnumerable<IOrder>>
	{
		public override IEnumerable<IOrder> Execute()
		{
			var orders = new List<IOrder>();
			var done = false;
			while(!done)
			{
				TimeSpan placed = TimeSpan.Zero;
				var numberOfOrders = GetInput<int>("How many sandwiches can I get you? ", base.IntValidator, base.IntTransformer);
				if (numberOfOrders == 0)
					done = true;
				else
				{
					if(orders.Count > 0)
						placed = GetInput<TimeSpan>("What time are the sandwiches ordered? ", base.TimeSpanValidator, base.TimeSpanTransformer);

					var currentOrderCount = orders.Count;
					for (int i = 0; i < numberOfOrders; i++)
					{
						var position = currentOrderCount + i + 1;
						orders.Add(new Order(new Sandwich($"sandwich", position), placed));
					}
				}
			}

			return orders;
		}
	}
}

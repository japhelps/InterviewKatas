using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Model
{
	public class Order : IOrder
	{
		#region Constructors
		public Order(IMenuItem item, TimeSpan placed)
		{
			if (item == null)
				throw new ArgumentNullException(nameof(item));

			this.Item = item;
			this.Placed = placed;
		}
		#endregion

		#region Public Properties
		public IMenuItem Item { get; }
		public TimeSpan Placed { get; }
		#endregion


	}
}

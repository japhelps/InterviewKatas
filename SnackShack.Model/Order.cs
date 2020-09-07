using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Model
{
	/// <summary>
	/// Represents an order placed by a customer.
	/// </summary>
	public class Order : IOrder
	{
		#region Constructors
		/// <summary>
		/// Creates an instance of a menu item being ordered by a customer.
		/// </summary>
		/// <param name="item">The item being ordered.</param>
		/// <param name="placed">The time the order was placed.</param>
		public Order(IMenuItem item, TimeSpan placed)
		{
			if (item == null)
				throw new ArgumentNullException(nameof(item));

			this.Item = item;
			this.Placed = placed;
		}
		#endregion

		#region Public Properties
		/// <inheritdoc/>
		public IMenuItem Item { get; }
		/// <inheritdoc/>
		public TimeSpan Placed { get; }
		#endregion


	}
}

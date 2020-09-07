using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShack.Api.Data
{
	/// <summary>
	/// Represents the properties and methods required by an order.
	/// </summary>
	public interface IOrder
	{
		/// <summary>
		/// Gets the menu item being ordered.
		/// </summary>
		IMenuItem Item { get; }
		/// <summary>
		/// Gets the time the item was ordered.
		/// </summary>
		TimeSpan Placed { get; }
	}
}

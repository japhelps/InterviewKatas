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
		string Item { get; }

		/// <summary>
		/// Gets the time the item was ordered.
		/// </summary>
		TimeSpan Placed { get; }

		/// <summary>
		/// Gets the steps needed to complete the order.
		/// </summary>
		IReadOnlyCollection<IStep> Steps { get; }

		/// <summary>
		/// Gets or sets the position of the order.
		/// </summary>
		int Position { get; set; }
	}
}

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
		/// Gets whether the production of the order is finished.
		/// </summary>
		bool StepsComplete { get; }

		/// <summary>
		/// Gets the next step in the production of the item.
		/// </summary>
		/// <returns>The next step in the production process, otherwise <see langword="null"/>, if there are no more steps.</returns>
		IStep GetNextStep();
	}
}

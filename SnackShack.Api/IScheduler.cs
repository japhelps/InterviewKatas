using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Api
{
	/// <summary>
	/// Represents the properties and methods of a work item scheduling system.
	/// </summary>
	public interface IScheduler
	{
		/// <summary>
		/// Creates a schedule from the provided orders.
		/// </summary>
		/// <param name="orders">The orders from which to create a schedule.</param>
		/// <returns>A schedule of work items.</returns>
		ISchedule Create();

		/// <summary>
		/// Places an order.
		/// </summary>
		/// <param name="order">The order to be placed.</param>
		/// <returns>The estimated time of completion of the order.</returns>
		void Add(IOrder order);

		/// <summary>
		/// Gets a read only list of the current orders.
		/// </summary>
		IReadOnlyList<IOrder> Orders { get; }
	}
}

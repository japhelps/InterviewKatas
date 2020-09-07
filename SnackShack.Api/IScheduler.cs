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
		ISchedule Create(IEnumerable<IOrder> orders);
	}
}

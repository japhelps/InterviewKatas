using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShack.Api.Data
{
	/// <summary>
	/// Represents the properties and methods of a list of work items in an efficiently ordered list.
	/// </summary>
	public interface ISchedule
	{
		/// <summary>
		/// Gets the start time of the schedule.
		/// </summary>
		TimeSpan Start { get; }
		/// <summary>
		/// Gets the work items in the schedule.
		/// </summary>
		IEnumerable<ITask> Tasks { get; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShack.Api.Data
{
	/// <summary>
	/// Represents the properties and methods for a task for a worker to perform.
	/// </summary>
	public interface ITask
	{
		/// <summary>
		/// Gets the name of the task.
		/// </summary>
		string Name { get; }
		/// <summary>
		/// Gets the start time of the task.
		/// </summary>
		TimeSpan Start { get; }
	}
}

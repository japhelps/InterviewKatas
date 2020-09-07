using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Model
{
	/// <summary>
	/// Represents a work item for a worker to perform.
	/// </summary>
	public class Task : ITask
	{
		#region Constructors
		/// <summary>
		/// Creates an instance of a work item for a worker to perform.
		/// </summary>
		/// <param name="name">The name of the work item.</param>
		/// <param name="start">When the work items starts.</param>
		public Task(string name, TimeSpan start)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name));

			this.Name = name;
			this.Start = start;
		}
		#endregion

		#region Public Properties
		/// <inheritdoc/>
		public string Name { get; }
		/// <inheritdoc/>
		public TimeSpan Start { get; } 
		#endregion
	}
}

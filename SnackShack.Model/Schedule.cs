using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SnackShack.Api.Data;

namespace SnackShack.Model
{
	/// <summary>
	/// Represents a list of efficiently organized work items.
	/// </summary>
	public class Schedule : ISchedule
	{
		#region Constructors
		/// <summary>
		/// Creates an instance of organized work items.
		/// </summary>
		/// <param name="tasks">The work items.</param>
		public Schedule(IEnumerable<ITask> tasks)
		{
			if (tasks == null)
				throw new ArgumentNullException(nameof(tasks));

			this.Tasks = tasks;
		}
		#endregion

		#region Public Methods
		/// <inheritdoc/>
		public TimeSpan Start => TimeSpan.Zero;
		/// <inheritdoc/>
		public IEnumerable<ITask> Tasks { get; } 
		#endregion
	}
}

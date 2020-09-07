using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SnackShack.Api.Data;

namespace SnackShack.Model
{
	public class Schedule : ISchedule
	{
		#region Constructors
		public Schedule(IEnumerable<ITask> tasks)
		{
			if (tasks == null)
				throw new ArgumentNullException(nameof(tasks));

			this.Tasks = tasks;
		}
		#endregion

		#region Public Methods
		public TimeSpan Start => TimeSpan.Zero;
		public IEnumerable<ITask> Tasks { get; } 
		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShack.Api.Data
{
	public interface ISchedule
	{
		TimeSpan Start { get; }
		IEnumerable<ITask> Tasks { get; }
	}
}

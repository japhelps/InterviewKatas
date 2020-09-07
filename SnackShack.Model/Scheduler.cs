using System;
using System.Collections.Generic;
using SnackShack.Api;
using SnackShack.Api.Data;

namespace SnackShack.Model
{
	public class Scheduler : IScheduler
	{
		#region Public Methods
		public ISchedule Create(IEnumerable<IOrder> order)
		{
			throw new NotImplementedException();
		} 
		#endregion
	}
}

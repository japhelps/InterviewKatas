using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Api
{
	public interface IScheduler
	{
		ISchedule Create(IEnumerable<IOrder> order);
	}
}

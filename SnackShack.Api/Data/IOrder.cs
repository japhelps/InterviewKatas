using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShack.Api.Data
{
	public interface IOrder
	{
		IMenuItem Item { get; }
	}
}

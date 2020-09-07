using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShack.Api.Data
{
	public interface IMenuItem
	{
		string Name { get; }
		int PreparationTime { get; }
		int ServeTime { get; }
	}
}

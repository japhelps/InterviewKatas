using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShack.Api.Data
{
	public interface ITask
	{
		string Name { get; }
		TimeSpan Start { get; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShack.Api.Data
{
	public interface IStep
	{
		string Name { get; }
		TimeSpan TimeToComplete { get; }
	}
}

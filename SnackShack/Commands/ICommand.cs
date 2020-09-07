using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShack.Commands
{
	internal interface ICommand<TResult>
	{
		TResult Execute();
	}
}

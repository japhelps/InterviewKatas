using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShack.Commands
{
	/// <summary>
	/// Represents the properties and methods for a command to be executed.
	/// </summary>
	/// <typeparam name="TResult">The type of the returned result.</typeparam>
	internal interface ICommand<TResult>
	{
		/// <summary>
		/// Performs the command.
		/// </summary>
		/// <returns>The result of the execution.</returns>
		TResult Execute();
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShack.Api.Data
{
	/// <summary>
	/// Represents properties and methods needed by a step in a menu items creation.
	/// </summary>
	public interface IStep
	{
		/// <summary>
		/// Gets the name of the step.
		/// </summary>
		string Name { get; }
		/// <summary>
		/// Gets the amount of time it takes to complete the step.
		/// </summary>
		TimeSpan TimeToComplete { get; }
	}
}

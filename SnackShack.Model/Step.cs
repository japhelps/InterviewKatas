using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Model
{
	/// <summary>
	/// Represents a step in the menu item production process.
	/// </summary>
	public class Step : IStep
	{
		/// <summary>
		/// Creates an instance of a production step of a menu item.
		/// </summary>
		/// <param name="name">The name of the step.</param>
		/// <param name="timeToComplete">The time to complete a step.</param>
		public Step(string name, TimeSpan timeToComplete)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name));

			if (timeToComplete == TimeSpan.Zero)
				throw new ArgumentException(nameof(timeToComplete));

			this.Name = name;
			this.TimeToComplete = timeToComplete;
		}

		/// <inheritdoc/>
		public string Name { get; }
		/// <inheritdoc/>
		public TimeSpan TimeToComplete { get; }
	}
}

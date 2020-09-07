using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Model
{
	public class Step : IStep
	{
		public Step(string name, TimeSpan timeToComplete)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name));

			if (timeToComplete == TimeSpan.Zero)
				throw new ArgumentException(nameof(timeToComplete));

			this.Name = name;
			this.TimeToComplete = timeToComplete;
		}
		public string Name { get; }
		public TimeSpan TimeToComplete { get; }
	}
}

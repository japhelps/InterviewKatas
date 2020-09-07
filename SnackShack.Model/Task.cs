using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Model
{
	public class Task : ITask
	{
		#region Constructors
		public Task(string name, TimeSpan start)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name));

			this.Name = name;
			this.Start = start;
		}
		#endregion

		#region Public Properties
		public string Name { get; }
		public TimeSpan Start { get; } 
		#endregion
	}
}

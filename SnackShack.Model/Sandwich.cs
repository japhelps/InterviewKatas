using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Model
{
	public class Sandwich : IMenuItem
	{
		#region Private Members
		private Queue<IStep> steps;
		#endregion

		#region Constructors
		public Sandwich(string name, int position)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name));

			if (position <= 0)
				throw new ArgumentOutOfRangeException(nameof(position), position, "Position must be greater than zero.");

			this.Name = name;
			this.PlaceInLine = position;
			this.steps = BuildSteps();
		}

		#endregion

		#region Public Properties
		public string Name { get; }
		public int PlaceInLine { get; }
		public bool StepsComplete => this.steps.Count == 0;
		#endregion

		#region Public Methods
		public IStep GetNextStep()
		{
			return this.steps.Dequeue();
		}
		#endregion

		#region Private Methods
		private Queue<IStep> BuildSteps()
		{
			var steps = new Queue<IStep>();
			steps.Enqueue(new Step($"make {this.Name} {this.PlaceInLine}", TimeSpan.FromMinutes(1)));
			steps.Enqueue(new Step($"serve {this.Name} {this.PlaceInLine}", TimeSpan.FromSeconds(30)));

			return steps;
		} 
		#endregion
	}
}

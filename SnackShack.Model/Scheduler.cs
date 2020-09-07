using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SnackShack.Api;
using SnackShack.Api.Data;

namespace SnackShack.Model
{
	/// <summary>
	/// Represents a work item scheduling system.
	/// </summary>
	public class Scheduler : IScheduler
	{
		#region Private Members
		private const string FINAL_TASK_NAME = "take a well earned break!";
		#endregion

		#region Public Methods
		/// <inheritdoc/>
		public ISchedule Create(IEnumerable<IOrder> orders)
		{
			var tasks = new List<Task>();

			var grouped = orders.GroupBy(x => new { x.Placed, x.Item.Name })
				.Select(x => new { Placed = x.Key.Placed, Type = x.Key.Name, Orders = x.ToList() })
				.ToList();

			var currentTime = TimeSpan.Zero;
			var builder = new StringBuilder();
			foreach (var order in orders)
			{
				while (!order.Item.StepsComplete)
				{
					builder.Clear();
					if (grouped.Any(x => x.Placed == currentTime))
					{
						var ordersMadeDescription = grouped.Where(x => x.Placed == currentTime)
							.Select(x => $"{x.Orders.Count} {x.Type} orders placed")
							.Aggregate((s1, s2) => $"{s1}, {s2}");
						builder.Append(ordersMadeDescription);
					}

					if (builder.Length > 0)
						builder.Append(", ");

					var step = order.Item.GetNextStep();

					builder.Append(step.Name);
					tasks.Add(new Task(builder.ToString(), currentTime));

					currentTime = currentTime.Add(step.TimeToComplete);
				}
			}

			tasks.Add(new Task(FINAL_TASK_NAME, currentTime));
			return new Schedule(tasks);
		} 
		#endregion
	}
}

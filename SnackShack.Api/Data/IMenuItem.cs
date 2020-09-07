using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnackShack.Api.Data
{
	/// <summary>
	/// Represents the properties and methods of an item the can be ordered.
	/// </summary>
	public interface IMenuItem
	{
		/// <summary>
		/// Gets the name of the item.
		/// </summary>
		string Name { get; }
		/// <summary>
		/// Gets the place in line for the item.
		/// </summary>
		int PlaceInLine { get; }
		/// <summary>
		/// Gets whether the production of the item is finished.
		/// </summary>
		bool StepsComplete { get; }
		/// <summary>
		/// Gets the next step in the production of the item.
		/// </summary>
		/// <returns>The next step in the production process, otherwise <see langword="null"/>, if there are no more steps.</returns>
		IStep GetNextStep();
	}
}

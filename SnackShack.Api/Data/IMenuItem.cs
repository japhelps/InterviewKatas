using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnackShack.Api.Data
{
	public interface IMenuItem
	{
		string Name { get; }
		int PlaceInLine { get; }
		bool StepsComplete { get; }
		IStep GetNextStep();
	}
}

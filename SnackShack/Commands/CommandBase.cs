using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SnackShack.Commands
{
	internal abstract class CommandBase<TResult> : ICommand<TResult>
	{
		#region Public Methods
		public abstract TResult Execute();
		#endregion

		#region Protected Methods
		protected T GetInput<T>(string prompt, Func<string, bool> inputValidator, Func<string, T> inputTransformer)
		{
			string input = null;
			var done = false;

			while (!done)
			{
				Console.Write(prompt);
				input = Console.ReadLine();
				done = inputValidator(input);
			}

			return inputTransformer(input);
		} 

		protected bool IntValidator(string input) => int.TryParse(input, out var result);
		protected int IntTransformer(string input) => int.Parse(input);
		protected bool BoolValidator(string input) => bool.TryParse(input, out var result);
		protected bool BoolTransformer(string input) => bool.Parse(input);
		protected bool TimeSpanValidator(string input) => TimeSpan.TryParseExact(input, "mm:ss", CultureInfo.InvariantCulture, out var result);
		protected TimeSpan TimeSpanTransformer(string input) => TimeSpan.ParseExact(input, "mm:ss", CultureInfo.InvariantCulture);
		#endregion
	}
}

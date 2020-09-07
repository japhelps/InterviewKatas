using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SnackShack.Commands
{
	/// <summary>
	/// Represents the base implementation of an ICommand.
	/// </summary>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	internal abstract class CommandBase<TResult> : ICommand<TResult>
	{
		#region Public Methods
		/// <inheritdoc/>
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

		/// <summary>
		/// Checks that the provided input is an <see cref="int"/>.
		/// </summary>
		/// <param name="input">The string to check.</param>
		/// <returns><see langword="true"/> if the string is an int, otherwise <see langword="false"/>.</returns>
		protected bool IntValidator(string input) => int.TryParse(input, out var result);

		/// <summary>
		/// Transforms the provided input to an <see cref="int"/>.
		/// </summary>
		/// <param name="input">The string to transform.</param>
		/// <returns>An int from the input.</returns>
		protected int IntTransformer(string input) => int.Parse(input);

		/// <summary>
		/// Checks that the provided input is an <see cref="bool"/>.
		/// </summary>
		/// <param name="input">The string to check.</param>
		/// <returns><see langword="true"/> if the string is an bool, otherwise <see langword="false"/>.</returns>
		protected bool BoolValidator(string input) => bool.TryParse(input, out var result);

		/// <summary>
		/// Transforms the provided input to an <see cref="bool"/>.
		/// </summary>
		/// <param name="input">The string to transform.</param>
		/// <returns>An bool from the input.</returns>
		protected bool BoolTransformer(string input) => bool.Parse(input);

		/// <summary>
		/// Checks that the provided input is an <see cref="TimeSpan"/>.
		/// </summary>
		/// <param name="input">The string to check.</param>
		/// <returns><see langword="true"/> if the string is an TimeSpan, otherwise <see langword="false"/>.</returns>
		protected bool TimeSpanValidator(string input) => TimeSpan.TryParseExact(input, "mm:ss", CultureInfo.InvariantCulture, out var result);

		/// <summary>
		/// Transforms the provided input to an <see cref="TimeSpan"/>.
		/// </summary>
		/// <param name="input">The string to transform.</param>
		/// <returns>A TimeSpan from the input.</returns>
		protected TimeSpan TimeSpanTransformer(string input) => TimeSpan.ParseExact(input, "mm:ss", CultureInfo.InvariantCulture);
		#endregion
	}
}

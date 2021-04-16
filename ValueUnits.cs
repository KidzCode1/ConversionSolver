using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConversionSolver
{
	public class ValueUnits
	{
		public double value { get; set; }
		public string units { get; set; }

		/// <summary>
		/// Creates a new ValueUnits based on the specified input text.
		/// </summary>
		/// <param name="input">The input text to get a match for. For example, "9 hours".</param>
		/// <returns>Returns the new ValueUnits, or null if a no matches were found for the specified input.</returns>
		public static ValueUnits Create(string input)
		{
			const string pattern = @"(?<value>[+-]?((\d+(\.\d+)?)))\s*(?<units>\w+)";

			Regex regex = new Regex(pattern);
			MatchCollection matches = regex.Matches(input);
			if (matches.Count == 0)
				return null;

			ValueUnits valueUnits = new ValueUnits();
			valueUnits.value = RegexHelper.GetValue<double>(matches, "value");
			valueUnits.units = RegexHelper.GetValue<string>(matches, "units");
			return valueUnits;
		}
	}
}

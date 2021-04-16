using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConversionSolver
{
	public class FactData
	{
		public string data1 { get; set; }
		public string data2 { get; set; }

		/// <summary>
		/// Creates a new FactData based on the specified input text.
		/// </summary>
		/// <param name="input">The input text to get a match for. For example, "Brenda can deliver 644 newspapers in 7 hours".</param>
		/// <returns>Returns the new FactData, or null if a no matches were found for the specified input.</returns>
		public static FactData Create(string input)
		{
			const string pattern = @"(?<data1>[+-]?((\d+(\.\d+)?))\s*\w+).*(?<data2>[+-]?((\d+(\.\d+)?))\s*\w+)";

			Regex regex = new Regex(pattern);
			MatchCollection matches = regex.Matches(input);
			if (matches.Count == 0)
				return null;

			FactData factData = new FactData();
			factData.data1 = RegexHelper.GetValue<string>(matches, "data1");
			factData.data2 = RegexHelper.GetValue<string>(matches, "data2");
			return factData;
		}
	}
}

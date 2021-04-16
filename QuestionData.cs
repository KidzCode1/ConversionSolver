using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConversionSolver
{
	public class QuestionData
	{
		public string question { get; set; }

		/// <summary>
		/// Creates a new QuestionData based on the specified input text.
		/// </summary>
		/// <param name="input">The input text to get a match for. For example, "How many newspapers can Brenda deliver in 9 hours?".</param>
		/// <returns>Returns the new QuestionData, or null if a no matches were found for the specified input.</returns>
		public static QuestionData Create(string input)
		{
			const string pattern = @"(?<question>[+-]?((\d+(\.\d+)?))\s*\w+)";

			Regex regex = new Regex(pattern);
			MatchCollection matches = regex.Matches(input);
			if (matches.Count == 0)
				return null;

			QuestionData questionData = new QuestionData();
			questionData.question = RegexHelper.GetValue<string>(matches, "question");
			return questionData;
		}
	}
}

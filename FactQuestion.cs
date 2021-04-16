using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConversionSolver
{
	// Sample usage: 
	// FactData factData = FactData.Create("Brenda can deliver 644 newspapers in 7 hours");


	public class FactQuestion
	{
		public string Fact { get; set; }
		public string Question { get; set; }

		/// <summary>
		/// Creates a new FactQuestion based on the specified input text.
		/// </summary>
		/// <param name="input">The input text to get a match for. For example, "blah blah blah. what what what?".</param>
		/// <returns>Returns the new FactQuestion, or null if a no matches were found for the specified input.</returns>
		public static FactQuestion Create(string input)
		{
			const string pattern = @"^(?<Fact>.*)\.\s+(?<Question>.*)\?";

			Regex regex = new Regex(pattern);
			MatchCollection matches = regex.Matches(input);
			if (matches.Count == 0)
				return null;

			FactQuestion factQuestion = new FactQuestion();
			factQuestion.Fact = RegexHelper.GetValue<string>(matches, "Fact");
			factQuestion.Question = RegexHelper.GetValue<string>(matches, "Question");
			return factQuestion;
		}
	}
}

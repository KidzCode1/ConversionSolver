using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConversionSolver
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		public static System.IO.Stream GenerateStreamFromString(string s)
		{
			var stream = new System.IO.MemoryStream();
			var writer = new System.IO.StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}

		double GetKahnAcademyDataValueFrom(string value)
		{
			if (value.Length % 3 == 0)
			{
				string realValueAsStr = value.Substring(0, value.Length / 3);
				if (double.TryParse(realValueAsStr, out double result))
					return result;
			}
			if (double.TryParse(value, out double result2))
				return result2;
			return 0;
		}
		private void btnPaste_Click(object sender, RoutedEventArgs e)
		{
			if (Clipboard.ContainsText(TextDataFormat.Html))
			{
				string htmlText = Clipboard.GetText(TextDataFormat.Html);
				//System.Xml.Linq.XDocument xDocument = System.Xml.Linq.XDocument.Load(GenerateStreamFromString(htmlText));
			}

			string text = Clipboard.GetText();
			FactQuestion factQuestion = FactQuestion.Create(text);


			FactData factData = FactData.Create(factQuestion.Fact);
			QuestionData questionData = QuestionData.Create(factQuestion.Question);

			ValueUnits data1 = ValueUnits.Create(factData.data1);
			ValueUnits data2 = ValueUnits.Create(factData.data2);
			ValueUnits question = ValueUnits.Create(questionData.question);

			double top;
			double bottom;
			string units;
			double data1Value = GetKahnAcademyDataValueFrom(data1.value);
			double data2Value = GetKahnAcademyDataValueFrom(data2.value); ;
			double questionValue = GetKahnAcademyDataValueFrom(question.value);

			tbxFact.Text = factQuestion.Fact.Replace(data1.value, data1Value.ToString()).Replace(data2.value, data2Value.ToString()) + '.';
			tbxQuestion.Text = factQuestion.Question.Replace(question.value, questionValue.ToString()) + '?';



			if (question.units == data1.units)
			{
				units = data2.units;
				top = data2Value;
				bottom = data1Value;
			}
			else if (question.units == data2.units)
			{
				units = data1.units;
				top = data1Value;
				bottom = data2Value;
			}
			else
			{
				Title = "Error in question. Units do not match!";
				return;
			}

			Title = $"Answer units are in {units}.";
			double answer = questionValue * top / bottom;
			tbAnswer.Text = $"{answer} {units}";

		}
	}
}

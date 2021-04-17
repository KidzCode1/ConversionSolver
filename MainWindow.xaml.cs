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

		double GetDataValueFrom(string value, bool fromKahnAcademy)
		{
			if (fromKahnAcademy && (value.Length % 3 == 0))
			{
				string realValueAsStr = value.Substring(0, value.Length / 3);
				if (double.TryParse(realValueAsStr, out double result))
					return result;
			}

			if (double.TryParse(value, out double result2))
				return result2;
			return 0;
		}

		void NoErrors()
		{
			Background = new SolidColorBrush(Colors.White);
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


			QuestionData questionData = QuestionData.Create(factQuestion.Question);
			
			ValueUnits data1, data2, question;
			double data1Value, data2Value, questionValue;
			try
			{
				GetQuestionData(factQuestion.Fact, questionData.question, true, out data1, out data2, out question, out data1Value, out data2Value, out questionValue);
			}
			catch (Exception ex)
			{
				ShowError(ex);
				return;
			}
			NoErrors();
			changingInternally = true;
			try
			{
				tbxQuestion.Text = factQuestion.Question.Replace(question.value, questionValue.ToString()) + '?';
				tbxFact.Text = factQuestion.Fact.Replace(data1.value, data1Value.ToString()).Replace(data2.value, data2Value.ToString()) + '.';
			}
			finally
			{
				changingInternally = false;
			}

			AnswerQuestion();
		}

		private void ShowError(Exception ex)
		{
			Title = ex.Message;
			Background = new SolidColorBrush(Color.FromRgb(255, 138, 138));
		}

		bool changingInternally;

		private void GetQuestionData(string fact, string question, bool fromKahnAcademy, out ValueUnits data1, out ValueUnits data2, out ValueUnits questionValueUnit, out double data1Value, out double data2Value, out double questionValue)
		{
			FactData factData = FactData.Create(fact);
			data1 = ValueUnits.Create(factData.data1);
			data2 = ValueUnits.Create(factData.data2);
			questionValueUnit = ValueUnits.Create(question);
			data1Value = GetDataValueFrom(data1.value, fromKahnAcademy);
			data2Value = GetDataValueFrom(data2.value, fromKahnAcademy);
			questionValue = GetDataValueFrom(questionValueUnit.value, fromKahnAcademy);
		}

		void AnswerQuestion()
		{
			ValueUnits data1, data2, question;
			double data1Value, data2Value, questionValue;
			try
			{
				GetQuestionData(tbxFact.Text, tbxQuestion.Text, false, out data1, out data2, out question, out data1Value, out data2Value, out questionValue);
			}
			catch (Exception ex)
			{
				ShowError(ex);
				return;
			}
			NoErrors();

			double top;
			double bottom;
			string units;
			if (UnitsMatch(data1, question))
			{
				units = data2.units;
				top = data2Value;
				bottom = data1Value;
			}
			else if (UnitsMatch(data2, question))
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

		private static bool UnitsMatch(ValueUnits data1, ValueUnits data2)
		{
			string unit1 = data1.units.ToLower().TrimEnd('s');
			string unit2 = data2.units.ToLower().TrimEnd('s');

			return unit1 == unit2;
		}

		private void tbxFact_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (changingInternally)
				return;
			AnswerQuestion();
		}

		private void tbxQuestion_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (changingInternally)
				return;
			AnswerQuestion();
		}
	}
}

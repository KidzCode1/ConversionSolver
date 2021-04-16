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

		private void btnPaste_Click(object sender, RoutedEventArgs e)
		{
			string text = Clipboard.GetText();
			FactQuestion factQuestion = FactQuestion.Create(text);
			tbxFact.Text = factQuestion.Fact + '.';
			tbxQuestion.Text = factQuestion.Question+'?';


			FactData factData = FactData.Create(factQuestion.Fact);
			QuestionData questionData = QuestionData.Create(factQuestion.Question);

			ValueUnits data1 = ValueUnits.Create(factData.data1);
			ValueUnits data2 = ValueUnits.Create(factData.data2);
			ValueUnits question = ValueUnits.Create(questionData.question);

			double top;
			double bottom;
			string units;
			if (question.units == data1.units)
			{
				units = data2.units;
				top = data2.value;
				bottom = data1.value;
			}
			else if (question.units == data2.units)
			{
				units = data1.units;
				top = data1.value;
				bottom = data2.value;
			}
			else
			{
				Title = "Error in question. Units do not match!";
				return;
			}

			Title = $"Answer units are in {units}.";
			double answer = question.value * top / bottom;
			tbAnswer.Text = $"{answer} {units}";
			if (Clipboard.ContainsText(TextDataFormat.Html))
			{
				string htmlText = Clipboard.GetText(TextDataFormat.Html);
				
			}
		}
	}
}

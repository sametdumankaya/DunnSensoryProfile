using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DunnCalculator
{
    /// <summary>
    /// Interaction logic for Scores.xaml
    /// </summary>
    public partial class Scores : Window
    {
        private List<string> results;
        private string name;
        private int ageYear;
        private int ageMonth;

        public Scores(string name, string ageYear, string ageMonth, List<string> results)
        {
            InitializeComponent();

            this.results = results;
            this.name = name;
            this.ageYear = Convert.ToInt32(ageYear);
            this.ageMonth = Convert.ToInt32(ageMonth);
        }

        private void Scores_OnLoaded(object sender, RoutedEventArgs e)
        {
            var textBlockList = MainWindow.FindVisualChildren<TextBlock>(Window).ToList();

            for (int i = 0; i < results.Count; i++)
            {
                textBlockList[i].Text = results[i];
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fileName = $"{name}_{ageYear}_{ageMonth}-{DateTime.Today.ToString("dd-MM-yyyy")}.txt";
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                writetext.WriteLine($"İsim: {name}");
                writetext.WriteLine($"Yaş (Yıl - Ay): {ageYear} - {ageMonth}");
                writetext.WriteLine("");
                writetext.WriteLine("------------------------------------------------------------------------------");
                writetext.WriteLine("");

                foreach (var item in results)
                {
                    var splitted = item.Split(':');
                    var category = splitted[0];
                    var splitted2 = splitted[1].Split('(');
                    var point = splitted2[0];
                    var interpretation = splitted2[1].Replace(")", "");

                    writetext.WriteLine(category.Trim());
                    writetext.WriteLine(point.Trim());
                    writetext.WriteLine(interpretation.Trim());
                    writetext.WriteLine();
                }
            }

            Process.Start(fileName);
        }
    }
}

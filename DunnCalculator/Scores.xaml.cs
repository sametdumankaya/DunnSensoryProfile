using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using OfficeOpenXml;
using Microsoft.Win32;

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
            using (var p = new ExcelPackage())
            {
                var worksheet = p.Workbook.Worksheets.Add("Sheet1");

                worksheet.Cells[1, 1].Value = "İsim";
                worksheet.Cells[2, 1].Value = $"{name}";
                worksheet.Cells[1, 2].Value = "Yaş (Yıl - Ay)";
                worksheet.Cells[2, 2].Value = $"{ageYear} - {ageMonth}";

                int columnNumber = 3;

                foreach (var item in results)
                {
                    var splitted = item.Split(':');
                    var category = splitted[0];
                    var splitted2 = splitted[1].Split('(');
                    var point = splitted2[0];
                    var interpretation = splitted2[1].Replace(")", "");

                    worksheet.Cells[1, columnNumber].Value = category;
                    worksheet.Cells[2, columnNumber].Value = point;
                    worksheet.Cells[3, columnNumber].Value = interpretation;
                    columnNumber++;
                }

                worksheet.Cells.AutoFitColumns();
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel dosyası (*.xlsx)|*.xlsx";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                var result = saveFileDialog.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    var file = saveFileDialog.FileName;
                    p.SaveAs(new FileInfo(file));
                    Process.Start(file);
                }
            }
        }
    }
}

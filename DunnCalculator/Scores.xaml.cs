using System.Collections.Generic;
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

        public Scores(List<string> results)
        {
            InitializeComponent();

            this.results = results;
        }
        
        private void Scores_OnLoaded(object sender, RoutedEventArgs e)
        {
            var textBlockList = MainWindow.FindVisualChildren<TextBlock>(Window).ToList();
            
            for (int i = 0; i < textBlockList.Count; i++)
            {
                textBlockList[i].Text = results[i];
            }
        }
    }
}

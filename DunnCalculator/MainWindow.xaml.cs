using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DunnCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> scores = new List<int>();

        // kayıt
        private int registration = 0;

        private int[] registrationIndexes =
        {
            6,
            7,
            47,
            50,
            53,
            66,
            67,
            68,
            69,
            70,
            71,
            72,
            73,
            74,
            75
        };

        //araştırma
        private int seeking = 0;

        private int[] seekingIndexes =
        {
            8,
            24,
            25,
            26,
            27,
            28,
            40,
            41,
            44,
            45,
            46,
            51,
            59,
            60,
            61,
            62,
            63,
            80,
            81,
            82,
            83,
            84,
            89,
            90,
            94,
            123
        };

        //hassasiyet
        private int sensitivity = 0;

        private int[] sensitivityIndexes =
        {
            3,
            4,
            14,
            18,
            19,
            21,
            30,
            31,
            32,
            33,
            34,
            39,
            48,
            49,
            55,
            56,
            57,
            58,
            77,
            78
        };

        //kaçınma
        private int avoiding = 0;

        private int[] avoidingIndexes =
        {
            1,
            2,
            5,
            9,
            10,
            11,
            15,
            20,
            22,
            29,
            36,
            37,
            54,
            76,
            85,
            86,
            87,
            88,
            93,
            103,
            104,
            105,
            107,
            108,
            109,
            110,
            111,
            112,
            114
        };

        //duyusal girdi arama
        private int sensorySeeking = 0;

        private int[] sensorySeekingIndexes =
        {
            8,
            24,
            25,
            26,
            44,
            45,
            46,
            51,
            80,
            81,
            82,
            83,
            84,
            89,
            90,
            94,
            123
        };

        //duygusal tepki
        private int emotionallyReactive = 0;

        private int[] emotionallyReactiveIndexes =
        {
            92,
            100,
            101,
            102,
            103,
            104,
            105,
            106,
            107,
            108,
            109,
            110,
            111,
            112,
            121,
            122
        };

        //düşük endurans / tonus
        private int lowEnduranceTone = 0;

        private int[] lowEnduranceToneIndexes =
        {
            66,
            67,
            68,
            69,
            70,
            71,
            72,
            73,
            74,
        };

        //oral duyusal hassasiyet
        private int oralSensorySensitivity = 0;

        private int[] oralSensorySensitivityIndexes =
        {
            55,
            56,
            57,
            58,
            59,
            60,
            61,
            62,
            63
        };

        //dikkatsizlik / dikkat dağınıklığı
        private int inattention = 0;

        private int[] inattentionIndexes =
        {
            3,
            4,
            5,
            6,
            7,
            48,
            49
        };

        //zayıf kayıt
        private int poorRegistration = 0;

        private int[] poorRegistrationIndexes =
        {
            35,
            42,
            43,
            95,
            99,
            115,
            116,
            125
        };

        //duyu hassasiyeti
        private int sensorySensitivity = 0;

        private int[] sensorySensitivityIndexes =
        {
            18,
            19,
            77,
            78
        };

        //hareketsiz
        private int sedentary = 0;

        private int[] sedentaryIndexes =
        {
            85,
            86,
            87,
            88
        };

        //algısal ince motor
        private int fineMotor = 0;

        private int[] fineMotorIndexes =
        {
            13,
            118,
            119
        };

        private int a, b, c, d, e, f, g, h, i, j, k, l, m, n;

        private IEnumerable<int> aIndexes = Enumerable.Range(1, 8);
        private IEnumerable<int> bIndexes = Enumerable.Range(9, 9);
        private IEnumerable<int> cIndexes = Enumerable.Range(18, 11);
        private IEnumerable<int> dIndexes = Enumerable.Range(29, 18);
        private IEnumerable<int> eIndexes = Enumerable.Range(47, 7);
        private IEnumerable<int> fIndexes = Enumerable.Range(54, 12);
        private IEnumerable<int> gIndexes = Enumerable.Range(66, 9);
        private IEnumerable<int> hIndexes = Enumerable.Range(75, 10);
        private IEnumerable<int> iIndexes = Enumerable.Range(85, 7);
        private IEnumerable<int> jIndexes = Enumerable.Range(92, 4);
        private IEnumerable<int> kIndexes = Enumerable.Range(96, 4);
        private IEnumerable<int> lIndexes = Enumerable.Range(100, 17);
        private IEnumerable<int> mIndexes = Enumerable.Range(117, 6);
        private IEnumerable<int> nIndexes = Enumerable.Range(123, 2);

        private List<int> zeroPointIndexes = new List<int>();

        private List<Dictionary<string, string>> registrationRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000"   , "Diğerlerine Göre Daha Az - Kesin Fark"},
                {"73-75", "Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"64-72", "Tipik - Tipik Performans"},
                {"59-63", "Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"15-58", "Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> seekingRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000"     ,"Diğerlerine Göre Daha Az - Kesin Fark"},
                {"124-130","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"103-123","Tipik - Tipik Performans"},
                {"92-102" ,"Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"26-91"  ,"Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> sensitivityRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000    ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"95-100","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"81-94 ","Tipik - Tipik Performans"},
                {"73-80 ","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"20-72 ","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> avoidingRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"141-145", "Diğerlerine Göre Daha Az - Kesin Fark"},
                {"134-140", "Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"113-133", "Tipik - Tipik Performans"},
                {"103-112", "Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"29-102 ", "Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> sensorySeekingRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"82-85","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"63-81","Tipik - Tipik Performans"},
                {"55-62","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"17-54","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> emotionallyReactiveRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"75-80","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"57-74","Tipik - Tipik Performans"},
                {"48-56","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"16-47","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> lowEnduranceToneRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"4000-5000  ","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"39-45","Tipik - Tipik Performans"},
                {"36-38","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"9-35 ","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> oralSensorySensitivityRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"45-45","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"33-44","Tipik - Tipik Performans"},
                {"27-32","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"9-26 ","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> inattentionRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"33-35","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"25-32","Tipik - Tipik Performans"},
                {"22-24","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"7-21 ","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> poorRegistrationRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000  ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"4000-5000   ","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"33-40","Tipik - Tipik Performans"},
                {"30-32","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"8-29 ","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> sensorySensitivityRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000  ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"4000-5000   ","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"16-20","Tipik - Tipik Performans"},
                {"14-15","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"4-13 ","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> sedentaryRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"18-20","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"12-17","Tipik - Tipik Performans"},
                {"10-11","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"4-9  ","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> fineMotorRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"4000-5000  ","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"10-15","Tipik - Tipik Performans"},
                {"8-9  ","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"3-7  ","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> aRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"39-40","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"30-38","Tipik - Tipik Performans"},
                {"26-29","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"8-25 ","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> bRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"42-45","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"32-41","Tipik - Tipik Performans"},
                {"27-31","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"9-26 ","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> cRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"4000-5000  ","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"48-55","Tipik - Tipik Performans"},
                {"45-47","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"11-44","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> dRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"89-90","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"73-88","Tipik - Tipik Performans"},
                {"65-72","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"18-64","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> eRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"34-35","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"27-33","Tipik - Tipik Performans"},
                {"24-26","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"7-23 ","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> fRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"60-60","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"46-59","Tipik - Tipik Performans"},
                {"40-45","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"12-39","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> gRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"4000-5000  ","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"39-45","Tipik - Tipik Performans"},
                {"36-38","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"9-35 ","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> hRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"50-50","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"41-49","Tipik - Tipik Performans"},
                {"36-40","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"10-35","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> iRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"34-35"   ,"Diğerlerine Göre Daha Az - Kesin Fark"},
                {"31-33"   ,"Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"23-30"   ,"Tipik - Tipik Performans"},
                {"19-22"   ,"Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"7-18 "   ,"Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> jRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"4000-5000  ","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"16-20","Tipik - Tipik Performans"},
                {"14-15","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"4-13 ","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> kRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"20-20","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"15-19","Tipik - Tipik Performans"},
                {"12-14","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"4-11 ","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> lRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"80-85","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"63-79","Tipik - Tipik Performans"},
                {"55-62","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"17-54","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> mRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"29-30","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"22-28","Tipik - Tipik Performans"},
                {"19-21","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"17-54","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        private List<Dictionary<string, string>> nRanges = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>()
            {
                {"4000-5000   ","Diğerlerine Göre Daha Az - Kesin Fark"},
                {"4000-5000  ","Diğerlerine Göre Daha Az - Muhtemel Fark"},
                {"12-15","Tipik - Tipik Performans"},
                {"10-11","Diğerlerine Göre Daha Fazla - Muhtemel Fark"},
                {"3-9  ","Diğerlerine Göre Daha Fazla - Kesin Fark"}
            }
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectAddress(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                tb.SelectAll();
            }
        }

        private void SelectivelyIgnoreMouseButton(object sender,
            MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                if (!tb.IsKeyboardFocusWithin)
                {
                    e.Handled = true;
                    tb.Focus();
                }
            }
        }

        private string GetInterpretation(List<Dictionary<string, string>> ranges, int value)
        {
            foreach (var item in ranges)
            {
                foreach (var key in item.Keys)
                {
                    var splitted = key.Split('-');
                    var start = Convert.ToInt32(splitted[0].Trim());
                    var end = Convert.ToInt32(splitted[1].Trim());

                    if (value >= start && value <= end)
                    {
                        return item[key];
                    }
                }
            }

            return "Bulunamadı";
        }

        private void SetTextBlocksTexts()
        {
            var count = 1;
            var textBlockList = FindVisualChildren<TextBlock>(ScrollViewer);

            var textBlocks = textBlockList.ToList();
            foreach (var tb in textBlocks)
            {
                tb.Text = $"Puan {count}: ";
                count++;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinizden emin misiniz?", "Çıkış", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-5]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NumberValidationTextBoxAll(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (var childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var count = 1;
            var hasError = false;
            zeroPointIndexes.Clear();
            scores.Clear();

            foreach (TextBox tb in FindVisualChildren<TextBox>(ScrollViewer))
            {
                try
                {
                    var number = Convert.ToInt32(tb.Text);
                    scores.Add(number);

                    if (number == 0)
                    {
                        zeroPointIndexes.Add(count);
                    }

                    count++;
                }
                catch (FormatException)
                {
                    MessageBox.Show($"Puan {count} değeri boş veya hatalı !", "Hata");
                    hasError = true;
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(patientName.Text) || string.IsNullOrWhiteSpace(patientAgeYear.Text) || string.IsNullOrWhiteSpace(patientAgeMonth.Text))
            {
                MessageBox.Show("Hastanın isim veya yaş bilgisi girilmedi !", "Hata");
                hasError = true;
            }

            if (!hasError)
            {
                MakeCalculations();
            }
        }

        private void MakeCalculations()
        {
            registration = 0;

            foreach (var index in registrationIndexes)
            {
                registration += scores[index - 1];
            }

            seeking = 0;

            foreach (var index in seekingIndexes)
            {
                seeking += scores[index - 1];
            }


            sensitivity = 0;

            foreach (var index in sensitivityIndexes)
            {
                sensitivity += scores[index - 1];
            }


            avoiding = 0;

            foreach (var index in avoidingIndexes)
            {
                avoiding += scores[index - 1];
            }


            sensorySeeking = 0;

            foreach (var index in sensorySeekingIndexes)
            {
                sensorySeeking += scores[index - 1];
            }


            emotionallyReactive = 0;

            foreach (var index in emotionallyReactiveIndexes)
            {
                emotionallyReactive += scores[index - 1];
            }


            lowEnduranceTone = 0;

            foreach (var index in lowEnduranceToneIndexes)
            {
                lowEnduranceTone += scores[index - 1];
            }


            oralSensorySensitivity = 0;

            foreach (var index in oralSensorySensitivityIndexes)
            {
                oralSensorySensitivity += scores[index - 1];
            }


            inattention = 0;

            foreach (var index in inattentionIndexes)
            {
                inattention += scores[index - 1];
            }


            poorRegistration = 0;

            foreach (var index in poorRegistrationIndexes)
            {
                poorRegistration += scores[index - 1];
            }


            sensorySensitivity = 0;

            foreach (var index in sensorySensitivityIndexes)
            {
                sensorySensitivity += scores[index - 1];
            }


            sedentary = 0;

            foreach (var index in sedentaryIndexes)
            {
                sedentary += scores[index - 1];
            }


            fineMotor = 0;

            foreach (var index in fineMotorIndexes)
            {
                fineMotor += scores[index - 1];
            }

            a = b = c = d = e = f = g = h = i = j = k = l = m = n = 0;

            for (int o = 0; o < scores.Count; o++)
            {
                if (o < 8)
                {
                    a += scores[o];
                }
                else if (o < 17)
                {
                    b += scores[o];
                }
                else if (o < 28)
                {
                    c += scores[o];
                }
                else if (o < 46)
                {
                    d += scores[o];
                }
                else if (o < 53)
                {
                    e += scores[o];
                }
                else if (o < 65)
                {
                    f += scores[o];
                }
                else if (o < 74)
                {
                    g += scores[o];
                }
                else if (o < 84)
                {
                    h += scores[o];
                }
                else if (o < 91)
                {
                    i += scores[o];
                }
                else if (o < 95)
                {
                    j += scores[o];
                }
                else if (o < 99)
                {
                    k += scores[o];
                }
                else if (o < 116)
                {
                    l += scores[o];
                }
                else if (o < 122)
                {
                    m += scores[o];
                }
                else if (o < 125)
                {
                    n += scores[o];
                }
            }

            var list = new List<string>
            {
                $"Kayıt (Registration){GetEmptyPointsString(registrationIndexes, zeroPointIndexes)}: {registration} ({GetInterpretation(registrationRanges, registration)})",
                $"Araştırma (Seeking){GetEmptyPointsString(seekingIndexes, zeroPointIndexes)}: {seeking} ({GetInterpretation(seekingRanges, seeking)})",
                $"Hassasiyet (Sensitivity){GetEmptyPointsString(sensitivityIndexes, zeroPointIndexes)}: {sensitivity} ({GetInterpretation(sensitivityRanges, sensitivity)})",
                $"Kaçınma (Avoiding){GetEmptyPointsString(avoidingIndexes, zeroPointIndexes)}: {avoiding} ({GetInterpretation(avoidingRanges, avoiding)})",
                $"Duyusal Girdi Arama (Sensory Seeking){GetEmptyPointsString(sensorySeekingIndexes, zeroPointIndexes)}: {sensorySeeking} ({GetInterpretation(sensorySeekingRanges, sensorySeeking)})",
                $"Duygusal Tepki Arama (Emotionally Reactive){GetEmptyPointsString(emotionallyReactiveIndexes, zeroPointIndexes)}: {emotionallyReactive} ({GetInterpretation(emotionallyReactiveRanges, emotionallyReactive)})",
                $"Düşük Endurans / Tonus (Low Endurance / Tone){GetEmptyPointsString(lowEnduranceToneIndexes, zeroPointIndexes)}: {lowEnduranceTone} ({GetInterpretation(lowEnduranceToneRanges, lowEnduranceTone)})",
                $"Oral Duyusal Hassasiyet (Oral Sensory Sensitivity){GetEmptyPointsString(oralSensorySensitivityIndexes, zeroPointIndexes)}: {oralSensorySensitivity} ({GetInterpretation(oralSensorySensitivityRanges, oralSensorySensitivity)})",
                $"Dikkatsizlik / Dikkat Dağınıklığı (Inattention / Distractibility){GetEmptyPointsString(inattentionIndexes, zeroPointIndexes)}: {inattention} ({GetInterpretation(inattentionRanges, inattention)})",
                $"Zayıf Kayıt (Poor Registration){GetEmptyPointsString(poorRegistrationIndexes, zeroPointIndexes)}: {poorRegistration} ({GetInterpretation(poorRegistrationRanges, poorRegistration)})",
                $"Duyu Hassasiyeti (Sensory Sensitivity){GetEmptyPointsString(sensorySensitivityIndexes, zeroPointIndexes)}: {sensorySensitivity} ({GetInterpretation(sensorySensitivityRanges, sensorySensitivity)})",
                $"Hareketsiz (Sedentary){GetEmptyPointsString(sedentaryIndexes, zeroPointIndexes)}: {sedentary} ({GetInterpretation(sedentaryRanges, sedentary)})",
                $"Algısal İnce Motor (Fine Motor / Perceptual){GetEmptyPointsString(fineMotorIndexes, zeroPointIndexes)}: {fineMotor} ({GetInterpretation(fineMotorRanges, fineMotor)})",
                $"A. Duyma İşlemi (Auditory Processing){GetEmptyPointsString(aIndexes, zeroPointIndexes)}: {a} ({GetInterpretation(aRanges, a)})",
                $"B. Görme İşlemi (Visual Processing){GetEmptyPointsString(bIndexes, zeroPointIndexes)}: {b} ({GetInterpretation(bRanges, b)})",
                $"C. Vestibüler Sistem (Vestibular Processing){GetEmptyPointsString(cIndexes, zeroPointIndexes)}: {c} ({GetInterpretation(cRanges, c)})",
                $"D. Dokunma İşlemi (Touch Processing){GetEmptyPointsString(dIndexes, zeroPointIndexes)}: {d} ({GetInterpretation(dRanges, d)})",
                $"E. Çoklu Duyusal İşlem (Multisensory Processing){GetEmptyPointsString(eIndexes, zeroPointIndexes)}: {e} ({GetInterpretation(eRanges, e)})",
                $"F. Oral Duyusal İşlem (Oral Sensory Processing){GetEmptyPointsString(fIndexes, zeroPointIndexes)}: {f} ({GetInterpretation(fRanges, f)})",
                $"G. Endurans ve Tonusla İlgili Duyusal İşlem (Sensory Processing Related to Endurance / Tone){GetEmptyPointsString(gIndexes, zeroPointIndexes)}: {g} ({GetInterpretation(gRanges, g)})",
                $"H. Hareket ve Vücut Pozisyonu ile İlgili Düzenlemeler (Modulation Related to Body Position and Movement){GetEmptyPointsString(hIndexes, zeroPointIndexes)}: {h} ({GetInterpretation(hRanges, h)})",
                $"I. Aktivite Seviyesini Etkileyen Hareket Düzenlemeleri (Modulation of Movement Affecting Activity Level){GetEmptyPointsString(iIndexes, zeroPointIndexes)}: {i} ({GetInterpretation(iRanges, i)})",
                $"J. Duygusal Cevapları Etkileyen Duyusal Girdilerin Düzenlenmesi (Modulation of Sensory Input Affecting Emotional Responses){GetEmptyPointsString(jIndexes, zeroPointIndexes)}: {j} ({GetInterpretation(jRanges, j)})",
                $"K. Duygusal Cevapları ve Aktivite Seviyesini Etkileyen Görsel Girdilerin Düzenlenmesi (Modulation of Visual Input Affecting Emotional Responses and Activity Level){GetEmptyPointsString(kIndexes, zeroPointIndexes)}: {k} ({GetInterpretation(kRanges, k)})",
                $"L. Duygusal ve Sosyal Cevaplar (Emotional / Social Responses){GetEmptyPointsString(lIndexes, zeroPointIndexes)}: {l} ({GetInterpretation(lRanges, l)})",
                $"M. Duyusal İşlemin Davranışsal Sonuçları (Behavioral Outcomes of Sensory Processing){GetEmptyPointsString(mIndexes, zeroPointIndexes)}: {m} ({GetInterpretation(mRanges, m)})",
                $"N. Tepki Verme Eşiğini Tanımlayan Maddeler (Items Indicating Thresholds for Response){GetEmptyPointsString(nIndexes, zeroPointIndexes)}: {n} ({GetInterpretation(nRanges, n)})"
            };

            var scoresWindow = new Scores(patientName.Text, patientAgeYear.Text, patientAgeMonth.Text, list);
            scoresWindow.Show();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            SetTextBlocksTexts();
        }

        private void CleanClicked(object sender, RoutedEventArgs routedEventArgs)
        {
            var result = MessageBox.Show("Değerler temizlenecektir. Onaylıyor musunuz?", "Onay", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                foreach (TextBox tb in FindVisualChildren<TextBox>(Window))
                {
                    tb.Text = "";
                }
            }
        }

        private string GetEmptyPointsString(IEnumerable<int> scoreIndexes, IEnumerable<int> zeroIndexes)
        {
            var result = " ";

            foreach (var zero in zeroIndexes)
            {
                if (scoreIndexes.Contains(zero))
                {
                    result += $"({zero}) ";
                }
            }

            return result;
        }
    }
}

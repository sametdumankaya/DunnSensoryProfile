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

        //düşük endurans / tons
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

        public MainWindow()
        {
            InitializeComponent();
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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-5]+");
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
                $"Kayıt (Registration){GetEmptyPointsString(registrationIndexes, zeroPointIndexes)}: {registration}",
                $"Araştırma (Seeking){GetEmptyPointsString(seekingIndexes, zeroPointIndexes)}: {seeking}",
                $"Hassasiyet (Sensitivity){GetEmptyPointsString(sensitivityIndexes, zeroPointIndexes)}: {sensitivity}",
                $"Kaçınma (Avoiding){GetEmptyPointsString(avoidingIndexes, zeroPointIndexes)}: {avoiding}",
                $"Duyusal Girdi Arama (Sensory Seeking){GetEmptyPointsString(sensorySeekingIndexes, zeroPointIndexes)}: {sensorySeeking}",
                $"Duygusal Tepki Arama (Emotionally Reactive){GetEmptyPointsString(emotionallyReactiveIndexes, zeroPointIndexes)}: {emotionallyReactive}",
                $"Düşük Endurans / Tons (Low Endurance / Tone){GetEmptyPointsString(lowEnduranceToneIndexes, zeroPointIndexes)}: {lowEnduranceTone}",
                $"Oral Duyusal Hassasiyet (Oral Sensory Sensitivity){GetEmptyPointsString(oralSensorySensitivityIndexes, zeroPointIndexes)}: {oralSensorySensitivity}",
                $"Dikkatsizlik / Dikkat Dağınıklığı (Inattention / Distractibility){GetEmptyPointsString(inattentionIndexes, zeroPointIndexes)}: {inattention}",
                $"Zayıf Kayıt (Poor Registration){GetEmptyPointsString(poorRegistrationIndexes, zeroPointIndexes)}: {poorRegistration}",
                $"Duyu Hassasiyeti (Sensory Sensitivity){GetEmptyPointsString(sensorySensitivityIndexes, zeroPointIndexes)}: {sensorySensitivity}",
                $"Hareketsiz (Sedentary){GetEmptyPointsString(sedentaryIndexes, zeroPointIndexes)}: {sedentary}",
                $"Algısal İnce Motor (Fine Motor / Perceptual){GetEmptyPointsString(fineMotorIndexes, zeroPointIndexes)}: {fineMotor}",
                $"A. Duyma İşlemi (Auditory Processing){GetEmptyPointsString(aIndexes, zeroPointIndexes)}: {a}",
                $"B. Görme İşlemi (Visual Processing){GetEmptyPointsString(bIndexes, zeroPointIndexes)}: {b}",
                $"C. Vestibüler Sistem (Vestibular Processing){GetEmptyPointsString(cIndexes, zeroPointIndexes)}: {c}",
                $"D. Dokunma İşlemi (Touch Processing){GetEmptyPointsString(dIndexes, zeroPointIndexes)}: {d}",
                $"E. Çoklu Duyusal İşlem (Multisensory Processing){GetEmptyPointsString(eIndexes, zeroPointIndexes)}: {e}",
                $"F. Oral Duyusal İşlem (Oral Sensory Processing){GetEmptyPointsString(fIndexes, zeroPointIndexes)}: {f}",
                $"G. Endurans ve Tonusla İlgili Duyusal İşlem (Sensory Processing Related to Endurance / Tone){GetEmptyPointsString(gIndexes, zeroPointIndexes)}: {g}",
                $"H. Hareket ve Vücut Pozisyonu ile İlgili Düzenlemeler (Modulation Related to Body Position and Movement){GetEmptyPointsString(hIndexes, zeroPointIndexes)}: {h}",
                $"I. Aktivite Seviyesini Etkileyen Hareket Düzenlemeleri (Modulation of Movement Affecting Activity Level){GetEmptyPointsString(iIndexes, zeroPointIndexes)}: {i}",
                $"J. Duygusal Cevapları Etkileyen Duyusal Girdilerin Düzenlenmesi (Modulation of Sensory Input Affecting Emotional Responses){GetEmptyPointsString(jIndexes, zeroPointIndexes)}: {j}",
                $"K. Duygusal Cevapları ve Aktivite Seviyesini Etkileyen Görsel Girdilerin Düzenlenmesi (Modulation of Visual Input Affecting Emotional Responses and Activity Level){GetEmptyPointsString(kIndexes, zeroPointIndexes)}: {k}",
                $"L. Duygusal ve Sosyal Cevaplar (Emotional / Social Responses){GetEmptyPointsString(lIndexes, zeroPointIndexes)}: {l}",
                $"M. Duyusal İşlemin Davranışsal Sonuçları (Behavioral Outcomes of Sensory Processing){GetEmptyPointsString(mIndexes, zeroPointIndexes)}: {m}",
                $"N. Tepki Verme Eşiğini Tanımlayan Maddeler (Items Indicating Thresholds for Response){GetEmptyPointsString(nIndexes, zeroPointIndexes)}: {n}"
            };

            var scoresWindow = new Scores(list);
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
                foreach (TextBox tb in FindVisualChildren<TextBox>(ScrollViewer))
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

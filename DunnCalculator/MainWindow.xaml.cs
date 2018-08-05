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

            foreach (TextBox tb in FindVisualChildren<TextBox>(ScrollViewer))
            {
                try
                {
                    var number = Convert.ToInt32(tb.Text);
                    scores.Add(number);
                    count++;
                }
                catch (FormatException)
                {
                    MessageBox.Show($"Puan {count} değeri boş veya hatalı !", "Hata");
                    hasError = true;
                    break;
                    ;
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
                $"Kayıt (Registration): {registration}",
                $"Araştırma (Seeking): {seeking}",
                $"Hassasiyet (Sensitivity): {sensitivity}",
                $"Kaçınma (Avoiding): {avoiding}",
                $"Duyusal Girdi Arama (Sensory Seeking): {sensorySeeking}",
                $"Duygusal Tepki Arama (Emotionally Reactive): {emotionallyReactive}",
                $"Düşük Endurans / Tons (Low Endurance / Tone): {lowEnduranceTone}",
                $"Oral Duyusal Hassasiyet (Oral Sensory Sensitivity): {oralSensorySensitivity}",
                $"Dikkatsizlik / Dikkat Dağınıklığı (Inattention / Distractibility): {inattention}",
                $"Zayıf Kayıt (Poor Registration): {poorRegistration}",
                $"Duyu Hassasiyeti (Sensory Sensitivity): {sensorySensitivity}",
                $"Hareketsiz (Sedentary): {sedentary}",
                $"Algısal İnce Motor (Fine Motor / Perceptual): {fineMotor}",
                $"Duyma İşlemi (Auditory Processing): {a}",
                $"Görme İşlemi (Visual Processing): {b}",
                $"Vestibüler Sistem (Vestibular Processing): {c}",
                $"Dokunma İşlemi (Touch Processing): {d}",
                $"Çoklu Duyusal İşlem (Multisensory Processing): {e}",
                $"Oral Duyusal İşlem (Oral Sensory Processing): {f}",
                $"Endurans ve Tonusla İlgili Duyusal İşlem (Sensory Processing Related to Endurance / Tone): {g}",
                $"Hareket ve Vücut Pozisyonu ile İlgili Düzenlemeler (Modulation Related to Body Position and Movement): {h}",
                $"Aktivite Seviyesini Etkileyen Hareket Düzenlemeleri (Modulation of Movement Affecting Activity Level): {i}",
                $"Duygusal Cevapları Etkileyen Duyusal Girdilerin Düzenlenmesi (Modulation of Sensory Input Affecting Emotional Responses): {j}",
                $"Duygusal Cevapları ve Aktivite Seviyesini Etkileyen Görsel Girdilerin Düzenlenmesi (Modulation of Visual Input Affecting Emotional Responses and Activity Level): {k}",
                $"Duygusal ve Sosyal Cevaplar (Emotional / Social Responses): {l}",
                $"Duyusal İşlemin Davranışsal Sonuçları (Behavioral Outcomes of Sensory Processing): {m}",
                $"Tepki Verme Eşiğini Tanımlayan Maddeler (Items Indicating Thresholds for Response): {n}"
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
    }
}

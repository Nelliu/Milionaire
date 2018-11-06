using makeQuestions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

namespace Milionare
{
    /// <summary>
    /// Interakční logika pro Game.xaml
    /// </summary>
    public partial class Game : Page
    {

        public static int status = 0;
        public static int money = 0;
        public static int Difficulity = 1;
        public static string RightAns = "";
        public static string Question1 = "";
        private bool used = false; // if specators help % was used

        public Game()
        {
            InitializeComponent();
            questionSet();
            
        }


        public static List<int> Prizes = new List<int>() { 0,10000,50000,100000,250000,500000,650000,800000,950000,1000000,10000000};
        private void finishCheck()
        {
            if(Difficulity == 10)
            {
                status = 2;
                lostDelay();
            }
            else
            {
                Difficulity++;
                questionSet();
            }
            
        }
        private void delay()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Start();
            timer.Tick += (sender, args) =>
            {
                LinearGradientBrush linbrush = new LinearGradientBrush();
                linbrush.EndPoint = new Point(0.5, 1);
                linbrush.StartPoint = new Point(0.5, 0);
                GradientStop gradStop = new GradientStop();
                gradStop.Color = Colors.Black;                          // style of answer buttons set up back
                gradStop.Offset = 0;
                GradientStop gradStop1 = new GradientStop();
                gradStop1.Color = (Color)ColorConverter.ConvertFromString("#FF181568");
                gradStop1.Offset = 1;
                linbrush.GradientStops.Add(gradStop);
                linbrush.GradientStops.Add(gradStop1);

                timer.Stop();
                finishCheck();
                ans3.Background = linbrush;
                ans1.Background = linbrush;
                ans2.Background = linbrush;
                ans4.Background = linbrush;
                ans1.IsEnabled = true;
                ans2.IsEnabled = true;
                ans3.IsEnabled = true;
                ans4.IsEnabled = true;
                money = Prizes[Difficulity];
                
                if (used)
                {
                    bHint.Visibility = Visibility.Hidden;
                    An1.Visibility = Visibility.Hidden;
                    An2.Visibility = Visibility.Hidden;
                    An3.Visibility = Visibility.Hidden;
                    An4.Visibility = Visibility.Hidden;
                }
               


            };
        }
        private void lostDelay()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Start();
            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                gameOverRedirect();

            };
        }
        private void gameOverRedirect()
        {
            if (status != 2)
            {
                status = 1;
            }
            this.NavigationService.Navigate(new GameOver());
        }
        private void questionSet()
        {
            var jsonData = File.ReadAllText("questions.json");
            List<Data> data1 = JsonSerialization.ReadFromJsonFile<List<Data>>("questions.json");
            jsonData = JsonConvert.SerializeObject(data1);              // Read file with questions <
            File.WriteAllText("questions.json", jsonData);

            Random rand = new Random();
            int ListLenght = data1.Count();
            List<Data> chosenQ = new List<Data>();

            for (int i = 0; i < ListLenght; i++)
            {
                if (data1[i].Diff == Difficulity)
                {
                    chosenQ.Add(data1[i]);
                }
            }
            
            int QChoose = rand.Next(0, chosenQ.Count());

            List<string> Answers = new List<string>();
            Question.Text = chosenQ[QChoose].Question;

            Answers.Add(chosenQ[QChoose].RightAnswer);
            Answers.Add(chosenQ[QChoose].Answer2);
            Answers.Add(chosenQ[QChoose].Answer3);
            Answers.Add(chosenQ[QChoose].Answer4);

            List<int> possible = Enumerable.Range(0, 4).ToList();
            List<int> listNumbers = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                int index = rand.Next(0, possible.Count);
                listNumbers.Add(possible[index]);
                possible.RemoveAt(index);
            }

            ans1.Content = Answers[listNumbers[0]];
            ans2.Content = Answers[listNumbers[1]];
            ans3.Content = Answers[listNumbers[2]];
            ans4.Content = Answers[listNumbers[3]];
            RightAns = chosenQ[QChoose].RightAnswer;
            Question1 = chosenQ[QChoose].Question;
            
        }

        private void ans1_Click(object sender, RoutedEventArgs e)
        {
            if (ans1.Content == RightAns)
            {
                ans1.Background = Brushes.Green;
                delay();
                
            }
            else
            {
                ans1.Background = Brushes.Red;
                lostDelay();
            }
        }
        private void ans2_Click(object sender, RoutedEventArgs e)
        {
            if (ans2.Content == RightAns)
            {
                ans2.Background = Brushes.Green;
                delay();
                
            }
            else
            {
                ans2.Background = Brushes.Red;
                lostDelay();
            }
        }
        private void ans3_Click(object sender, RoutedEventArgs e)
        {
            if (ans3.Content == RightAns)
            {
                ans3.Background = Brushes.Green;
                delay();
                
            }
            else
            {
                ans3.Background = Brushes.Red;
                lostDelay();
            }
        }
        private void ans4_Click(object sender, RoutedEventArgs e)
        {
            if (ans4.Content == RightAns)
            {
                ans4.Background = Brushes.Green;
                delay();
            }
            else
            {
                ans4.Background = Brushes.Red;
                lostDelay();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) // 50/50 hint button click
        {
            fifty.IsEnabled = false;
            int disabled = 0;
            Random rand = new Random();

            List<int> possible = Enumerable.Range(0, 4).ToList();
            List<int> listNumbers = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                int index = rand.Next(0, possible.Count);
                listNumbers.Add(possible[index]);
                possible.RemoveAt(index);
            }
            for (int i = 0; i < 4; i++)
            {
                if (disabled != 2)
                {
                    switch (i)
                    {
                        case 1:
                            if (ans1.Content == RightAns)
                            {
                                break;
                            }
                            else
                            {
                                ans1.IsEnabled = false;
                                disabled++;
                                break;
                            }

                        case 2:
                            if (ans2.Content == RightAns)
                            {
                                break;
                            }
                            else
                            {
                                ans2.IsEnabled = false;
                                disabled++;
                                break;
                            }

                        case 3:
                            if (ans3.Content == RightAns)
                            {
                                break;
                            }
                            else
                            {
                                ans3.IsEnabled = false;
                                disabled++;
                                break;
                            }

                        case 4:
                            if (ans4.Content == RightAns)
                            {
                                break;
                            }
                            else
                            {
                                ans4.IsEnabled = false;
                                disabled++;
                                break;
                            }

                    }
                }
                
                    
            }
            
        }
        private void Button_Click2(object sender, RoutedEventArgs e) // watcher hint click
        {
            int disabled = 0;
            watcher.IsEnabled = false;
            if (Difficulity < 6)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (disabled != 1)
                    {
                        switch (i)
                        {
                            case 1:
                                if (ans1.Content == RightAns)
                                {
                                    ans1.Background = Brushes.Yellow;
                                    disabled++;
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                                

                            case 2:
                                if (ans2.Content == RightAns)
                                {
                                    ans2.Background = Brushes.Yellow;
                                    disabled++;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            case 3:
                                if (ans3.Content == RightAns)
                                {
                                    ans3.Background = Brushes.Yellow;
                                    disabled++;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            case 4:
                                if (ans4.Content == RightAns)
                                {
                                    ans4.Background = Brushes.Yellow;
                                    disabled++;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                        }
                    }


                }
            }
            else
            {
                Random rand = new Random();
                int number = rand.Next(0, 4);

                if(number == 0)
                {
                    ans1.Background = Brushes.Yellow;
                }
                else if(number == 1)
                {
                    ans2.Background = Brushes.Yellow;
                    
                } 
                else if(number == 2)
                {
                    ans3.Background = Brushes.Yellow;

                }
                else
                {
                    ans4.Background = Brushes.Yellow;

                }



            }
        }
        private void Button_Click3(object sender, RoutedEventArgs e) // spectators help 
        {
            bHint.Visibility = Visibility.Visible;
            used = true;
            spectators.IsEnabled = false;
            Random rand = new Random();
            int an1 = 0;
            int an2 = 0;
            int an3 = 0;
            int an4 = 0;
            List<int> possible = Enumerable.Range(0, 3).ToList();
            List<int> listNumbers = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                int index = rand.Next(0, possible.Count);
                listNumbers.Add(possible[index]);
                possible.RemoveAt(index);
            }

            for (int i = 0; i < 100; i++)
            {
                int a = rand.Next(0, 6);
                if(a == 1)
                {
                    an1++;
                }
                else if(a == 2)
                {
                    an1++;
                }
                else if(a == 3)
                {
                    an1++;
                }
                else if(a == 4)
                {
                    an2++;
                }
                else if(an2 == 5)
                {
                    an3++;
                }
                else
                {
                    an4++;
                }

            }
            
            if(ans1.Content == RightAns)
            {
                An1.Text = ans1.Content + " " + an1 + "%";
            }
            else
            {
                
                switch(listNumbers[0])
                {
                    case 0:
                        An1.Text = ans1.Content + " " + an2 + "%";
                        break;
                    case 1:
                        An1.Text = ans1.Content + " " + an3 + "%";
                        break;
                    case 2:
                        An1.Text = ans1.Content + " " + an4 + "%";
                        break;
                }

            }

            
            if(ans2.Content == RightAns)
            {
                An2.Text = ans2.Content + " " + an1 + "%";
            }
            else
            {
                int random = rand.Next(0, 3);
                switch (listNumbers[1])
                {
                    case 0:
                        An2.Text = ans2.Content + " " + an2 + "%";
                        break;
                    case 1:
                        An2.Text = ans2.Content + " " + an3 + "%";
                        break;
                    case 2:
                        An2.Text = ans2.Content + " " + an4 + "%";
                        break;
                }
            }



            if(ans3.Content == RightAns)
            {
                An3.Text = ans3.Content + " " + an1 + "%";
            }
            else
            {
                int random = rand.Next(0, 3);
                switch (listNumbers[2])
                {
                    case 0:
                        An3.Text = ans3.Content + " " + an2 + "%";
                        break;
                    case 1:
                        An3.Text = ans3.Content + " " + an3 + "%";
                        break;
                    case 2:
                        An3.Text = ans3.Content + " " + an4 + "%";
                        break;
                }
            }



            if(ans4.Content == RightAns)
            {
                An4.Text = ans4.Content + " " + an1 + "%";
            }
            else
            {
                int random = rand.Next(0, 3);
                switch (listNumbers[2])
                {
                    case 0:
                        An4.Text = ans4.Content + " " + an2 + "%";
                        break;
                    case 1:
                        An4.Text = ans4.Content + " " + an3 + "%";
                        break;
                    case 2:
                        An4.Text = ans4.Content + " " + an4 + "%";
                        break;
                }
            }

            
        }
    }
}

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

namespace Milionare
{
    /// <summary>
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : Page
    {
        public GameOver()
        {
            InitializeComponent();
            check();
        }
        private void check()
        {
           if(Game.status == 2)
            {
                FinalBox2.Visibility = Visibility.Hidden;
                FinalBox1.Visibility = Visibility.Visible;
                FinalBox1.Text = "Vyhrál jsi 10 000 000!!!";
            }
           else if(Game.Difficulity >= 5 && Game.status == 1)
           {
                FinalBox1.Visibility = Visibility.Visible;
                FinalBox1.Text = "Vyhrál jsi 500 000!!";
           }
           if (Game.status == 1)
            {
                string answ = Game.RightAns;
                string quest = Game.Question1;
                FinalBox2.Text = quest + " : " + answ;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
          
            this.NavigationService.Navigate(new Menu());
        }
    }
}

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
using System.Windows.Shapes;

namespace RPG_Jahr_words
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Confirm : Window
    {
        public static bool choix = true;
        public Confirm(string phrase)
        {
            InitializeComponent();
            textBlock.Text = phrase;
        }

        private void yup(object sender, RoutedEventArgs e)
        {
            choix = true;
            this.Close();
        }

        private void nop(object sender, RoutedEventArgs e)
        {
            choix = false;
            Close();
        }
    }
}

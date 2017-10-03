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
    /// Logique d'interaction pour Perso_newCat.xaml
    /// </summary>

    public partial class AddSmthng : Window
    {
        private string _nouveau = "";
        private bool _maj = false;
        private bool _validate = false;

        public AddSmthng(string what)
        {
            InitializeComponent();
            DataContext = this;
            txt.Text = what;
        }

        public string Nouveau { get => _nouveau; set => _nouveau = value; }
        public bool Maj { get => _maj; set => _maj = value; }
        public bool Validate { get => _validate; set => _validate = value; }

        private void Valid_Click(object sender, RoutedEventArgs e)
        {
            Nouveau = textBox.Text;
            Maj = char.IsUpper(textBox.Text[0]);
            Validate = true;
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) { }

        private void Button_Click(object sender, RoutedEventArgs e) { Close(); }
    }
}

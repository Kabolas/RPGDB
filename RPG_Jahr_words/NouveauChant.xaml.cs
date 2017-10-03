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
using System.Data.SqlClient;

namespace RPG_Jahr_words
{
    /// <summary>
    /// Logique d'interaction pour NouveauChant.xaml
    /// </summary>
    public partial class NouveauChant : Window
    {
        private bool _cat, _chant, _succes, _valid;
        private string _chmp = "";


        public NouveauChant() { InitializeComponent(); DataContext = this; }

        public bool Cat { get => _cat; set => _cat = value; }
        public bool Chant { get => _chant; set => _chant = value; }
        public bool Succes { get => _succes; set => _succes = value; }

        private void Abord(object sender, RoutedEventArgs e)
        {
            Succes = false;
            this.Close();
        }

        public bool Valid { get => _valid; set => _valid = value; }
        public string Chmp { get => _chmp; set => _chmp = value; }

        private void Save(object sender, RoutedEventArgs e)
        {
            Succes = true;
            Valid = char.IsUpper(Chmp[0]) && Chmp =="";
            Close();
        }
    }
}

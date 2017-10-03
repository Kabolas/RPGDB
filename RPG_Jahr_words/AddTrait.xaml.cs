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
    /// Logique d'interaction pour AddTrait.xaml
    /// </summary>
    public partial class AddTrait : Window
    {

        private Trais nouveau = new Trais();
        private bool _valid, _maj;
        public AddTrait()
        {
            InitializeComponent();
            DataContext = this;
        }

        public Trais Nouveau { get => nouveau; set => nouveau = value; }
        public bool Valid { get => _valid; set => _valid = value; }
        public bool Maj { get => _maj; set => _maj = value; }

        private void Validate(object sender, RoutedEventArgs e)
        {
            Valid = true;
            Maj = char.IsUpper(Nouveau.nom[0]);
            Close();
        }

        private void Anuler(object sender, RoutedEventArgs e)
        {
            Valid = false;
            Close();
        }
    }
}

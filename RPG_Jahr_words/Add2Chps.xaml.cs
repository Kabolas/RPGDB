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
    /// Logique d'interaction pour Add2Chps.xaml
    /// </summary>
    public partial class Add2Chps : Window
    {
        private string _ch1, ch2;
        private bool _validate, _maj, _both, _valid;
        public Add2Chps(string instruct, bool need)
        {
            InitializeComponent();
            instructions.Text = instruct;
            Both = need;
            DataContext = this;
        }

        public string Ch1 { get => _ch1; set => _ch1 = value; }

        private void Validation(object sender, RoutedEventArgs e)
        {
            Validate = true;
            Maj = char.IsUpper(Ch1[0]);
            if (Both) Valid = Maj && Ch2 != "";
            else Valid = true;
            this.Close();
        }

        public string Ch2 { get => ch2; set => ch2 = value; }
        public bool Validate { get => _validate; set => _validate = value; }
        public bool Maj { get => _maj; set => _maj = value; }
        public bool Both { get => _both; set => _both = value; }
        public bool Valid { get => _valid; set => _valid = value; }
    }
}

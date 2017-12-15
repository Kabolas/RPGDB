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

namespace RPG_Jahr_words
{
    /// <summary>
    /// Logique d'interaction pour GenOptions.xaml
    /// </summary>
    public partial class GenOptions : UserControl
    {
        private int _letters;
        private string _wordinword;
        private bool _before, _tripht, _symbole;

        public NameGen Gen
        {
            get => (NameGen)GetValue(GenProperty);
            set => SetValue(GenProperty, value);
        }

        public static readonly DependencyProperty GenProperty =
            DependencyProperty.Register("Gen",
                typeof(NameGen),
                typeof(GenOptions), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));


        public int Value { get { return _letters; } set { _letters = (int)value; } }
        public string Word { get { return _wordinword; } set { _wordinword = value; } }
        public bool Symbol { get { return _symbole; } set { _symbole = value; } }
        public bool Before { get { return _before; } set { _before = value; } }
        public bool Triphtongue { get { return _tripht; } set { _tripht = value; } }
        public GenOptions()
        {
            Value = 5;
            Word = "";
            Before = true;
            Triphtongue = _symbole = false;
            InitializeComponent();
            DataContext = this;
        }

        public string Generation() { return Gen.default_Generation_Sons(Value, Word, Before, Triphtongue, Symbol); }
        public string Generation_gn() { return Gen.Generation_gn_Sons(Value, Word, Before, Triphtongue, Symbol); }
    }
}

using System;
using System.Collections;
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
    /// Logique d'interaction pour AddRestrict.xaml
    /// </summary>
    public partial class AddRestrict : Window
    {
        private bool _validate, _isValid, _maj;
        private string _ch1 = "", _ch2;
        private IList _choices;
        private object choice;

        public bool Validate { get => _validate; set => _validate = value; }
        public bool IsValid { get => _isValid; set => _isValid = value; }
        public string Ch1 { get => _ch1; set => _ch1 = value; }
        public IList Choices { get => _choices; set => _choices = value; }
        public object Choice { get => choice; set => choice = value; }
        public bool Maj { get => _maj; set => _maj = value; }
        public string Ch2 { get => _ch2; set => _ch2 = value; }

        public AddRestrict(string instruct, System.Collections.IEnumerable population, SelectionMode mode, string displayMember, bool doubleEntry = false)
        {
            InitializeComponent();
            instructions.Text = instruct;
            List.ItemsSource = population;
            List.SelectionMode = mode;
            List.DisplayMemberPath = displayMember;
            DataContext = this;
        }

        private void Validation(object sender, RoutedEventArgs e)
        {
            Validate = true;
            try
            {
                Maj = char.IsUpper(Ch1, 0);
                IsValid =  List.SelectedItems.Count == 0;
                if (List.SelectionMode == SelectionMode.Single)
                    Choice = List.SelectedItem;
                else
                    Choices = List.SelectedItems;
            }
            catch { IsValid = false; }
            Close();
        }

        private void Close(object sender, RoutedEventArgs e) { Close(); }
    }
}

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
    
    public enum Sorting{
        None,
        Up,
        Down
    }

    public class SortWrap
    {
        public Sorting Sorting;
    }
    /// <summary>
    /// Logique d'interaction pour GodControl.xaml
    /// </summary>
    public partial class GodControl : UserControl
    {
        private bool ignore = true;
        public NameGen Gen
        {
            get => (NameGen)GetValue(GenProperty);
            set => SetValue(GenProperty, value);
        }

        public static readonly DependencyProperty GenProperty =
            DependencyProperty.Register("Gen",
                typeof(NameGen),
                typeof(GodControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public RPGEntities15 Bdd
        {
            get => (RPGEntities15)GetValue(BddProperty);
            set => SetValue(BddProperty, value);
        }
        public static readonly DependencyProperty BddProperty =
            DependencyProperty.Register("Bdd",
                typeof(RPGEntities15),
                typeof(GodControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(ChargeFromDb)));

        private static void ChargeFromDb(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as GodControl).DataContext = new ViewModel.GodViewModel(e.NewValue as RPGEntities15);
        }

        public GodControl()
        {
            InitializeComponent();
        }

        private void Generation(object sender, RoutedEventArgs e) { God_name.Text = Gods.Generation_gn(); }

        private void SurGen(object sender, RoutedEventArgs e) { Godnick.Text = Gods.Generation_gn(); }

        private void Pant_Check(object sender, RoutedEventArgs e) { GodOrPant.isPant = true; }
        private void Pant_Unchecked(object sender, RoutedEventArgs e) { GodOrPant.isPant = false; }
        //private Sorting 
        private void ImportanceSelect()
        {
            if (importance.Text == "Absolue") absport.IsChecked = true;
            if (importance.Text == "Majeure") majport.IsChecked = true;
            if (importance.Text == "Mineure") minport.IsChecked = true;
            if (importance.Text == "Négligeable") negport.IsChecked = true;
        }

        private void MinImp(object sender, RoutedEventArgs e) { if (ignore && importance != null) importance.Text = "Mineure"; ignore = true; }

        private void AbImp(object sender, RoutedEventArgs e) { if (ignore && importance != null) importance.Text = "Absolue"; ignore = true; }

        private void MajImp(object sender, RoutedEventArgs e) { if (ignore && importance != null) importance.Text = "Majeure"; ignore = true; }

        private void NegImp(object sender, RoutedEventArgs e) { if (ignore && importance != null) importance.Text = "Négligeable"; ignore = true; }

        private void Changed(object sender, TextChangedEventArgs e)
        {
            ignore = false;
            ImportanceSelect();
            ignore = true;
        }

        private void Placeholder(object sender, SelectionChangedEventArgs e) { if (((ComboBox)sender).SelectedItem == null) ((ComboBox)sender).SelectedIndex = 0; }

        private void NameSort(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

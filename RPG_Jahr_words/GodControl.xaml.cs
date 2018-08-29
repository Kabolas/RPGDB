using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public enum Sorting
    {
        None,
        Up,
        Down
    }

    public class SortWrap
    {
        private Sorting _sorting;

        public Sorting Sorting { get => _sorting; set => _sorting = value; }
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
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(divtown);
            //view.SortDescriptions.Add(new System.ComponentModel.SortDescription("nom", ))
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

        private void Ordering(ListView view, string sorting, Sorting direction)
        {
            view.Items.SortDescriptions.Clear();
            if (direction != Sorting.None)
                view.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(sorting, direction == Sorting.Up ? System.ComponentModel.ListSortDirection.Ascending : System.ComponentModel.ListSortDirection.Descending));
        }

        private void NameSort(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            Sorting s = ((SortWrap)column.Resources["Sort"]).Sorting;
            foreach (GridViewColumn col in ((GridView)divtown.View).Columns)
            {
                ((SortWrap)((GridViewColumnHeader)col.Header)?.Resources["Sort"]).Sorting = Sorting.None;
                ((GridViewColumnHeader)col.Header).Column.HeaderTemplate = null;
            }
            s = Sort(s, column);
            Ordering(divtown, column.Tag.ToString(), s);
        }

        private Sorting Sort(Sorting s, GridViewColumnHeader column)
        {
            switch (s)
            {
                case Sorting.None:
                    ((SortWrap)column.Resources["Sort"]).Sorting = s = Sorting.Up;
                    column.Column.HeaderTemplate = Resources["SortUp"] as DataTemplate;
                    break;
                case Sorting.Up:
                    ((SortWrap)column.Resources["Sort"]).Sorting = s = Sorting.Down;
                    column.Column.HeaderTemplate = Resources["SortDown"] as DataTemplate;
                    break;
                case Sorting.Down:
                    ((SortWrap)column.Resources["Sort"]).Sorting = s = Sorting.None;
                    column.Column.HeaderTemplate = null;
                    break;
                default:
                    break;
            }
            return s;
        }

        private void RegSort(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            Sorting s = ((SortWrap)column.Resources["Sort"]).Sorting;
            foreach (GridViewColumn col in ((GridView)Pantown.View).Columns)
            {
                ((SortWrap)((GridViewColumnHeader)col.Header)?.Resources["Sort"]).Sorting = Sorting.None;
                ((GridViewColumnHeader)col.Header).Column.HeaderTemplate = null;
            }
            s = Sort(s, column);
            Ordering(Pantown, column.Tag.ToString(), s);
        }
    }
}

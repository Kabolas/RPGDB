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
    /// Logique d'interaction pour EnchantControl.xaml
    /// </summary>
    public partial class EnchantControl : UserControl
    {
        private decimal minpricepose = 0, minpricesell = 0;
        public NameGen Gen
        {
            get => (NameGen)GetValue(GenProperty);
            set => SetValue(GenProperty, value);
        }

        public static readonly DependencyProperty GenProperty =
            DependencyProperty.Register("Gen",
                typeof(NameGen),
                typeof(EnchantControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public RPGEntities15 Bdd
        {
            get => (RPGEntities15)GetValue(BddProperty);
            set => SetValue(BddProperty, value);
        }
        public static readonly DependencyProperty BddProperty =
            DependencyProperty.Register("Bdd",
                typeof(RPGEntities15),
                typeof(EnchantControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(ChargeFromDb)));

        private static void ChargeFromDb(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EnchantControl).DataContext = new ViewModel.EnchantViewModel(e.NewValue as RPGEntities15);
        }

        public EnchantControl()
        {
            InitializeComponent();
        }

        private void Generation(object sender, RoutedEventArgs e) { Enchant_name.Text = Gen.Generation_gn_Sons(Ench.Value, Ench.Word, (bool)Ench.Before, Ench.Triphtongue, Ench.Symbol); }

        private void AjoutClick(object sender, RoutedEventArgs e)
        {
            if (Item_list.SelectedItems.Count>0)
            {
                foreach (Items item in Item_list.SelectedItems)
                {
                    RecipeItem b = new RecipeItem
                    {
                        N_recette = (int)recipeId.SelectedItem,
                        Component = (item),
                        Quantite = 0
                    };
                    (Item_rec.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeItem>).Add(b);
                    Component_type.SelectedIndex = 0;
                    Item_rec.ItemsSource = new System.Collections.ObjectModel.ObservableCollection<RecipeItem>((Item_rec.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeItem>).OrderBy(r => r.N_recette));
                    minpricepose += (decimal)(b).Component.prix_mago;
                    minpricesell += (decimal)(b).Component.prix_mago;
                    sellPlus.Text = "" + (decimal.Parse(sellPlus.Text) + (decimal)(b).Component.prix_mago);
                }
                Item_list.SelectedItems.Clear();
            }
            else Enchantu_Label.Text += "Veuillez selectionner un composant.\n";

        }

        private void Del_but_Click(object sender, RoutedEventArgs e)
        {
            if (Item_rec.SelectedItems != null)
            {
                List<object> remove = Item_rec.SelectedItems as List<object>;
                while (Item_rec.SelectedItems.Count > 0)
                {
                    minpricepose -= (decimal)(Item_rec.SelectedItems[0] as RecipeItem).Component.prix_mago;
                    minpricesell -= (decimal)(Item_rec.SelectedItems[0] as RecipeItem).Component.prix_mago;
                    sellPlus.Text = "" + (decimal.Parse(sellPlus.Text) - (decimal)(Item_rec.SelectedItems[0] as RecipeItem).Component.prix_mago);
                    (Item_rec.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeItem>).Remove(Item_rec.SelectedItems[0] as RecipeItem);
                }
            }
            else Enchantu_Label.Text += "Veuillez choisir au moins une entrée à supprimer de la liste.\n";
        }

        private void MinposePrice(object sender, TextChangedEventArgs e)
        {
            if(decimal.TryParse((sender as TextBox).Text, out decimal prix))
            {
                if (prix < minpricepose) (sender as TextBox).Text = "" + minpricepose;
            }
        }

        private void MinsellPrice(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse((sender as TextBox).Text, out decimal prix))
            {
                if (prix < minpricesell) (sender as TextBox).Text = "" + minpricesell;
            }
        }

        private void Effect_limits(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItems.Count > 3)
                (sender as ListBox).SelectedItems.Remove(e.AddedItems);
        }
    }
}

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
        public event EventHandler EnchantAdded, TypeAdded, EffectAdded;
        public event EventHandler CallItemRfresh, CallWeaponsRefresh;
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
            ViewModel.EnchantViewModel vm = new ViewModel.EnchantViewModel(e.NewValue as RPGEntities15);
            vm.EnchantCreated += (d as EnchantControl).RaiseEnchantCreated;
            vm.EffecCreated += (d as EnchantControl).RaiseEffectCreated;
            vm.TypeCreated += (d as EnchantControl).RaiseTypeCreated;
            (d as EnchantControl).CallItemRfresh += vm.RefreshItems;
            (d as EnchantControl).DataContext = vm;
        }

        private void RaiseTypeCreated(object sender, EventArgs e) { TypeAdded?.Invoke(sender, e); }

        private void RaiseEffectCreated(object sender, EventArgs e) { EffectAdded?.Invoke(sender, e); }

        private void RaiseEnchantCreated(object sender, EventArgs e) { EnchantAdded?.Invoke(sender, e); }

        public EnchantControl()
        {
            InitializeComponent();
        }

        private void Generation(object sender, RoutedEventArgs e) { Enchant_name.Text = Ench.Generation_gn(); }

        private void AjoutClick(object sender, RoutedEventArgs e)
        {
            if (Item_list.SelectedItems.Count > 0)
            {
                foreach (Items item in Item_list.SelectedItems)
                {
                    RecipeItem b = new RecipeItem
                    {
                        N_recette = (int)recipeId.SelectedItem,
                        Component = (item),
                        Quantite = 1
                    };
                    (Item_rec.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeItem>).Add(b);
                    Component_type.SelectedIndex = 0;
                    Item_rec.ItemsSource = new System.Collections.ObjectModel.ObservableCollection<RecipeItem>((Item_rec.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeItem>).OrderBy(r => r.N_recette));
                    minpricepose += (decimal)(b).Component.prix_mago;
                    minpricesell += (decimal)(b).Component.prix_mago / 2;
                    if (sellPlus.Text != "")
                    {
                        sellPlus.Text = ("" + (decimal.Parse(sellPlus.Text.Replace(".", ",")) + (decimal)(b).Component.prix_mago / 2)).Replace(",", ".");
                        posePrice.Text = ("" + (decimal.Parse(posePrice.Text.Replace(".", ",")) + (decimal)(b).Component.prix_mago)).Replace(",", ".");
                    }
                    else
                    {
                        sellPlus.Text = ("" + ((decimal)(b).Component.prix_mago / 2)).Replace(",", ".");
                        posePrice.Text = ("" + ((decimal)(b).Component.prix_mago)).Replace(",", ".");
                    }
                }
                Item_list.SelectedItems.Clear();
            }
            else Enchantu_Label.Text += "Veuillez selectionner un composant.\n";

        }

        internal void CallWeapsRefresh(object sender, EventArgs e) { CallWeaponsRefresh?.Invoke(sender, e); }
        internal void CallItemRefresh(object sender, EventArgs e) { CallItemRfresh?.Invoke(sender, e); }

        private void Del_but_Click(object sender, RoutedEventArgs e)
        {
            if (Item_rec.SelectedItems != null)
            {
                List<object> remove = Item_rec.SelectedItems as List<object>;
                while (Item_rec.SelectedItems.Count > 0)
                {
                    minpricepose -= (decimal)(Item_rec.SelectedItems[0] as RecipeItem).Component.prix_mago;
                    minpricesell -= (decimal)(Item_rec.SelectedItems[0] as RecipeItem).Component.prix_mago / 2;
                    sellPlus.Text = ("" + (decimal.Parse(sellPlus.Text.Replace('.', ',')) - (decimal)(Item_rec.SelectedItems[0] as RecipeItem).Component.prix_mago / 2)).Replace(',', '.');
                    posePrice.Text = ("" + (decimal.Parse(posePrice.Text.Replace('.', ',')) - (decimal)(Item_rec.SelectedItems[0] as RecipeItem).Component.prix_mago)).Replace(',', '.');
                    (Item_rec.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeItem>).Remove(Item_rec.SelectedItems[0] as RecipeItem);
                }
            }
            else Enchantu_Label.Text += "Veuillez choisir au moins une entrée à supprimer de la liste.\n";
        }

        private void MinposePrice(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse((sender as TextBox).Text, out decimal prix))
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

        private void MultPrice(object sender, TextChangedEventArgs e)
        {
            decimal pval = decimal.Parse(posePrice.Text.Replace('.', ',')) - minpricepose, sval = decimal.Parse(sellPlus.Text.Replace('.', ',')) - minpricesell;
            minpricesell = minpricepose = 0;
            foreach (RecipeItem rec in Item_rec.Items)
            {
                decimal n = (decimal)rec.Quantite;
                minpricepose += ((decimal)rec.Component.prix_mago)*n;
                minpricesell += ((decimal)rec.Component.prix_mago/2)*n;
                    }
            pval += minpricepose;
            sval += minpricesell;
            posePrice.Text = ("" + pval).Replace(',', '.');
            sellPlus.Text = ("" + sval).Replace(',', '.');
        }

        private void EnableChoice(object sender, SelectionChangedEventArgs e)
        {
            if (CatChoice.SelectedItems.Count == 1) PieceChoice.IsEnabled = (CatChoice.SelectedItem as Armor_cat).categorie != "Exosquelette";
            else PieceChoice.IsEnabled = true;
        }

        private void ShowEnchants(object sender, RoutedEventArgs e)
        {
            foreach (Enchantements enchant in Bdd.Enchantements.Where(ech => (showeffect.SelectedIndex > 0 ? ech.effects.Contains(showeffect.SelectedValue as string) : true) && (showtype.SelectedIndex > 0 ? ech.Enchant_Type == showtype.SelectedItem as Enchant_Type : true)
             && (ShowOri.SelectedIndex > 0 ? ech.Monde_w == ShowOri.SelectedItem as Monde_w : true) && ((bool)ShowUnder.IsChecked ? ech.niveau < int.Parse(ShowLevel.Text) : (bool)Showabove.IsChecked ? ech.niveau > int.Parse(ShowLevel.Text) : ech.niveau == int.Parse(ShowLevel.Text))
             && ((bool)showVarpow.IsChecked ? ech.power_on_craft : true) && ((bool)ShowCando.IsChecked ? ech.unlockable && ((bool)ShowOrd.IsChecked ? !ech.expert && !ech.lengendary : true) && ((bool)ShowExprt.IsChecked ? ech.expert : true) && ((bool)ShowLgndr.IsChecked ? ech.lengendary : true) : true)))
                Enchantu_Label.Text += enchant.nom + ", d'origine" + enchant.origine + " : " + enchant.type + (enchant.expert ? " expert" : enchant.lengendary ? " légendaire" : " ordinaire") + " de niveau " + enchant.niveau +
                    "de puissance " + (enchant.power_on_craft ? "variable d'un rapoort de " + enchant.rapport + " par rapport à la maitrise de l'enchantement en plus de la puissance de base de " + enchant.puissance + " de l'enchantement " : "" + enchant.puissance) +
                    "d'effet:" + enchant.effects +
                    "\nutilisable sur " +
                    (enchant.on_armor ? "des armures; à savoir:\n" + enchant.armors_cats + "\n" + enchant.armors + "\n" : "") +
                    (enchant.on_jewel ? "des bijoux; à savoir:\n" + enchant.jewels + "\n" : "") +
                    (enchant.on_cac ? "des armes de corps à corps; à savoir :\n" + enchant.weapons_cac + "\n" : "") +
                    (enchant.on_dist ? "des armes à distance; à savoir:\n" + enchant.weapons_dist + "\n" : "") +
                    (enchant.on_mag ? "des armes magiques; à savoir:\n" + enchant.weapons_mag + "\n" : "") +
                    (enchant.unlockable ? "dont la pose necessite : " + enchant.requirements : "") + enchant.descr;
        }

        private void PlaceHolder(object sender, SelectionChangedEventArgs e) { if (((ComboBox)sender).SelectedItem == null) ((ComboBox)sender).SelectedIndex = 0; }

        private void Effect_limits(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItems.Count > 3)
                (sender as ListBox).SelectedItems.Remove(e.AddedItems);
        }
    }
}

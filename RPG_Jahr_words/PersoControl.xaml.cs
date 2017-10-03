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
    /// <summary>
    /// Logique d'interaction pour PersoControl.xaml
    /// </summary>
    public partial class PersoControl : UserControl
    {
        public RPGEntities15 Bdd
        {
            get => (RPGEntities15)GetValue(BddProperty);
            set => SetValue(BddProperty, value);
        }
        public static readonly DependencyProperty BddProperty =
            DependencyProperty.Register("Bdd",
                typeof(RPGEntities15),
                typeof(PersoControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(ChargeFromDb)));

        private static void ChargeFromDb(DependencyObject d, DependencyPropertyChangedEventArgs e) { (d as PersoControl).DataContext = new ViewModel.PersoViewModel(e.NewValue as RPGEntities15); }
        public NameGen Gen
        {
            get => (NameGen)GetValue(GenProperty);
            set => SetValue(GenProperty, value);
        }
        public static readonly DependencyProperty GenProperty =
            DependencyProperty.Register("Gen",
                typeof(NameGen),
                typeof(PersoControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public PersoControl()
        {
            InitializeComponent();
            CollectionView stuf = (CollectionView)CollectionViewSource.GetDefaultView(StuffList.Items);
            //CollectionView stuf = (CollectionView)CollectionViewSource.GetDefaultView(StuffList.Items);
            //CollectionView stuf = (CollectionView)CollectionViewSource.GetDefaultView(StuffList.Items);
            CollectionView loot = (CollectionView)CollectionViewSource.GetDefaultView(ListLoot.Items);
            stuf.Filter = FilterStuff;
            loot.Filter = FilterLoot;
        }

        private bool FilterLoot(object obj)
        {
            if (Loottype.SelectedIndex > 0)
            {
                switch (Loottype.SelectedItem as string)
                {
                    case "Armes": return (obj as Items).Weaponry != null;
                    case "Armures": return (obj as Items).Armory != null;
                    case "Véhicule":
                    case "Vehicule": return (obj as Items).Vehicule != null;
                    case "Munition":
                    case "Munitions": return (obj as Items).Munition != null;
                    case "Alliages": return (obj as Items).Alliage != null;
                    case "Bijoux": return (obj as Items).Bijoux != null;
                    case "Consommable": return (obj as Items).Consommables != null;
                    case "Loot": return (obj as Items).Loot != null;
                    case "Livre": return (obj as Items).Livre != null;
                    case "Métaux": return (obj as Items).Mineraux != null && (obj as Items).Mineraux.Minerai_type.type == "Métal";
                    case "Parchemins": return (obj as Items).Parchemins != null;
                    case "Pierres":
                    case "Pierre": return (obj as Items).Mineraux != null && (obj as Items).Mineraux.Minerai_type.type == "Pierre";
                    case "Végétaux": return (obj as Items).Mineraux != null && (obj as Items).Mineraux.Minerai_type.type == "Végétal";
                    case "Conteneur": return (obj as Items).Conteneurs != null;
                    case "Commun":
                    case "Communs":
                        return (obj as Items).Armory == null && (obj as Items).Mineraux == null && (obj as Items).Bijoux == null &&
        (obj as Items).Weaponry == null && (obj as Items).Vehicule == null && (obj as Items).Loot == null && (obj as Items).Mineraux == null && (obj as Items).Munition == null && (obj as Items).Conteneurs == null && (obj as Items).Consommables
        == null && (obj as Items).Parchemins == null;
                    default:
                        return true;
                }
            }
            return true;
        }

        private bool FilterStuff(object obj)
        {
            if (Stufftype.SelectedIndex > 0)
            {
                switch ((Stufftype.SelectedItem as ComboBoxItem).Content as string)
                {
                    case "Armes": return (obj as Items).Weaponry != null;
                    case "Armures": return (obj as Items).Armory != null;
                    case "Véhicule":
                    case "Vehicule": return (obj as Items).Vehicule != null;
                    case "Munition":
                    case "Munitions": return (obj as Items).Munition != null;
                    case "Alliages": return (obj as Items).Alliage != null;
                    case "Bijoux": return (obj as Items).Bijoux != null;
                    case "Consommable": return (obj as Items).Consommables != null;
                    case "Loot": return (obj as Items).Loot != null;
                    case "Livre": return (obj as Items).Livre != null;
                    case "Métaux": return (obj as Items).Mineraux != null && (obj as Items).Mineraux.Minerai_type.type == "Métal";
                    case "Parchemins": return (obj as Items).Parchemins != null;
                    case "Pierres":
                    case "Pierre": return (obj as Items).Mineraux != null && (obj as Items).Mineraux.Minerai_type.type == "Pierre";
                    case "Végétaux": return (obj as Items).Mineraux != null && (obj as Items).Mineraux.Minerai_type.type == "Végétal";
                    case "Conteneur": return (obj as Items).Conteneurs != null;
                    case "Commun":
                    case "Communs":
                        return (obj as Items).Armory == null && (obj as Items).Mineraux == null && (obj as Items).Bijoux == null &&
        (obj as Items).Weaponry == null && (obj as Items).Vehicule == null && (obj as Items).Loot == null && (obj as Items).Mineraux == null && (obj as Items).Munition == null && (obj as Items).Conteneurs == null && (obj as Items).Consommables
        == null && (obj as Items).Parchemins == null;
                    default:
                        return true;
                }
            }
            return true;
        }

        private void Generation(object sender, RoutedEventArgs e) { Perso_name.Text = Gen.Generation_gn_Sons(Perso.Value, Perso.Word, Perso.Before, Perso.Triphtongue, Perso.Symbol); }

        private void TraitDetail(object sender, RoutedEventArgs e)
        {
            Inconvenient.Text = "Inconvenient:\n" + Bdd.Trais.ToList().Find(t => t.nom == (sender as Button).Tag as string).inconvenient;
            Avantages.Text = "Avantages:\n" + Bdd.Trais.ToList().Find(t => t.nom == (sender as Button).Tag as string).avantage;
        }

        private void Evol_Generation(object sender, RoutedEventArgs e) { Perso_evolved_name.Text = Gen.Generation_gn_Sons(Perso.Value, Perso.Word, Perso.Before, Perso.Triphtongue, Perso.Symbol); }

        private void AjoutClick(object sender, RoutedEventArgs e)
        {
            foreach (Items item in ListLoot.SelectedItems)
                (PersoLoot.ItemsSource as ObservableCollection<LootItem>).Add(new LootItem { Loot = item, Chance = 0, Quantite = 0 });
        }

        private void AjoutClickStuff(object sender, RoutedEventArgs e)
        {
            foreach (Items item in StuffList.SelectedItems)
                (ChosenStuff.ItemsSource as ObservableCollection<StuffItem>).Add(new StuffItem { Stuff = item, Nombre = 0 });
        }

        private void ElemCheck(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse((sender as TextBox).Text, out int inutil))
                (DataContext as ViewModel.PersoViewModel).ElementMasterCheck((sender as TextBox).Text);
        }

        private void RefreshSpell(object sender, SelectionChangedEventArgs e)
        {
            if(SpellList!= null)
            ((CollectionView)CollectionViewSource.GetDefaultView(SpellList.ItemsSource)).Refresh();
        }

        private void RefreshStuff(object sender, SelectionChangedEventArgs e)
        {
            if(StuffList!= null)
            ((CollectionView)CollectionViewSource.GetDefaultView(StuffList.ItemsSource)).Refresh();
        }

        private void RefreshLoot(object sender, SelectionChangedEventArgs e)
        {
            if(ListLoot!= null)
            ((CollectionView)CollectionViewSource.GetDefaultView(ListLoot.ItemsSource)).Refresh();
        }

        private void RefreshCombo(object sender, SelectionChangedEventArgs e)
        {
            if (ComboList != null)
                ((CollectionView)CollectionViewSource.GetDefaultView(ComboList.ItemsSource)).Refresh();
        }
    }
}

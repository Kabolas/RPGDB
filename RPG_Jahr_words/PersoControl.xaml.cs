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
            CollectionView weaps = (CollectionView)CollectionViewSource.GetDefaultView(weapMaster.Items);
            CollectionView spells = (CollectionView)CollectionViewSource.GetDefaultView(SpellList.Items);
            CollectionView combos = (CollectionView)CollectionViewSource.GetDefaultView(ComboList.Items);
            CollectionView loot = (CollectionView)CollectionViewSource.GetDefaultView(ListLoot.Items);
            stuf.Filter = FilterStuff;
            loot.Filter = FilterLoot;
            weaps.Filter = FilterWeps;
            combos.Filter = FilterSpells;
            combos.Filter = FilterCombs;
        }

        private bool FilterSpells(object obj)
        {
            if (SpellSchool.SelectedIndex > 0)
                return (obj as Sorts).Magie_type == SpellSchool.SelectedItem as Magie_type;
            return true;
        }

        private bool FilterCombs(object obj)
        {
            if (ComboType.SelectedIndex > 0)
                return (obj as Combo).ComboCat == ComboType.SelectedItem as ComboCat;
            return true;
        }

        private bool FilterWeps(object obj)
        {
            if (WeapSort.SelectedIndex > 0)
                switch (WeapSort.SelectedItem as string)
                {
                    case "Corps à corps":
                        return (obj as Perso_weap_Master).Weapon_type.type.Contains("Cac");
                    case "Distance":
                        return (obj as Perso_weap_Master).Weapon_type.type.Contains("Distance");
                    case "Magique":
                        return (obj as Perso_weap_Master).Weapon_type.type.Contains("Magique");
                    default:
                        return true;
                }
            return true;
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
            bool ret = true;
            if (stuffsize.SelectedIndex > 0)
                ret = ret && (obj as Items).Tailles == stuffsize.SelectedItem as Tailles;
            if (stuffOri.SelectedIndex > 0)
                ret = ret && (obj as Items).Monde_w == stuffOri.SelectedItem as Monde_w;
            if (Stufftype.SelectedIndex > 0)
            {
                switch ((Stufftype.SelectedItem as ComboBoxItem).Content as string)
                {
                    case "Armes":
                        ret = ret && (obj as Items).Weaponry != null;
                        if (ret)
                            switch (filterweaptype.SelectedItem as string)
                            {
                                case "Corps à corps":
                                    ret = ret && (obj as Items).Weaponry.Armes_cac != null;
                                    break;
                                case "Distance":
                                    ret = ret && (obj as Items).Weaponry.Arme_distance != null;
                                    break;
                                case "Magique":
                                    ret = ret && (obj as Items).Weaponry.Armes_magique != null;
                                    break;
                                default:
                                    ret = true;
                                    break;
                            }
                        break;
                    case "Armures":
                        ret = ret && (obj as Items).Armory != null;
                        if (ret && filterArmor.SelectedIndex > 0)
                            if (filterArmor.SelectedItem as string == "Exosquelette")
                                ret = ret && (obj as Items).Armory.categorie == "Exosquelette";
                            else
                            {
                                ret = ret && (obj as Items).Armory.categorie == filterArmor.SelectedItem as string;
                                ret = ret && (filterPiece.SelectedIndex == 0 || (obj as Items).Armory.Piece1 == filterPiece.SelectedItem as Piece);
                            }
                        break;
                    case "Véhicule":
                    case "Vehicule":
                        ret = ret && (obj as Items).Vehicule != null;
                        if (ret)
                        {
                            ret = ret && (filtercarbs.SelectedIndex == 0 || (obj as Items).Vehicule.carburant.Contains((filtercarbs.SelectedItem as Carburant).fuel));
                            ret = ret && (filterway.SelectedIndex == 0 || (obj as Items).Vehicule.accessibilite.Contains((filterway.SelectedItem as Voies).voie));
                            ret = ret && (filtermoove.SelectedIndex == 0 || (obj as Items).Vehicule.deplacement_mde.Contains((filtermoove.SelectedItem as Mode_deplacement).mode));
                            ret = ret && (filtermania.SelectedIndex == 0 || (obj as Items).Vehicule.Maniabilite1 == filtermania.SelectedItem as Maniabilite);
                        }
                        break;
                    case "Munition":
                    case "Munitions":
                        ret = ret && (obj as Items).Munition != null;
                        if (ret)
                            ret = ret && (filtermun.SelectedIndex == 0 || (obj as Items).Munition.Munition_type == filtermun.SelectedItem as Munition_type);
                        break;
                    case "Alliages":
                        ret = ret && (obj as Items).Alliage != null;
                        break;
                    case "Bijoux":
                        ret = ret && (obj as Items).Bijoux != null;
                        if (ret)
                            ret = ret && (filterJew.SelectedIndex == 0 || (obj as Items).Bijoux.Bijoux_place == filterJew.SelectedItem as Bijoux_place);
                        break;
                    case "Consommable":
                        ret = ret && (obj as Items).Consommables != null;
                        if (ret)
                        {
                            ret = ret && (filterConso.SelectedIndex == 0 || (obj as Items).Consommables.Conso_type == filterConso.SelectedItem as Conso_type);
                            ret = ret && (filterEffects.SelectedIndex == 0 || (obj as Items).Consommables.Effets == filterEffects.SelectedItem as Effets || (obj as Items).Consommables.Effets1 == filterConso.SelectedItem as Effets);
                            if (int.TryParse(filterUsesconso.Text, out int res) && res > 0)
                            {
                                if ((bool)filterinf.IsChecked) ret = ret && (obj as Items).Consommables.n_uses < res;
                                if ((bool)filtereql.IsChecked) ret = ret && (obj as Items).Consommables.n_uses == res;
                                if ((bool)filtersup.IsChecked) ret = ret && (obj as Items).Consommables.n_uses > res;
                            }
                        }
                        break;
                    case "Loot":
                        ret = ret && (obj as Items).Loot != null;
                        break;
                    case "Livre":
                        ret = ret && (obj as Items).Livre != null;
                        break;
                    case "Métaux":
                        ret = ret && (obj as Items).Mineraux != null && (obj as Items).Mineraux.Minerai_type.type == "Métal";
                        if (ret)
                            ret = ret && (filteruses.SelectedIndex == 0 || (obj as Items).Mineraux.usage.Contains((filteruses.SelectedItem as Usage).utilisation));
                        break;
                    case "Parchemins":
                        ret = ret && (obj as Items).Parchemins != null;
                        if (ret)
                            ret = ret && (filterparc.SelectedIndex == 0 || (obj as Items).Parchemins.Sorts.Magie_type == filterparc.SelectedItem as Magie_type);
                        break;
                    case "Pierres":
                    case "Pierre":
                        ret = ret && (obj as Items).Mineraux != null && (obj as Items).Mineraux.Minerai_type.type == "Pierre";
                        if (ret)
                            ret = ret && (filteruses.SelectedIndex == 0 || (obj as Items).Mineraux.usage.Contains((filteruses.SelectedItem as Usage).utilisation));
                        break;
                    case "Végétaux":
                        ret = ret && (obj as Items).Mineraux != null && (obj as Items).Mineraux.Minerai_type.type == "Végétal";
                        if (ret)
                            ret = ret && (filteruses.SelectedIndex == 0 || (obj as Items).Mineraux.usage.Contains((filteruses.SelectedItem as Usage).utilisation));
                        break;
                    case "Conteneur":
                        ret = ret && (obj as Items).Conteneurs != null;
                        if (ret)
                        {
                            ret = ret && (filtercont.SelectedIndex == 0 || (obj as Items).Conteneurs.Tailles == filtercont.SelectedItem as Tailles);
                            if (int.TryParse(filtercontsiz.Text, out int res) && res > 0)
                                if ((bool)filterinf.IsChecked) ret = ret && (obj as Items).Conteneurs.taille < res;
                                else if ((bool)filtereql.IsChecked) ret = ret && (obj as Items).Conteneurs.taille == res;
                                else if ((bool)filtersup.IsChecked) ret = ret && (obj as Items).Conteneurs.taille > res;

                        }
                        break;
                    case "Commun":
                    case "Communs":
                        ret = ret && (obj as Items).Armory == null && (obj as Items).Mineraux == null && (obj as Items).Bijoux == null &&
        (obj as Items).Weaponry == null && (obj as Items).Vehicule == null && (obj as Items).Loot == null && (obj as Items).Mineraux == null && (obj as Items).Munition == null && (obj as Items).Conteneurs == null && (obj as Items).Consommables
        == null && (obj as Items).Parchemins == null;
                        break;
                    default:
                        break;
                }
            }
            return ret;
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
            if (SpellList != null)
                ((CollectionView)CollectionViewSource.GetDefaultView(SpellList.ItemsSource)).Refresh();
        }

        private void RefreshStuff(object sender, SelectionChangedEventArgs e)
        {
            if (StuffList != null && StuffList.ItemsSource != null)
                ((CollectionView)CollectionViewSource.GetDefaultView(StuffList.ItemsSource)).Refresh();
        }

        private void RefreshLoot(object sender, SelectionChangedEventArgs e)
        {
            if (ListLoot != null)
                ((CollectionView)CollectionViewSource.GetDefaultView(ListLoot.ItemsSource)).Refresh();
        }

        private void RefreshCombo(object sender, SelectionChangedEventArgs e)
        {
            if (ComboList != null)
                ((CollectionView)CollectionViewSource.GetDefaultView(ComboList.ItemsSource)).Refresh();
        }

        private void RefreshWeaps(object sender, SelectionChangedEventArgs e)
        {
            if (weapMaster != null && weapMaster.ItemsSource != null)
                ((CollectionView)CollectionViewSource.GetDefaultView(weapMaster.ItemsSource)).Refresh();
        }

        private void RefreshStuff(object sender, TextChangedEventArgs e)
        {
            if (StuffList != null && StuffList.ItemsSource != null)
                ((CollectionView)CollectionViewSource.GetDefaultView(StuffList.ItemsSource)).Refresh();
        }

        private void StuffDetail(object sender, RoutedEventArgs e)
        {
            Items obj = Bdd.Items.First(i => i.Id == (int)(sender as Button).Tag);
            switch ((Stufftype.SelectedItem as ComboBoxItem).Content as string)
            {
                case "Armes":
                    Peruso_Label.Text += obj.nom + ": pèse " + obj.masse + " Kg, obtensible par " + obj.obtention + ", " + ((obj.origine != "Magocosme") ? "Prix tech : " + obj.prix_tech + " " : "") + ((obj.origine != "Technocosme") ? "Prix tech : " + obj.prix_tech + " " : "")
                        + " origine: " + obj.origine + " \n"
                        + (obj.Weaponry.element_1 != null ? "Element " + obj.Weaponry.element_1 + ", avec une puissance de " + obj.Weaponry.puissance_1 + obj.Weaponry.chance_1 + "% de chances d'infliger " + obj.Weaponry.Mag_element.etat + ", durant " + obj.Weaponry.duree_1 + '\n'
                        : "") + (obj.Weaponry.element_2 != null ? "Element " + obj.Weaponry.element_1 + ", avec une puissance de " + obj.Weaponry.puissance_1 + obj.Weaponry.chance_1 + "% de chances d'infliger " + obj.Weaponry.Mag_element.etat + ", durant " + obj.Weaponry.duree_1 : "");
                    foreach (Armes_cac item in obj.Weaponry.Armes_cac)
                        Peruso_Label.Text += "Attaque : " + item.attaque + ", Puissance :" + item.puissance + ", Cout d'utilisation :" + item.mana_cost + ", Precision :" + item.precision + ", " + item.type + "Chances de critique : " + item.critique + "%"
                            + ", Multiplicateur de degats sur critique " + item.critmult + ", inflige des degats de type " + item.degats_type + '\n';
                    foreach (Arme_distance item in obj.Weaponry.Arme_distance)
                        Peruso_Label.Text += "Attaque : " + item.attaque + ", Puissance :" + item.puissance + ", Portée : " + item.range + ", Cout d'utilisation :" + item.mana_cost + ", Precision" + item.precision + ", " + item.type + "Chances de critique : " + item.critique + "%"
                            + ", Multiplicateur de degats sur critique " + item.critmult + ", munitions : " + item.munition + ", Rechargement +" + item.reload_tps + "\n";
                    foreach (Armes_magique item in obj.Weaponry.Armes_magique)
                        Peruso_Label.Text += "Precision : " + item.precision + ", Puissance :" + item.puissance + ", Portée : " + item.range + ", Cout d'utilisation :" + item.manacost + ", " + item.type + "Chances de critique : " + item.critique + "%"
                            + ", Multiplicateur de degats sur critique " + item.critmult + ", " + item.spells + "\n";
                    Peruso_Label.Text += (!obj.craftable ? "non craftable; " : "craftable " + obj.recette) + '\n' + obj.description;
                    break;
                case "Armures":
                    Peruso_Label.Text += obj.nom + ": pèse " + obj.masse + " Kg, obtensible par " + obj.obtention + ", " + ((obj.origine != "Magocosme") ? "Prix tech : " + obj.prix_tech + " " : "") + ((obj.origine != "Technocosme") ? "Prix tech : " + obj.prix_tech + " " : "")
+ " origine: " + obj.origine + " \n"
+ "Attaque : " + obj.Armory.atk + ", Defense : " + obj.Armory.def + ", Puissance : " + obj.Armory.puissance + ", Resistance : " + obj.Armory.resistance + ", " + obj.Armory.categorie + '\n'
+ "Malus de Dexterité : " + obj.Armory.dex_malus + ", Malus de vitesse : " + obj.Armory.vit_malus
+ (obj.Armory.enchantable ? " Enchantable, " : "") + (obj.Armory.enchantement ?? " Aucun Enchantement, ") + obj.Armory.capacites;
                    foreach (ElemResArmorAssoc er in obj.Armory.ElemResArmorAssoc)
                        Peruso_Label.Text += (er.element != null ? "Elements resistants " + er.element + ", resistance de " + er.resValue : "") + '\n';
                    Peruso_Label.Text += (!obj.craftable ? "non craftable; " : "craftable " + obj.recette) + '\n' + obj.description;
                    break;
                case "Véhicule":
                case "Vehicule":
                    Peruso_Label.Text += obj.nom + ": pèse " + obj.masse + " Kg, obtensible par " + obj.obtention + ", " + ((obj.origine != "Magocosme") ? "Prix tech : " + obj.prix_tech + " " : "") + ((obj.origine != "Technocosme") ? "Prix tech : " + obj.prix_tech + " " : "")
+ " origine: " + obj.origine + " \n"
+ "Vitesse maximale : " + obj.Vehicule.vitesse_max + " " + obj.Vehicule.solidite + " Point de solidité, taille de reservoir" + obj.Vehicule.reservoir + ", peut progresser par voie " + obj.Vehicule.accessibilite + ", " + obj.Vehicule.maniabilite + ", carbure à" + obj.Vehicule.carburant
+ (!obj.craftable ? "non craftable; " : "craftable " + obj.recette) + '\n' + obj.description;
                    break;
                case "Munition":
                case "Munitions":
                    Peruso_Label.Text += obj.nom + ": pèse " + obj.masse + " Kg, obtensible par " + obj.obtention + ", " + ((obj.origine != "Magocosme") ? "Prix tech : " + obj.prix_tech + " " : "") + ((obj.origine != "Technocosme") ? "Prix tech : " + obj.prix_tech + " " : "")
+ " origine: " + obj.origine + " \n"
+ obj.Munition.categorie + ": Degats : " + obj.Munition.degats + " de type " + obj.Munition.degats_type
+ (obj.Munition.element_1 != null ? "Element : " + obj.Munition.element_1 + ", " + obj.Munition.puissance_1 + " de Puissance, " + obj.Munition.chance_1 + "% de chances d'infliger " + obj.Munition.Mag_element.etat + " Pendant " + obj.Munition.duree_1 : "")
+ (obj.Munition.element_2 != null ? "Element : " + obj.Munition.element_2 + ", " + obj.Munition.puissance_2 + " de Puissance, " + obj.Munition.chance_2 + "% de chances d'infliger " + obj.Munition.Mag_element1.etat + " Pendant " + obj.Munition.duree_2 : "")
+ (!obj.craftable ? "non craftable; " : "craftable " + obj.recette) + '\n' + obj.description;
                    break;

                case "Bijoux":
                    Peruso_Label.Text += obj.nom + ": pèse " + obj.masse + " Kg, obtensible par " + obj.obtention + ", " + ((obj.origine != "Magocosme") ? "Prix tech : " + obj.prix_tech + " " : "") + ((obj.origine != "Technocosme") ? "prix mago : " + obj.prix_mago + " " : "")
                        + " origine: " + (obj.origine == "Tous" ? " Tout les mondes" : obj.origine) + ", " + (obj.Bijoux.enchantable ? ", Enchantable, " : "") + (obj.Bijoux.enchantements ?? " Auncun enchantement, ")
                        + (!obj.craftable ? "non craftable; " : "craftable.\nRecette: " + (obj.recette.Replace("\n", "; "))) + '\n' + obj.description;
                    break;
                case "Consommable":
                    Peruso_Label.Text += obj.nom + ": pèse " + obj.masse + " Kg, obtensible par " + obj.obtention + ", " + ((obj.origine != "Magocosme") ? "Prix tech : " + obj.prix_tech + " " : "") + ((obj.origine != "Technocosme") ? "prix mago : " + obj.prix_mago + " " : "")
+ " origine: " + (obj.origine == "Tous" ? " Tout les mondes" : obj.origine) + ", " + obj.Consommables.type + ", Premier Effet : " + obj.Consommables.effet_1 + ", dure " + obj.Consommables.duree1 + ", puissance : " + obj.Consommables.modulo_1 + ", minimum : " + obj.Consommables.minimum_1 + '\n'
+ (obj.Consommables.effet_2 != null ? ", Second Effet : " + obj.Consommables.effet_2 + ", dure " + obj.Consommables.duree2 + ", puissance : " + obj.Consommables.modulo_2 + ", minimum : " + obj.Consommables.minimum_2 : "")
+ (!obj.craftable ? "non craftable; " : "craftable.\nRecette: " + (obj.recette.Replace("\n", "; "))) + '\n' + obj.description;
                    break;
                case "Alliages":
                case "Commun":
                case "Communs":
                case "Loot":
                    Peruso_Label.Text += obj.nom + ": pèse " + obj.masse + " Kg, obtensible par " + obj.obtention + ", " + ((obj.origine != "Magocosme") ? "Prix tech : " + obj.prix_tech + " " : "") + ((obj.origine != "Technocosme") ? "prix mago : " + obj.prix_mago + " " : "")
+ " origine: " + (obj.origine == "Tous" ? " Tout les mondes" : obj.origine) + ", "
+ (!obj.craftable ? "non craftable; " : "craftable.\nRecette: " + (obj.recette.Replace("\n", "; "))) + '\n' + obj.description;
                    break;
                case "Livre":
                    Peruso_Label.Text += obj.nom + ": pèse " + obj.masse + " Kg, obtensible par " + obj.obtention + ", " + ((obj.origine != "Magocosme") ? "Prix tech : " + obj.prix_tech + " " : "") + ((obj.origine != "Technocosme") ? "prix mago : " + obj.prix_mago + " " : "")
+ " origine: " + (obj.origine == "Tous" ? " Tout les mondes" : obj.origine) + ", "
+ (!obj.craftable ? "non craftable; " : "craftable.\nRecette: " + (obj.recette.Replace("\n", "; "))) + '\n' + obj.description;
                    break;
                case "Parchemins":
                    Peruso_Label.Text += obj.nom + ": pèse " + obj.masse + " Kg, obtensible par " + obj.obtention + ", " + ((obj.origine != "Magocosme") ? "Prix tech : " + obj.prix_tech + " " : "") + ((obj.origine != "Technocosme") ? "prix mago : " + obj.prix_mago + " " : "")
+ " origine: " + (obj.origine == "Tous" ? " Tout les mondes" : obj.origine) + ", contient " + obj.Parchemins.Sorts.nom + "\n"
+ (!obj.craftable ? "non craftable; " : "craftable.\nRecette: " + (obj.recette.Replace("\n", "; "))) + '\n' + obj.description;
                    break;
                case "Métaux":
                case "Pierres":
                case "Pierre":
                case "Végétaux":
                    Peruso_Label.Text += obj.nom + ": pèse " + obj.masse + " Kg, obtensible par " + obj.obtention + ", " + ((obj.origine != "Magocosme") ? "Prix tech : " + obj.prix_tech + " " : "") + ((obj.origine != "Technocosme") ? "prix mago : " + obj.prix_mago + " " : "")
+ " origine: " + (obj.origine == "Tous" ? " Tout les mondes" : obj.origine) + ", " + obj.Mineraux.usage + '\n'
+ (!obj.craftable ? "non craftable; " : "craftable.\nRecette: " + (obj.recette.Replace("\n", "; "))) + '\n' + obj.description;
                    break;
                case "Conteneur":
                    Peruso_Label.Text += obj.nom + ": pèse " + obj.masse + " Kg, obtensible par " + obj.obtention + ", " + ((obj.origine != "Magocosme") ? "Prix tech : " + obj.prix_tech + " " : "") + ((obj.origine != "Technocosme") ? "Prix tech : " + obj.prix_tech + " " : "")
+ " origine: " + obj.origine + ", peut contenir des objets de taille inferieure à " + obj.Conteneurs.Tailles.categorie + ", pour une capacité de " + obj.Conteneurs.taille + "Kg \n"
+ (!obj.craftable ? "non craftable; " : "craftable " + obj.recette) + '\n' + obj.description;
                    break;
                default:
                    break;
            }
        }

        private void DelStuff(object sender, RoutedEventArgs e)
        {
            while (ChosenStuff.SelectedItems.Count > 0)
                ChosenStuff.Items.Remove(ChosenStuff.SelectedItems[0]);
        }

        private void DelLoot(object sender, RoutedEventArgs e)
        {
            while (PersoLoot.SelectedItems.Count > 0)
                PersoLoot.Items.Remove(PersoLoot.SelectedItems[0]);
        }

        private void DelSpell(object sender, RoutedEventArgs e)
        {
            while (ChosenSpell.SelectedItems.Count > 0)
                ChosenSpell.Items.Remove(ChosenSpell.SelectedItems[0]);
        }

        private void DelCombos(object sender, RoutedEventArgs e)
        {
            while (ChosenCombos.SelectedItems.Count > 0)
                ChosenCombos.Items.Remove(ChosenCombos.SelectedItems[0]);
        }

        private void Placeholder(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem == null)
                (sender as ComboBox).SelectedIndex = 0;
        }
    }
}

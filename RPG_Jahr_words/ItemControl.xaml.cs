using Microsoft.Win32;
using RPG_Jahr_words.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Logique d'interaction pour ItemControl.xaml
    /// </summary>

    public partial class ItemControl : UserControl
    {
        public RPGEntities15 Bdd
        {
            get => (RPGEntities15)GetValue(BddProperty);
            set => SetValue(BddProperty, value);
        }
        public static readonly DependencyProperty BddProperty =
            DependencyProperty.Register("Bdd",
                typeof(RPGEntities15),
                typeof(ItemControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(ChargeFromDb)));

        public event EventHandler ItemAdded;
        public event EventHandler NewWeapontype;
        private static void ChargeFromDb(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ViewModel.ItemViewModel vm = new ItemViewModel(e.NewValue as RPGEntities15);
            vm.WeaponAdded += (d as ItemControl).RaiseWeaponAdded;
            vm.ItemAdded += (d as ItemControl).RaiseItemAdded;
            (d as ItemControl).DataContext = vm;
        }

        private void RaiseItemAdded(object sender, EventArgs e)
        {
            ItemAdded?.Invoke(sender, e);
        }
        private void RaiseWeaponAdded(object sender, EventArgs e)
        {
            NewWeapontype?.Invoke(sender, e);
        }

        public NameGen Gen
        {
            get => (NameGen)GetValue(GenProperty);
            set => SetValue(GenProperty, value);
        }
        public static readonly DependencyProperty GenProperty =
            DependencyProperty.Register("Gen",
                typeof(NameGen),
                typeof(ItemControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));


        public ItemControl()
        {
            InitializeComponent();
            CollectionView spells = (CollectionView)CollectionViewSource.GetDefaultView(Weap_Spel_list.Items);
            CollectionView compo = (CollectionView)CollectionViewSource.GetDefaultView(Component.Items);
            CollectionView links = (CollectionView)CollectionViewSource.GetDefaultView(Bijoux_link.Items);
            spells.Filter = FilterSpell;
            compo.Filter = FilterCompo;
            links.Filter = FilterLink;
        }

        private bool FilterLink(object obj)
        {
            if ((bool)jewel_linked.IsChecked)
                if (Bijoux_item_link.SelectedIndex > 0)
                    switch (Bijoux_item_link.SelectedItem as string)
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
                else return true;
            return false;
        }

        private bool FilterCompo(object obj)
        {
            if ((bool)craftable.IsChecked)
                if (Component_type.SelectedIndex > 0)
                    return PropertyIsNotNull(obj as Items, (string)Component_type.SelectedItem);
            return false;
        }

        private bool PropertyIsNotNull(Items obj, string propname)
        {
            switch ((string)Component_type.SelectedItem)
            {
                case "Armes": return obj.Weaponry != null;
                case "Armures": return obj.Armory != null;
                case "Véhicule":
                case "Vehicule": return obj.Vehicule != null;
                case "Munition":
                case "Munitions": return obj.Munition != null;
                case "Alliages": return obj.Alliage != null;
                case "Bijoux": return obj.Bijoux != null;
                case "Consommable": return obj.Consommables != null;
                case "Loot": return obj.Loot != null;
                case "Métaux": return obj.Mineraux != null && obj.Mineraux.Minerai_type.type == "Métal";
                case "Parchemins": return obj.Parchemins != null;
                case "Pierres":
                case "Pierre": return obj.Mineraux != null && obj.Mineraux.Minerai_type.type == "Pierre";
                case "Végétaux": return obj.Mineraux != null && obj.Mineraux.Minerai_type.type == "Végétal";
                case "Conteneur": return obj.Conteneurs != null;
                case "Commun":
                case "Communs":
                    return obj.Armory == null && obj.Mineraux == null && obj.Bijoux == null &&
    obj.Weaponry == null && obj.Vehicule == null && obj.Loot == null && obj.Mineraux == null && obj.Munition == null && obj.Conteneurs == null && obj.Consommables
    == null && obj.Parchemins == null;
                default:
                    return false;
            }
        }
        private bool FilterSpell(object obj)
        {
            if (Item_Spell_typ.SelectedIndex > 0)
                return (obj as Sorts).Magie_type == Item_Spell_typ.SelectedItem;
            return false;
        }

        private void Generation(object sender, RoutedEventArgs e) { Item_name.Text = Item.Generation_gn(); }
        private void Selecion_with_all(object sender, SelectionChangedEventArgs e)
        {
            ListBox lys = (ListBox)sender;
            if ((string)lys.SelectedItem == "Tous" && lys.SelectedItems.Count != 1)
                lys.SelectedItems.RemoveAt(1);
            else if ((lys.SelectedItems.Contains("Tous") && lys.SelectedItems.Count > 2) || (lys.SelectedItems.Count == lys.Items.Count - 1 && !lys.SelectedItems.Contains("Tous")))
                lys.SelectedItem = "Tous";
        }

        private void Item_critere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!Show_criter.Items.IsEmpty)
                Show_criter.Items.Clear();
            Item_show_val.IsEnabled = show_inf.IsEnabled = show_sup.IsEnabled = false;
            switch ((string)((ComboBoxItem)Item_critere.SelectedItem).Content)
            {
                case "Element":
                    Show_criter.DisplayMemberPath = "element";
                    Show_criter.ItemsSource = Bdd.Mag_element;
                    Item_show_val.IsEnabled = show_inf.IsEnabled = show_sup.IsEnabled = true;
                    break;
                case "Type d'arme":
                    if (Item_show_elem.SelectedItem == Armes_cac_show)
                        Show_criter.ItemsSource = Bdd.Weapon_type.Where(w => w.categorie == "CaC");
                    else if (Item_show_elem.SelectedItem == Armes_dist_show)
                        Show_criter.ItemsSource = Bdd.Weapon_type.Where(w => w.categorie == "Distance");
                    else if (Item_show_elem.SelectedItem == Armes_mag_show)
                        Show_criter.ItemsSource = Bdd.Weapon_type.Where(w => w.categorie == "Magique");
                    Show_criter.DisplayMemberPath = "type";
                    break;
                case "Type de Munition":
                    Show_criter.ItemsSource = Bdd.Munition_type;
                    Show_criter.DisplayMemberPath = "type";
                    break;
                case "Piece d'armure":
                    Show_criter.ItemsSource = Bdd.Piece;
                    Show_criter.DisplayMemberPath = "emplacement";
                    break;
                case "Type d'armure":
                    Show_criter.ItemsSource = Bdd.Armor_cat.ToList();
                    Show_criter.DisplayMemberPath = "categorie"; break;
                case "Mode de deplacement":
                    Show_criter.ItemsSource = Bdd.Mode_deplacement;
                    Show_criter.DisplayMemberPath = "mode";
                    break;
                case "Maneuvrabilité":
                    Show_criter.ItemsSource = Bdd.Maniabilite.ToList();
                    Show_criter.DisplayMemberPath = "categorie"; break;
                case "Carburant":
                    Show_criter.ItemsSource = Bdd.Carburant;
                    Show_criter.DisplayMemberPath = "fuel";
                    break;
                case "Type de Bijoux":
                    Show_criter.ItemsSource = Bdd.Bijoux_place;
                    Show_criter.DisplayMemberPath = "place"; break;
                case "Effet de consommable":
                    Show_criter.ItemsSource = Bdd.Effets;
                    Show_criter.DisplayMemberPath = "effet"; break;
                case "Type de consommable":
                    Show_criter.ItemsSource = Bdd.Conso_type;
                    Show_criter.DisplayMemberPath = "type";
                    break;
                case "Type de degats":
                    Show_criter.ItemsSource = Bdd.Degat_type;
                    Show_criter.DisplayMemberPath = "categorie";
                    break;
                case "Rechargement":
                    Show_criter.ItemsSource = Bdd.Reload_cat;
                    Show_criter.DisplayMemberPath = "cat";
                    break;
                case "Origine":
                    Show_criter.ItemsSource = Bdd.Monde_w;
                    Show_criter.DisplayMemberPath = "nom";
                    break;
                case "Capacité (armure)":
                    Show_criter.ItemsSource = Bdd.Capacites_armor;
                    Show_criter.DisplayMemberPath = "pouvoir";
                    break;
                case "Taille":
                    Show_criter.ItemsSource = Bdd.Tailles;
                    Show_criter.DisplayMemberPath = "categorie";
                    break;
                default:
                    Item_show_val.IsEnabled = show_inf.IsEnabled = show_sup.IsEnabled = true;
                    break;
            }
        }
        private void Del_but_Click(object sender, RoutedEventArgs e)
        {
            if (Item_rec.SelectedItems != null)
            {
                List<object> remove = Item_rec.SelectedItems as List<object>;
                while (Item_rec.SelectedItems.Count > 0)
                    (Item_rec.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeItem>).Remove(Item_rec.SelectedItems[0] as RecipeItem);
            }
            else Itemu_Label.Text += "Veuillez choisir au moins une entrée à supprimer de la liste.\n";
        }
        private void AjoutClick(object sender, RoutedEventArgs e)
        {
            if (Component.SelectedItem != null)
            {
                RecipeItem b = new RecipeItem
                {
                    N_recette = (int)recipeId.SelectedItem,
                    Component = (Component.SelectedItem as Items),
                    Quantite = 0
                };
                (Item_rec.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeItem>).Add(b);
                Component_type.SelectedIndex = 0;
                Component.SelectedItem = null;
                Item_rec.ItemsSource = new System.Collections.ObjectModel.ObservableCollection<RecipeItem>((Item_rec.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeItem>).OrderBy(r => r.N_recette));
            }
            else Itemu_Label.Text += "Veuillez selectionner un composant.\n";
        }

        private void Item_show_but_Click(object sender, RoutedEventArgs e)
        {
            Itemu_Label.Text = "";
            List<Items> Show = Bdd.Items.ToList();
            if (Item_critere.SelectedItem == criter_ori && Show_criter.SelectedItem != null)
                Show = Show.Where(i => i.Monde_w == (Show_criter.SelectedItem as Monde_w)).ToList();
            else if (Item_critere.SelectedItem == criter_prix && Item_show_val.Text != "")
                Show = Show.Where(i => ((bool)show_inf.IsChecked ? i.prix_mago <= int.Parse(Item_show_val.Text) : i.prix_mago >= int.Parse(Item_show_val.Text)) ||
                ((bool)show_inf.IsChecked ? i.prix_tech <= int.Parse(Item_show_val.Text) : i.prix_tech >= int.Parse(Item_show_val.Text))).ToList();
            else if (Item_critere.SelectedItem == criter_size && Show_criter.SelectedItem != null)
                Show = Show.Where(i => i.Tailles == (Tailles)Show_criter.SelectedItem).ToList();
            if (Item_show_elem.SelectedIndex == 0)
            {
                foreach (Items Item in Show)
                    Itemu_Label.Text += Item.nom + ": pèse " + Item.masse + " Kg, obtensible par " + Item.obtention + ", " + ((Item.origine != "Magocosme") ? "Prix tech : " + Item.prix_tech + " " : "") + ((Item.origine != "Technocosme") ? "prix mago : " + Item.prix_mago + " " : "")
                        + " origine: " + (Item.origine == "Tous" ? " Tout les mondes" : Item.origine) + ", "
                        + (!Item.craftable ? "non craftable; " : "craftable.\nRecette: " + (Item.recette.Replace("\n", "; "))) + '\n' + Item.description;
            }
            else if (Item_show_elem.SelectedItem == Armes_cac_show)
            {
                Show = Show.Where(w => w.Weaponry != null && w.Weaponry.Armes_cac.Count > 0).ToList();
                if (Item_critere.SelectedIndex == 0)
                    Show = Show.Where(w => w.Weaponry.Armes_cac.Count > 0).ToList();
                else if (Item_critere.SelectedItem == criter_atk && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Armes_cac.Any(a => ((bool)show_inf.IsChecked ? a.attaque <= int.Parse(Item_show_val.Text) : a.attaque >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_cout && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Armes_cac.Any(a => ((bool)show_inf.IsChecked ? a.mana_cost <= int.Parse(Item_show_val.Text) : a.mana_cost >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_pwr && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Armes_cac.Any(a => ((bool)show_inf.IsChecked ? a.puissance <= int.Parse(Item_show_val.Text) : a.puissance >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_crit && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Armes_cac.Any(a => ((bool)show_inf.IsChecked ? a.critique <= int.Parse(Item_show_val.Text) : a.critique >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_mult && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Armes_cac.Any(a => ((bool)show_inf.IsChecked ? a.critmult <= int.Parse(Item_show_val.Text) : a.critmult >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_elm && Show_criter.SelectedItem != null)
                    Show = Show.Where(w => (w.Weaponry.Mag_element == (Show_criter.SelectedItem as Mag_element)) || (w.Weaponry.Mag_element1 == (Show_criter.SelectedItem as Mag_element))).ToList();
                else if (Item_critere.SelectedItem == criter_ty_degat && Show_criter.SelectedItem != null)
                    Show = Show.Where(w => w.Weaponry.Armes_cac.Any(a => a.degats_type.Contains((Show_criter.SelectedItem as Degat_type).categorie))).ToList();
                else if (Item_critere.SelectedItem == criter_ty_weap && Show_criter.SelectedItem != null)
                    Show = Show.Where(w => w.Weaponry.Armes_cac.Any(a => a.Weapon_type == (Show_criter.SelectedItem as Weapon_type))).ToList();
                else if (Item_critere.SelectedItem == criter_prec && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Armes_cac.Any(a => ((bool)show_inf.IsChecked ? a.precision <= int.Parse(Item_show_val.Text) : a.precision >= int.Parse(Item_show_val.Text)))).ToList();
                foreach (Items row in Show)
                {
                    Itemu_Label.Text += row.nom + ": pèse " + row.masse + " Kg, obtensible par " + row.obtention + ", " + ((row.origine != "Magocosme") ? "Prix tech : " + row.prix_tech + " " : "") + ((row.origine != "Technocosme") ? "Prix tech : " + row.prix_tech + " " : "")
                        + " origine: " + row.origine + " \n"
                        + (row.Weaponry.element_1 != null ? "Element " + row.Weaponry.element_1 + ", avec une puissance de " + row.Weaponry.puissance_1 + row.Weaponry.chance_1 + "% de chances d'infliger " + row.Weaponry.Mag_element.etat + ", durant " + row.Weaponry.duree_1 + '\n'
                        : "") + (row.Weaponry.element_2 != null ? "Element " + row.Weaponry.element_1 + ", avec une puissance de " + row.Weaponry.puissance_1 + row.Weaponry.chance_1 + "% de chances d'infliger " + row.Weaponry.Mag_element.etat + ", durant " + row.Weaponry.duree_1 : "");
                    foreach (Armes_cac item in row.Weaponry.Armes_cac)
                        Itemu_Label.Text += "Attaque : " + item.attaque + ", Puissance :" + item.puissance + ", Cout d'utilisation :" + item.mana_cost + ", Precision :" + item.precision + ", " + item.type + "Chances de critique : " + item.critique + "%"
                            + ", Multiplicateur de degats sur critique " + item.critmult + ", inflige des degats de type " + item.degats_type + '\n';
                    Itemu_Label.Text += (!row.craftable ? "non craftable; " : "craftable " + row.recette) + '\n' + row.description;
                }
            }
            else if (Item_show_elem.SelectedItem == Armes_dist_show)
            {
                Show = Show.Where(w => w.Weaponry != null && w.Weaponry.Arme_distance.Count > 0).ToList();
                if (Item_critere.SelectedIndex == 0)
                    Show = Show.Where(w => w.Weaponry.Arme_distance.Any(a => a.Id_weapon == w.Id)).ToList();
                else if (Item_critere.SelectedItem == criter_atk && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Arme_distance.Any(a => ((bool)show_inf.IsChecked ? a.attaque <= int.Parse(Item_show_val.Text) : a.attaque >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_cout && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Arme_distance.Any(a => ((bool)show_inf.IsChecked ? a.mana_cost <= int.Parse(Item_show_val.Text) : a.mana_cost >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_crit && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Arme_distance.Any(a => ((bool)show_inf.IsChecked ? a.critique <= int.Parse(Item_show_val.Text) : a.critique >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_mult && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Arme_distance.Any(a => ((bool)show_inf.IsChecked ? a.critmult <= int.Parse(Item_show_val.Text) : a.critmult >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_elm && Item_show_val.Text != "")
                    Show = Show.Where(w => (w.Weaponry.Mag_element == (Show_criter.SelectedItem as Mag_element) || w.Weaponry.Mag_element1 == (Show_criter.SelectedItem as Mag_element))).ToList();
                else if (Item_critere.SelectedItem == criter_pwr && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Arme_distance.Any(a => ((bool)show_inf.IsChecked ? a.puissance <= int.Parse(Item_show_val.Text) : a.puissance >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_rld && Show_criter.SelectedItem != null)
                    Show = Show.Where(w => w.Weaponry.Arme_distance.Any(a => a.Reload_cat == (Reload_cat)Show_criter.SelectedItem)).ToList();
                else if (Item_critere.SelectedItem == criter_rng && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Arme_distance.Any(a => ((bool)show_inf.IsChecked ? a.range <= int.Parse(Item_show_val.Text) : a.range >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_ty_mun && Show_criter.SelectedItem != null)
                    Show = Show.Where(w => w.Weaponry.Arme_distance.Any(a => a.munition == (string)Show_criter.SelectedItem)).ToList();
                else if (Item_critere.SelectedItem == criter_ty_weap && Show_criter.SelectedItem != null)
                    Show = Show.Where(w => w.Weaponry.Arme_distance.Any(a => a.Weapon_type == (Weapon_type)Show_criter.SelectedItem)).ToList();
                else if (Item_critere.SelectedItem == criter_prec && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Arme_distance.Any(a => ((bool)show_inf.IsChecked ? a.precision <= int.Parse(Item_show_val.Text) : a.precision >= int.Parse(Item_show_val.Text)))).ToList();
                foreach (Items row in Show)
                {
                    Itemu_Label.Text += row.nom + ": pèse " + row.masse + " Kg, obtensible par " + row.obtention + ", " + ((row.origine != "Magocosme") ? "Prix tech : " + row.prix_tech + " " : "") + ((row.origine != "Technocosme") ? "Prix tech : " + row.prix_tech + " " : "")
                        + " origine: " + row.origine + " \n"
                        + (row.Weaponry.element_1 != null ? "Element " + row.Weaponry.element_1 + ", avec une puissance de " + row.Weaponry.puissance_1 + row.Weaponry.chance_1 + "% de chances d'infliger " + row.Weaponry.Mag_element.etat + ", durant " + row.Weaponry.duree_1 + '\n'
                       : "") + (row.Weaponry.element_2 != null ? "Element " + row.Weaponry.element_1 + ", avec une puissance de " + row.Weaponry.puissance_1 + row.Weaponry.chance_1 + "% de chances d'infliger " + row.Weaponry.Mag_element.etat + ", durant " + row.Weaponry.duree_1 : "");
                    foreach (Arme_distance item in row.Weaponry.Arme_distance)
                        Itemu_Label.Text += "Attaque : " + item.attaque + ", Puissance :" + item.puissance + ", Portée : " + item.range + ", Cout d'utilisation :" + item.mana_cost + ", Precision" + item.precision + ", " + item.type + "Chances de critique : " + item.critique + "%"
                            + ", Multiplicateur de degats sur critique " + item.critmult + ", munitions : " + item.munition + ", Rechargement +" + item.reload_tps + "\n";
                    Itemu_Label.Text += (!row.craftable ? "non craftable; " : "craftable " + row.recette) + '\n' + row.description;
                }
            }
            else if (Item_show_elem.SelectedItem == Armes_mag_show)
            {
                Show = Show.Where(w => w.Weaponry != null && w.Weaponry.Armes_magique.Count > 0).ToList();
                if (Item_critere.SelectedIndex == 0)
                    Show = Show.Where(w => w.Weaponry.Armes_magique.Any(a => a.Id_weapon == w.Id)).ToList();
                else if (Item_critere.SelectedItem == criter_prec && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Armes_magique.Any(a => ((bool)show_inf.IsChecked ? a.precision <= int.Parse(Item_show_val.Text) : a.precision >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_crit && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Armes_magique.Any(a => ((bool)show_inf.IsChecked ? a.critique <= int.Parse(Item_show_val.Text) : a.critique >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_mult && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Armes_magique.Any(a => ((bool)show_inf.IsChecked ? a.critmult <= int.Parse(Item_show_val.Text) : a.critmult >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_cout && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Armes_magique.Any(a => ((bool)show_inf.IsChecked ? a.manacost <= int.Parse(Item_show_val.Text) : a.manacost >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_elm && Show_criter.SelectedItem != null)
                    Show = Show.Where(w => (w.Weaponry.Mag_element == (Show_criter.SelectedItem as Mag_element) || w.Weaponry.Mag_element1 == (Mag_element)Show_criter.SelectedItem)).ToList();
                else if (Item_critere.SelectedItem == criter_pwr && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Armes_magique.Any(a => ((bool)show_inf.IsChecked ? a.puissance <= int.Parse(Item_show_val.Text) : a.puissance >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_rng && Item_show_val.Text != "")
                    Show = Show.Where(w => w.Weaponry.Armes_magique.Any(a => ((bool)show_inf.IsChecked ? a.range <= int.Parse(Item_show_val.Text) : a.range >= int.Parse(Item_show_val.Text)))).ToList();
                else if (Item_critere.SelectedItem == criter_ty_weap && Show_criter.SelectedItem != null)
                    Show = Show.Where(w => w.Weaponry.Armes_magique.Any(a => a.Weapon_type == (Weapon_type)Show_criter.SelectedItem)).ToList();
                foreach (Items row in Show)
                {
                    Itemu_Label.Text += row.nom + ": pèse " + row.masse + " Kg, obtensible par " + row.obtention + ", " + ((row.origine != "Magocosme") ? "Prix tech : " + row.prix_tech + " " : "") + ((row.origine != "Technocosme") ? "Prix tech : " + row.prix_tech + " " : "")
                        + " origine: " + row.origine + " \n"
                        + (row.Weaponry.element_1 != null ? "Element " + row.Weaponry.element_1 + ", avec une puissance de " + row.Weaponry.puissance_1 + row.Weaponry.chance_1 + "% de chances d'infliger " + row.Weaponry.Mag_element.etat + ", durant " + row.Weaponry.duree_1 + '\n'
                        : "") + (row.Weaponry.element_2 != null ? "Element " + row.Weaponry.element_1 + ", avec une puissance de " + row.Weaponry.puissance_1 + row.Weaponry.chance_1 + "% de chances d'infliger " + row.Weaponry.Mag_element.etat + ", durant " + row.Weaponry.duree_1 : "");
                    foreach (Armes_magique item in row.Weaponry.Armes_magique)
                        Itemu_Label.Text += "Precision : " + item.precision + ", Puissance :" + item.puissance + ", Portée : " + item.range + ", Cout d'utilisation :" + item.manacost + ", " + item.type + "Chances de critique : " + item.critique + "%"
                            + ", Multiplicateur de degats sur critique " + item.critmult + ", " + item.spells + "\n";
                    Itemu_Label.Text += (!row.craftable ? "non craftable; " : "craftable " + row.recette) + '\n' + row.description;
                }
            }
            else if (Item_show_elem.SelectedItem == Armure_show)
            {
                Show = Show.Where(a => a.Armory != null).ToList();
                if (Item_critere.SelectedIndex == 0) { }
                else if (Item_critere.SelectedItem == criter_def && Item_show_val.Text != "")
                    Show = Show.Where(a => ((bool)show_inf.IsChecked ? (a.Armory).def <= double.Parse(Item_show_val.Text) : (a.Armory).def >= double.Parse(Item_show_val.Text))).ToList();
                else if (Item_critere.SelectedItem == criter_atk && Item_show_val.Text != "")
                    Show = Show.Where(a => ((bool)show_inf.IsChecked ? (a.Armory).atk <= double.Parse(Item_show_val.Text) : (a.Armory).atk >= double.Parse(Item_show_val.Text))).ToList();
                else if (Item_critere.SelectedItem == criter_res && Item_show_val.Text != "")
                    Show = Show.Where(a => ((bool)show_inf.IsChecked ? (a.Armory).resistance <= double.Parse(Item_show_val.Text) : (a.Armory).resistance >= double.Parse(Item_show_val.Text))).ToList();
                else if (Item_critere.SelectedItem == criter_pwr && Item_show_val.Text != "")
                    Show = Show.Where(a => ((bool)show_inf.IsChecked ? (a.Armory).puissance <= double.Parse(Item_show_val.Text) : (a.Armory).puissance >= double.Parse(Item_show_val.Text))).ToList();
                else if (Item_critere.SelectedItem == criter_mal_dex && Item_show_val.Text != "")
                    Show = Show.Where(a => ((bool)show_inf.IsChecked ? (a.Armory).dex_malus <= double.Parse(Item_show_val.Text) : (a.Armory).dex_malus >= double.Parse(Item_show_val.Text))).ToList();
                else if (Item_critere.SelectedItem == criter_mal_vit && Item_show_val.Text != "")
                    Show = Show.Where(a => ((bool)show_inf.IsChecked ? (a.Armory).vit_malus <= double.Parse(Item_show_val.Text) : (a.Armory).vit_malus >= double.Parse(Item_show_val.Text))).ToList();
                else if (Item_critere.SelectedItem == criter_ty_armor && Show_criter.SelectedItem != null)
                    Show = Show.Where(a => (a.Armory).Armor_cat == (Armor_cat)Show_criter.SelectedItem).ToList();
                else if (Item_critere.SelectedItem == criter_elm && Show_criter.SelectedItem != null)
                    Show = Show.Where(a => (a.Armory).ElemResArmorAssoc.Any(er => er.element == (Show_criter.SelectedItem as Mag_element).element)).ToList();
                else if (Item_critere.SelectedItem == criter_armor && Show_criter.SelectedItem != null)
                    Show = Show.Where(a => (a.Armory).Piece1 == (Piece)Show_criter.SelectedItem).ToList();
                else if (Item_critere.SelectedItem == criter_cap && Show_criter.SelectedItem != null)
                    Show = Show.Where(a => (a.Armory).capacites.Contains((Show_criter.SelectedItem as Capacites_armor).pouvoir)).ToList();
                foreach (Items row in Show)
                {
                    Itemu_Label.Text += row.nom + ": pèse " + row.masse + " Kg, obtensible par " + row.obtention + ", " + ((row.origine != "Magocosme") ? "Prix tech : " + row.prix_tech + " " : "") + ((row.origine != "Technocosme") ? "Prix tech : " + row.prix_tech + " " : "")
                          + " origine: " + row.origine + " \n"
                          + "Attaque : " + row.Armory.atk + ", Defense : " + row.Armory.def + ", Puissance : " + row.Armory.puissance + ", Resistance : " + row.Armory.resistance + ", " + row.Armory.categorie + '\n'
                          + "Malus de Dexterité : " + row.Armory.dex_malus + ", Malus de vitesse : " + row.Armory.vit_malus
                          + (row.Armory.enchantable ? " Enchantable, " : "") + (row.Armory.enchantement ?? " Aucun Enchantement, ") + row.Armory.capacites;
                    foreach (ElemResArmorAssoc er in row.Armory.ElemResArmorAssoc)
                        Itemu_Label.Text += (er.element != null ? "Elements resistants " + er.element + ", resistance de " + er.resValue : "") + '\n';
                    Itemu_Label.Text += (!row.craftable ? "non craftable; " : "craftable " + row.recette) + '\n' + row.description;
                }
            }
            else if (Item_show_elem.SelectedItem == Bijoux_show)
            {
                Show = Show.Where(b => b.Bijoux != null).ToList();
                if (Item_critere.SelectedIndex == 0) { }
                else if (Item_critere.SelectedItem == criter_ty_bij && Show_criter.SelectedItem != null)
                    Show = Show.Where(b => (b.Bijoux).Bijoux_place == (Bijoux_place)Show_criter.SelectedItem).ToList();
                foreach (Items Item in Show)
                    Itemu_Label.Text += Item.nom + ": pèse " + Item.masse + " Kg, obtensible par " + Item.obtention + ", " + ((Item.origine != "Magocosme") ? "Prix tech : " + Item.prix_tech + " " : "") + ((Item.origine != "Technocosme") ? "prix mago : " + Item.prix_mago + " " : "")
                        + " origine: " + (Item.origine == "Tous" ? " Tout les mondes" : Item.origine) + ", " + (Item.Bijoux.enchantable ? ", Enchantable, " : "") + (Item.Bijoux.enchantements ?? " Auncun enchantement, ")
                        + (!Item.craftable ? "non craftable; " : "craftable.\nRecette: " + (Item.recette.Replace("\n", "; "))) + '\n' + Item.description;
            }
            else if (Item_show_elem.SelectedItem == Consommables_show)
            {
                Show = Show.Where(c => c.Consommables != null).ToList();
                if (Item_critere.SelectedIndex == 0) { }
                else if (Item_critere.SelectedItem == criter_ty_cons && Show_criter.SelectedItem != null)
                    Show = Show.Where(c => (c.Consommables).Conso_type == (Conso_type)Show_criter.SelectedItem).ToList();
                else if (Item_critere.SelectedItem == criter_eft_cons && Show_criter.SelectedItem != null)
                    Show = Show.Where(c => (c.Consommables).Effets == (Effets)Show_criter.SelectedItem || (c.Consommables).Effets1 == (Effets)Show_criter.SelectedItem).ToList();
                foreach (Items Item in Show)
                    Itemu_Label.Text += Item.nom + ": pèse " + Item.masse + " Kg, obtensible par " + Item.obtention + ", " + ((Item.origine != "Magocosme") ? "Prix tech : " + Item.prix_tech + " " : "") + ((Item.origine != "Technocosme") ? "prix mago : " + Item.prix_mago + " " : "")
                        + " origine: " + (Item.origine == "Tous" ? " Tout les mondes" : Item.origine) + ", " + Item.Consommables.type + ", Premier Effet : " + Item.Consommables.effet_1 + ", dure " + Item.Consommables.duree1 + ", puissance : " + Item.Consommables.modulo_1 + ", minimum : " + Item.Consommables.minimum_1 + '\n'
                        + (Item.Consommables.effet_2 != null ? ", Second Effet : " + Item.Consommables.effet_2 + ", dure " + Item.Consommables.duree2 + ", puissance : " + Item.Consommables.modulo_2 + ", minimum : " + Item.Consommables.minimum_2 : "")
                        + (!Item.craftable ? "non craftable; " : "craftable.\nRecette: " + (Item.recette.Replace("\n", "; "))) + '\n' + Item.description;

            }
            else if (Item_show_elem.SelectedItem == Container_show)
            {
                Show = Show.Where(c => c.Conteneurs != null).ToList();
                if (Item_critere.SelectedIndex == 0) { }
                else if (Item_critere.SelectedItem == criter_siz && Item_show_val.Text != "")
                    Show = Show.Where(c => ((bool)show_inf.IsChecked ? (c.Conteneurs).taille <= double.Parse(Item_show_val.Text) : (c.Conteneurs).taille >= double.Parse(Item_show_val.Text))).ToList();
                else if (Item_critere.SelectedItem == criter_limsiz)
                    foreach (Items row in Show)
                        Itemu_Label.Text += row.nom + ": pèse " + row.masse + " Kg, obtensible par " + row.obtention + ", " + ((row.origine != "Magocosme") ? "Prix tech : " + row.prix_tech + " " : "") + ((row.origine != "Technocosme") ? "Prix tech : " + row.prix_tech + " " : "")
                            + " origine: " + row.origine + ", peut contenir des objets de taille inferieure à " + row.Conteneurs.Tailles.categorie + ", pour une capacité de " + row.Conteneurs.taille + "Kg \n"
                         + (!row.craftable ? "non craftable; " : "craftable " + row.recette) + '\n' + row.description;
            }
            else if (Item_show_elem.SelectedItem == Munitions_show)
            {
                Show = Show.Where(m => m.Munition != null).ToList();
                if (Item_critere.SelectedIndex == 0) { }
                else if (Item_critere.SelectedItem == criter_atk && Item_show_val.Text != "")
                    Show = Show.Where(m => ((bool)show_inf.IsChecked ? (m.Munition).degats <= double.Parse(Item_show_val.Text) : (m.Munition).degats >= double.Parse(Item_show_val.Text))).ToList();
                else if (Item_critere.SelectedItem == criter_elm && Show_criter.SelectedItem != null)
                    Show = Show.Where(m => (m.Munition).Mag_element == (Mag_element)Show_criter.SelectedItem || (m.Munition).Mag_element1 == (Mag_element)Show_criter.SelectedItem).ToList();
                else if (Item_critere.SelectedItem == criter_pwr && Item_show_val.Text != "")
                    Show = Show.Where(m => ((bool)show_inf.IsChecked ? (m.Munition).puissance <= double.Parse(Item_show_val.Text) : (m.Munition).puissance >= double.Parse(Item_show_val.Text))).ToList();
                else if (Item_critere.SelectedItem == criter_cout && Item_show_val.Text != "")
                    Show = Show.Where(m => ((bool)show_inf.IsChecked ? (m.Munition).mana_cout <= double.Parse(Item_show_val.Text) : (m.Munition).mana_cout >= double.Parse(Item_show_val.Text))).ToList();
                else if (Item_critere.SelectedItem == criter_ty_degat && Show_criter.SelectedItem != null)
                    Show = Show.Where(m => (m.Munition).degats_type.Contains((Show_criter.SelectedItem as Degat_type).categorie)).ToList();
                else if (Item_critere.SelectedItem == criter_ty_mun && Show_criter.SelectedItem != null)
                    Show = Show.Where(m => (m.Munition).Munition_type == (Munition_type)Show_criter.SelectedItem).ToList();
                foreach (Items row in Show)
                    Itemu_Label.Text += row.nom + ": pèse " + row.masse + " Kg, obtensible par " + row.obtention + ", " + ((row.origine != "Magocosme") ? "Prix tech : " + row.prix_tech + " " : "") + ((row.origine != "Technocosme") ? "Prix tech : " + row.prix_tech + " " : "")
                        + " origine: " + row.origine + " \n"
                        + row.Munition.categorie + ": Degats : " + row.Munition.degats + " de type " + row.Munition.degats_type
                        + (row.Munition.element_1 != null ? "Element : " + row.Munition.element_1 + ", " + row.Munition.puissance_1 + " de Puissance, " + row.Munition.chance_1 + "% de chances d'infliger " + row.Munition.Mag_element.etat + " Pendant " + row.Munition.duree_1 : "")
                        + (row.Munition.element_2 != null ? "Element : " + row.Munition.element_2 + ", " + row.Munition.puissance_2 + " de Puissance, " + row.Munition.chance_2 + "% de chances d'infliger " + row.Munition.Mag_element1.etat + " Pendant " + row.Munition.duree_2 : "")
                        + (!row.craftable ? "non craftable; " : "craftable " + row.recette) + '\n' + row.description;
            }
            else if (Item_show_elem.SelectedItem == Loot_show)
            {
                Show = Show.Where(l => l.Loot != null).ToList();
                foreach (Items Item in Show)
                    Itemu_Label.Text += Item.nom + ": pèse " + Item.masse + " Kg, obtensible par " + Item.obtention + ", " + ((Item.origine != "Magocosme") ? "Prix tech : " + Item.prix_tech + " " : "") + ((Item.origine != "Technocosme") ? "prix mago : " + Item.prix_mago + " " : "")
                        + " origine: " + (Item.origine == "Tous" ? " Tout les mondes" : Item.origine) + ", "
                        + (!Item.craftable ? "non craftable; " : "craftable.\nRecette: " + (Item.recette.Replace("\n", "; "))) + '\n' + Item.description;

            }
            else if (Item_show_elem.SelectedItem == Vehicule_show)
            {
                Show = Show.Where(v => v.Vehicule != null).ToList();
                if (Item_critere.SelectedIndex == 0) { }
                else if (Item_critere.SelectedItem == criter_carbu && Show_criter.SelectedItem != null)
                    Show = Show.Where(v => (v.Vehicule).carburant.Contains((Show_criter.SelectedItem as Carburant).fuel)).ToList();
                else if (Item_critere.SelectedItem == criter_deplac_mde && Show_criter.SelectedItem != null)
                    Show = Show.Where(v => (v.Vehicule).deplacement_mde.Contains((Show_criter.SelectedItem as Mode_deplacement).mode)).ToList();
                else if (Item_critere.SelectedItem == criter_maneu && Show_criter.SelectedItem != null)
                    Show = Show.Where(v => (v.Vehicule).Maniabilite1 == (Maniabilite)Show_criter.SelectedItem).ToList();
                else if (Item_critere.SelectedItem == criter_vit && Item_show_val.Text != "")
                    Show = Show.Where(v => ((bool)show_inf.IsChecked ? (v.Vehicule).vitesse_max <= int.Parse(Item_show_val.Text) : (v.Vehicule).vitesse_max >= int.Parse(Item_show_val.Text))).ToList();
                else if (Item_critere.SelectedItem == criter_reser && Item_show_val.Text != "")
                    Show = Show.Where(v => ((bool)show_inf.IsChecked ? (v.Vehicule).reservoir <= int.Parse(Item_show_val.Text) : (v.Vehicule).reservoir >= int.Parse(Item_show_val.Text))).ToList();
                else if (Item_critere.SelectedItem == criter_sol && Item_show_val.Text != "")
                    Show = Show.Where(v => ((bool)show_inf.IsChecked ? (v.Vehicule).solidite <= int.Parse(Item_show_val.Text) : (v.Vehicule).solidite >= int.Parse(Item_show_val.Text))).ToList();
                else if (Item_critere.SelectedItem == criter_voie && Show_criter.SelectedItem != null)
                    Show = Show.Where(v => (v.Vehicule).accessibilite.Contains((Show_criter.SelectedItem as Voies).voie)).ToList();
                foreach (Items row in Show)
                    Itemu_Label.Text += row.nom + ": pèse " + row.masse + " Kg, obtensible par " + row.obtention + ", " + ((row.origine != "Magocosme") ? "Prix tech : " + row.prix_tech + " " : "") + ((row.origine != "Technocosme") ? "Prix tech : " + row.prix_tech + " " : "")
                        + " origine: " + row.origine + " \n"
                        + "Vitesse maximale : " + row.Vehicule.vitesse_max + " " + row.Vehicule.solidite + " Point de solidité, taille de reservoir" + row.Vehicule.reservoir + ", peut progresser par voie " + row.Vehicule.accessibilite + ", " + row.Vehicule.maniabilite + ", carbure à" + row.Vehicule.carburant
                     + (!row.craftable ? "non craftable; " : "craftable " + row.recette) + '\n' + row.description;
            }
            else if (Item_show_elem.SelectedItem == Common_show)
            {
                Show = Show.Where(i => i.Armory == null && i.Mineraux == null && i.Bijoux == null && i.Weaponry == null && i.Vehicule == null && i.Loot == null && i.Mineraux == null && i.Munition == null && i.Conteneurs == null && i.Consommables == null && i.Parchemins == null).ToList();
                foreach (Items row in Show)
                    Itemu_Label.Text += row.nom + ": pèse " + row.masse + " Kg, obtensible par " + row.obtention + ", " + ((row.origine != "Magocosme") ? "Prix tech : " + row.prix_tech + " " : "") + ((row.origine != "Technocosme") ? "Prix tech : " + row.prix_tech + " " : "")
                        + " origine: " + row.origine + " \n"
                     + (!row.craftable ? "non craftable; " : "craftable " + row.recette) + '\n' + row.description;
            }
        }

        private void Weap_Spel_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Weap_Spel_list.SelectedItems.Count < 3)
                Weap_Spel_list.Items.Remove(Weap_Spel_list.Items[2]);
        }

        private void jewel_linked_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Bijoux_link != null)
                Bijoux_link.SelectedItem = null;
            Bijoux_item_link.SelectedIndex = 0;
            if (DataContext != null)
                (DataContext as ViewModel.ItemViewModel).LinkList = new List<Items>((DataContext as ViewModel.ItemViewModel).LinkList);
        }

        private void process_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem == null) ((ComboBox)sender).SelectedIndex = 0;
        }
        private void RefreshSpell(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null)
                (DataContext as ViewModel.ItemViewModel).Sorts = new List<Sorts>((DataContext as ViewModel.ItemViewModel).Sorts);
        }

        public void Dispose() { Bdd.Dispose(); (DataContext as ItemViewModel).Dispose(); }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Item_rec_manacost.Text != "" && int.TryParse(Item_rec_manacost.Text, out int ncost))
                if ((Item_rec.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeItem>).Any(r => r.N_recette == (int)recipeId.SelectedItem && r.Component.Id == 0))
                    (Item_rec.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeItem>).First(r => r.N_recette == (int)recipeId.SelectedItem && r.Component.Id == 0).Quantite = ncost;
                else (Item_rec.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeItem>).Add(new RecipeItem { Component = new Items { Id = 0, nom = "Mana", origine = "Originel" }, Quantite = ncost, N_recette = (int)recipeId.SelectedItem });
            else Itemu_Label.Text += "Veuillez entrer un nombre entier valide.\n";
            (Item_rec.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeItem>).OrderBy(r => r.N_recette);
        }

        private void SizeShow(object sender, RoutedEventArgs e)
        {
            foreach (Tailles t in Bdd.Tailles)
                Itemu_Label.Text += t.categorie + " = " + t.intervalle + t.unite + (t == Bdd.Tailles.ToList().Last() ? "" : "\n");
        }

        private void RefreshCompo(object sender, SelectionChangedEventArgs e)
        {
            if (Component != null)
                Component.SelectedItem = null;
            if (DataContext != null)
                (DataContext as ViewModel.ItemViewModel).Components = new List<Items>((DataContext as ViewModel.ItemViewModel).Components);
        }

        private void RefreshLinks(object sender, SelectionChangedEventArgs e)
        {
            if (Bijoux_link != null)
                Bijoux_link.SelectedItem = null;
            if (DataContext != null)
                (DataContext as ViewModel.ItemViewModel).LinkList = new List<Items>((DataContext as ViewModel.ItemViewModel).LinkList);
        }

        private void ChargeFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog charge = new OpenFileDialog() { Filter = "textFiles .txt | *.txt", Multiselect = false };
            if ((bool)charge.ShowDialog())
            {
                filename.Text = charge.SafeFileName;
                bookcontent.Text = File.ReadAllText(charge.FileName);
            }
        }

        private void ProcessToRecipe(object sender, RoutedEventArgs e)
        {
            (Results.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeResult>).First(r => r.IdRecipe == (int)recipeId.SelectedItem).Process = procRecipe.SelectedItem as Procede;
            (Results.Items as CollectionView).Refresh();
        }
    }
}

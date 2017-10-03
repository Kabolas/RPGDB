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
    /// Logique d'interaction pour SpellControl.xaml
    /// </summary>
    public partial class SpellControl : UserControl
    {
        public RPGEntities15 Bdd
        {
            get => (RPGEntities15)GetValue(BddProperty);
            set => SetValue(BddProperty, value);
        }
        public static readonly DependencyProperty BddProperty =
            DependencyProperty.Register("Bdd",
                typeof(RPGEntities15),
                typeof(SpellControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(ChargeFromDb)));

        private static void ChargeFromDb(DependencyObject d, DependencyPropertyChangedEventArgs e) { (d as SpellControl).DataContext = new ViewModel.SpellViewModel(e.NewValue as RPGEntities15); }
        public NameGen Gen
        {
            get => (NameGen)GetValue(GenProperty);
            set => SetValue(GenProperty, value);
        }
        public static readonly DependencyProperty GenProperty =
            DependencyProperty.Register("Gen",
                typeof(NameGen),
                typeof(SpellControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public SpellControl()
        {
            InitializeComponent();
        }

        private void Display(object sender, RoutedEventArgs e)
        {
            if ((bool)combo_show.IsChecked)
            {
                List<Combo> Show = Bdd.Combo.ToList();
                switch ((Spell_criter.SelectedItem as ComboBoxItem).Content)
                {
                    case "Puissance":
                        Show = Show.FindAll(c => ((bool)is_inf.IsChecked) ? c.puissance <= int.Parse(spel_cri_val.Text) : c.puissance > int.Parse(spel_cri_val.Text));
                        break;
                    case "Attaque":
                        Show = Show.FindAll(c => ((bool)is_inf.IsChecked) ? c.attaque <= int.Parse(spel_cri_val.Text) : c.attaque > int.Parse(spel_cri_val.Text));
                        break;
                    case "Crowd Control":
                        Show = Show.FindAll(c => c.Crowd_control == Spell_critere.SelectedItem as Crowd_control);
                        break;
                    case "Cout (Mana)":
                        Show = Show.FindAll(c => ((bool)is_inf.IsChecked) ? c.mana_cout <= int.Parse(spel_cri_val.Text) : c.mana_cout > int.Parse(spel_cri_val.Text));
                        break;
                    case "Etat":
                        Show = Show.FindAll(c => c.Etat1 == Spell_critere.SelectedItem as Etat);
                        break;
                    case "Ecole":
                        Show = Show.FindAll(c => c.magie_use.Contains((Spell_critere.SelectedItem as Magie_type).ecole));
                        break;
                    case "Cout (Stamina)":
                        Show = Show.FindAll(c => ((bool)is_inf.IsChecked) ? c.stamina_cout <= int.Parse(spel_cri_val.Text) : c.stamina_cout > int.Parse(spel_cri_val.Text));
                        break;
                    case "Portee":
                        Show = Show.FindAll(c => ((bool)is_inf.IsChecked) ? c.portee <= int.Parse(spel_cri_val.Text) : c.portee > int.Parse(spel_cri_val.Text));
                        break;
                    case "Rayon":
                        Show = Show.FindAll(c => ((bool)is_inf.IsChecked) ? c.rayon <= int.Parse(spel_cri_val.Text) : c.rayon > int.Parse(spel_cri_val.Text));
                        break;
                    case "Armes":
                        Show = Show.FindAll(c => c.weapons.Contains((Spell_critere.SelectedItem as Weapon_type).type));
                        break;
                    case "Buff/DeBuff":
                        Show = Show.FindAll(c => c.Buff1 == Spell_critere.SelectedItem as Buff);
                        break;
                    case "Niveau":
                        Show = Show.FindAll(c => ((bool)is_inf.IsChecked) ? c.niveau <= int.Parse(spel_cri_val.Text) : c.niveau > int.Parse(spel_cri_val.Text));
                        break;
                    case "Ciblage":
                        Show = Show.FindAll(c => c.Ciblage1 == Spell_critere.SelectedItem as Ciblage);
                        break;
                    case "Categorie":
                    case "Catégorie":
                        Show = Show.FindAll(c => c.ComboCat == Spell_critere.SelectedItem);
                        break;
                    default:
                        break;
                }
                foreach (Combo combo in Show)
                    Spellu_Label.Text += combo.nom + " -- " + combo.type + ", niveau "+combo.niveau+" ==> attaque : " + combo.attaque + ", puissance : " + combo.puissance + ", type de ciblage : " + combo.ciblage +
                                            "\ncout en stamina : " + combo.stamina_cout + "cout en mana : " + combo.mana_cout + ", portée : " + combo.portee + ", rayon d'action : " + combo.rayon + "\n" +
                                          (combo.Crowd_control != null ? combo.Cc_chance + "% de chances d'infliger : " + combo.Cc + " pendant " + combo.Cc_duree + "s+\n" : "") +
                                          (combo.Buff1 != null ? combo.buff_chance + "% de chances d'avoir " + combo.buff + " pendant " + combo.buff_duree + "s\n" : "") +
                                          (combo.Etat1 != null ? combo.etat_chance + "% de chances d'infliger " + combo.etat + " pendant " + combo.etat_duree + "s\n" : "") +
                                          "Utilisable par :" + combo.weapons + "\nUtilise les ecoles de " + combo.magie_use + " au niveau " + combo.lvl_use + ".\n Puissance du Combo basé sur "+combo.stat+"\n";
            }
            else
            {
                List<Sorts> Show = Bdd.Sorts.ToList();
                switch ((Spell_criter.SelectedItem as ComboBoxItem).Content)
                {
                    case "Puissance":
                        Show = Show.FindAll(c => ((bool)is_inf.IsChecked) ? c.puissance <= int.Parse(spel_cri_val.Text) : c.puissance > int.Parse(spel_cri_val.Text));
                        break;
                    case "Crowd Control":
                        Show = Show.FindAll(c => c.Crowd_control == Spell_critere.SelectedItem as Crowd_control);
                        break;
                    case "Cout (Mana)":
                        Show = Show.FindAll(c => ((bool)is_inf.IsChecked) ? c.mana_cost <= int.Parse(spel_cri_val.Text) : c.mana_cost > int.Parse(spel_cri_val.Text));
                        break;
                    case "Element":
                        Show = Show.FindAll(c => c.Mag_element == Spell_critere.SelectedItem as Mag_element);
                        break;
                    case "Ecole":
                        Show = Show.FindAll(c => c.Magie_type == Spell_critere.SelectedItem );
                        break;
                    case "Cout (Stamina)":
                        Show = Show.FindAll(c => ((bool)is_inf.IsChecked) ? c.stamina_cout <= int.Parse(spel_cri_val.Text) : c.stamina_cout > int.Parse(spel_cri_val.Text));
                        break;
                    case "Portee":
                        Show = Show.FindAll(c => ((bool)is_inf.IsChecked) ? c.portee <= int.Parse(spel_cri_val.Text) : c.portee > int.Parse(spel_cri_val.Text));
                        break;
                    case "Rayon":
                        Show = Show.FindAll(c => ((bool)is_inf.IsChecked) ? c.rayon <= int.Parse(spel_cri_val.Text) : c.rayon > int.Parse(spel_cri_val.Text));
                        break;
                    case "Buff/DeBuff":
                        Show = Show.FindAll(c => c.Buff1 == Spell_critere.SelectedItem as Buff);
                        break;
                    case "Niveau":
                        Show = Show.FindAll(c => ((bool)is_inf.IsChecked) ? c.niveau <= int.Parse(spel_cri_val.Text) : c.niveau > int.Parse(spel_cri_val.Text));
                        break;
                    case "Ciblage":
                        Show = Show.FindAll(c => c.Ciblage1 == Spell_critere.SelectedItem as Ciblage);
                        break;
                    default:
                        break;
                }
                foreach (Sorts combo in Show)
                    Spellu_Label.Text += combo.nom + "==> puissance : " + combo.puissance + ", type de ciblage : " + combo.ciblage +
                                            "\ncout en stamina : " + combo.stamina_cout + "cout en mana : " + combo.mana_cost + ", portée : " + combo.portee + ", rayon d'action : " + combo.rayon + "\n" +
                                          (combo.Crowd_control != null ? combo.Cc_chance + "% de chances d'infliger : " + combo.Cc + " pendant " + combo.Cc_duree + "s+\n" : "") +
                                          (combo.Buff1 != null ? combo.buff_chance + "% de chances d'avoir " + combo.buff + " pendant " + combo.buff_duree + "s\n" : "") +
                                          (combo.Mag_element!= null ? combo.etat_chance + "% de chances d'infliger " + combo.Mag_element.etat+ " pendant " + combo.etat_duree + "s\n" : "") +
                                          "Puissance du Sort basée sur :"+combo.stat + "\nSort de" + combo.Magie_type.ecole+ " de niveau " + combo.niveau+ ".\n\n";
            }
        }

        private void Generation(object sender, RoutedEventArgs e) { Spell_name.Text = Gen.Generation_gn_Sons(Spell.Value, Spell.Word, (bool)Spell.Before, Spell.Triphtongue, Spell.Symbol); }

        private void Selecion_with_all(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Combo_check_Checked(object sender, RoutedEventArgs e) { ComboOrSpell.isCombo = true; }

        private void Combo_check_Unchecked(object sender, RoutedEventArgs e) { ComboOrSpell.isCombo = false; }

        private void ShowBuf(object sender, RoutedEventArgs e)
        {
            Spellu_Label.Text += "Buff / Debuff --[\n";
            foreach (Buff buf in Bdd.Buff)
                Spellu_Label.Text += buf.nom + ": " + buf.descr + "\n\n";
            Spellu_Label.Text += "]--\n";
        }

        private void ShowState(object sender, RoutedEventArgs e)
        {
            Spellu_Label.Text += "Etats --[\n";
            foreach (Etat state in Bdd.Etat)
                Spellu_Label.Text += state.etat1 + ": " + state.decr + "\n\n";
            Spellu_Label.Text += "]--\n";
        }

        private void ShowCc(object sender, RoutedEventArgs e)
        {
            Spellu_Label.Text += "Crowd Controls --[\n";
            foreach (Crowd_control cc in Bdd.Crowd_control)
                Spellu_Label.Text += cc.CC + ": " + cc.descr + "\n\n";
            Spellu_Label.Text += "]--\n";
        }

        private void ShowElement(object sender, RoutedEventArgs e)
        {
            Spellu_Label.Text += "Elements --[\n";
            foreach (Mag_element element in Bdd.Mag_element)
                Spellu_Label.Text += element.element + "|\t" + element.etat + ": " + element.desc_etat + "\n\n";
            Spellu_Label.Text += "]--\n";
        }

        private void ShowEcole(object sender, RoutedEventArgs e)
        {
            Spellu_Label.Text += "Ecoles de magie --[\n";
            foreach (Magie_type ecole in Bdd.Magie_type)
                Spellu_Label.Text += ecole.ecole + ": " + ecole.descr + "\n\n";
            Spellu_Label.Text += "]--\n";
        }

        private void Placeholder(object sender, SelectionChangedEventArgs e) { if ((sender as ComboBox).SelectedItem == null) (sender as ComboBox).SelectedIndex = 0; }

        private void CriterChange(object sender, SelectionChangedEventArgs e)
        {
            switch (((sender as ComboBox).SelectedItem as ComboBoxItem).Content)
            {
                case "Crowd Control":
                    Spell_critere.ItemsSource = (DataContext as ViewModel.SpellViewModel).CCs;
                    Spell_critere.DisplayMemberPath = "CC";
                    break;
                case "Element":
                    Spell_critere.ItemsSource = (DataContext as ViewModel.SpellViewModel).Elements;
                    Spell_critere.DisplayMemberPath = "element";
                    break;
                case "Etat":
                    Spell_critere.ItemsSource = (DataContext as ViewModel.SpellViewModel).States;
                    Spell_critere.DisplayMemberPath = "etat1";
                    break;
                case "Ecole":
                    Spell_critere.ItemsSource = (DataContext as ViewModel.SpellViewModel).Schools;
                    Spell_critere.DisplayMemberPath = "ecole";
                    break;
                case "Armes":
                    Spell_critere.ItemsSource = (DataContext as ViewModel.SpellViewModel).Weapons;
                    Spell_critere.DisplayMemberPath = "type";
                    break;
                case "Buff/DeBuff":
                    Spell_critere.ItemsSource = (DataContext as ViewModel.SpellViewModel).Buffs;
                    Spell_critere.DisplayMemberPath = "nom";
                    break;
                case "Ciblage":
                    Spell_critere.ItemsSource = (DataContext as ViewModel.SpellViewModel).Ciblages;
                    Spell_critere.DisplayMemberPath = "ciblage1";
                    break;
                case "Categorie":
                case "Catégorie":
                    Spell_critere.ItemsSource = (DataContext as ViewModel.SpellViewModel).Categories;
                    Spell_critere.DisplayMemberPath = "categorie";
                    break;
                default:
                    break;
            }
        }
    }
}

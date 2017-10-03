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
    /// Logique d'interaction pour JahrWordsControl.xaml
    /// </summary>
    public partial class JahrWordsControl : UserControl
    {
        public RPGEntities15 Bdd
        {
            get => (RPGEntities15)GetValue(BddProperty);
            set => SetValue(BddProperty, value);
        }
        public static readonly DependencyProperty BddProperty =
            DependencyProperty.Register("Bdd",
                typeof(RPGEntities15),
                typeof(JahrWordsControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(ChargeFromDb)));

        private static void ChargeFromDb(DependencyObject d, DependencyPropertyChangedEventArgs e) {CommonToAlph.DicoConvert = (d as JahrWordsControl).Gen; (d as JahrWordsControl).DataContext = new ViewModel.JahrWordsViewModel(e.NewValue as RPGEntities15); }
        public NameGen Gen
        {
            get => (NameGen)GetValue(GenProperty);
            set => SetValue(GenProperty, value);
        }
        public static readonly DependencyProperty GenProperty =
            DependencyProperty.Register("Gen",
                typeof(NameGen),
                typeof(JahrWordsControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        private bool ignore = false;

        public JahrWordsControl()
        {
            
            InitializeComponent();
            infos.Text = "Informations sur la generation et les sons;\n" +
                    "les sons fait avec plusieurs caractère sont encadrés par [].\n" +
                    "Voici les sons a plusieurs caracteres disponibles;\n" +
                    "\tVoyelles:\n\t\t" +
                    "[ou]\n\t\t[an]\n\t\t[eu]\n\t\t[on]\n\t\t[in]\n\t\t[ouo]\n\t\tò est le o qu'on entend dans or.\n\t" +
                    "Consonnes:\n\t\t" +
                    "[ch]\n\t\t[th] est le son th anglais\n\t\t[rw] est la prononciation anglaise du r\n\t\t[rr]est le r roulé\n\t\t[hr] est une raclement de gorge melangé à un r roulé.";

        }

        private void Generation(object sender, RoutedEventArgs e) { genwordnorm.Text = Gen.default_Generation_Sons(Jahr_dic.Value, Jahr_dic.Word, Jahr_dic.Before, Jahr_dic.Triphtongue, Jahr_dic.Symbol); }

        private void Display(object sender, RoutedEventArgs e)
        {
            if ((bool)ShowMot.IsChecked)
            {
                List<Mots> showing = Bdd.Mots.ToList();
                if (type.SelectedIndex != 0)
                    showing = showing.Where(mot => mot.type == (type.SelectedItem as Type).mot_type).ToList();
                if (showchamp.SelectedIndex != 0)
                    showing = showing.Where(mot => mot.ChampLexicAssoc.Any(c => c.Chp == (showchamp.SelectedItem as ChampLex).Registre)).ToList();
                foreach (Mots mot in showing)
                    label1.Text += mot.mo_initial + " => " + mot.mot_common_alph + '\n';
            }
            else if ((bool)ShowWorld.IsChecked)
            {
                List<Monde> showing = Bdd.Monde.ToList();
                if (showcat.SelectedIndex != 0)
                    showing = showing.Where(mnde => mnde.CatWorldAssoc.Any(c => c.Cat_monde == (showcat.SelectedItem as Categories_monde).Cat_monde)).ToList();
                foreach(Monde mnd in showing)
                    label1.Text += mnd.mot_monde + "=>" + mnd.mot_commun + '\n';
            }
            else
            {
                label1.Text += "Mots : ";
                foreach (Mots mot in Bdd.Mots)
                    label1.Text += mot.mo_initial + " => " + mot.mot_common_alph + '\n';
                label1.Text += "Monde : ";
                foreach (Monde mnd in Bdd.Monde)
                    label1.Text += mnd.mot_monde + "=>" + mnd.mot_commun + '\n';
            }
        }

        private void Regle_Click(object sender, RoutedEventArgs e)
        {
            label1.Text += "Regles de la langue: \n"
                + "Formation d'une phrase : [sujet] [verbe] [complement].\n\n"
                + "Accords : \n\t-pluriel: radical-[noy].\n\t"
                + "-feminin : radical-[saj].\n\t"
                + "-masculin : radical-[caj].\n\t"
                + "-neutre :  redical.\n"
                + "Ordre de l'accord : -genre -nombre."
                + "\n\nComplement du nom : radical-oje."
                + "\nMetier (celui qui utilise) : radical + no[rr] + accord."
                + "\nMetier profession : radical+[ch]i[on] + accord."
                + "\nMetier (celui qui fait parti de) : radical + g[ou]s + accord."
                + "\n\nConjugaison : "
                + "\n2 type de verbes, verbes d'action, verbes d'état. \nVerbe d'action "
                + "\n\n";
        }

        private void IsWorld_Checked(object sender, RoutedEventArgs e) { WorldOrWord.isWorld = true; }

        private void IsWorld_Unchecked(object sender, RoutedEventArgs e) { WorldOrWord.isWorld = false; }

        private void ChmpDisp(object sender, RoutedEventArgs e)
        {
            label1.Text += "Chants lexicaux : ";
            foreach (ChampLex chp in Bdd.ChampLex)
                label1.Text += chp.Registre + '\n';
        }

        private void CatDisp(object sender, RoutedEventArgs e)
        {
            label1.Text += "Categories : ";
            foreach (Categories_monde chp in Bdd.Categories_monde)
                label1.Text += chp.Cat_monde + '\n';
        }
    }
}

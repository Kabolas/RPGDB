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
using System.Data.SqlClient;
using System.Data;

namespace RPG_Jahr_words
{
    public enum Tabitem
    {
        Dictionnaire,
        Items,
        Personnages,
        Bestiaire,
        Enchant,
        Spells,
        The_World,
        Pantheon,
        Dico_commun,
        Dico_human,
        Dico_aqua,
        Dico_avien,
        Dico_terre,
    };

    public partial class MainWindow : Window, IDisposable
    {
        public Tabitem Page { get; private set; }
        private NameGen _gen = new NameGen();
        public NameGen Gen { get => _gen; private set => _gen = value; }
        private SqlConnection con;
        private SqlCommand com;
        private RPGEntities15 _bdd = new RPGEntities15();
        public RPGEntities15 Bdd { get => _bdd; private set => _bdd = value; }

        public MainWindow()
        {
            DataContext = new ViewModel.MainViewModel { Bdd = this.Bdd, Gen = this.Gen };
            InitializeComponent();
            //con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Projects\RPG_Jahr_words\RPG_Jahr_words\RPG.mdf;Integrated Security=True;");// Connect Timeout=30;");// User Instance=True");

            NameGen.adjectif = false;
            NameGen.monde = false;
            NameGen.mots = false;
            NameGen.objets = false;
            NameGen.verbes = false;

            ImageBrush reu = new ImageBrush();
            ImageBrush dist = new ImageBrush();
            ImageBrush armor = new ImageBrush();
            ImageBrush cac = new ImageBrush();
            //ImageBrush momo = new ImageBrush();
            ImageBrush hando = new ImageBrush();
            reu.ImageSource = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/Ressources/" + "reunion.jpg", UriKind.Absolute));
            Acceuil.Background = reu;
            hando.ImageSource = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/Ressources/" + "main canalisation.jpg", UriKind.Absolute));
            Sorts.Background = hando;
            Icon = BitmapFrame.Create(new Uri(System.IO.Directory.GetCurrentDirectory() + "/Ressources/" + "Icon1.ico", UriKind.RelativeOrAbsolute));
            dist.ImageSource = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/Ressources/" + "Distance.jpg", UriKind.Absolute));
            armor.ImageSource = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/Ressources/" + "Vouivre_armor.jpg", UriKind.Absolute));
            cac.ImageSource = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/Ressources/" + "Corpsacorps.jpg", UriKind.Absolute));
            //mo.ImageSource = new BitmapImage(new Uri(rectory + "nextprec.jpg", UriKind.Absolute));
            //button13_Copy.Background = button13_Copy2.Background= lele;
            //button13_Copy4.Background = button13_Copy6.Background= momo;
            //button13.Background = button13_Copy1.Background= mo;
            //con.Open();
            Enchants.EnchantAdded += Persos.CallEnchRfrsh;
            Enchants.TypeAdded += Persos.CallEnchTypeRfrsh;
            Enchants.EffectAdded += Persos.CallEnchEffectRfrsh;
            Enchants.EnchantAdded += Item.CallEnchRfrsh;
            Enchants.TypeAdded += Item.CallEnchTypeRfrsh;
            Enchants.EffectAdded += Item.CallEnchEffectRfrsh;
            Item.ItemAdded += Enchants.CallItemRefresh;
            Item.NewWeapontype += Enchants.CallWeapsRefresh;
            Item.ItemAdded += Persos.CallItemRfrsh;
            Item.NewWeapontype += Persos.CallWeaponAdd;
            Spells.AddedSpell += Item.CallSpellRefrsh;
            Spells.AddedSpell += Persos.CallSpellsRefrsh;
            UpdateWin();
        }
        #region Bestiaire
        private void BeastUpdate()
        {
            //DataSet set = new DataSet();
            //new SqlDataAdapter("SELECT * FROM Comportements", con).Fill(set, "Comportement");
            //new SqlDataAdapter("SELECT * FROM Best_cat", con).Fill(set, "Categorie");
            //new SqlDataAdapter("SELECT nom FROM Items, Loot WHERE Items.Id = id_item", con).Fill(set, "Loots");
            //new SqlDataAdapter("SELECT * FROM Capacites_best", con).Fill(set, "Capacites");
            //new SqlDataAdapter("SELECT nom FROM Continent", con).Fill(set, "Continent");
            //new SqlDataAdapter("SELECT * FROM Biomes", con).Fill(set, "Biomes");
            //new SqlDataAdapter("SELECT nom FROM Region", con).Fill(set, "Region");
            //if (set.Tables["Categorie"].Rows.Count + 1 > Beast_cat.Items.Count)
            //    foreach (DataRow row in set.Tables["Categorie"].Rows)
            //        if (!Beast_cat.Items.Contains(row.ItemArray[0]))
            //        {
            //            Beast_cat.Items.Add(row.ItemArray[0]);
            //            evol_cat.Items.Add(row.ItemArray[0]);
            //        }
            //if (Beast_lootlootable.Items.Count < set.Tables["Loots"].Rows.Count + 1)
            //    foreach (DataRow row in set.Tables["Loots"].Rows)
            //        if (!Beast_lootlootable.Items.Contains(row.ItemArray[0]))
            //            Beast_lootlootable.Items.Add(row.ItemArray[0]);
            //if (Beast_behav.Items.Count < set.Tables["Comportement"].Rows.Count + 1)
            //    foreach (DataRow row in set.Tables["Coportement"].Rows)
            //        if (!Beast_behav.Items.Contains(row.ItemArray[0]))
            //            Beast_behav.Items.Add(row.ItemArray[0]);
            //if (cap_list.Items.Count < set.Tables["Capacites"].Rows.Count)
            //    foreach (DataRow row in set.Tables["Capacites"].Rows)
            //        if (!cap_list.Items.Contains(row.ItemArray[0]))
            //            cap_list.Items.Add(row.ItemArray[0]);
            //if (habitat_list.Items.Count < (set.Tables["Continent"].Rows.Count + set.Tables["Region"].Rows.Count + set.Tables["Biomes"].Rows.Count))
            //{
            //    foreach (DataRow row in set.Tables["Continent"].Rows)
            //        if (!habitat_list.Items.Contains(row.ItemArray[0]))
            //            habitat_list.Items.Add(row.ItemArray[0]);
            //    foreach (DataRow row in set.Tables["Region"].Rows)
            //        if (!habitat_list.Items.Contains(row.ItemArray[0]))
            //            habitat_list.Items.Add(row.ItemArray[0]);
            //    foreach (DataRow row in set.Tables["Biomes"].Rows)
            //        if (!habitat_list.Items.Contains(row.ItemArray[0]))
            //            habitat_list.Items.Add(row.ItemArray[0]);
            //}
        }
        private void AddBeast_cat()
        {
            //AddSmthng fntre = new AddSmthng("Entrer votre nouveau Categorie de créature .\n"
            //    + "Les noms de categorie doivent commencer par des Majuscules.\n")
            //{
            //    Title = "Nouvelle Categorie de Créature"
            //};
            //fntre.ShowDialog();
            //if (AddSmthng.maj)
            //{
            //    com = new SqlCommand("INSERT INTO Best_cat (categorie) VALUES ('" + AddSmthng.nouveau + "');", con);
            //    try
            //    {
            //        if (com.ExecuteNonQuery() > 0)
            //            Beasto_Label.Content += "Nouvelle catégorie " + AddSmthng.nouveau + " ajouté.\n";
            //        else
            //            Beasto_Label.Content += "Nouvelle catégorie " + AddSmthng.nouveau + " non ajouté.\n";
            //    }
            //    catch { Beasto_Label.Content += "catégorie déjà existante.\n"; }
            //}
            //else
            //    Beasto_Label.Content += "Nouvelle categorie " + AddSmthng.nouveau + " non ajouté.\n";
            //BeastUpdate();
        }
        private void AddBehav()
        {
            //AddSmthng fntre = new AddSmthng("Entrer votre nouveau Comportement de créature .\n"
            //    + "Les noms de comportement doivent commencer par des Majuscules.\n"
            //    + "Vous pouvez ajouter une description de votre école en séparant votre nom et la description par un retour à la ligne.")
            //{
            //    Title = "Nouveau Comportement de Créature"
            //};
            //fntre.ShowDialog();
            //if (AddSmthng.maj)
            //{
            //    if (AddSmthng.nouveau.Contains("\n"))
            //        com = new SqlCommand("INSERT INTO Comportement (behaviour, descr) VALUES ('" + AddSmthng.nouveau.Split('\n')[0].Remove(AddSmthng.nouveau.Split('\n')[0].Length - 1).Replace("'", "''") + "','" + AddSmthng.nouveau.Split('\n')[1].Trim('\n').Replace("'", "''") + "');", con);
            //    else com = new SqlCommand("INSERT INTO Comportement (behaviour) VALUES ('" + AddSmthng.nouveau + "');", con);
            //    try
            //    {
            //        if (com.ExecuteNonQuery() > 0)
            //            Beasto_Label.Content += "Nouveau Comportement " + AddSmthng.nouveau + " ajouté.\n";
            //        else
            //            Beasto_Label.Content += "Nouveau catégorie " + AddSmthng.nouveau + " non ajouté.\n";
            //    }
            //    catch { Beasto_Label.Content += "Comportement déjà existante.\n"; }
            //}
            //else
            //    Beasto_Label.Content += "Nouvelle Comportement " + AddSmthng.nouveau + " non ajouté.\n";
            //BeastUpdate();
        }
        private void AddBeastCap()
        {
            //        AddSmthng fntre = new AddSmthng("Entrer votre nouvelle Capacité de créature .\n"
            //+ "Les noms de capacité doivent commencer par des Majuscules.\n")
            //        {
            //            Title = "Nouvelle Capacité de Créature"
            //        };
            //        fntre.ShowDialog();
            //        if (AddSmthng.maj)
            //        {
            //            if (AddSmthng.nouveau.Contains("\n"))
            //                com = new SqlCommand("INSERT INTO Capacites_best (capacite, descr) VALUES ('" + AddSmthng.nouveau.Split('\n')[0].Remove(AddSmthng.nouveau.Split('\n')[0].Length - 1).Replace("'", "''") + "','" + AddSmthng.nouveau.Split('\n')[1].Trim('\n').Replace("'", "''") + "');", con);
            //            else com = new SqlCommand("INSERT INTO Capacites_best (capacite) VALUES ('" + AddSmthng.nouveau + "');", con);
            //            try
            //            {
            //                if (com.ExecuteNonQuery() > 0)
            //                    Beasto_Label.Content += "Nouvelle capacité " + AddSmthng.nouveau + " ajouté.\n";
            //                else
            //                    Beasto_Label.Content += "Nouvelle capacité " + AddSmthng.nouveau + " non ajouté.\n";
            //            }
            //            catch { Beasto_Label.Content += "capacité déjà existante.\n"; }
            //        }
            //        else
            //            Beasto_Label.Content += "Nouvelle capacité " + AddSmthng.nouveau + " non ajouté.\n";
            //        BeastUpdate();

        }
        private bool Beast_checkelem_mag()
        {
            int max = 0;
            if (max < int.Parse(Beast_feu.Text))
                max = int.Parse(Beast_feu.Text);
            if (max < int.Parse(Beast_eau.Text))
                max = int.Parse(Beast_eau.Text);
            if (max < int.Parse(Beast_fou.Text))
                max = int.Parse(Beast_fou.Text);
            if (max < int.Parse(Beast_gla.Text))
                max = int.Parse(Beast_gla.Text);
            if (max < int.Parse(Beast_ven.Text))
                max = int.Parse(Beast_ven.Text);
            if (max < int.Parse(Beast_ter.Text))
                max = int.Parse(Beast_ter.Text);
            if (max < int.Parse(Beast_ten.Text))
                max = int.Parse(Beast_ten.Text);
            if (max < int.Parse(Beast_lum.Text))
                max = int.Parse(Beast_lum.Text);
            return max < int.Parse(Beast_mag_elm.Text);
        }
        private void AddBeast()
        {
            if (Beast_name.Text == "")
            {
                Beasto_Label.Content = "Votre créature a besoin d'un nom!";
                return;
            }
            if (Beast_origin.SelectedIndex == 0)
            {
                Beasto_Label.Content = "D'où vient cette créature?";
                return;
            }
            if (Beast_cat.SelectedIndex == 0)
            {
                Beasto_Label.Content = "Cette creature appartient a quelle categorie";
                return;
            }
            if (Beast_behav.SelectedIndex == 0)
            {
                Beasto_Label.Content = "Comment se comprte cette créature?";
                return;
            }
            if ((bool)isevol.IsChecked && subevol.SelectedItem == null)
            {
                Beasto_Label.Content = "De quel créature est_ce l'evolution ?";
                return;
            }
            if (!Beast_checkelem_mag())
            {
                Beasto_Label.Content = "La maitrise d'un element ne peut pas depasser la maitrise de la magie elementaire.";
                return;
            }
            string living = "", capacite = "", looting = "";
            foreach (string str in habitat_list.SelectedItems)
                living += str + ";";
            foreach (string str in cap_list.SelectedItems)
                capacite += str + ";";
            foreach (string str in Beast_Lootlist.SelectedItems)
                looting += str + ";";
            com = new SqlCommand("INSERT INTO Bestiaire_Beast (nom, origine, evolution, sub_beast, petable, armorable, cat, comportement, habitat, capacites, loots, descr)" +
                "VALUES ('" + Beast_name.Text.Replace("'", "''") + "','" + Beast_origin.SelectedItem + "','" + ((bool)isevol.IsChecked ? "True', '" + subevol.SelectedItem + "'" : "False', NULL") +
                ",'" + ((bool)petable.IsChecked ? "True" : "False") + "','" + ((bool)Stuffable.IsChecked ? "True" : "False") + "','" + Beast_cat.SelectedItem + "','" + Beast_behav.SelectedItem +
                "','" + living + "','" + capacite + "','" + looting + "','" + Beast_desc.Text.Replace("'", "''") + "');", con);
            if (com.ExecuteNonQuery() > 0)
            {
                Beasto_Label.Content = "Nouvelle Bete ajoutée.\nAjout des Stats de la Bete\n";
                DataSet set = new DataSet();
                new SqlDataAdapter("SELECT * FROM Bestiaire_Beast", con).Fill(set, "Bete");
                int id = (int)set.Tables["Bete"].Rows[set.Tables["Bete"].Rows.Count - 1].ItemArray[0];
                int pv = int.Parse(Beast_end.Text) * 10, str = int.Parse(Beast_str.Text), fma = int.Parse(Beast_pwr.Text) * 10, stam = int.Parse(Beast_end.Text) * 12, pwr = int.Parse(Beast_pwr.Text), def = int.Parse(Beast_def.Text),
                    res = int.Parse(Beast_res.Text), dex = int.Parse(Beast_dex.Text), sag = int.Parse(Beast_wis.Text), inte = int.Parse(Beast_int.Text), cha = int.Parse(Beast_cha.Text), vit_s = int.Parse(Beast_vog.Text),
                    vit_e = int.Parse(Beast_vow.Text), vit_v = int.Parse(Beast_voa.Text);
                if (new SqlCommand("INSERT INTO Best_stats (Id_beast, pv, stamina, magie_fuel, force, puissance, defense, resistance, dexterite, sagesse, intelligence, charisme, vitesse_sol, vitesse_eau, vitesse_vol) " +
                    "VALUES (" + id + "," + pv + "," + stam + "," + fma + "," + str + "," + pwr + "," + def + "," + res + "," + dex + "," + sag + "," + inte + "," + cha + "," + vit_s + "," + vit_e + "," + vit_v + ")", con).ExecuteNonQuery() > 0)
                {
                    Beasto_Label.Content += "Stats de la Bete enregistrées.\nAjoute des maitrises des écoles magiques de la Bete\n";
                    if (new SqlCommand("INSERT INTO Beast_mago (Id_beast, alteration, assistance, chamanisme, creation, druidisme, elementaire, invocation, mentalisme, metamorphose, sapement, sceaux, spiritisme, soin)" +
                        " VALUES (" + Beast_mag_all.Text + "," + Beast_mag_ass.Text + "," + Beast_mag_cha.Text + "," + Beast_mag_cre.Text + "," + Beast_mag_dru.Text + "," + Beast_mag_elm.Text + "," + Beast_mag_inv.Text + "," + Beast_mag_men.Text +
                        "," + Beast_mag_met.Text + "," + Beast_mag_sap.Text + "," + Beast_mag_sce.Text + "," + Beast_mag_spi.Text + "," + Beast_mag_hea.Text + ")", con).ExecuteNonQuery() > 0)
                    {
                        Beasto_Label.Content += "Maitrises de la Bete ajoutées.\nAjout des maitrises elementaires de la Bete.\n";
                    }
                }
            }
        }
        #endregion Bestiaire
        private void UpdateWin()
        {
            switch (Page)
            {
                case Tabitem.Dictionnaire:
                    break;
                case Tabitem.Items:
                    try { }
                    catch { }
                    break;
                case Tabitem.Personnages:
                    try { }
                    catch { }
                    break;
                case Tabitem.Bestiaire:
                    try { BeastUpdate(); }
                    catch { }
                    break;
                case Tabitem.Enchant:
                    break;
                case Tabitem.Spells:
                    try { }
                    catch { }
                    break;
                case Tabitem.The_World:
                    try { }
                    catch { }
                    break;
                case Tabitem.Dico_commun:
                    break;
                case Tabitem.Dico_human:
                    break;
                case Tabitem.Dico_aqua:
                    break;
                case Tabitem.Dico_avien:
                    break;
                case Tabitem.Dico_terre:
                    break;
                default:
                    break;
            }
        }

        private void Generation(object sender, RoutedEventArgs e)
        {
            switch (Page)
            {
                case Tabitem.Dictionnaire:
                    break;
                case Tabitem.Items:
                    break;
                case Tabitem.Personnages:
                    break;
                case Tabitem.Bestiaire:
                    Beast_name.Text = Gen.Generation_gn_Sons(BeastGen.Value, BeastGen.Word, BeastGen.Before, BeastGen.Triphtongue, BeastGen.Symbol);
                    break;
                case Tabitem.Enchant:
                    break;
                case Tabitem.Spells:
                    break;
                case Tabitem.The_World:
                    break;
                case Tabitem.Dico_commun:
                    break;
                case Tabitem.Dico_human:
                    break;
                case Tabitem.Dico_aqua:
                    break;
                case Tabitem.Dico_avien:
                    break;
                case Tabitem.Dico_terre:
                    break;
                default:
                    break;
            }
        }

        private void Ad_sel(object sender, RoutedEventArgs e)
        {
            NameGen.adjectif = true;
            NameGen.monde = false;
            NameGen.mots = false;
            NameGen.objets = false;
            NameGen.verbes = false;
            UpdateWin();
        }
        private void Mnd_sel(object sender, RoutedEventArgs e)
        {
            NameGen.adjectif = false;
            NameGen.monde = true;
            NameGen.mots = false;
            NameGen.objets = false;
            NameGen.verbes = false;
            UpdateWin();
        }
        private void Mot_sel(object sender, RoutedEventArgs e)
        {
            NameGen.adjectif = false;
            NameGen.monde = false;
            NameGen.mots = true;
            NameGen.objets = false;
            NameGen.verbes = false;
            UpdateWin();
        }
        private void O_sel(object sender, RoutedEventArgs e)
        {
            NameGen.adjectif = false;
            NameGen.monde = false;
            NameGen.mots = false;
            NameGen.objets = true;
            NameGen.verbes = false;
            UpdateWin();
        }
        private void V_sel(object sender, RoutedEventArgs e)
        {
            NameGen.adjectif = false;
            NameGen.monde = false;
            NameGen.mots = false;
            NameGen.objets = false;
            NameGen.verbes = true;
            UpdateWin();
        }
        private void Void_sel(object sender, RoutedEventArgs e)
        {
            NameGen.adjectif = false;
            NameGen.monde = false;
            NameGen.mots = false;
            NameGen.objets = false;
            NameGen.verbes = false;
            UpdateWin();
        }

        private void new_elem(object sender, RoutedEventArgs e)
        {
            switch (Page)
            {
                case Tabitem.Dictionnaire:
                    break;
                case Tabitem.Items:
                    break;
                case Tabitem.Personnages:
                    break;
                case Tabitem.Bestiaire:
                    if (sender == new_catbut) AddBeast_cat();
                    else if (sender == newbehav) AddBehav();
                    break;
                case Tabitem.Enchant:
                    break;
                case Tabitem.Spells:
                    break;
                case Tabitem.The_World:
                    break;
                case Tabitem.Dico_commun:
                    break;
                case Tabitem.Dico_human:
                    break;
                case Tabitem.Dico_aqua:
                    break;
                case Tabitem.Dico_avien:
                    break;
                case Tabitem.Dico_terre:
                    break;
                default:
                    break;
            }
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            switch (Page)
            {
                case Tabitem.Dictionnaire:
                    break;
                case Tabitem.Items:

                    break;
                case Tabitem.Personnages:
                    break;
                case Tabitem.Bestiaire:
                    break;
                case Tabitem.Enchant:
                    break;
                case Tabitem.Spells:
                    break;
                case Tabitem.The_World:
                    break;
                case Tabitem.Dico_commun:
                    break;
                case Tabitem.Dico_human:
                    break;
                case Tabitem.Dico_aqua:
                    break;
                case Tabitem.Dico_avien:
                    break;
                case Tabitem.Dico_terre:
                    break;
                default:
                    break;
            }
        }
        private void Display(object sender, RoutedEventArgs e)
        {
            switch (Page)
            {
                case Tabitem.Dictionnaire:
                    break;
                case Tabitem.Items:
                    break;
                case Tabitem.Personnages:
                    break;
                case Tabitem.Bestiaire:
                    break;
                case Tabitem.Enchant:
                    break;
                case Tabitem.Spells:
                    break;
                case Tabitem.The_World:
                    break;
                case Tabitem.Dico_commun:
                    break;
                case Tabitem.Dico_human:
                    break;
                case Tabitem.Dico_aqua:
                    break;
                case Tabitem.Dico_avien:
                    break;
                case Tabitem.Dico_terre:
                    break;
                default:
                    break;
            }
        }


        private void Regle_Click(object sender, RoutedEventArgs e)
        {
            switch (Page)
            {
                case Tabitem.Dictionnaire:
                    break;
                case Tabitem.Items:
                    break;
                case Tabitem.Personnages:
                    break;
                case Tabitem.Bestiaire:
                    break;
                case Tabitem.Enchant:
                    break;
                case Tabitem.Spells:
                    break;
                case Tabitem.The_World:
                    break;
                case Tabitem.Dico_commun:
                    break;
                case Tabitem.Dico_human:
                    break;
                case Tabitem.Dico_aqua:
                    break;
                case Tabitem.Dico_avien:
                    break;
                case Tabitem.Dico_terre:
                    break;
                default:
                    break;
            }
        }

        private void Selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Persos.GetType();
            if (Selection.SelectedItem == Dico_Jahr) Page = Tabitem.Dictionnaire;
            else if (Selection.SelectedItem == Items) Page = Tabitem.Items;
            else if (Selection.SelectedItem == Personnages) Page = Tabitem.Personnages;
            else if (Selection.SelectedItem == Bestiaire) Page = Tabitem.Bestiaire;
            else if (Selection.SelectedItem == Enchantements) Page = Tabitem.Enchant;
            else if (Selection.SelectedItem == Sorts) Page = Tabitem.Spells;
            else if (Selection.SelectedItem == Monde) Page = Tabitem.The_World;
            else if (Selection.SelectedItem == Panth) Page = Tabitem.Pantheon;
            else if (Selection.SelectedItem == Dico_Aqua) Page = Tabitem.Dico_aqua;
            else if (Selection.SelectedItem == Dico_Av) Page = Tabitem.Dico_avien;
            else if (Selection.SelectedItem == Dico_comm) Page = Tabitem.Dico_commun;
            else if (Selection.SelectedItem == Dico_hum) Page = Tabitem.Dico_human;
            else if (Selection.SelectedItem == Dico_Ter) Page = Tabitem.Dico_terre;
            UpdateWin();
        }

        private void AjoutClick(object sender, RoutedEventArgs e)
        {
            switch (Page)
            {
                case Tabitem.Items:
                    break;
                case Tabitem.Personnages:
                    break;
                case Tabitem.Bestiaire:
                    if (sender == loot_add)
                    {
                        if (((bool)lootingcnso.IsChecked && loot_conso.SelectedIndex == 0) || ((bool)lootingloot.IsChecked) && Beast_lootlootable.SelectedIndex == 0)
                            Beasto_Label.Content = "Selectionnez un item a ajouter a la liste des loots de cette créature.";
                        else
                        {
                            if ((bool)lootingloot.IsChecked)
                                Beast_Lootlist.Items.Add(Beast_lootqty.Value + "," + Beast_lootlootable.SelectedItem + ":" + Beast_lootchc.Value);
                            else Beast_Lootlist.Items.Add(Beast_lootqty.Value + "," + loot_conso.SelectedItem + ":" + Beast_lootchc.Value);
                        }
                    }
                    break;
                case Tabitem.The_World:
                    break;
                case Tabitem.Pantheon:
                    break;
                default:
                    break;
            }
        }
        private void del_but_Click(object sender, RoutedEventArgs e)
        {
            switch (Page)
            {
                case Tabitem.Items:
                    break;
                case Tabitem.Personnages:
                    break;
                case Tabitem.Bestiaire:
                    if (sender == loot_del)
                    {
                        if (Beast_Lootlist.SelectedItems == null)
                            Beasto_Label.Content = "Veuillez selectionner le ou les loots à supprimer de la liste";
                        else
                        {
                            string[] remove = new string[Beast_Lootlist.SelectedItems.Count];
                            for (int i = 0; i < Beast_Lootlist.SelectedItems.Count; i++) remove[i] = (string)Beast_Lootlist.SelectedItems[i];
                            foreach (string str in remove) Beast_Lootlist.Items.Remove(str);
                        }
                    }
                    break;
                case Tabitem.Enchant:
                    break;
                case Tabitem.The_World:
                    break;
                case Tabitem.Pantheon:
                    break;
                default:
                    break;
            }
        }
        private void Selecion_with_all(object sender, SelectionChangedEventArgs e)
        {
            ListBox lys = (ListBox)sender;
            if ((string)lys.SelectedItem == "Tous" && lys.SelectedItems.Count != 1)
                lys.SelectedItems.RemoveAt(1);
            else if ((lys.SelectedItems.Contains("Tous") && lys.SelectedItems.Count > 2) || (lys.SelectedItems.Count == lys.Items.Count - 1 && !lys.SelectedItems.Contains("Tous")))
                lys.SelectedItem = "Tous";
        }

        private void Chiffre_check(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text != "")
                if (!"0123456789.".Contains(((TextBox)sender).Text[((TextBox)sender).Text.Length - 1]))
                    ((TextBox)sender).Text = ((TextBox)sender).Text.Remove(((TextBox)sender).Text.Length - 1);
        }

        private void Chiffre_minCheck(object sender, TextChangedEventArgs e)
        {
            String remp = "";
            if (((TextBox)sender).Text != "")
                if (!"0123456789.".Contains(((TextBox)sender).Text[((TextBox)sender).Text.Length - 1]))
                    remp = ((TextBox)sender).Text.Remove(((TextBox)sender).Text.Length - 1);
            ((TextBox)sender).Text = remp;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) { Bdd.Dispose(); }

        private void moreorless(object sender, RoutedEventArgs e)
        {
            if (sender == lessloot) Beast_lootqty.Value -= 1;
            else if (sender == muchlessloot) Beast_lootqty.Value -= 10;
            else if (sender == lesschances) Beast_lootchc.Value -= 1;
            else if (sender == muchlesschance) Beast_lootchc.Value -= 10;
            else if (sender == morechance) Beast_lootchc.Value += 1;
            else if (sender == muchmorechance) Beast_lootchc.Value += 10;
            else if (sender == moreloot) Beast_lootqty.Value += 1;
            else if (sender == muchmoreloot) Beast_lootqty.Value += 10;
        }

        private void lootingloot_Checked(object sender, RoutedEventArgs e)
        {
            Beast_lootlootable.IsEnabled = true;
            Beast_lootlootable.Visibility = Visibility.Visible;
            loot_conso.IsEnabled = loot_consotype.IsEnabled = false;
            loot_consotype.Visibility = loot_conso.Visibility = Visibility.Hidden;
        }

        private void lootingcnso_Checked(object sender, RoutedEventArgs e)
        {
            Beast_lootlootable.IsEnabled = false;
            Beast_lootlootable.Visibility = Visibility.Hidden;
            loot_conso.IsEnabled = loot_consotype.IsEnabled = true;
            loot_consotype.Visibility = loot_conso.Visibility = Visibility.Visible;

        }

        private void loot_consotype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loot_consotype.SelectedIndex != 0)
            {
                loot_conso.Items.Clear();
                loot_conso.Items.Add("~Consommables~");
                DataSet set = new DataSet();
                new SqlDataAdapter("SELECT nom FROM Items, Consommables WHERE Items.Id = Consommables.id_item AND type = '" + loot_consotype.SelectedItem + "'", con).Fill(set, "Conso");
                foreach (DataRow row in set.Tables["Conso"].Rows)
                    loot_conso.Items.Add(row.ItemArray[0]);
            }
        }

        private void isevol_Checked(object sender, RoutedEventArgs e) { evol_cat.IsEnabled = subevol.IsEnabled = true; }

        private void isevol_Unchecked(object sender, RoutedEventArgs e) { evol_cat.IsEnabled = subevol.IsEnabled = false; }

        private void evol_cat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (evol_cat.SelectedIndex != 0)
            {
                DataSet set = new DataSet();
                new SqlDataAdapter("SELECT Id, nom, cat FROM Beastiaire_Beast WHERE cat = '" + evol_cat.SelectedItem + "'", con).Fill(set, "Beast");
                foreach (DataRow row in set.Tables["Beasts"].Rows)
                    subevol.Items.Add(row.ItemArray[1] + ";" + row.ItemArray[2] + "," + row.ItemArray[0]);
            }
        }

        public void Dispose()
        {
            Bdd.Dispose();
        }

        private void openImage(object sender, RoutedEventArgs e)
        {
            AddImage im = new AddImage { Title = "Ajouter une image", Draws = true };
            im.ImageFor = Imaging.Regions;
            im.NameToPrint = "Lapatest";
            im.Draws = true;
            im.ShowDialog();
        }
    }
}
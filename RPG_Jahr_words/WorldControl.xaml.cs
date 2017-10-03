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
    /// Logique d'interaction pour WorldControl.xaml
    /// </summary>
    public partial class WorldControl : UserControl
    {
        public RPGEntities15 Bdd
        {
            get => (RPGEntities15)GetValue(BddProperty);
            set => SetValue(BddProperty, value);
        }
        public static readonly DependencyProperty BddProperty =
            DependencyProperty.Register("Bdd",
                typeof(RPGEntities15),
                typeof(WorldControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(ChargeFromDb)));

        private static void ChargeFromDb(DependencyObject d, DependencyPropertyChangedEventArgs e) { (d as WorldControl).DataContext = new ViewModel.WorldViewModel(e.NewValue as RPGEntities15); }
        public NameGen Gen
        {
            get => (NameGen)GetValue(GenProperty);
            set => SetValue(GenProperty, value);
        }
        public static readonly DependencyProperty GenProperty =
            DependencyProperty.Register("Gen",
                typeof(NameGen),
                typeof(WorldControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        private bool ignore = false;
        public WorldControl()
        {
            InitializeComponent();
            //DataContext = new ViewModel.WorldViewModel(Bdd);
            CollectionView biom = (CollectionView)CollectionViewSource.GetDefaultView(ShowThing.Items);
            CollectionView mats = (CollectionView)CollectionViewSource.GetDefaultView(Materials.Items);
            mats.Filter = FilterMinerals;
            biom.Filter = FilterBiomes;
        }

        private bool FilterBiomes(object obj)
        {
            if (World_Ville_conti.SelectedIndex != 0)
                if (Region.IsSelected)
                    return World_Ville_conti.SelectedItem != null && (World_Ville_conti.SelectedItem as Continent).biomes.Contains((obj as Biomes).nom);
                else if (Ville.IsSelected)
                    return (obj as Region).Continent1 == World_Ville_conti.SelectedItem;
            return Phenomene.IsSelected || Continent.IsSelected;
        }

        private void FilterCriterias(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null && DataContext is ViewModel.WorldViewModel)
                switch ((DataContext as ViewModel.WorldViewModel).Show.Type)
                {
                    case ViewModel.WorldType.None:
                        break;
                    case ViewModel.WorldType.Continent:
                        World_show_more.DisplayMemberPath = "nom";
                        if (World_show_OptCont.SelectedItem as string == "Origine")
                            World_show_more.ItemsSource = (DataContext as ViewModel.WorldViewModel).Worlds;
                        else if(World_show_OptCont.SelectedIndex!=0)
                            World_show_more.ItemsSource = Bdd.Continent.ToList();
                        break;
                    case ViewModel.WorldType.Ville:
                        switch (World_show_OptVille.SelectedItem as string)
                        {
                            case "Peuplade":
                                World_show_more.DisplayMemberPath = "nom";
                                World_show_more.ItemsSource = Bdd.Peuplade.ToList();
                                break;
                            case "Type":
                                World_show_more.DisplayMemberPath = "type";
                                World_show_more.ItemsSource = Bdd.Ville_type.ToList();
                                break;
                            case "Region":
                                World_show_more.DisplayMemberPath = "nom";
                                World_show_more.ItemsSource = Bdd.Region.ToList();
                                break;
                            default:
                                World_show_more.ItemsSource = null;
                                break;
                        }
                        break;
                    case ViewModel.WorldType.Region:
                        World_show_more.DisplayMemberPath = "nom";
                        World_show_more.ItemsSource = Bdd.Region.ToList();
                        break;
                    case ViewModel.WorldType.Mineraux:
                        switch (World_show_OptMiner.SelectedItem as string)
                        {
                            case "Type":
                                World_show_more.DisplayMemberPath = "type";
                                World_show_more.ItemsSource = Bdd.Minerai_type.ToList();
                                break;
                            case "Utilisation":
                                World_show_more.DisplayMemberPath = "utilisation";
                                World_show_more.ItemsSource = Bdd.Usage.ToList();
                                break;
                            case "Origine":
                                World_show_more.DisplayMemberPath = "nom";
                                World_show_more.ItemsSource = (DataContext as ViewModel.WorldViewModel).Worlds;
                                break;
                            default:
                                World_show_more.ItemsSource = null;
                                break;
                        }
                        break;
                    case ViewModel.WorldType.Alliage:
                        switch (World_show_OptAlliage.SelectedItem as string)
                        {
                            case "Procede":
                                World_show_more.DisplayMemberPath = "";
                                World_show_more.ItemsSource = new List<string>() { "Alchimie", "Forge" };
                                break;
                            case "Materiaux":
                                World_show_more.DisplayMemberPath = "nom";
                                World_show_more.ItemsSource = (DataContext as ViewModel.WorldViewModel).Materiau;
                                break;
                            default:
                                World_show_more.ItemsSource = null;
                                break;
                        }
                        break;
                    case ViewModel.WorldType.Phenomene:
                                World_show_more.DisplayMemberPath = "nom";
                        switch (World_show_OptVille.SelectedItem as string)
                        {
                            case "Region":
                                World_show_more.ItemsSource = Bdd.Region.ToList();
                                break;
                            case "Biomes":
                                World_show_more.ItemsSource = Bdd.Biomes.ToList(); ;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
        }

        private bool FilterMinerals(object obj)
        {
            if (Mat_type.SelectedIndex > 0)
            {
                return (obj as Items).Mineraux.Minerai_type == Mat_type.SelectedItem;
            }
            return true;
        }

        private void Generation(object sender, RoutedEventArgs e) { World_name.Text = Gen.Generation_gn_Sons(World.Value, World.Word, (bool)World.Before, World.Triphtongue, World.Symbol); }

        private void Selecion_with_all(object sender, SelectionChangedEventArgs e)
        {
            ListBox lys = (ListBox)sender;
            if ((string)lys.SelectedItem == "Tous" && lys.SelectedItems.Count != 1)
                lys.SelectedItems.RemoveAt(1);
            else if ((lys.SelectedItems.Contains("Tous") && lys.SelectedItems.Count > 2) || (lys.SelectedItems.Count == lys.Items.Count - 1 && !lys.SelectedItems.Contains("Tous")))
                lys.SelectedItem = "Tous";
        }

        private void Display(object sender, RoutedEventArgs e)
        {
            switch ((DataContext as ViewModel.WorldViewModel).Show.Type)
            {
                case ViewModel.WorldType.None:
                    break;
                case ViewModel.WorldType.Continent:
                    switch (World_show_OptCont.SelectedItem as string)
                    {
                        case "Origine":
                            foreach (Continent showCont in Bdd.Continent.Where(c=>c.monde == World_show_more.SelectedItem as string))
                                Worldo_Label.Text += showCont.nom + " du " + showCont.monde + ".\nBiomes : " + showCont.biomes + "\n";
                            break;
                        case "Biomes":
                            Worldo_Label.Text += (World_show_more.SelectedItem as Continent).biomes + '\n';
                            break;
                        case "Villes":
                            foreach (Region showReg in (World_show_more.SelectedItem as Continent).Region)
                                foreach (Ville showvill in showReg.Ville)
                                    Worldo_Label.Text += showvill.nom + "\n";
                            break;
                        case "Regions":
                            foreach (Region showReg in (World_show_more.SelectedItem as Continent).Region)
                                Worldo_Label.Text += showReg.nom + "\n";
                            break;
                                default:
                            foreach (Continent showCont in Bdd.Continent)
                                Worldo_Label.Text += showCont.nom + " du " + showCont.monde + ".\nBiomes : " + showCont.biomes + "\n";
                            break;
                    }
                    break;
                case ViewModel.WorldType.Ville:
                    switch (World_show_OptVille.SelectedItem as string)
                    {
                        case "Peuplade":
                            foreach (Ville showville in Bdd.Ville.Where(v => v.peuplade_principales.Contains((World_show_more.SelectedItem as Peuplade).nom)))
                                Worldo_Label.Text += showville.nom + '\n';
                            break;
                        case "Type":
                            foreach (Ville showville in Bdd.Ville.Where(v => v.Ville_type == ((World_show_more.SelectedItem as Ville_type))))
                                Worldo_Label.Text += showville.nom + '\n';
                            break;
                        case "Region":
                            foreach (Ville showville in Bdd.Ville.Where(v => v.peuplade_principales.Contains((World_show_more.SelectedItem as Peuplade).nom)))
                                Worldo_Label.Text += showville.nom + '\n';
                            break;
                        default:
                            break;
                    }
                    break;
                case ViewModel.WorldType.Region:
                    if (World_show_more.SelectedItem != null)
                        Worldo_Label.Text += (World_show_more.SelectedItem as Region).biomes + '\n';
                    break;
                case ViewModel.WorldType.Mineraux:
                        switch (World_show_OptMiner.SelectedItem as string)
                        {
                            case "Type":
                            foreach (Mineraux min in Bdd.Mineraux.Where(m => m.Minerai_type == World_show_more.SelectedItem as Minerai_type))
                                Worldo_Label.Text += min.Items.nom + '\n'; 
                                break;
                            case "Utilisation":
                                foreach(Mineraux min in Bdd.Mineraux.Where(m=>m.usage.Contains((World_show_more.SelectedItem as Usage).utilisation)))
                                Worldo_Label.Text += min.Items.nom + '\n';
                            break;
                            case "Origine":
                               foreach(Mineraux min in Bdd.Mineraux.Where(m=>m.Items.nom == (World_show_more.SelectedItem as Monde_w).nom))
                                Worldo_Label.Text += min.Items.nom + '\n';
                            break;
                            default:
                            foreach (Mineraux min in Bdd.Mineraux)
                            Worldo_Label.Text += min.Items.nom + '\n';
                            break;
                        }
                    break;
                case ViewModel.WorldType.Alliage:
                        switch (World_show_OptAlliage.SelectedItem as string)
                        {
                            case "Procede":
                               foreach(Alliage all in Bdd.Alliage.Where(a=>a.Items.obtention.Contains((World_show_more.SelectedItem as Procede).process)))
                                Worldo_Label.Text += all.Items.nom+'\n';
                                break;
                            case "Materiaux":
                               foreach(Alliage all in Bdd.Alliage.Where(a=>a.Items.recette.Contains((World_show_more.SelectedItem  as Items).nom)))
                                Worldo_Label.Text += all.Items.nom + '\n';
                            break;
                            default:
                            foreach (Alliage all in Bdd.Alliage)
                                Worldo_Label.Text += all.Items.nom + '\n';
                            break;
                        }
                    break;
                case ViewModel.WorldType.Phenomene:
                        switch (World_show_OptVille.SelectedItem as string)
                        {
                            case "Region":
                            foreach (Phenomene phen in Bdd.Phenomene.Where(p => p.location.Contains((World_show_more.SelectedItem as Region).nom)))
                                Worldo_Label.Text += phen.nom + '\n';
                                break;
                            case "Biomes":
                            foreach (Phenomene phen in Bdd.Phenomene.Where(p => p.location.Contains((World_show_more.SelectedItem as Biomes).nom)))
                                Worldo_Label.Text += phen.nom + '\n';
                            break;
                            default:
                            foreach (Phenomene phen in Bdd.Phenomene)
                                Worldo_Label.Text += phen.nom + '\n';
                            break;
                        }
                    break;
                default:
                    break;
            }
        }

        private void AjoutClick(object sender, RoutedEventArgs e)
        {
            if (Materials.SelectedItem != null && alliageqtity.Value > 0)
            {
                RecipeItem b = new RecipeItem
                {
                    N_recette = (int)recipeId.SelectedItem,
                    Id = (Materials.SelectedItem as Items).Id,
                    Nom = (Materials.SelectedItem as Items).nom,
                    Origine = (Materials.SelectedItem as Items).origine,
                    Quantite = alliageqtity.Value
                };
                (AlliageRecipe.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeItem>).Add(b);
                alliageqtity.Value = Mat_type.SelectedIndex = 0;
                Materials.SelectedItem = null;
                AlliageRecipe.ItemsSource = new System.Collections.ObjectModel.ObservableCollection<RecipeItem>((AlliageRecipe.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeItem>).OrderBy(r => r.N_recette));
            }
            else Worldo_Label.Text += "Veuillez selectionner un composant.\n";
        }
        private void Del_but_Click(object sender, RoutedEventArgs e)
        {
            if (AlliageRecipe.SelectedItems != null)
            {
                List<object> remove = AlliageRecipe.SelectedItems as List<object>;
                while (AlliageRecipe.SelectedItems.Count > 0)
                    (AlliageRecipe.ItemsSource as System.Collections.ObjectModel.ObservableCollection<RecipeItem>).Remove(AlliageRecipe.SelectedItems[0] as RecipeItem);
            }
            else Worldo_Label.Text += "Veuillez choisir au moins une entrée à supprimer de la liste.\n";
        }

        private void RefreshMaterials(object sender, SelectionChangedEventArgs e)
        {
            if (Materials != null)
                (DataContext as ViewModel.WorldViewModel).Materiau = new System.Collections.ObjectModel.ObservableCollection<Items>((DataContext as ViewModel.WorldViewModel).Materiau);
        }

        private void PlaceHolder(object sender, SelectionChangedEventArgs e) { if (((ComboBox)sender).SelectedItem == null) ((ComboBox)sender).SelectedIndex = 0; }

        private void RefreshCrit(object sender, SelectionChangedEventArgs e)
        {
            if (Materials != null && DataContext != null)
                (DataContext as ViewModel.WorldViewModel).Materiau = new System.Collections.ObjectModel.ObservableCollection<Items>((DataContext as ViewModel.WorldViewModel).Materiau);
        }

        private void Refreshing(object sender, SelectionChangedEventArgs e)
        {
            PlaceHolder(sender, e);
            if (Region.IsSelected)
                (DataContext as ViewModel.WorldViewModel).Bioming = new ObservableCollection<Biomes>((DataContext as ViewModel.WorldViewModel).Bioming);
            else if (Ville.IsSelected)
                (DataContext as ViewModel.WorldViewModel).ShowReg = new ObservableCollection<Region>((DataContext as ViewModel.WorldViewModel).ShowReg);
            else if (Phenomene.IsSelected)
                (DataContext as ViewModel.WorldViewModel).PhenomCrit = new List<object>((DataContext as ViewModel.WorldViewModel).PhenomCrit);
            else if (Continent.IsSelected)
                (DataContext as ViewModel.WorldViewModel).Biome = new List<Biomes>((DataContext as ViewModel.WorldViewModel).Biome);
        }

        private void PlaceHolderElm(object sender, SelectionChangedEventArgs e)
        {
            PlaceHolder(sender, e);
            if (WorldElemWrite.which != null)
                switch (WorldElemWrite.which.Type)
                {
                    case ViewModel.WorldType.None:
                        break;
                    case ViewModel.WorldType.Continent:
                        ignore = true;
                        ShowThing.SelectedItems.Clear();
                        foreach (Biomes biome in (DataContext as ViewModel.WorldViewModel).SelectedBiomes)
                        { ignore = true; ShowThing.SelectedItems.Add(biome); }
                        break;
                    case ViewModel.WorldType.Ville:
                        ShowThing.SelectedItem = (DataContext as ViewModel.WorldViewModel).SaveTown.Region1;
                        if ((DataContext as ViewModel.WorldViewModel).SaveTown.peuplade_principales != null)
                            foreach (string str in (DataContext as ViewModel.WorldViewModel).SaveTown.peuplade_principales.Split('\n'))
                                Ville_pop.SelectedItems.Add(Bdd.Peuplade.ToList().Find(p => p.nom == str));
                        break;
                    case ViewModel.WorldType.Region:
                        ignore = true;
                        ShowThing.SelectedItems.Clear();
                        foreach (Biomes biome in (DataContext as ViewModel.WorldViewModel).SelectedRegBiomes)
                        { ignore = true; ShowThing.SelectedItems.Add(biome); }
                        break;
                    case ViewModel.WorldType.Mineraux:
                        if ((DataContext as ViewModel.WorldViewModel).MinerSave.usage != null)
                            foreach (string str in (DataContext as ViewModel.WorldViewModel).MinerSave.usage.Split('\n'))
                                Ville_pop.SelectedItems.Add(Bdd.Usage.ToList().Find(p => p.utilisation == str));
                        break;
                    case ViewModel.WorldType.Alliage:
                        break;
                    case ViewModel.WorldType.Phenomene:
                        ignore = true;
                        ShowThing.SelectedItems.Clear();
                        foreach (object biome in (DataContext as ViewModel.WorldViewModel).SelectedPhenomLoc)
                        { ignore = true; ShowThing.SelectedItems.Add(biome); }
                        break;
                    default:
                        break;
                }
        }

        private void Showthingselection(object sender, SelectionChangedEventArgs e)
        {
            if (!ignore)
                switch (WorldElemWrite.which.Type)
                {
                    case ViewModel.WorldType.None:
                        break;
                    case ViewModel.WorldType.Continent:
                        if ((sender as ListBox).SelectedItems != null)
                        {
                            if ((DataContext as ViewModel.WorldViewModel).SaveConti.biomes == null) (DataContext as ViewModel.WorldViewModel).SaveConti.biomes = "";
                            if ((DataContext as ViewModel.WorldViewModel).SelectedBiomes.Count > (sender as ListBox).SelectedItems.Count)
                            {
                                (DataContext as ViewModel.WorldViewModel).SelectedBiomes.Clear();
                                (DataContext as ViewModel.WorldViewModel).SaveConti.biomes = "";
                            }
                            foreach (Biomes bi in (sender as ListBox).SelectedItems)
                                if (!(DataContext as ViewModel.WorldViewModel).SelectedBiomes.Contains(bi))
                                {
                                    (DataContext as ViewModel.WorldViewModel).SaveConti.biomes += bi.nom + '\n';
                                    (DataContext as ViewModel.WorldViewModel).SelectedBiomes.Add(bi);
                                }
                        }
                        break;
                    case ViewModel.WorldType.Ville:
                        if ((sender as ListBox).SelectedItem != null)
                        {
                            (DataContext as ViewModel.WorldViewModel).SaveTown.Region1 = (sender as ListBox).SelectedItem as Region;
                            (DataContext as ViewModel.WorldViewModel).SaveTown.region = ((sender as ListBox).SelectedItem as Region).nom;
                        }
                        break;
                    case ViewModel.WorldType.Region:
                        if ((sender as ListBox).SelectedItems != null)
                        {
                            if ((DataContext as ViewModel.WorldViewModel).SaveRegion.biomes == null) (DataContext as ViewModel.WorldViewModel).SaveRegion.biomes = "";
                            if ((DataContext as ViewModel.WorldViewModel).SelectedRegBiomes.Count > (sender as ListBox).SelectedItems.Count)
                            {
                                (DataContext as ViewModel.WorldViewModel).SelectedRegBiomes.Clear();
                                (DataContext as ViewModel.WorldViewModel).SaveRegion.biomes = "";
                            }
                            foreach (Biomes biome in (sender as ListBox).SelectedItems)
                                if (!(DataContext as ViewModel.WorldViewModel).SelectedRegBiomes.Contains(biome))
                                {
                                    (DataContext as ViewModel.WorldViewModel).SaveRegion.biomes += biome.nom + '\n';
                                    (DataContext as ViewModel.WorldViewModel).SelectedRegBiomes.Add(biome);
                                }
                        }
                        break;
                    case ViewModel.WorldType.Mineraux:
                        break;
                    case ViewModel.WorldType.Alliage:
                        break;
                    case ViewModel.WorldType.Phenomene:
                        if ((sender as ListBox).SelectedItems != null)
                        {
                            if ((DataContext as ViewModel.WorldViewModel).SavePhenom.location == null) (DataContext as ViewModel.WorldViewModel).SavePhenom.location = "";
                            if ((DataContext as ViewModel.WorldViewModel).SelectedPhenomLoc.Count > (sender as ListBox).SelectedItems.Count)
                            {
                                (DataContext as ViewModel.WorldViewModel).SelectedPhenomLoc.Clear();
                                (DataContext as ViewModel.WorldViewModel).SavePhenom.location = "";
                            }
                            foreach (object obj in (sender as ListBox).SelectedItems)
                                if (!(DataContext as ViewModel.WorldViewModel).SelectedPhenomLoc.Contains(obj))
                                {
                                    (DataContext as ViewModel.WorldViewModel).SelectedPhenomLoc.Add(obj);
                                    (DataContext as ViewModel.WorldViewModel).SavePhenom.location += obj.GetType().GetProperty("nom").GetValue(obj);
                                    (DataContext as ViewModel.WorldViewModel).SavePhenom.location += '\n';
                                }
                        }
                        break;
                    default:
                        break;
                }
            ignore = false;
        }

        private void UsagePop(object sender, SelectionChangedEventArgs e)
        {
            if (Ville.IsSelected)
            {
                if ((DataContext as ViewModel.WorldViewModel).SaveTown.peuplade_principales == null) (DataContext as ViewModel.WorldViewModel).SaveTown.peuplade_principales = "";
                if (e.AddedItems.Count > 0)
                {
                    if ((e.AddedItems[0] as Peuplade).nom != "Tous")
                        (DataContext as ViewModel.WorldViewModel).SaveTown.peuplade_principales += (e.AddedItems[0] as Peuplade).nom + "\n";
                    else if ((e.AddedItems[0] as Peuplade).nom == "Tous" && !ignore)
                    {
                        (sender as ListBox).UnselectAll();
                        (DataContext as ViewModel.WorldViewModel).SaveTown.peuplade_principales = "Tous";
                        ignore = true;
                        (sender as ListBox).SelectedItems.Add(e.AddedItems[0]);
                    }
                    else ignore = false;
                    if ((DataContext as ViewModel.WorldViewModel).SaveTown.peuplade_principales.Contains("Tous") && (sender as ListBox).SelectedItems.Count > 1) (sender as ListBox).SelectedItems.Remove(e.AddedItems[0]);

                }
                if (e.RemovedItems.Count > 0 && e.RemovedItems[0] is Peuplade)
                {
                    foreach (Peuplade people in e.RemovedItems)
                        (DataContext as ViewModel.WorldViewModel).SaveTown.peuplade_principales = (DataContext as ViewModel.WorldViewModel).SaveTown.peuplade_principales.Replace(people.nom + ((DataContext as ViewModel.WorldViewModel).SaveTown.peuplade_principales.Contains("\n") ? "\n" : ""), "");
                }
            }
            else
            {
                if ((DataContext as ViewModel.WorldViewModel).MinerSave.usage == null) (DataContext as ViewModel.WorldViewModel).MinerSave.usage = "";
                if (e.AddedItems.Count > 0)
                {
                    if ((e.AddedItems[0] as Usage).utilisation != "Tous")
                        (DataContext as ViewModel.WorldViewModel).MinerSave.usage += (e.AddedItems[0] as Usage).utilisation + "\n";
                    else if ((e.AddedItems[0] as Usage).utilisation == "Tous" && !ignore)
                    {
                        (sender as ListBox).UnselectAll();
                        (DataContext as ViewModel.WorldViewModel).MinerSave.usage = "Tous";
                        ignore = true;
                        (sender as ListBox).SelectedItems.Add(e.AddedItems[0]);
                    }
                    else ignore = false;
                    if ((DataContext as ViewModel.WorldViewModel).MinerSave.usage.Contains("Tous") && (sender as ListBox).SelectedItems.Count > 1) (sender as ListBox).SelectedItems.Remove(e.AddedItems[0]);

                }
                if (e.RemovedItems.Count > 0 && e.RemovedItems[0] is Usage && (sender as ListBox).Items.Count>0)
                {
                    foreach (Usage use in e.RemovedItems)
                        (DataContext as ViewModel.WorldViewModel).MinerSave.usage = (DataContext as ViewModel.WorldViewModel).MinerSave.usage.Replace(use.utilisation + ((DataContext as ViewModel.WorldViewModel).MinerSave.usage.Contains("\n") ? "\n" : ""), "");
                }
            }
        }

        private void FromFor_Checked(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModel.WorldViewModel).SaveAll.obtention += "Forge\n";
        }

        private void FromFor_Unchecked(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModel.WorldViewModel).SaveAll.obtention.Replace("Forge\n", "");
        }

        private void FromAlc_Checked(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModel.WorldViewModel).SaveAll.obtention += "Alchimie\n";
        }

        private void FromAlc_Unchecked(object sender, RoutedEventArgs e)        {            (DataContext as ViewModel.WorldViewModel).SaveAll.obtention.Replace("Forge\n","");        }
    }
}

using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.Windows.Data;

namespace RPG_Jahr_words.ViewModel
{
    public enum WorldType
    {
        None,
        Continent,
        Ville,
        Region,
        Mineraux,
        Alliage,
        Phenomene
    }

    public class WorldWrap
    {
        private WorldType _type;

        public WorldType Type { get => _type; set => _type = value; }
    }

    class WorldViewModel : ViewModelBase
    {
        private RPGEntities15 _bd;

        private WorldWrap _elem = new WorldWrap(), _show = new WorldWrap();
        private Continent _saveConti = new Continent(), _selected;
        private Ville _saveTown = new Ville();
        private Region _saveRegion = new Region();
        private Items _saveMiner = new Items(), _saveAll = new Items();
        private Mineraux _minerSave = new Mineraux();
        private Phenomene _savePhenom = new Phenomene();
        private string _printedText = "", _alcManacost;

        private List<Usage> _uses;
        private List<Ville_type> _townTypes;
        private List<Minerai_type> _minerTypes;
        private List<Peuplade> _people;
        private List<Biomes> _biome;
        private ObservableCollection<Biomes> _bioming;
        private List<Continent> _continents;
        private List<Ville> _villes;
        private List<Region> _regions;
        private ObservableCollection<Region> _showReg;
        private List<object> _phenomCrit = new List<object>();

        private ObservableCollection<Items> _materiau;
        private ObservableCollection<RecipeItem> _recipe = new ObservableCollection<RecipeItem>();
        private ObservableCollection<RecipeResult> _results = new ObservableCollection<RecipeResult>() { new RecipeResult { IdRecipe = 1, Nombre = 1 } };
        private List<Monde_w> _worlds;
        private List<int> _recipeCount = new List<int>() { 1 };

        private RelayCommand _newUse, _newBiome, _newMine;
        private RelayCommand _save, _moreRecipe;
        private List<Biomes> _selectedBiomes = new List<Biomes>(), _selectedRegBiomes = new List<Biomes>();
        private List<object> _selectedPhenomLoc = new List<object>();
        private List<Peuplade> _selectedPeople = new List<Peuplade>();
        private List<Usage> _selectedUsages = new List<Usage>();

        private bool _buyable, _lootable;
        public WorldViewModel(RPGEntities15 context)
        {
            Bd = context;
            Uses = Bd.Usage.ToList();
            TownTypes = Bd.Ville_type.ToList();
            MinerTypes = Bd.Minerai_type.ToList();
            People = Bd.Peuplade.ToList();
            Bioming = new ObservableCollection<Biomes>(Bd.Biomes.ToList());
            Biome = Bd.Biomes.ToList();
            Continents = Bd.Continent.ToList();
            Villes = Bd.Ville.ToList();
            ShowReg = new ObservableCollection<Region>(Bd.Region.ToList());
            Regions = Bd.Region.ToList();
            Worlds = Bd.Monde_w.ToList();
            Materiau = new ObservableCollection<Items>(Bd.Items.Where(i => i.Mineraux != null));
            foreach (Biomes bi in Biome)
                PhenomCrit.Add(bi);
            foreach (Region reg in Regions)
                PhenomCrit.Add(reg);
            WorldElemWrite.which = Elem;
        }

        public RPGEntities15 Bd { get => _bd; set { _bd = value; RaisePropertyChanged(); } }
        public WorldWrap Elem { get => _elem; set { _elem = value; RaisePropertyChanged(); } }
        public Continent SaveConti { get => _saveConti; set { _saveConti = value; RaisePropertyChanged(); } }
        public Ville SaveTown { get => _saveTown; set { _saveTown = value; RaisePropertyChanged(); } }
        public Region SaveRegion { get => _saveRegion; set { _saveRegion = value; RaisePropertyChanged(); } }
        public Items SaveMiner { get => _saveMiner; set { _saveMiner = value; RaisePropertyChanged(); } }
        public Items SaveAll { get => _saveAll; set { _saveAll = value; RaisePropertyChanged(); } }
        public Phenomene SavePhenom { get => _savePhenom; set { _savePhenom = value; RaisePropertyChanged(); } }
        public string PrintedText { get => _printedText; set { _printedText = value; RaisePropertyChanged(); } }
        public List<Usage> Uses { get => _uses; set { _uses = value; RaisePropertyChanged(); } }
        public List<Ville_type> TownTypes { get => _townTypes; set { _townTypes = value; RaisePropertyChanged(); } }
        public List<Minerai_type> MinerTypes { get => _minerTypes; set { _minerTypes = value; RaisePropertyChanged(); } }
        public List<Peuplade> People { get => _people; set { _people = value; RaisePropertyChanged(); } }
        public List<Biomes> Biome { get => _biome; set { _biome = value; RaisePropertyChanged(); } }
        public List<Continent> Continents { get => _continents; set { _continents = value; RaisePropertyChanged(); } }
        public List<Ville> Villes { get => _villes; set { _villes = value; RaisePropertyChanged(); } }
        public List<Region> Regions { get => _regions; set { _regions = value; RaisePropertyChanged(); } }

        public ObservableCollection<Items> Materiau { get => _materiau; set { _materiau = value; RaisePropertyChanged(); } }
        public ObservableCollection<RecipeItem> Recipe { get => _recipe; set { _recipe = value; RaisePropertyChanged(); } }

        public ObservableCollection<RecipeResult> Results { get => _results; set { _results = value; RaisePropertyChanged(); } }

        public string AlcManacost { get => _alcManacost; set { _alcManacost = value; RaisePropertyChanged(); } }

        public WorldWrap Show { get => _show; set { _show = value; RaisePropertyChanged(); } }

        public List<Monde_w> Worlds { get => _worlds; set => _worlds = value; }
        public RelayCommand NewUse { get => _newUse ?? (_newUse = new RelayCommand(MakeUse)); }
        public event EventHandler BiomeAdded, ContinentAdded, MineAdded, MinerAdded, AllAdded;
        private void MakeUse()
        {
            AddSmthng fntre = new AddSmthng("Entrer votre nouvelle utilisation.\n" + "Les noms d'utilisation doivent commencer par des Majuscules.");
            fntre.ShowDialog();
            fntre.Title = "Nouvelle utilisation";
            if (fntre.Validate && fntre.Maj)
            {
                Bd.Usage.Add(new Usage() { utilisation = fntre.Nouveau });
                try
                {
                    Bd.SaveChanges();
                    Uses = Bd.Usage.ToList();
                    PrintedText += "Nouvelle utilisation " + fntre.Nouveau + " ajouté.\n";
                }
                catch { PrintedText += "utilisation déja existante.\n"; }
            }
            else if (!fntre.Maj)
                PrintedText += "Le nom des utilisations doit commencer par une majuscule.\n";
        }

        public RelayCommand NewBiome { get => _newBiome ?? (_newBiome = new RelayCommand(MakeBiome)); }

        private void MakeBiome()
        {
            AddSmthng fntre = new AddSmthng("Entrer votre nouveau Biome.\n" + "Les noms de Biomes doivent commencer par des Majuscules.");
            fntre.ShowDialog();
            fntre.Title = "Nouveau Biome";
            if (fntre.Validate && fntre.Maj)
            {
                Bd.Biomes.Add(new Biomes() { nom = fntre.Nouveau });
                try
                {
                    Bd.SaveChanges();
                    Biome = Bd.Biomes.ToList();
                    PhenomCrit.Add(Bd.Biomes.Last());
                    PrintedText += "Nouveau Biome " + fntre.Nouveau + " ajouté.\n";
                    BiomeAdded?.Invoke(this, EventArgs.Empty);
                }
                catch { PrintedText += "Biome deja existant.\n"; }
            }
            else if (!fntre.Maj)
                PrintedText += "Le nom du Biome doit commencer par une majuscule.\n";

        }

        public RelayCommand NewMine { get => _newMine ?? (_newMine = new RelayCommand(MakeMineType)); }
        public RelayCommand Save { get => _save ?? (_save = new RelayCommand(Saving)); }
        public List<int> RecipeCount { get => _recipeCount; set { _recipeCount = value; RaisePropertyChanged(); } }

        public RelayCommand MoreRecipe { get => _moreRecipe ?? (_moreRecipe = new RelayCommand(AddRecipe)); }
        public List<Biomes> SelectedBiomes { get => _selectedBiomes; set { _selectedBiomes = value; RaisePropertyChanged(); } }
        public List<Usage> SelectedUsages { get => _selectedUsages; set { _selectedUsages = value; RaisePropertyChanged(); } }
        public List<Peuplade> SelectedPeople { get => _selectedPeople; set { _selectedPeople = value; RaisePropertyChanged(); } }
        public ObservableCollection<Region> ShowReg { get => _showReg; set { _showReg = value; RaisePropertyChanged(); } }

        public List<object> PhenomCrit { get => _phenomCrit; set { _phenomCrit = value; RaisePropertyChanged(); } }
        public ObservableCollection<Biomes> Bioming { get => _bioming; set { _bioming = value; RaisePropertyChanged(); } }

        public List<Biomes> SelectedRegBiomes { get => _selectedRegBiomes; set => _selectedRegBiomes = value; }
        public List<object> SelectedPhenomLoc { get => _selectedPhenomLoc; set => _selectedPhenomLoc = value; }
        public Mineraux MinerSave { get => _minerSave; set { _minerSave = value; RaisePropertyChanged(); } }

        public Continent Selected { get => _selected; set { _selected = value; RaisePropertyChanged(); } }

        public bool Buyable { get => _buyable; set { _buyable = value; RaisePropertyChanged(); } }
        public bool Lootable { get => _lootable; set { _lootable = value; RaisePropertyChanged(); } }

        private void AddRecipe()
        {
            RecipeCount.Add(RecipeCount.Count + 1);
            RecipeResult nR = new RecipeResult() { IdRecipe = RecipeCount.Last(), Nombre = 1 };
            Results.Add(nR);
            RecipeCount = new List<int>(RecipeCount);
        }

        private void Saving()
        {
            try
            {
                switch (Elem.Type)
                {
                    case WorldType.None:
                        PrintedText += "Choississez un element à ajouter.\n";
                        break;
                    case WorldType.Continent:
                        PrintedText += "Nouveau continent créé.\nSauvegarde du continent.\n";
                        Bd.Continent.Add(SaveConti);
                        Bd.SaveChanges();
                        PrintedText += "Nouveau continent " + SaveConti.nom + " Ajouté.\n";
                        ContinentAdded?.Invoke(this, EventArgs.Empty);
                        SaveConti = new Continent();
                        Continents = Bd.Continent.ToList();
                        SelectedBiomes.Clear();
                        break;
                    case WorldType.Ville:
                        PrintedText += "Nouvelle ville créée.\nSauvegarde de la ville\n";
                        Bd.Ville.Add(SaveTown);
                        Bd.SaveChanges();
                        PrintedText += "Nouvelle ville " + SaveTown.nom + " Ajoutée.\n";
                        SaveTown = new Ville();
                        break;
                    case WorldType.Region:
                        PrintedText += "Nouvelle region créée.\nSauvegarde de la region\n";
                        Bd.Region.Add(SaveRegion);
                        Bd.SaveChanges();
                        PrintedText += "Nouvelle region " + SaveTown.nom + " Ajoutée.\n";
                        SaveRegion = new Region();
                        Regions = Bd.Region.ToList();
                        SelectedRegBiomes.Clear();
                        break;
                    case WorldType.Mineraux:
                        SaveMiner.storable = true;
                        if (Buyable)
                            SaveMiner.obtention += "\nAchat";
                        if (Lootable)
                            SaveMiner.obtention += "\nLoot";
                        SaveMiner.masse = 1;
                        MinerSave.Items = SaveMiner;
                        PrintedText += "Nouveau minerai créé.\nSauvegarde du minerai.\n";
                        Bd.Items.Add(SaveMiner);
                        Bd.Mineraux.Add(MinerSave);
                        Bd.SaveChanges();
                        PrintedText += "Nouveau minerai " + SaveMiner.nom + " Ajouté.\n";
                        MinerAdded?.Invoke(this, EventArgs.Empty);
                        SaveMiner = new Items();
                        MinerSave = new Mineraux();
                        Materiau = new ObservableCollection<Items>(Bd.Items.Where(i => i.Mineraux != null));
                        break;
                    case WorldType.Alliage:
                        SaveAll.storable = true;
                        SaveAll.masse = 1;
                        if (Buyable)
                            SaveMiner.obtention += "\nAchat";
                        if (Lootable)
                            SaveMiner.obtention += "\nLoot";
                        SaveAll.recette = "";
                        foreach (RecipeItem step in Recipe)
                        {
                            if (Recipe.First() == step)
                                SaveAll.recette += step.N_recette + "[" + step + '\n';
                            else if (Recipe.Last() == step)
                                SaveAll.recette += "" + step + "]=>" + Results.First(r => r.IdRecipe == step.N_recette);
                            else if (Recipe.IndexOf(step) != Recipe[Recipe.IndexOf(step) + 1].N_recette)
                                SaveAll.recette += "" + step + "]=>" + Results.First(r => r.IdRecipe == step.N_recette) + "\n" + (step.N_recette + 1) + "[";
                            else SaveAll.recette += "" + step + '\n';
                        }

                        PrintedText += "Nouvel alliage créé.\nSauvegarde du alliage.\n";
                        Bd.Items.Add(SaveMiner);
                        Bd.Alliage.Add(new Alliage { Items = SaveAll });
                        Bd.SaveChanges();
                        AllAdded?.Invoke(this, EventArgs.Empty);
                        PrintedText += "Nouvel alliage " + SaveAll.nom + " Ajouté.\n";
                        SaveAll = new Items();
                        break;
                    case WorldType.Phenomene:
                        PrintedText += "Nouveau phenomene créé.\nSauvegarde du phenomene.\n";
                        Bd.Phenomene.Add(SavePhenom);
                        Bd.SaveChanges();
                        PrintedText += "Nouveau phenomene " + SaveMiner.nom + " Ajouté.\n";
                        SaveMiner = new Items();
                        Materiau = new ObservableCollection<Items>(Bd.Items.Where(i => i.Mineraux != null));
                        break;
                    default:
                        break;
                }
            }
            catch { PrintedText += "Sauvegarde impossible, element non ajouté\n"; }
        }

        private void MakeMineType()
        {
            AddSmthng fntre = new AddSmthng("Entrer votre nouveau type de minerai.\n" + "Les noms de type de minerai doivent commencer par des Majuscules.");
            fntre.ShowDialog();
            fntre.Title = "Nouveau Type de minerai";
            if (fntre.Validate && fntre.Maj)
            {
                Bd.Minerai_type.Add(new Minerai_type() { type = fntre.Nouveau });
                try
                {
                    Bd.SaveChanges();
                    MinerTypes = Bd.Minerai_type.ToList();
                    MineAdded?.Invoke(this, EventArgs.Empty);
                    PrintedText += "Nouvelle utilisation " + fntre.Nouveau + " ajouté.\n";
                }
                catch { PrintedText += "utilisation déja existante.\n"; }
            }
            else if (!fntre.Maj)
                PrintedText += "Les types de minerais doivent commencer par une majuscule.\n";
        }
    }
}

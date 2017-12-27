using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
namespace RPG_Jahr_words.ViewModel
{
    class EnchantViewModel : ViewModelBase
    {
        private RPGEntities15 _bdd;
        private Enchantements _saveEnchant;
        private List<Enchant_Effets> _effects;
        private ObservableCollection<Enchant_Effets> _selectedEffects = new ObservableCollection<Enchant_Effets>();
        private List<Enchant_Type> _types;
        private List<Monde_w> _origines;
        private List<Armor_cat> _armorCats;
        private ObservableCollection<Armor_cat> _selectedCats = new System.Collections.ObjectModel.ObservableCollection<Armor_cat>();
        private List<Weapon_type> _cacTypes, _dstTypes, _magTypes;
        private ObservableCollection<Weapon_type> _selectedmag = new ObservableCollection<Weapon_type>(), _selecteddist = new ObservableCollection<Weapon_type>(), _selectedCac = new System.Collections.ObjectModel.ObservableCollection<Weapon_type>();
        private List<Piece> _armors;
        private ObservableCollection<Piece> _selectedArmors = new ObservableCollection<Piece>();
        private List<Bijoux_place> _bijoux;
        private ObservableCollection<Bijoux_place> _selectedJewels = new ObservableCollection<Bijoux_place>();
        private List<Items> _items;
        private ObservableCollection<RecipeItem> _requirements = new System.Collections.ObjectModel.ObservableCollection<RecipeItem>();
        private RelayCommand _makeType, _make_effet, _moreRecipe, _save, _showEffects, _showTypes;
        private List<int> _recipeCount = new List<int> { 1 };
        private string _printedText;
        public EnchantViewModel(RPGEntities15 Bd)
        {
            Bdd = Bd;
            SaveEnchant = new Enchantements();
            Effects = Bdd.Enchant_Effets.ToList();
            Types = Bdd.Enchant_Type.ToList();
            Items = Bdd.Items.Where(i => i.origine == "Tous" || i.origine == "Magocosme" || i.origine == "Originel").ToList();
            Armors = Bdd.Piece.ToList();
            Bijoux = Bdd.Bijoux_place.ToList();
            ArmorCats = Bdd.Armor_cat.ToList();
            Origines = Bdd.Monde_w.Where(m => m.nom != "Technocosme" && m.nom != "Tous").ToList();
            CacTypes = Bdd.Weapon_type.Where(c => c.categorie.Contains("CaC")).ToList();
            MagTypes = Bdd.Weapon_type.Where(m => m.categorie.Contains("Magique")).ToList();
            DstTypes = Bdd.Weapon_type.Where(d => d.categorie.Contains("Distance")).ToList();
        }

        private void AddRecipe()
        {
            RecipeCount.Add(RecipeCount.Count + 1);
            RecipeCount = new List<int>(RecipeCount);
        }

        public Enchantements SaveEnchant { get => _saveEnchant; set { _saveEnchant = value; RaisePropertyChanged(); } }
        public RPGEntities15 Bdd { get => _bdd; set { _bdd = value; RaisePropertyChanged(); } }
        public List<Enchant_Effets> Effects { get => _effects; set { _effects = value; RaisePropertyChanged(); } }
        public List<Enchant_Type> Types { get => _types; set { _types = value; RaisePropertyChanged(); } }
        public List<Items> Items { get => _items; set { _items = value; RaisePropertyChanged(); } }
        public System.Collections.ObjectModel.ObservableCollection<RecipeItem> Requirements { get => _requirements; set { _requirements = value; RaisePropertyChanged(); } }
        public RelayCommand MakeType { get => _makeType ?? (_makeType = new RelayCommand(MakeNewType)); }

        private void MakeNewType()
        {
            Add2Chps add = new Add2Chps("Ajouter un type d'enchantement.\nLe nom des types d'enchantement \ndoit commencer par une majuscule", false)
            {
                Title = "Nouveau type d'enchantement"
            };
            add.ShowDialog();
            if (add.Maj && add.Validate)
                try
                {
                    Enchant_Type proc = new Enchant_Type { type = add.Ch1, descr = add.Ch2 };
                    Bdd.Enchant_Type.Add(proc);
                    Bdd.SaveChanges();
                    TypeCreated?.Invoke(this, EventArgs.Empty);
                    Types = Bdd.Enchant_Type.ToList();
                    PrintedText += "Nouveau type d'enchantement " + add.Ch1 + " ajouté.\n";
                }
                catch
                {
                    PrintedText += "Nouveau type d'enchantement non ajouté\n";
                }
            else PrintedText += "Type d'enchantement non ajouté.\n";
        }

        public RelayCommand Make_effet { get => _make_effet ?? (_make_effet = new RelayCommand(MakeNewEffect)); }

        private void MakeNewEffect()
        {
            Add2Chps add = new Add2Chps("Ajouter un effet d'enchantement.\nLe nom des types d'enchantement \ndoit commencer par une majuscule", false)
            {
                Title = "Nouveau effet d'enchantement"
            };
            add.ShowDialog();
            if (add.Maj && add.Validate)
                try
                {
                    Enchant_Effets proc = new Enchant_Effets { effet = add.Ch1, descr = add.Ch2 };
                    Bdd.Enchant_Effets.Add(proc);
                    Bdd.SaveChanges();
                    EffecCreated?.Invoke(this, EventArgs.Empty);
                    Effects = Bdd.Enchant_Effets.ToList();
                    PrintedText += "Nouvel effet d'enchantement " + add.Ch1 + " ajouté.\n";
                }
                catch
                {
                    PrintedText += "Nouveau effet d'enchantement non ajouté\n";
                }
            else PrintedText += "Effet d'enchantement non ajouté.\n";
        }

        public List<int> RecipeCount { get => _recipeCount; set { _recipeCount = value; RaisePropertyChanged(); } }
        public RelayCommand MoreRecipe { get => _moreRecipe; set { _moreRecipe = value; RaisePropertyChanged(); } }
        public List<Weapon_type> CacTypes { get => _cacTypes; set { _cacTypes = value; RaisePropertyChanged(); } }
        public ObservableCollection<Weapon_type> SelectedCac { get => _selectedCac; set { _selectedCac = value; RaisePropertyChanged(); } }
        public List<Weapon_type> DstTypes { get => _dstTypes; set { _dstTypes = value; RaisePropertyChanged(); } }
        public ObservableCollection<Weapon_type> Selecteddist { get => _selecteddist; set { _selecteddist = value; RaisePropertyChanged(); } }
        public List<Weapon_type> MagTypes { get => _magTypes; set { _magTypes = value; RaisePropertyChanged(); } }
        public ObservableCollection<Weapon_type> Selectedmag { get => _selectedmag; set { _selectedmag = value; RaisePropertyChanged(); } }
        public List<Piece> Armors { get => _armors; set { _armors = value; RaisePropertyChanged(); } }
        public ObservableCollection<Piece> SelectedArmors { get => _selectedArmors; set { _selectedArmors = value; RaisePropertyChanged(); } }
        public List<Bijoux_place> Bijoux { get => _bijoux; set { _bijoux = value; RaisePropertyChanged(); } }
        public ObservableCollection<Bijoux_place> SelectedJewels { get => _selectedJewels; set { _selectedJewels = value; RaisePropertyChanged(); } }
        public string PrintedText { get => _printedText; set { _printedText = value; RaisePropertyChanged(); } }

        public List<Armor_cat> ArmorCats { get => _armorCats; set { _armorCats = value; RaisePropertyChanged(); } }

        public ObservableCollection<Armor_cat> SelectedCats { get => _selectedCats; set { _selectedCats = value; RaisePropertyChanged(); } }

        public List<Monde_w> Origines { get => _origines; set { _origines = value; RaisePropertyChanged(); } }

        public RelayCommand Save { get => _save ?? (_save = new RelayCommand(Saving)); }

        private void Saving()
        {
            PrintedText += "Sauvegarde du ouvel enchantement." +
                "\nCréation de l'enchantement.\n";
            if (SaveEnchant.unlockable)
                if (Requirements.Count > 0)
                    foreach (RecipeItem step in Requirements)
                    {
                        if (Requirements.First() == step)
                            SaveEnchant.requirements += step.N_recette + "[" + step + '\n';
                        else if (Requirements.Last() == step)
                            SaveEnchant.requirements += "" + step + "]";
                        else if (Requirements.IndexOf(step) != Requirements[Requirements.IndexOf(step) + 1].N_recette)
                            SaveEnchant.requirements += "" + step + "]";
                        else SaveEnchant.requirements += "" + step + '\n';
                    }
            if (SelectedEffects.Count == 0) { PrintedText += "Un Enchantement ne peut pas etre dépourvu d'effet.\n"; return; }
            else
                foreach (Enchant_Effets effet in SelectedEffects)
                    SaveEnchant.effects += effet.effet + (effet != SelectedEffects.Last() ? "\n" : "");
            PrintedText += "Effets d'enchantement ajoutés.\n";
            if (SaveEnchant.on_armor)
            {
                if (SelectedCats.Count == 0) { PrintedText += "Choisir au moins une categorie d'armure sur laquelle l'enchantement peut etre placé.\n"; return; }
                else
                {
                    foreach (Armor_cat categorie in SelectedCats)
                        SaveEnchant.armors_cats += categorie.categorie + (categorie != SelectedCats.Last() ? "\n" : "");
                    PrintedText += "Categories d'armure ajoutées.\n";
                }
                if (SelectedArmors.Count == 0 && SaveEnchant.armors_cats != "Exosquelette") { PrintedText += "Choisir au moins une piece d'armure sur laquelle l'enchantement est applicable.\n"; return; }
                else
                {
                    foreach (Piece piece in SelectedArmors)
                        SaveEnchant.armors += piece.emplacement + (piece != SelectedArmors.Last() ? "\n" : "");
                    PrintedText += "Pieces d'armures ajoutées.\n";
                }
            }
            if (SaveEnchant.on_jewel)
            {
                if (SelectedJewels.Count == 0) { PrintedText += "Choisir au moins un type de bijous sur lequel cet enchantement peut etre appliqué.\n"; return; }
                else
                {
                    foreach (Bijoux_place place in SelectedJewels)
                        SaveEnchant.jewels += place.place + (place != SelectedJewels.Last() ? "\n" : "");
                    PrintedText += "Bijoux ajoutés.\n";
                }
            }
            if (SaveEnchant.on_cac)
            {
                if (SelectedCac.Count == 0) { PrintedText += "Choisir au moins une arme de corps à corps sur laquelle cet enchantement peut etre appliqué.\n"; return; }
                else
                {
                    foreach (Weapon_type type in SelectedCac)
                        SaveEnchant.weapons_cac += type.type + (type != SelectedCac.Last() ? "\n" : "");
                    PrintedText += "Armes de corps à corps ajoutées.\n";
                }
            }
            if (SaveEnchant.on_dist)
            {
                if (Selecteddist.Count == 0) { PrintedText += "Choisir au moins une arme a distance sur laquelle l'enchantement est applicable.\n"; return; }
                else
                {
                    foreach (Weapon_type type in Selecteddist)
                        SaveEnchant.weapons_dist += type.type + (type != SelectedCac.Last() ? "\n" : "");
                    PrintedText += "Armes à distance ajoutées.\n";
                }
            }
            if (SaveEnchant.on_mag)
            {
                if (Selectedmag.Count == 0) { PrintedText += "Choisir au moins une arme magique sur laquelle l'enchantement est applicable.\n"; return; }
                else
                {
                    foreach (Weapon_type type in Selectedmag)
                        SaveEnchant.weapons_mag += type.type + (type != SelectedCac.Last() ? "\n" : "");
                    PrintedText += "Armes magiques ajoutées.\n";
                }
            }
            try
            {
                PrintedText += "Enchantement créé.\n" +
                    "Sauvegarde de l'enchantement.\n";
                Bdd.Enchantements.Add(SaveEnchant);
                if (Bdd.SaveChanges() > 0)
                {
                    PrintedText += "Enchantement Sauvegardé.\n";
                    Selectedmag.Clear();
                    Selecteddist.Clear();
                    SelectedCac.Clear();
                    SelectedEffects.Clear();
                    SelectedJewels.Clear();
                    SelectedCats.Clear();
                    SelectedArmors.Clear();
                    Requirements.Clear();
                    RecipeCount = new List<int> { 1 };
                    SaveEnchant = new Enchantements();
                    EnchantCreated?.Invoke(this, new EventArgs());
                }
            }
            catch (Exception e)
            {
                PrintedText += "Enchantement non enregistré.\n";
            }
        }

        public RelayCommand ShowEffects { get => _showEffects ?? (_showEffects = new RelayCommand(EffectsDisplay)); }

        private void EffectsDisplay()
        {
            PrintedText += "Effets d'enchantements : \n";
            foreach (Enchant_Effets effet in Bdd.Enchant_Effets)
                PrintedText += effet.effet + " : " + effet.descr + "\n";
        }

        public RelayCommand ShowTypes { get => _showTypes ?? (_showTypes = new RelayCommand(TypesDisplay)); }
        public ObservableCollection<Enchant_Effets> SelectedEffects { get => _selectedEffects; set { _selectedEffects = value; RaisePropertyChanged(); } }

        private void TypesDisplay()
        {
            PrintedText += "Types d'enchantements : \n";
            foreach (Enchant_Type type in Bdd.Enchant_Type)
                PrintedText += type.type + " : " + type.descr + "\n";
        }
        public void RefreshItems(object sender, EventArgs e) { Items = Bdd.Items.Where(i => i.origine == "Tous" || i.origine == "Magocosme" || i.origine == "Originel").ToList(); }
        public void RefreshWeapons(object sender, EventArgs e)
        {
            CacTypes = Bdd.Weapon_type.Where(c => c.categorie.Contains("CaC")).ToList();
            MagTypes = Bdd.Weapon_type.Where(m => m.categorie.Contains("Magique")).ToList();
            DstTypes = Bdd.Weapon_type.Where(d => d.categorie.Contains("Distance")).ToList();
        }
        public event EventHandler EnchantCreated, TypeCreated, EffecCreated;
    }
}

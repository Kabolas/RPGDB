using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Jahr_words.ViewModel
{
    class EnchantViewModel : ViewModelBase
    {
        private RPGEntities15 _bdd;
        private Enchantements _saveEnchant;
        private List<Enchant_Effets> _effects;
        private List<Enchant_Type> _types;
        private List<Monde_w> _origines;
        private List<Armor_cat> _armorCats, _selectedCats = new List<Armor_cat>();
        private List<Weapon_type> _cacTypes, _selectedCac = new List<Weapon_type>();
        private List<Weapon_type> _dstTypes, _selecteddist = new List<Weapon_type>();
        private List<Weapon_type> _magTypes, _selectedmag = new List<Weapon_type>();
        private List<Piece> _armors, _selectedArmors = new List<Piece>();
        private List<Bijoux_place> _bijoux, _selectedJewels = new List<Bijoux_place>();
        private List<Items> _items;
        private System.Collections.ObjectModel.ObservableCollection<RecipeItem> _requirements = new System.Collections.ObjectModel.ObservableCollection<RecipeItem>();
        private RelayCommand _makeType, _make_effet, _moreRecipe, _save, _showEffects, _showTypes;
        private List<int> _recipeCount = new List<int> { 1 };
        private string _printedText;
        public EnchantViewModel(RPGEntities15 Bd)
        {
            Bdd = Bd;
            SaveEnchant = new Enchantements();
            Effects = Bdd.Enchant_Effets.ToList();
            Types = Bd.Enchant_Type.ToList();
            Items = Bd.Items.Where(i => i.origine == "Tous" || i.origine == "Magocosme" || i.origine == "Originel").ToList();
            Armors = Bd.Piece.ToList();
            Bijoux = Bd.Bijoux_place.ToList();
            ArmorCats = Bd.Armor_cat.ToList();
            Origines = Bd.Monde_w.Where(m => m.nom != "Technocosme" && m.nom != "Tous").ToList();
            CacTypes = Bd.Weapon_type.Where(c => c.categorie.Contains("CaC")).ToList();
            MagTypes = Bd.Weapon_type.Where(m => m.categorie.Contains("Magique")).ToList();
            DstTypes = Bd.Weapon_type.Where(d => d.categorie.Contains("Distance")).ToList();
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
        public List<Weapon_type> SelectedCac { get => _selectedCac; set { _selectedCac = value; RaisePropertyChanged(); } }
        public List<Weapon_type> DstTypes { get => _dstTypes; set { _dstTypes = value; RaisePropertyChanged(); } }
        public List<Weapon_type> Selecteddist { get => _selecteddist; set { _selecteddist = value; RaisePropertyChanged(); } }
        public List<Weapon_type> MagTypes { get => _magTypes; set { _magTypes = value; RaisePropertyChanged(); } }
        public List<Weapon_type> Selectedmag { get => _selectedmag; set { _selectedmag = value; RaisePropertyChanged(); } }
        public List<Piece> Armors { get => _armors; set { _armors = value; RaisePropertyChanged(); } }
        public List<Piece> SelectedArmors { get => _selectedArmors; set { _selectedArmors = value; RaisePropertyChanged(); } }
        public List<Bijoux_place> Bijoux { get => _bijoux; set { _bijoux = value; RaisePropertyChanged(); } }
        public List<Bijoux_place> SelectedJewels { get => _selectedJewels; set { _selectedJewels = value; RaisePropertyChanged(); } }
        public string PrintedText { get => _printedText; set { _printedText = value; RaisePropertyChanged(); } }

        public List<Armor_cat> ArmorCats { get => _armorCats; set { _armorCats = value; RaisePropertyChanged(); } }

        public List<Armor_cat> SelectedCats { get => _selectedCats; set { _selectedCats = value; RaisePropertyChanged(); } }

        public List<Monde_w> Origines { get => _origines; set { _origines = value; RaisePropertyChanged(); } }

        public RelayCommand Save { get => _save ?? (_save = new RelayCommand(Saving)); }

        private void Saving()
        {

        }

        public RelayCommand ShowEffects { get => _showEffects ?? (_showEffects = new RelayCommand(EffectsDisplay)); }

        private void EffectsDisplay()
        {
            foreach (Enchant_Effets effet in Bdd.Enchant_Effets)
                PrintedText += effet.effet + " : " + effet.descr + "\n";
        }

        public RelayCommand ShowTypes { get => _showTypes ?? (_showTypes = new RelayCommand(TypesDisplay)); }

        private void TypesDisplay()
        {
            foreach (Enchant_Type type in Bdd.Enchant_Type)
                PrintedText += type.type + " : " + type.descr + "\n";
        }
    }
}

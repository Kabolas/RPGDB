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
        private List<Items> _items;
        private List<RecipeItem>_requirements = new List<RecipeItem>();
        private RelayCommand _makeType, make_effet, _moreRecipe;
        private List<int> _recipeCount = new List<int> { 1 };
        public EnchantViewModel(RPGEntities15 Bd)
        {
            Bdd = Bd;
            SaveEnchant = new Enchantements();
            Effects = Bdd.Enchant_Effets.ToList();
            Types = Bd.Enchant_Type.ToList();
            Items = Bd.Items.ToList();
        }

        private void AddRecipe()
        {
            RecipeCount.Add(RecipeCount.Count + 1);
            RecipeCount = new List<int>(RecipeCount);
        }

        public Enchantements SaveEnchant { get => _saveEnchant; set => _saveEnchant = value; }
        public RPGEntities15 Bdd { get => _bdd; set => _bdd = value; }
        public List<Enchant_Effets> Effects { get => _effects; set => _effects = value; }
        public List<Enchant_Type> Types { get => _types; set => _types = value; }
        public List<Items> Items { get => _items; set => _items = value; }
        public List<RecipeItem> Requirements { get => _requirements; set => _requirements = value; }
        public RelayCommand MakeType { get => _makeType; set => _makeType = value; }
        public RelayCommand Make_effet { get => make_effet; set => make_effet = value; }
        public List<int> RecipeCount { get => _recipeCount; set => _recipeCount = value; }
        public RelayCommand MoreRecipe { get => _moreRecipe; set => _moreRecipe = value; }
    }
}

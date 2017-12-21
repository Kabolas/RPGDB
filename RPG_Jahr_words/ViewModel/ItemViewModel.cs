using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Jahr_words.ViewModel
{

    enum ItemType
    {
        None,
        [Description("Armes")]
        Armes,
        [Description("Armure")]
        Armure,
        [Description("Bijoux")]
        Bijoux,
        [Description("Consommables")]
        Consommables,
        [Description("Conteneur")]
        Conteneur,
        [Description("Munition")]
        Munition,
        [Description("Loot")]
        Loot,
        [Description("Vehicule")]
        Vehicule,
        [Description ("Livre")]
        Livre,
        [Description("Communs")]
        Comon,
    }

    class ItemViewModel : ViewModelBase
    {
        private RPGEntities15 _bd;
        public RPGEntities15 Bd { get => _bd; set { _bd = value; RaisePropertyChanged(); } }
        #region Item
        private bool _newCac, _newDist, _newMag;
        private int _capacite = 0, _value_critere = 0, _manaCraftCost;
        private ItemType _choice;
        private Items _saving = new Items();
        private bool _linked;
        private bool _enchantable;
        private string _printedText;

        private Weaponry _saveWeapon = new Weaponry();
        private Armes_cac _saveCac = new Armes_cac();
        private Arme_distance _saveDist = new Arme_distance();
        private Armes_magique _saveMag = new Armes_magique();
        private Armory _saveArmory = new Armory();
        private Bijoux _saveJewel = new Bijoux();
        private Consommables _saveConso = new Consommables();
        private Conteneurs _saveContain = new Conteneurs();
        private Munition _saveMun = new Munition();
        private Vehicule _saveVehicule = new Vehicule();
        private Loot _saveLoot = new Loot();
        private Livre _saveBookin = new Livre();

        #region PrintedLists
        private List<Items> _components, _linkList;
        private List<Capacites_armor> _armor_caps;
        private List<Piece> _pieces;
        private List<Procede> _processes;
        private List<Mag_element> _elements;
        private List<Weapon_type> _armes_adist;
        private List<Weapon_type> _armes_magique;
        private List<Weapon_type> _armes_aucac;
        private List<Munition_type> _munition_list;
        private List<Reload_cat> _rechargement;
        private List<Magie_type> _schools;
        private List<Bijoux_place> _place;
        private List<Effets> _conso_effects;
        private List<Conso_type> _conso_type;
        private List<Mode_deplacement> _modes;
        private List<Carburant> _carburants;
        private List<Voies> _voie;
        private List<Sorts> _sorts;
        private List<Monde_w> _origines;
        private List<Mag_element> _truncElements = new List<Mag_element>();
        private List<Tailles> _sizes;
        private List<Idiome> _idiomes;
        private List<Book_type> _types;
        private List<int> _recipeCount = new List<int> { 1 };
        private RelayCommand _newMun;
        private RelayCommand _newWeap;
        private RelayCommand _newCaps;
        private RelayCommand _newEffect;
        private RelayCommand _newProcess;
        private RelayCommand _sauvegarde;
        private RelayCommand _moreRecipe;
        #endregion

        #region SelectLists
        private List<string> _selCacdmgtype = new List<string>(), _selMundmgtype = new List<string>();
        private List<Sorts> _selectedSpells = new List<Sorts>();
        private ObservableCollection<Procede> _selectedObtentions = new ObservableCollection<Procede>();
        private List<Capacites_armor> _selectedCaps = new List<Capacites_armor>();
        private List<Mode_deplacement> _selectedMode = new List<Mode_deplacement>();
        private List<Carburant> _selectedCarbu = new List<Carburant>();
        private List<Voies> _selectedVoie = new List<Voies>();
        private ObservableCollection<RecipeItem> _recipe = new ObservableCollection<RecipeItem>();
        private ObservableCollection<RecipeResult> _recipeResults = new ObservableCollection<RecipeResult>() { new RecipeResult() { IdRecipe = 1, Nombre = 1 } };
        #endregion

        public int Capacite { get => _capacite; set { _capacite = value; RaisePropertyChanged(); } }
        public int Value_critere { get => _value_critere; set { _value_critere = value; RaisePropertyChanged(); } }
        public Items Saving { get => _saving; set { _saving = value; RaisePropertyChanged(); } }

        public bool Linked { get => _linked; set { _linked = value; RaisePropertyChanged(); } }
        public Weaponry SaveWeapon { get => _saveWeapon; set { _saveWeapon = value; RaisePropertyChanged(); } }
        public Armes_cac SaveCac { get => _saveCac; set { _saveCac = value; RaisePropertyChanged(); } }
        public Arme_distance SaveDist { get => _saveDist; set { _saveDist = value; RaisePropertyChanged(); } }
        public Armes_magique SaveMag { get => _saveMag; set { _saveMag = value; RaisePropertyChanged(); } }
        public Armory SaveArmory { get => _saveArmory; set { _saveArmory = value; RaisePropertyChanged(); } }
        public Bijoux SaveJewel { get => _saveJewel; set { _saveJewel = value; RaisePropertyChanged(); } }
        public Consommables SaveConso { get => _saveConso; set { _saveConso = value; RaisePropertyChanged(); } }
        public Conteneurs SaveContain { get => _saveContain; set { _saveContain = value; RaisePropertyChanged(); } }
        public Munition SaveMun { get => _saveMun; set { _saveMun = value; RaisePropertyChanged(); } }
        public Vehicule SaveVehicule { get => _saveVehicule; set { _saveVehicule = value; RaisePropertyChanged(); } }
        public Loot SaveLoot { get => _saveLoot; set { _saveLoot = value; RaisePropertyChanged(); } }

        public List<Capacites_armor> Armor_caps { get => _armor_caps; set { _armor_caps = value; RaisePropertyChanged(); } }
        public List<Piece> Pieces { get => _pieces; set { _pieces = value; RaisePropertyChanged(); } }
        public List<Procede> Processes { get => _processes; set { _processes = value; RaisePropertyChanged(); } }
        public List<Mag_element> Elements { get => _elements; set { _elements = value; RaisePropertyChanged(); } }
        public List<Weapon_type> Armes_adist { get => _armes_adist; set { _armes_adist = value; RaisePropertyChanged(); } }
        public List<Weapon_type> Armes_magique { get => _armes_magique; set { _armes_magique = value; RaisePropertyChanged(); } }
        public List<Weapon_type> Armes_aucac { get => _armes_aucac; set { _armes_aucac = value; RaisePropertyChanged(); } }
        public List<Munition_type> Munition_list { get => _munition_list; set { _munition_list = value; RaisePropertyChanged(); } }
        public List<Reload_cat> Rechargement { get => _rechargement; set { _rechargement = value; RaisePropertyChanged(); } }
        public List<Magie_type> Schools { get => _schools; set { _schools = value; RaisePropertyChanged(); } }
        public List<Bijoux_place> Place { get => _place; set { _place = value; RaisePropertyChanged(); } }
        public List<Effets> Conso_effects { get => _conso_effects; set { _conso_effects = value; RaisePropertyChanged(); } }
        public List<Conso_type> Conso_type { get => _conso_type; set { _conso_type = value; RaisePropertyChanged(); } }
        public List<Mode_deplacement> Modes { get => _modes; set { _modes = value; RaisePropertyChanged(); } }
        public List<Carburant> Carburants { get => _carburants; set { _carburants = value; RaisePropertyChanged(); } }
        public List<Voies> Voie { get => _voie; set { _voie = value; RaisePropertyChanged(); } }

        public List<Mag_element> TruncElements { get => _truncElements; set => _truncElements = value; }
        public bool Enchantable { get => _enchantable; set { _enchantable = value; RaisePropertyChanged(); } }

        public List<string> SelCacdmgtype { get => _selCacdmgtype; set { _selCacdmgtype = value; RaisePropertyChanged(); } }
        public List<string> SelMundmgtype { get => _selMundmgtype; set { _selMundmgtype = value; RaisePropertyChanged(); } }
        public List<Sorts> SelectedSpells { get => _selectedSpells; set { _selectedSpells = value; RaisePropertyChanged(); } }
        public List<Capacites_armor> SelectedCaps { get => _selectedCaps; set { _selectedCaps = value; RaisePropertyChanged(); } }
        public List<Mode_deplacement> SelectedMode { get => _selectedMode; set { _selectedMode = value; RaisePropertyChanged(); } }
        public List<Voies> SelectedVoie { get => _selectedVoie; set { _selectedVoie = value; RaisePropertyChanged(); } }
        public List<Carburant> SelectedCarbu { get => _selectedCarbu; set { _selectedCarbu = value; RaisePropertyChanged(); } }
        public int ManaCraftCost { get => _manaCraftCost; set { _manaCraftCost = value; RaisePropertyChanged(); } }

        public RelayCommand NewProcess { get { return _newProcess ?? (_newProcess = new RelayCommand(AddProcess)); } }

        public string PrintedText { get => _printedText; set { _printedText = value; RaisePropertyChanged(); } }

        public RelayCommand NewMun { get { return _newMun ?? (_newMun = new RelayCommand(AddMun)); } }
        public RelayCommand NewEffect { get { return _newEffect ?? (_newEffect = new RelayCommand(AddEffect)); } }
        public RelayCommand NewWeap { get { return _newWeap ?? (_newWeap = new RelayCommand(AddWeap)); } }
        public RelayCommand NewCaps { get { return _newCaps ?? (_newCaps = new RelayCommand(AddCap)); } }

        public RelayCommand Sauvegarde { get => _sauvegarde ?? (_sauvegarde = new RelayCommand(Sauver)); }
        public ObservableCollection<RecipeItem> Recipe { get => _recipe; set { _recipe = value; RaisePropertyChanged(); } }

        public ItemType Choice { get => _choice; set { _choice = value; RaisePropertyChanged(); } }

        public bool NewCac { get => _newCac; set { _newCac = value; RaisePropertyChanged(); } }
        public bool NewDist { get => _newDist; set { _newDist = value; RaisePropertyChanged(); } }
        public bool NewMag { get => _newMag; set { _newMag = value; RaisePropertyChanged(); } }

        public List<Monde_w> Origines { get => _origines; set => _origines = value; }
        public List<Tailles> Sizes { get => _sizes; set => _sizes = value; }
        public RelayCommand MoreRecipe { get => _moreRecipe ?? (_moreRecipe = new RelayCommand(AddRecipe)); }



        public List<int> RecipeCount { get => _recipeCount; set { _recipeCount = value; RaisePropertyChanged(); } }

        public ObservableCollection<RecipeResult> RecipeResults { get => _recipeResults; set { _recipeResults = value; RaisePropertyChanged(); } }

        public ObservableCollection<Procede> SelectedObtentions { get => _selectedObtentions; set => _selectedObtentions = value; }
        public List<Sorts> Sorts { get => _sorts; set { _sorts = value; RaisePropertyChanged(); } }
        public List<Items> Components { get => _components; set { _components = value; RaisePropertyChanged(); } }
        public List<Items> LinkList { get => _linkList; set { _linkList = value; RaisePropertyChanged(); } }

        public Livre SaveBookin { get => _saveBookin; set { _saveBookin = value; RaisePropertyChanged(); } }

        public List<Idiome> Idiomes { get => _idiomes; set { _idiomes = value; RaisePropertyChanged(); } }

        public List<Book_type> Types { get => _types; set { _types = value; RaisePropertyChanged(); } }
        #endregion Item

        public event EventHandler ItemAdded;
        public event EventHandler WeaponAdded;
        public void Dispose() { Bd.Dispose(); }
        private void AddRecipe()
        {
            RecipeCount.Add(RecipeCount.Count + 1);
            RecipeResult nR = new RecipeResult() { IdRecipe = RecipeCount.Last(), Nombre = 1 };
            RecipeResults.Add(nR);
            RecipeCount = new List<int>(RecipeCount);
        }
        public ItemViewModel(RPGEntities15 context)
        {
            Bd = context;
            Components = Bd.Items.ToList();
            LinkList = Bd.Items.ToList();
            Origines = Bd.Monde_w.ToList();
            Processes = Bd.Procede.ToList();
            Elements = Bd.Mag_element.ToList();
            Rechargement = Bd.Reload_cat.ToList();
            Armes_aucac = Bd.Weapon_type.ToList().FindAll(w => w.categorie == "CaC");
            Armes_adist = Bd.Weapon_type.ToList().FindAll(w => w.categorie == "Distance");
            Armes_magique = Bd.Weapon_type.ToList().FindAll(w => w.categorie == "Magique");
            Armor_caps = Bd.Capacites_armor.ToList();
            Munition_list = Bd.Munition_type.ToList();
            Schools = Bd.Magie_type.ToList();
            Place = Bd.Bijoux_place.ToList();
            Conso_effects = Bd.Effets.ToList();
            Conso_type = Bd.Conso_type.ToList();
            Modes = Bd.Mode_deplacement.ToList();
            Voie = Bd.Voies.ToList();
            Carburants = Bd.Carburant.ToList();
            Pieces = Bd.Piece.ToList();
            Sizes = Bd.Tailles.ToList();
            Sorts = Bd.Sorts.ToList();
            Idiomes = Bd.Idiome.ToList();
            Types = Bd.Book_type.ToList();
            foreach (Mag_element element in Elements)
                if (!element.element.Contains("Tous"))
                {
                    TruncElements.Add(element);
                    SaveArmory.ElemResArmorAssoc.Add(new ElemResArmorAssoc { Armory = SaveArmory, Mag_element = element, element=element.element, resValue = 0 });
                }
        }
        public void Sauver()
        {
            try
            {
                if (!Linked)
                {
                    PrintedText += "Création de l'Item " + Saving.nom + ".\n";
                    if (Saving.craftable)
                    {
                        foreach (RecipeItem step in Recipe)
                        {
                            if (Recipe.First() == step)
                                Saving.recette += step.N_recette + "[" + step + '\n';
                            else if (Recipe.Last() == step)
                                Saving.recette += "" + step + "]=>" + RecipeResults.First(r => r.IdRecipe == step.N_recette);
                            else if (Recipe.IndexOf(step) != Recipe[Recipe.IndexOf(step) + 1].N_recette)
                                Saving.recette += "" + step + "]=>" + RecipeResults.First(r => r.IdRecipe == step.N_recette) + "\n" + (step.N_recette + 1) + "[";
                            else Saving.recette += "" + step + '\n';
                        }
                    }
                    foreach (Procede proc in SelectedObtentions)
                        Saving.obtention += proc.process + (proc == SelectedObtentions.Last() ? "," : "");
                    PrintedText += Saving.nom + " créé.\n";
                    Bd.Items.Add(Saving);
                    PrintedText += "Sauvegarde de " + Saving.nom + ".\n";
                    Bd.SaveChanges();
                    Recipe.Clear();
                    RecipeCount = new List<int> { 1 };
                    RecipeResults.Clear();
                    RecipeResults.Add(new RecipeResult() { Nombre = 1, IdRecipe = 1 });
                    PrintedText += "Sauvegarde réussie, le nouvel item " + Saving.nom + " a été sauvegardé.\n";
                    Saving = Bd.Items.ToList().Last();
                }
                switch (Choice)
                {
                    case ItemType.Armes:
                        #region Weapon
                        if ((Saving.Weaponry == null))
                        {
                            PrintedText += "Création de l'arme.\n";
                            SaveWeapon.Items = Saving;
                            if (!Bd.Mag_element.Any(e => e.element == SaveWeapon.Mag_element.element))
                            {
                                SaveWeapon.Mag_element = null;
                                SaveWeapon.element_1 = null;
                                SaveWeapon.puissance_1 = SaveWeapon.duree_1 = SaveWeapon.chance_1 = null;
                            }
                            if (!Bd.Mag_element.Any(e => e.element == SaveWeapon.Mag_element1.element))
                            {
                                SaveWeapon.Mag_element1 = null;
                                SaveWeapon.element_2 = null;
                                SaveWeapon.puissance_2 = SaveWeapon.duree_2 = SaveWeapon.chance_2 = null;
                            }

                            SaveWeapon.enchantements = "";
                            SaveWeapon.enchantable = Enchantable;
                            PrintedText += "Arme créée.\n";
                            Bd.Weaponry.Add(SaveWeapon);
                            PrintedText += "Sauvegarde de l'arme.\n";
                            Bd.SaveChanges();
                            PrintedText += "Sauvegarde réussie, nouvelle Arme sauvegardée.\n";
                            SaveWeapon = Bd.Weaponry.ToList().Last();
                        }
                        else SaveWeapon = Saving.Weaponry;
                        if (NewCac)
                        {
                            PrintedText += "Création de l'arme de corps à corps.\n";
                            SaveCac.Weaponry = SaveWeapon;
                            PrintedText += "Arme de corps à corps créée.\n";
                            Bd.Armes_cac.Add(SaveCac);
                            PrintedText += "Sauvegarde de l'arme.\n";
                            Bd.SaveChanges();
                            PrintedText += "Sauvegarde réussie, nouvelle arme de corps à corps sauvegardée.\n";
                            SaveCac = new Armes_cac();
                        }
                        if (NewDist)
                        {
                            PrintedText += "Création de l'arme à distance.\n";
                            SaveDist.Weaponry = SaveWeapon;
                            PrintedText += "Arme à distance créée.\n";
                            Bd.Arme_distance.Add(SaveDist);
                            PrintedText += "Sauvegarde de l'arme.\n";
                            Bd.SaveChanges();
                            PrintedText += "Sauvegarde réussie, nouvelle arme à distance sauvegardée.\n";
                            SaveDist = new Arme_distance();
                        }
                        if (NewMag)
                        {
                            PrintedText += "Création de l'arme magique.\n";
                            SaveMag.Weaponry = SaveWeapon;
                            if (SelectedSpells.Count > 0)
                            {
                                SaveMag.spells = "";
                                foreach (Sorts spell in SelectedSpells)
                                    if (spell == SelectedSpells.Last())
                                        SaveMag.spells += spell.Id + "|" + spell.nom;
                                    else SaveMag.spells += spell.Id + "|" + spell.nom;
                            }
                            PrintedText += "Arme magique créée.\n";
                            Bd.Armes_magique.Add(SaveMag);
                            PrintedText += "Sauvegarde de l'arme.\n";
                            Bd.SaveChanges();
                            PrintedText += "Sauvegarde réussie, nouvelle arme magique sauvegardée.\n";
                            SaveMag = new Armes_magique();
                        }
                        SaveWeapon = new Weaponry();
                        Saving = new Items();
                        Linked = false;
                        Choice = ItemType.None;
                        break;
                    #endregion
                    case ItemType.Armure:
                        #region Armures
                        PrintedText += "Création de l'armure.\n";
                        SaveArmory.Items = Saving;
                        SaveArmory.enchantable = Enchantable;
                        //SaveArmory.capacites = SaveArmory.elements = SaveArmory.enchantement = "";
                        if (!Bd.Piece.Any(p => p.emplacement == SaveArmory.piece))
                        {
                            SaveArmory.piece = null;
                            SaveArmory.Piece1 = null;
                        }
                        //enchantements
                        if (SelectedCaps.Count > 0)
                            foreach (Capacites_armor cap in SelectedCaps)
                                if (cap != SelectedCaps.Last())
                                    SaveArmory.capacites += cap.pouvoir + '\n';
                                else SaveArmory.capacites += cap.pouvoir;
                        PrintedText += "Armure créée.\n";
                        Bd.Armory.Add(SaveArmory);
                        foreach (ElemResArmorAssoc er in SaveArmory.ElemResArmorAssoc)
                            Bd.ElemResArmorAssoc.Add(er);
                        PrintedText += "Sauvegarde de l'armure.\n";
                        Bd.SaveChanges();
                        PrintedText += "Sauvegarde réussie, nouvelle Armure souvegardée.\n";
                        SaveArmory = new Armory();
                        foreach (Mag_element element in Elements)
                            if (!element.element.Contains("Tous"))
                                SaveArmory.ElemResArmorAssoc.Add(new ElemResArmorAssoc { Armory = SaveArmory, Mag_element = element, element = element.element, resValue = 0 });
                        Saving = new Items();
                        Linked = false;
                        Choice = ItemType.None;
                        break;
                    #endregion
                    case ItemType.Bijoux:
                        #region Bijoux
                        PrintedText += "Création du bijoux.\n";
                        SaveJewel.enchantable = Enchantable;
                        SaveJewel.Items = Saving;
                        SaveJewel.enchantements = "";
                        //enchantements
                        PrintedText += "Bijou créé.\n";
                        Bd.Bijoux.Add(SaveJewel);
                        PrintedText += "Sauvegarde du Bijou.\n";
                        Bd.SaveChanges();
                        PrintedText += "Sauvegarde réussie, nouveeu Bijou souvegardée.\n";
                        SaveJewel = new Bijoux();
                        Saving = new Items();
                        Linked = false;
                        Choice = ItemType.None;
                        break;
                    #endregion
                    case ItemType.Consommables:
                        #region Consommables
                        PrintedText += "Création du consommable.\n";
                        SaveConso.Items = Saving;
                        if (!Bd.Effets.Any(e => e.effet == SaveConso.effet_2))
                        {
                            SaveConso.effet_2 = null;
                            SaveConso.Effets1 = null;
                            SaveConso.duree2 = SaveConso.modulo_2 = SaveConso.minimum_2 = null;

                        }
                        if (!Bd.Effets.Any(e => e.effet == SaveConso.effet_1))
                        {
                            SaveConso.effet_1 = null;
                            SaveConso.Effets = null;
                            SaveConso.duree1 = SaveConso.modulo_1 = SaveConso.minimum_1 = 0;

                        }
                        PrintedText += "Consommable créé.\n";
                        Bd.Consommables.Add(SaveConso);
                        PrintedText += "Sauvegarde du consommable.\n";
                        Bd.SaveChanges();
                        PrintedText += "Sauvegarde réussie, nouveau consommable sauvegardé.\n";
                        SaveConso = new Consommables();
                        Saving = new Items();
                        Linked = false;
                        Choice = ItemType.None;
                        break;
                    #endregion
                    case ItemType.Conteneur:
                        #region Conteneurs
                        PrintedText += "Création du conteneur.\n";
                        SaveContain.Items = Saving;

                        PrintedText += "Conteneur créé.\n";
                        Bd.Conteneurs.Add(SaveContain);
                        PrintedText += "Sauvegarde du conteneur.\n";
                        Bd.SaveChanges();
                        PrintedText += "Sauvegarde réussie, nouveau conteneur sauvegardé.\n";
                        SaveContain = new Conteneurs();
                        Saving = new Items();
                        Linked = false;
                        Choice = ItemType.None;
                        break;
                    #endregion
                    case ItemType.Munition:
                        #region Munition
                        PrintedText += "Création de Munitions.\n";
                        SaveMun.Items = Saving;
                        if (!Bd.Mag_element.Any(e => e.element == SaveMun.Mag_element.element))
                        {
                            SaveMun.Mag_element = null;
                            SaveMun.element_1 = null;
                            SaveMun.puissance_1 = SaveMun.duree_1 = SaveMun.chance_1 = null;
                        }
                        if (!Bd.Mag_element.Any(e => e.element == SaveMun.Mag_element1.element))
                        {
                            SaveMun.Mag_element1 = null;
                            SaveMun.element_2 = null;
                            SaveMun.puissance_2 = SaveMun.duree_2 = SaveMun.chance_2 = null;
                        }
                        if (SelMundmgtype.Count > 0)
                            foreach (string str in SelMundmgtype)
                                if (str != SelMundmgtype.Last())
                                    SaveMun.degats_type += str + '\n';
                                else
                                    SaveMun.degats_type += str;
                        PrintedText += "Muniton créée.\n";
                        Bd.Entry(SaveMun).State = System.Data.Entity.EntityState.Added;
                        Bd.Munition.Add(SaveMun);
                        PrintedText += "Sauvegarde de la munition.\n";
                        Bd.SaveChanges();
                        PrintedText += "Sauvegarde réussie, nouvelle munition sauvegardée.\n";
                        SaveMun = new Munition();
                        Saving = new Items();
                        Linked = false;
                        Choice = ItemType.None;
                        break;
                    #endregion
                    case ItemType.Loot:
                        #region Loot
                        PrintedText += "Création de Loot.\n";
                        SaveLoot = new Loot
                        {
                            Items = Saving,
                            Id = Saving.Id
                        };
                        PrintedText += "Loot créé.\n";
                        Bd.Loot.Add(SaveLoot);
                        PrintedText += "Sauvegarde de Loot.\n";
                        Bd.SaveChanges();
                        PrintedText += "Sauvegarde réussie, nouveau Loot sauvegardé.\n";
                        SaveLoot = new Loot();
                        Saving = new Items();
                        Choice = ItemType.None;
                        break;
                    #endregion
                    case ItemType.Vehicule:
                        #region Vehicule
                        PrintedText += "Création du vehicule.\n";
                        SaveVehicule.Items = Saving;
                        if (SelectedCarbu.Count != 0)
                            foreach (Carburant carbu in SelectedCarbu)
                                if (carbu != SelectedCarbu.Last())
                                    SaveVehicule.carburant += carbu.fuel + '\n';
                                else SaveVehicule.carburant += carbu;
                        if (SelectedMode.Count != 0)
                            foreach (Mode_deplacement mode in SelectedMode)
                                if (mode != SelectedMode.Last())
                                    SaveVehicule.deplacement_mde += mode.mode + '\n';
                                else SaveVehicule.deplacement_mde += mode.mode;
                        if (SelectedVoie.Count != 0)
                            foreach (Voies voie in SelectedVoie)
                                if (voie != SelectedVoie.Last())
                                    SaveVehicule.accessibilite += voie.voie;
                                else SaveVehicule.accessibilite += voie.voie;
                        PrintedText += "Vehicule créé.\n";
                        Bd.Vehicule.Add(SaveVehicule);
                        PrintedText += "Sauvegarde du vehicule.\n";
                        Bd.SaveChanges();
                        PrintedText += "Sauvegarde réussie, nouveau vehicule ajouté.\n";
                        SaveVehicule = new Vehicule();
                        Saving = new Items();
                        Linked = false;
                        Choice = ItemType.None;
                        break;
                    #endregion
                    case ItemType.Livre:
                        SaveBookin.Items = Saving;
                        PrintedText +="Sauvegarde du livre \n";
                        Bd.Livre.Add(SaveBookin);
                        Bd.SaveChanges();
                        PrintedText += "Sauvegarde réussie\n";
                        SaveBookin = new Livre();
                        Saving = new Items();
                        break;
                    case ItemType.Comon:
                        Saving = new Items();
                        Linked = false;
                        Choice = ItemType.None;
                        break;
                    case ItemType.None:
                        PrintedText += "Veuillez choisir un type d'item à ajouter!\n";
                        break;
                    default:
                        break;
                }
                ItemAdded?.Invoke(this, EventArgs.Empty);
            }
            catch { PrintedText += "Item non sauvegardé"; }

        }
        public void AddProcess()
        {
            AddSmthng add = new AddSmthng("Ajouter un moyen d'obtention.\nLe nom des moyens d'obtention \ndoit commencer par une majuscule")
            {
                Title = "Nouveau moyen d'obtention"
            };
            add.ShowDialog();
            if (add.Maj && add.Validate)
                try
                {
                    Procede proc = new Procede { process = add.Nouveau };
                    Bd.Procede.Add(proc);
                    Bd.SaveChanges();
                    Processes = Bd.Procede.ToList();
                    PrintedText += "Nouveau moyen d'obtention " + add.Nouveau + " ajouté.\n";
                }
                catch
                {
                    PrintedText += "Nouveau moyen d'obtention non ajouté\n";
                }
        }
        public void AddMun()
        {
            Add2Chps add = new Add2Chps("Ajouter un type de munition.\nLe nom des types de munitions\n doit commencer par une majuscule\nDeux champs a remplir: le nom et la taille d'un lot a la vente.", true)
            {
                Title = "Nouveau type de munitions"
            };
            add.ShowDialog();
            if (add.Validate)
                if (add.Maj && add.Valid && int.TryParse(add.Ch2, out int taille))
                {
                    try
                    {
                        Munition_type mun = new Munition_type { type = add.Ch1, taile_lot = taille };
                        Bd.Munition_type.Add(mun);
                        Bd.SaveChanges();
                        //Munition_list.Add(mun);
                        //mun_typ.Items.Add(mun);
                        Munition_list = Bd.Munition_type.ToList();
                        PrintedText += "Nouveau type de munitions ajouté.\n";
                    }
                    catch
                    {
                        PrintedText = "Nouveau type de munition non ajouté.\n";
                    }

                }
                else
                {
                    if (add.Maj)
                        PrintedText = "Entrez la taille du lot!\n";
                    else
                        PrintedText = "Les types de munition doivent commencer par une majuscule.\n";
                }
        }
        public void AddCap()
        {
            AddSmthng add = new AddSmthng("Ajouter une capacité.\nLe nom des capacités doit commencer par une majuscule")
            {
                Title = "Nouveelle Capacité"
            };
            add.ShowDialog();
            if (add.Validate)
                if (add.Maj)
                {
                    try
                    {
                        Capacites_armor cap = new Capacites_armor { pouvoir = add.Nouveau.Split(',')[0] };
                        Bd.Capacites_armor.Add(cap);
                        Bd.SaveChanges();
                        Armor_caps = Bd.Capacites_armor.ToList();
                        //armor_cap.ItemsSource = Armor_caps;
                        PrintedText += "Nouvelle Capacité ajoutée.\n";
                    }
                    catch
                    {
                        PrintedText = "Nouvelle Capacité non ajoutée\n";
                    }

                }
                else PrintedText = "Les Capacités doivent commencer par une majuscule\n";
        }
        public void AddEffect()
        {
            AddSmthng add = new AddSmthng("Ajouter un effet de consommable.\nLe nom des effets doit commencer par une majuscule")
            {
                Title = "Nouvel effet de consommable"
            };
            add.ShowDialog();
            if (add.Maj && add.Validate)
                try
                {
                    Effets effet = new Effets { effet = add.Nouveau };
                    Bd.Effets.Add(effet);
                    Bd.SaveChanges();
                    Conso_effects = Bd.Effets.ToList();
                    //conso_effet_1.Items.Add(effet);
                    PrintedText += "Nouvel effet " + add.Nouveau + " ajouté.";
                }
                catch
                {
                    PrintedText += "Nouvel effet non ajouté\n";
                }
        }
        public void AddWeap()
        {
            AddRestrict add = new AddRestrict("Ajouter un type d'arme.\nLe nom des types d'arme doit commencer par une majuscule\n" +
    "Preciser de quel categorie d'arme il s'agit, arme de corps a corps, a distance ou magique.", new List<string>() { "CaC", "Distance", "Magique" }, System.Windows.Controls.SelectionMode.Multiple, "")
            {
                Title = "Nouveau type d'arme de consommable"
            };
            add.ShowDialog();
            if (add.Validate)
                if (add.Maj)
                {
                    if (add.Choices.Count != 0)
                        try
                        {
                            string typee = "";
                            foreach (string str in add.Choices)
                                if (add.Choices.IndexOf(str) == 0)
                                    typee += str;
                                else typee += ',' + str;
                            Weapon_type weapon = new Weapon_type { categorie = add.Ch1, type = typee };
                            Bd.Weapon_type.Add(weapon);
                            Bd.SaveChanges();
                            PrintedText += "Nouveau type d'arme " + add.Ch1 + " ajouté.";
                            if (add.Choices.Contains("CaC")) Armes_aucac = Bd.Weapon_type.ToList().FindAll(w => w.categorie == "CaC");
                            if (add.Choices.Contains("Distance")) Armes_adist = Bd.Weapon_type.ToList().FindAll(w => w.categorie == "Distance");
                            if (add.Choices.Contains("Magique")) Armes_magique = Bd.Weapon_type.ToList().FindAll(w => w.categorie == "Magique");
                            WeaponAdded?.Invoke(this, EventArgs.Empty);
                        }
                        catch
                        {
                            PrintedText += "Nouveau type d'arme non ajouté.\n";
                        }
                    else
                        PrintedText += "La categorie d'arme doit exister et etre bien orthographiée.\n";
                }
                else
                    PrintedText += "Ce que vous entrez doit commencer par une majuscule.\n";

        }
    }
}

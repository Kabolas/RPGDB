using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Jahr_words.ViewModel
{
    class PersoViewModel : ViewModelBase
    {
        private RPGEntities15 _bd;
        private Persos _newPerso = new Persos();
        //Perso_elem _saveElem = new Perso_elem();
        //Perso_elemRes _saveElemRes = new Perso_elemRes();
        //Pers_mago _saveMago = new Pers_mago();
        //Pers_magoRes _saveMagoRes = new Pers_magoRes();
        private Pers_stats _saveStats = new Pers_stats();
        private Pers_carac _saveCaracs = new Pers_carac();
        private List<Trais> _trais;
        private List<Races> _races;
        private List<Monde_w> _origines;
        private List<PersoCategorie> _cats;
        private ObservableCollection<LootItem> _loots = new ObservableCollection<LootItem>();
        private ObservableCollection<StuffItem> _stuff = new ObservableCollection<StuffItem>();
        private List<Perso_Creature> _creature;
        private List<Bestiaire_Beast> _beasts;
        private List<Items> _lootList, _stuffList;
        private List<Magie_type> _magie;
        private List<ComboCat> _categoriescombo;
        private List<Sorts> _sort;
        private List<Sorts> _chosenSort = new List<Sorts>();
        private List<Combo> _combos;
        private List<Combo> _chosenCombos = new List<Combo>();
        private List<Piece> _pieces;
        private List<Bijoux_place> _place;
        private List<Conso_type> _consoTypes;
        private List<Effets> _conso_effects;
        private List<Munition_type> _munType;
        private List<Carburant> _carbus;
        private List<Voies> _voie;
        private List<Mode_deplacement> _deplacement;
        private List<Maniabilite> _maniabilities;
        private List<Usage> _uses;
        private List<Trais> _selectedTrais = new List<Trais>();
        private List<Condition> _conditions;

        private List<Tailles> _criSize;
        public NameGen Gen { get; set; } = new NameGen();
        private RelayCommand _newTrais, _newCat, _newCreature, _save;
        private RelayCommand _newCondition;
        private string _printedText;
        public PersoViewModel(RPGEntities15 entities15)
        {
            Bd = entities15;
            Conditions = Bd.Condition.ToList();
            Categoriescombo = Bd.ComboCat.ToList();
            NewPerso.Pers_stats = SaveStats;
            SaveStats.Persos = NewPerso;
            Magie = Bd.Magie_type.ToList();
            Pieces = Bd.Piece.ToList();
            Place = Bd.Bijoux_place.ToList();
            ConsoTypes = Bd.Conso_type.ToList();
            Conso_effects = Bd.Effets.ToList();
            MunType = Bd.Munition_type.ToList();
            Carbus = Bd.Carburant.ToList();
            Voie = Bd.Voies.ToList();
            Deplacement = Bd.Mode_deplacement.ToList();
            Maniabilities = Bd.Maniabilite.ToList();
            Uses = Bd.Usage.ToList();
            Sort = Bd.Sorts.ToList();
            Combos = Bd.Combo.ToList();
            foreach (Mag_element elem in Bd.Mag_element.Where(e => !e.element.Contains("Tous")))
            {
                NewPerso.Perso_elemRes.Add(new Perso_elemRes { Mag_element = elem, Persos = NewPerso, maitrise = 0, });
                NewPerso.Perso_elem.Add(new Perso_elem { Mag_element = elem, Persos = NewPerso, maitrise = 0 });
            }
            foreach (Magie_type mag in Bd.Magie_type)
            {
                NewPerso.Pers_magoRes.Add(new Pers_magoRes { Magie_type = mag, Persos = NewPerso, maitrise = 0 });
                NewPerso.Pers_mago.Add(new Pers_mago { Magie_type = mag, Persos = NewPerso, maitrise = 0 });
            }
            foreach (Weapon_type weapon in Bd.Weapon_type)
                NewPerso.Perso_weap_Master.Add(new Perso_weap_Master { Persos = NewPerso, Weapon_type = weapon, maitrise = 0 });
            Trais = Bd.Trais.ToList();
            CriSize = Bd.Tailles.ToList();
            LootList = Bd.Items.ToList();
            StuffList = Bd.Items.ToList();
            Races = Bd.Races.Where(r => !r.Race_Stat_Cap.evolved || r.nom == "Jahr").ToList();
            Cats = Bd.PersoCategorie.ToList();
            Origines = Bd.Monde_w.Where(m => m.nom != "Tous").ToList();
            SaveCaracs.Persos = NewPerso;
            NewPerso.Pers_carac = SaveCaracs;
        }

        public RPGEntities15 Bd { get => _bd; set { _bd = value; RaisePropertyChanged(); } }
        public Persos NewPerso { get => _newPerso; set { _newPerso = value; RaisePropertyChanged(); } }
        public Pers_stats SaveStats { get => _saveStats; set { _saveStats = value; RaisePropertyChanged(); } }
        public List<Trais> Trais { get => _trais; set { _trais = value; RaisePropertyChanged(); } }

        public List<Races> Races { get => _races; set { _races = value; RaisePropertyChanged(); } }
        public RelayCommand NewTrais { get => _newTrais ?? (_newTrais = new RelayCommand(MakeTrait)); }

        public RelayCommand NewCat { get => _newCat ?? (_newCat = new RelayCommand(MakeCat)); }
        public List<Monde_w> Origines { get => _origines; set { _origines = value; RaisePropertyChanged(); } }
        public List<PersoCategorie> Cats { get => _cats; set { _cats = value; RaisePropertyChanged(); } }

        public ObservableCollection<LootItem> Loots { get => _loots; set { _loots = value; RaisePropertyChanged(); } }
        public List<Perso_Creature> Creature { get => _creature; set { _creature = value; RaisePropertyChanged(); } }
        public List<Bestiaire_Beast> Beasts { get => _beasts; set { _beasts = value; RaisePropertyChanged(); } }
        public RelayCommand NewCreature { get => _newCreature ?? (_newCreature = new RelayCommand(MakeCreature)); }
        public string PrintedText { get => _printedText; set { _printedText = value; RaisePropertyChanged(); } }

        public List<Items> LootList { get => _lootList; set { _lootList = value; RaisePropertyChanged(); } }
        public List<Items> StuffList { get => _stuffList; set { _stuffList = value; RaisePropertyChanged(); } }

        public List<ComboCat> Categoriescombo { get => _categoriescombo; set { _categoriescombo = value; RaisePropertyChanged(); } }
        public List<Magie_type> Magie { get => _magie; set { _magie = value; RaisePropertyChanged(); } }

        public ObservableCollection<StuffItem> Stuff { get => _stuff; set { _stuff = value; RaisePropertyChanged(); } }

        public List<Sorts> Sort { get => _sort; set { _sort = value; RaisePropertyChanged(); } }
        public List<Combo> Combos { get => _combos; set { _combos = value; RaisePropertyChanged(); } }

        public RelayCommand Save { get => _save ?? (_save = new RelayCommand(Saving)); }
        public List<Tailles> CriSize { get => _criSize; set { _criSize = value; RaisePropertyChanged(); } }

        public List<Piece> Pieces { get => _pieces; set { _pieces = value; RaisePropertyChanged(); } }
        public List<Bijoux_place> Place { get => _place; set { _place = value; RaisePropertyChanged(); } }
        public List<Conso_type> ConsoTypes { get => _consoTypes; set { _consoTypes = value; RaisePropertyChanged(); } }
        public List<Effets> Conso_effects { get => _conso_effects; set { _conso_effects = value; RaisePropertyChanged(); } }
        public List<Munition_type> MunType { get => _munType; set { _munType = value; RaisePropertyChanged(); } }
        public List<Carburant> Carbus { get => _carbus; set { _carbus = value; RaisePropertyChanged(); } }
        public List<Voies> Voie { get => _voie; set { _voie = value; RaisePropertyChanged(); } }
        public List<Mode_deplacement> Deplacement { get => _deplacement; set { _deplacement = value; RaisePropertyChanged(); } }
        public List<Maniabilite> Maniabilities { get => _maniabilities; set { _maniabilities = value; RaisePropertyChanged(); } }
        public List<Usage> Uses { get => _uses; set { _uses = value; RaisePropertyChanged(); } }

        public List<Sorts> ChosenSort { get => _chosenSort; set { _chosenSort = value; RaisePropertyChanged(); } }
        public List<Combo> ChosenCombos { get => _chosenCombos; set { _chosenCombos = value; RaisePropertyChanged(); } }

        public Pers_carac SaveCaracs { get => _saveCaracs; set { _saveCaracs = value; RaisePropertyChanged(); } }

        public List<Trais> SelectedTrais { get => _selectedTrais; set { _selectedTrais = value; RaisePropertyChanged(); } }

        public RelayCommand NewCondition { get => _newCondition ?? (_newCondition = new RelayCommand(MakeCondition)); }
        public List<Condition> Conditions { get => _conditions; set { _conditions = value; RaisePropertyChanged(); } }

        private void MakeCondition()
        {
            Add2Chps fntre = new Add2Chps("Ajouter une créature.\nLe nom des créatures \ndoit commencer par une majuscule", false)
            {
                Title = "Nouvelle condition de loot"
            };
            fntre.ShowDialog();
            if (fntre.Validate && fntre.Maj)
                try
                {
                    Bd.Condition.Add(new Condition { facon = fntre.Ch1, description = fntre.Ch2 });
                    Bd.SaveChanges();
                    PrintedText += "Nouvelle condition ajoutée.\n";
                    Conditions = Bd.Condition.ToList();
                }
                catch { PrintedText += "Nouvelle condition non ajouée.\n"; }
            else if (!fntre.Maj)
                PrintedText += "Les conditions doivent commencer par une majuscule.\n";
        }

        public event EventHandler PersoAdded;

        public void IsPers()
        {
            NewPerso.Bestiaire_Beast = null;
            NewPerso.Id_Beast = null;
            NewPerso.nom_crea = null;
            NewPerso.Perso_Creature = null;
            if (NewPerso.nom != "Humain")
                NewPerso.origine = "Magocosme";
        }
        public void IsCrea()
        {
            NewPerso.Bestiaire_Beast = null;
            NewPerso.Id_Beast = null;
            NewPerso.race = null;
            NewPerso.Races = null;
        }
        public void IsPet()
        {
            NewPerso.Races = null;
            NewPerso.race = null;
            NewPerso.nom_crea = null;
            NewPerso.Perso_Creature = null;
        }
        private void Saving()
        {
            try
            {
                PrintedText += "Création du personnage.\n";
                PrintedText += "Personnage créé.\n";
                PrintedText += "Sauvegarde du personnage.\n";
                Bd.Persos.Add(NewPerso);
                Bd.Pers_stats.Add(SaveStats);
                Bd.Pers_carac.Add(SaveCaracs);
                foreach (Perso_weap_Master master in NewPerso.Perso_weap_Master)
                    Bd.Perso_weap_Master.Add(master);
                foreach (Perso_elem master in NewPerso.Perso_elem)
                    Bd.Perso_elem.Add(master);
                foreach (Perso_elemRes master in NewPerso.Perso_elemRes)
                    Bd.Perso_elemRes.Add(master);
                foreach (Pers_mago master in NewPerso.Pers_mago)
                    Bd.Pers_mago.Add(master);
                foreach (Pers_magoRes master in NewPerso.Pers_magoRes)
                    Bd.Pers_magoRes.Add(master);
                Bd.SaveChanges();
                PrintedText += "Personnage Sauvegardé.\n";
                NewPerso = new Persos();
                SaveCaracs = new Pers_carac { Persos = NewPerso };
                SaveStats = new Pers_stats { Persos = NewPerso };
                foreach (Mag_element elem in Bd.Mag_element.Where(e => !e.element.Contains("Tous")))
                {
                    NewPerso.Perso_elemRes.Add(new Perso_elemRes { Mag_element = elem, Persos = NewPerso, maitrise = 0, });
                    NewPerso.Perso_elem.Add(new Perso_elem { Mag_element = elem, Persos = NewPerso, maitrise = 0 });
                }
                foreach (Magie_type mag in Bd.Magie_type)
                {
                    NewPerso.Pers_magoRes.Add(new Pers_magoRes { Magie_type = mag, Persos = NewPerso, maitrise = 0 });
                    NewPerso.Pers_mago.Add(new Pers_mago { Magie_type = mag, Persos = NewPerso, maitrise = 0 });
                }
                foreach (Weapon_type weapon in Bd.Weapon_type)
                    NewPerso.Perso_weap_Master.Add(new Perso_weap_Master { Persos = NewPerso, Weapon_type = weapon, maitrise = 0 });

                PersoAdded?.Invoke(this, new EventArgs());
            }
            catch
            {
                PrintedText += "Personnage non Sauvegardé.\n";
            }
        }

        private void MakeCreature()
        {
            AddSmthng add = new AddSmthng("Ajouter une créature.\nLe nom des créatures \ndoit commencer par une majuscule")
            {
                Title = "Nouvelle créature"
            };
            add.ShowDialog();
            if (add.Maj && add.Validate)
                try
                {
                    Perso_Creature proc = new Perso_Creature { nom = add.Nouveau };
                    Bd.Perso_Creature.Add(proc);
                    Bd.SaveChanges();
                    Creature = Bd.Perso_Creature.ToList();
                    PrintedText += "Nouvelle créature " + add.Nouveau + " ajoutée.\n";
                }
                catch
                {
                    PrintedText += "Nouvelle créature non ajoutée\n";
                }
        }
        private void MakeCat()
        {
            AddSmthng add = new AddSmthng("Ajouter une catégorie.\nLe nom des catégories \ndoit commencer par une majuscule")
            {
                Title = "Nouvelle catégorie"
            };
            add.ShowDialog();
            if (add.Maj && add.Validate)
                try
                {
                    PersoCategorie proc = new PersoCategorie { type = add.Nouveau };
                    Bd.PersoCategorie.Add(proc);
                    Bd.SaveChanges();
                    Cats = Bd.PersoCategorie.ToList();
                    PrintedText += "Nouvelle catégorie " + add.Nouveau + " ajoutée.\n";
                }
                catch
                {
                    PrintedText += "Nouvealle catégorie non ajoutée\n";
                }
        }
        public int ElementMasterCheck(int value)
        {
            foreach (Perso_elem master in NewPerso.Perso_elem)
                if (value > NewPerso.Pers_mago.First(m => m.Magie_type.ecole.Contains("Elémentaire")).maitrise)
                {
                    PrintedText += "La maitrise d'un élément ne peut pas être supérieure à la maitrise de la magie élémentaire.\n";
                    return NewPerso.Pers_mago.First(m => m.Magie_type.ecole.Contains("Elémentaire")).maitrise;
                }
            return value;
        }
        private void MakeTrait()
        {
            AddTrait addTrait = new AddTrait();
            addTrait.ShowDialog();
            if (addTrait.Valid && addTrait.Maj)
            {
                try
                {
                    Bd.Trais.Add(addTrait.Nouveau);
                    Bd.SaveChanges();
                    PrintedText += "Nouveau trait enregistré.\n";
                    Trais = Bd.Trais.ToList();
                }
                catch (Exception)
                {
                    PrintedText += "Nouveau trait non enregistré, format incorrect, champs manquants.\n";
                }
            }
            else if (addTrait.Valid)
                PrintedText += "Les trais doivent commencer par une majuscule.\n";
        }
    }
}

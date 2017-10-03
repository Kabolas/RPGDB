using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
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
        private List<Trais> _trais;
        private List<Races> _races;
        private List<Monde_w> _origines;
        private List<PersoCategorie> _cats;
        private List<LootItem> _loots = new List<LootItem>();
        private List<StuffItem> _stuff = new List<StuffItem>();
        private List<Perso_Creature> _creature;
        private List<Bestiaire_Beast> _beasts;
        private List<Items> _lootList, _stuffList;
        private List<Magie_type> _magie;
        private List<ComboCat> _categoriescombo;
        private List<Sorts> _sort;
        private List<Combo> _combos;
        public NameGen Gen { get; set; } = new NameGen();
        private RelayCommand _newTrais, _newCat, _newCreature;

        private string _printedText;
        public PersoViewModel(RPGEntities15 entities15)
        {
            Bd = entities15;
            Categoriescombo = Bd.ComboCat.ToList();
            NewPerso.Pers_stats = SaveStats;
            Magie = Bd.Magie_type.ToList();
            foreach (Mag_element elem in Bd.Mag_element)
            {
                NewPerso.Perso_elemRes.Add(new Perso_elemRes { Mag_element = elem, Persos = NewPerso, maitrise = 0, });
                NewPerso.Perso_elem.Add(new Perso_elem { Mag_element = elem, Persos = NewPerso, maitrise = 0 });
            }
            foreach (Magie_type mag in Bd.Magie_type)
            {
                NewPerso.Pers_magoRes.Add(new Pers_magoRes { Magie_type = mag, Persos = NewPerso, maitrise = 0 });
                NewPerso.Pers_mago.Add(new Pers_mago { Magie_type = mag, Persos = NewPerso, maitrise = 0 });
            }
            foreach(Weapon_type weapon in Bd.Weapon_type)
                NewPerso.Perso_weap_Master.Add(new Perso_weap_Master { Persos = NewPerso, Weapon_type = weapon, maitrise = 0 });
            Trais = Bd.Trais.ToList();
            LootList = Bd.Items.ToList();
            StuffList = Bd.Items.ToList();
            Races = Bd.Races.Where(r => !r.Race_Stat_Cap.evolved || r.nom == "Jahr").ToList();
            Cats = Bd.PersoCategorie.ToList();
            Origines = Bd.Monde_w.Where(m => m.nom != "Tous").ToList();
        }

        public RPGEntities15 Bd { get => _bd; set { _bd = value; RaisePropertyChanged(); } }
        public Persos NewPerso { get => _newPerso; set { _newPerso = value; RaisePropertyChanged(); } }
        public Pers_stats SaveStats { get => _saveStats; set { _saveStats = value; RaisePropertyChanged(); } }
        public List<Trais> Trais { get => _trais; set { _trais = value; RaisePropertyChanged(); } }

        public List<Races> Races { get => _races; set => _races = value; }
        public RelayCommand NewTrais { get => _newTrais ?? (_newTrais = new RelayCommand(MakeTrait)); }

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

        public RelayCommand NewCat { get => _newCat ?? (_newCat = new RelayCommand(MakeCat)); }
        public List<Monde_w> Origines { get => _origines; set { _origines = value; RaisePropertyChanged(); } }
        public List<PersoCategorie> Cats { get => _cats; set { _cats = value; RaisePropertyChanged(); } }

        public List<LootItem> Loots { get => _loots; set { _loots = value; RaisePropertyChanged(); } }
        public List<Perso_Creature> Creature { get => _creature; set { _creature = value; RaisePropertyChanged(); } }
        public List<Bestiaire_Beast> Beasts { get => _beasts; set { _beasts = value; RaisePropertyChanged(); } }
        public RelayCommand NewCreature { get => _newCreature ?? (_newCreature = new RelayCommand(MakeCreature)); }
        public string PrintedText { get => _printedText; set { _printedText = value; RaisePropertyChanged(); } }

        public List<Items> LootList { get => _lootList; set { _lootList = value; RaisePropertyChanged(); } }
        public List<Items> StuffList { get => _stuffList; set { _stuffList = value; RaisePropertyChanged(); } }

        public List<ComboCat> Categoriescombo { get => _categoriescombo; set { _categoriescombo = value; RaisePropertyChanged(); } }
        public List<Magie_type> Magie { get => _magie; set { _magie = value; RaisePropertyChanged(); } }

        public List<StuffItem> Stuff { get => _stuff; set { _stuff = value; RaisePropertyChanged(); } }

        public List<Sorts> Sort { get => _sort; set { _sort = value; RaisePropertyChanged(); } }
        public List<Combo> Combos { get => _combos; set { _combos = value; RaisePropertyChanged(); } }

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

        public void ElementMasterCheck(string value)
        {
            foreach (Perso_elem master in NewPerso.Perso_elem)
                if (master.maitrise > NewPerso.Pers_mago.First(m => m.Magie_type.ecole.Contains("Element")).maitrise)
                    master.maitrise = NewPerso.Pers_mago.First(m => m.Magie_type.ecole.Contains("Element")).maitrise;
        }
    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Jahr_words.ViewModel
{
    class SpellViewModel : ViewModelBase
    {
        private RPGEntities15 _bd;
        private Combo _saveCombo = new Combo();
        private Sorts _saveSort = new Sorts();
        private string _printedText;
        private bool _isCombo = true, _isSpell = false, _fromOri = true, _fromMago = false;
        //etat == saignement etc...
        private List<Mag_element> _elements;
        private List<Weapon_type> _weapons;
        private List<Crowd_control> _cCs;
        private List<ComboCat> _categories;
        private List<Etat> _states;
        private List<Magie_type> _schools;
        private List<Buff> _buffs;
        private List<Ciblage> _ciblages;
        private List<Stat> _statPhys, _statMag;
        private List<Weapon_type> _selectedWeapons = new List<Weapon_type>();
        private List<Magie_type> _selectedEcole = new List<Magie_type>();

        private RelayCommand _save;
        private RelayCommand _nCible, _nBuff, _nState, _nCc;

        public SpellViewModel(RPGEntities15 bd)
        {
            Bd = bd;
            StatPhys = new List<Stat>() { Bd.Stat.FirstOrDefault(s => s.valeur == "Dexterité"), Bd.Stat.FirstOrDefault(s => s.valeur == "Endurance"), Bd.Stat.FirstOrDefault(s => s.valeur == "Force") };
            StatMag = new List<Stat>() { Bd.Stat.FirstOrDefault(s => s.valeur == "Charisme"), Bd.Stat.FirstOrDefault(s => s.valeur == "Intelligence"), Bd.Stat.FirstOrDefault(s => s.valeur == "Sagesse") };
            Elements = Bd.Mag_element.ToList();
            Weapons = Bd.Weapon_type.ToList();
            CCs = Bd.Crowd_control.ToList();
            Categories = bd.ComboCat.ToList();
            States = Bd.Etat.ToList();
            Schools = Bd.Magie_type.ToList();
            Buffs = Bd.Buff.ToList();
            Ciblages = Bd.Ciblage.ToList();
        }

        private void Saving()
        {
            if (IsCombo)
            {
                PrintedText += "Création du Combo";
                if (Bd.ComboCat.Any(c => c.categorie == SaveCombo.ComboCat.categorie) && Bd.Stat.Any(s => s.valeur == SaveCombo.stat) && Bd.Ciblage.Any(c => c.ciblage1 == SaveCombo.ciblage))
                {
                    if (!Bd.Buff.Any(b => b.nom == SaveCombo.buff))
                    {
                        SaveCombo.Buff1 = null;
                        SaveCombo.buff = null;
                    }
                    if (!Bd.Crowd_control.Any(cc => cc.CC == SaveCombo.Cc))
                    {
                        SaveCombo.Crowd_control = null;
                        SaveCombo.Cc = null;
                    }
                    if (!Bd.Etat.Any(e => e.etat1 == SaveCombo.etat))
                    {
                        SaveCombo.Etat1 = null;
                        SaveCombo.etat = null;
                    }
                    if (SaveCombo.mana_cout > 0)
                    {
                        string weaps = "", schools = "";
                        foreach (Weapon_type weapon in SelectedWeapons)
                            weaps += (weapon == SelectedWeapons.First() ? weapon.type : "\n" + weapon.type);
                        foreach (Magie_type ecole in SelectedEcole)
                            schools += (ecole == SelectedEcole.First() ? ecole.ecole : '\n' + ecole.ecole);
                        SaveCombo.weapons = weaps;
                        SaveCombo.magie_use = schools;
                    }
                    else
                        SaveCombo.magie_use = SaveCombo.weapons = null;
                    PrintedText += "Sauvegarde du combo.\n";
                    Bd.Combo.Add(SaveCombo);
                    Bd.SaveChanges();
                    PrintedText += "Combo " + SaveCombo.nom + " sauvegardé.\n";
                    SpellAdded?.Invoke(this, new EventArgs());
                    SaveCombo = new Combo();
                    SelectedWeapons = new List<Weapon_type>();
                    SelectedEcole = new List<Magie_type>();
                }
                else PrintedText += "Combo incomplet, veuillez le completer avant de sauvegarder.\nElement manquant : "+(Bd.ComboCat.Any(c=>c.categorie == SaveCombo.ComboCat.categorie)?"":"Categorie ")+ (Bd.Ciblage.Any(c => c.ciblage1 == SaveCombo.ciblage) ? "" : "Ciblage ")+ (Bd.Stat.Any(c => c.valeur == SaveCombo.stat) ? "" : "Statistique ")+'\n';
            }
            else
            {
                if (Bd.Stat.Any(s => s.valeur == SaveSort.stat) && Bd.Ciblage.Any(c => c.ciblage1 == SaveSort.ciblage) && Bd.Magie_type.Any(m => m.ecole == SaveSort.ecole))
                {
                    PrintedText += "Création du Sort\n";
                    if (!Bd.Buff.Any(b => b.nom == SaveSort.buff))
                    {
                        SaveSort.Buff1 = null;
                        SaveSort.buff = null;
                    }
                    if (!Bd.Crowd_control.Any(cc => cc.CC == SaveSort.Cc))
                    {
                        SaveSort.Crowd_control = null;
                        SaveSort.Cc = null;
                    }
                    if (!Bd.Mag_element.Any(e => e.element == SaveSort.element))
                    {
                        SaveSort.Mag_element = null;
                        SaveSort.element = null;
                    }
                    PrintedText += "Sauvegarde du sort\n";
                    Bd.Sorts.Add(SaveSort);
                    Bd.SaveChanges();
                    SpellAdded?.Invoke(this, EventArgs.Empty); 
                    PrintedText += "Sort " + SaveSort.nom + " sauvegardé.\nCréation du parchemin.\n";
                    Items itemParc = new Items
                    {
                        nom = "Parchemin de " + SaveSort.nom,
                        craftable = true,
                        description = "Parchemin servant à lancer le sort " + SaveSort.nom + " :\n" + SaveSort.descr,
                        masse = Bd.Items.ToList().FirstOrDefault(i => i.Id == 7).masse,
                        origine = (FromMago ? "Magocosme" : "Originel"),
                        prix_mago = SaveSort.mana_cost,
                        prix_tech = null,
                        resizable = false,
                        storable = true,
                        obtention = "Achat,Loot,Ecriture",
                        recette = "1[7|Papier vierge:1\n9|Flacon d'encre:1\n0|Mana:" + (SaveSort.mana_cost * 1.5) + "]=>1",
                    };
                    PrintedText += "Parchemin créé.\n";
                    Bd.Items.Add(itemParc);
                    Bd.SaveChanges();
                    PrintedText += "Sauvegarde du parchemin.\n";
                    Bd.Parchemins.Add(new Parchemins { Items = Bd.Items.ToList().Last(), Sorts = Bd.Sorts.ToList().Last(), spell_id = Bd.Sorts.ToList().Last().Id, Id = Bd.Items.ToList().Last().Id });
                    Bd.SaveChanges();
                    PrintedText += "Parchemin sauvegardé.\n";
                    SaveSort = new Sorts();
                }
                else PrintedText += "Sort incomplet, veuillez le completer avant de sauegarder.\nElement manquant : " + (Bd.Magie_type.Any(c => c.ecole == SaveSort.ecole) ? "" : "Ecole ") + (Bd.Ciblage.Any(c => c.ciblage1 == SaveSort.ciblage) ? "" : "Ciblage ") + (Bd.Stat.Any(c => c.valeur == SaveSort.stat) ? "" : "Statistique ") + '\n';
            }
        }

        public RPGEntities15 Bd { get => _bd; set { _bd = value; RaisePropertyChanged(); } }
        public Combo SaveCombo { get => _saveCombo; set { _saveCombo = value; RaisePropertyChanged(); } }
        public Sorts SaveSort { get => _saveSort; set { _saveSort = value; RaisePropertyChanged(); } }
        public string PrintedText { get => _printedText; set { _printedText = value; RaisePropertyChanged(); } }
        public bool IsCombo { get => _isCombo; set { _isCombo = value; RaisePropertyChanged(); } }
        public bool IsSpell { get => _isSpell; set { _isSpell = value; RaisePropertyChanged(); } }

        public List<Mag_element> Elements { get => _elements; set { _elements = value; RaisePropertyChanged(); } }
        public List<Weapon_type> Weapons { get => _weapons; set { _weapons = value; RaisePropertyChanged(); } }
        public List<Crowd_control> CCs { get => _cCs; set { _cCs = value; RaisePropertyChanged(); } }
        public List<Etat> States { get => _states; set { _states = value; RaisePropertyChanged(); } }
        public List<Magie_type> Schools { get => _schools; set { _schools = value; RaisePropertyChanged(); } }
        public List<Buff> Buffs { get => _buffs; set { _buffs = value; RaisePropertyChanged(); } }

        public List<Ciblage> Ciblages { get => _ciblages; set { _ciblages = value; RaisePropertyChanged(); } }

        public List<ComboCat> Categories { get => _categories; set { _categories = value; RaisePropertyChanged(); } }

        public RelayCommand Save { get => _save ?? (_save = new RelayCommand(Saving)); }
        public RelayCommand NCible { get => _nCible ?? (_nCible = new RelayCommand(CreateCible)); }
        public RelayCommand NBuff { get => _nBuff ?? (_nBuff = new RelayCommand(CreateBuff)); }
        public RelayCommand NState { get => _nState ?? (_nState = new RelayCommand(CreateState)); }
        public RelayCommand NCc { get => _nCc ?? (_nCc = new RelayCommand(CreateCc)); }
        public List<Stat> StatPhys { get => _statPhys; set => _statPhys = value; }
        public List<Stat> StatMag { get => _statMag; set => _statMag = value; }
        public List<Weapon_type> SelectedWeapons { get => _selectedWeapons; set { _selectedWeapons = value; RaisePropertyChanged(); } }
        public List<Magie_type> SelectedEcole { get => _selectedEcole; set { _selectedEcole = value; RaisePropertyChanged(); } }
        public bool FromOri { get => _fromOri; set { _fromOri = value; RaisePropertyChanged(); } }
        public bool FromMago { get => _fromMago; set { _fromMago = value; RaisePropertyChanged(); } }
        public event EventHandler SpellAdded;

        private void CreateCible()
        {
            AddSmthng fntre = new AddSmthng("Entrer votre nouveau type de ciblage .\n"
                + "Les noms des types de ciblage doivent commencer par des Majuscules.\n")
            {
                Title = "Nouveau Type de ciblage"
            };
            fntre.ShowDialog();
            if (fntre.Validate)
                if (fntre.Maj)
                {
                    Ciblage cible = new Ciblage { ciblage1 = fntre.Nouveau };
                    try
                    {
                        Bd.Ciblage.Add(cible);
                        Bd.SaveChanges();
                        Ciblages = Bd.Ciblage.ToList();
                        PrintedText += "Nouveau type de ciblage " + fntre.Nouveau + " ajouté.\n";
                    }
                    catch
                    {
                        PrintedText += "Nouveau type de ciblage " + fntre.Nouveau + " non ajouté.\n";
                    }
                }
                else if (!fntre.Maj) PrintedText += "Il faut que le nouvel element ajouté commence par une majuscule.\n";
        }

        private void CreateBuff()
        {
            AddRestrict fntre = new AddRestrict("Entrer votre nouveau Buff.\n"
                + "Les noms des Buffs doivent commencer par des Majuscules.\n", new List<string>() { "Buff", "Debuff" }, System.Windows.Controls.SelectionMode.Single, "", true)
            {
                Title = "Nouveau Buff-Debuff"
            };
            fntre.ShowDialog();
            if (fntre.Validate)
                if (fntre.Maj)
                {
                    Buff buf = new Buff { nom = fntre.Ch1, descr = fntre.Ch2, categorie = fntre.Choice as string };
                    try
                    {
                        Bd.Buff.Add(buf);
                        Bd.SaveChanges();
                        Buffs = Bd.Buff.ToList();
                        PrintedText += "Nouveau Buff " + fntre.Ch1 + " ajouté.\n";
                    }
                    catch
                    {
                        PrintedText += "Nouveau Buff " + fntre.Ch1 + " non ajouté.\n";
                    }
                }
                else
                {
                    PrintedText += "Les elements ajoutés doivent commencer par une majuscule.\n";
                }
        }

        private void CreateState()
        {
            Add2Chps fntre = new Add2Chps("Entrer votre nouvel état.\n"
                + "Les noms des états doivent commencer par des Majuscules.\n", false)
            {
                Title = "Nouvel Etat"
            };
            fntre.ShowDialog();
            if (fntre.Validate)
                if (fntre.Maj)
                {
                    Etat state = new Etat { etat1 = fntre.Ch1, decr = fntre.Ch2 };
                    try
                    {
                        Bd.Etat.Add(state);
                        Bd.SaveChanges();
                        States = Bd.Etat.ToList();
                        PrintedText += "Nouvel état " + fntre.Ch1 + " ajouté.\n";
                    }
                    catch
                    {
                        PrintedText += "Nouvel état " + fntre.Ch1 + " non ajouté.\n";
                    }
                }
                else
                    PrintedText += "les nouveaux elements ajoutés doivent commencer par une majuscule.\n";
        }

        private void CreateCc()
        {
            Add2Chps fntre = new Add2Chps("Entrer votre nouveau crowd control.\n"
                + "Les noms des crowd control doivent commencer par des Majuscules.\n", false)
            {
                Title = "Nouveau Crowd Control"
            };
            fntre.ShowDialog();
            if (fntre.Validate)
                if (fntre.Maj)
                {
                    Crowd_control cc = new Crowd_control { CC = fntre.Ch1, descr = fntre.Ch2 };
                    try
                    {
                        Bd.Crowd_control.Add(cc);
                        Bd.SaveChanges();
                        CCs = Bd.Crowd_control.ToList();
                        PrintedText += "Nouveau Crowd Control " + fntre.Ch1 + " ajouté.\n";
                    }
                    catch
                    {
                        PrintedText += "Nouvel crowd control " + fntre.Ch1 + " non ajouté.\n";
                    }
                }
                else
                    PrintedText += "les Nouveaux elements ajoutés doivent commencer par une majuscule.\n";
        }
    }
}

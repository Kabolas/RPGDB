using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Jahr_words.ViewModel
{
    class JahrWordsViewModel : ViewModelBase
    {
        private RPGEntities15 _bd;
        private bool _isWorld = true;
        private Mots _saveMot = new Mots();
        private ChampLexicAssoc _chpAssoc = new ChampLexicAssoc();
        private Monde _saveMonde = new Monde();
        private CatWorldAssoc _worldAssoc = new CatWorldAssoc();
        private string _printedText;
        private List<ChampLex> _champs;
        private List<Categories_monde> _cats;
        private List<Type> _types;

        private List<ChampLex> _selecteChmp = new List<ChampLex>();
        private List<ChampLexicAssoc> _ChmpsSelected = new List<ChampLexicAssoc>();
        private List<Categories_monde> _selecteCat = new List<Categories_monde>();
        private List<CatWorldAssoc> _CatSelected = new List<CatWorldAssoc>();

        private RelayCommand _newChamp, _newType, _save;

        public JahrWordsViewModel(RPGEntities15 bdd)
        {
            Bd = bdd;
            Champs = Bd.ChampLex.ToList();
            Types = Bd.Type.ToList();
            Cats = Bd.Categories_monde.ToList();
        }

        public RPGEntities15 Bd { get => _bd; set { _bd = value; RaisePropertyChanged(); } }
        public Mots SaveMot { get => _saveMot; set { _saveMot = value; RaisePropertyChanged(); } }
        public ChampLexicAssoc ChpAssoc { get => _chpAssoc; set { _chpAssoc = value; RaisePropertyChanged(); } }
        public Monde SaveMonde { get => _saveMonde; set { _saveMonde = value; RaisePropertyChanged(); } }
        public CatWorldAssoc WorldAssoc { get => _worldAssoc; set { _worldAssoc = value; RaisePropertyChanged(); } }
        public List<ChampLex> Champs { get => _champs; set { _champs = value; RaisePropertyChanged(); } }
        public List<Categories_monde> Cats { get => _cats; set { _cats = value; RaisePropertyChanged(); } }
        public RelayCommand NewChamp { get => _newChamp ?? (_newChamp = new RelayCommand(MakeChamp)); }

        private void MakeChamp()
        {
            NouveauChant fenetre = new NouveauChant();
            fenetre.ShowDialog();
            if (fenetre.Succes)
                if (fenetre.Valid && (fenetre.Cat || fenetre.Chant))
                {
                    if (Bd.ChampLex.Any(c => c.Registre == fenetre.Chmp)) PrintedText += (fenetre.Cat ? "Categorie " : "Chant lexical ") + fenetre.Chmp + " deja existant" + (fenetre.Cat ? "e." : ".") + "\n";
                    else
                    {
                        if (fenetre.Cat)
                            Bd.Categories_monde.Add(new Categories_monde { Cat_monde = fenetre.Chmp });
                        else
                            Bd.ChampLex.Add(new ChampLex { Registre = fenetre.Chmp });
                        Bd.SaveChanges();
                        Champs = Bd.ChampLex.ToList();
                        Cats = Bd.Categories_monde.ToList();
                        PrintedText += (fenetre.Cat ? "Nouvelle categorie " : "Nouveau chant lexical ") + fenetre.Chmp + " Ajouté" + (fenetre.Cat ? "e." : ".") + '\n';
                    }
                }
                else PrintedText += (fenetre.Cat ? "Nouvelle categorie " : "Nouveau chant lexical ") + fenetre.Chmp + " non ajouté" + (fenetre.Cat ? "e." : ".") + '\n';
        }

        public RelayCommand NewType { get => _newType ?? (_newType = new RelayCommand(MakeType)); }

        private void MakeType()
        {
            AddSmthng add = new AddSmthng("Ajouter un type de mot.\nLe nom des types de mots\ndoit commencer par une majuscule")
            {
                Title = "Nouveau type de mots"
            };
            add.ShowDialog();
            if (add.Maj && add.Validate)
                try
                {

                    Bd.Type.Add(new Type { mot_type = add.Nouveau });
                    Bd.SaveChanges();
                    Types = Bd.Type.ToList();
                    PrintedText += "Nouveau types de mots " + add.Nouveau + " ajouté.\n";
                }
                catch
                {
                    PrintedText += "Nouveau type de mots non ajouté\n";
                }
        }

        public RelayCommand Save { get => _save ?? (_save = new RelayCommand(Saving)); }
        public bool IsWorld { get => _isWorld; set { _isWorld = value; RaisePropertyChanged(); } }

        public List<Type> Types { get => _types; set { _types = value; RaisePropertyChanged(); } }

        public string PrintedText { get => _printedText; set { _printedText = value; RaisePropertyChanged(); } }

        public List<ChampLex> SelectedChmp { get => _selecteChmp; set { _selecteChmp = value; RaisePropertyChanged(); } }
        public List<Categories_monde> SelectedCat { get => _selecteCat; set { _selecteCat = value; RaisePropertyChanged(); } }

        public List<ChampLexicAssoc> ChmpsSelected { get => _ChmpsSelected; set { _ChmpsSelected = value; RaisePropertyChanged(); } }
        public List<CatWorldAssoc> CatSelected { get => _CatSelected; set { _CatSelected = value; RaisePropertyChanged(); } }

        private void Saving()
        {
            try
            {
                PrintedText += "Ajout du mot dans le dictionnaire.\n";
                if (IsWorld)
                {
                    foreach (Categories_monde cat in SelectedCat)
                    {
                        CatWorldAssoc assoc = new CatWorldAssoc { Categories_monde = cat, Monde = SaveMonde };
                        SaveMonde.CatWorldAssoc.Add(assoc);
                        Bd.CatWorldAssoc.Add(assoc);
                    }
                    Bd.Monde.Add(SaveMonde);
                    Bd.SaveChanges();
                    SaveMonde = new Monde();
                    SelectedCat.Clear();
                }
                else
                {
                    foreach (ChampLex lex in SelectedChmp)
                    {
                        ChampLexicAssoc assoc = new ChampLexicAssoc { ChampLex = lex, Mots = SaveMot };
                        SaveMot.ChampLexicAssoc.Add(assoc);
                        Bd.ChampLexicAssoc.Add(assoc);
                    }
                    Bd.Mots.Add(SaveMot);
                    Bd.SaveChanges();
                    SaveMot = new Mots();
                    SelectedChmp.Clear();
                }
                PrintedText += "Mot ajouté dans le dictionnaire.\n";
            }
            catch { PrintedText += "Mot non ajouté dans le dictionnaire.\n"; }
        }
    }
}

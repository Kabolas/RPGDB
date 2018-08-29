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
    class GodViewModel : ViewModelBase
    {
        private RPGEntities15 _bd;
        private bool _pantAdd = true;
        private string _printedText;
        private Pantheon _savePant = new Pantheon();
        private Divinite _saveDivin = new Divinite();
        private List<Alignement> _alignements;
        private List<Pantheon> _pantheons;
        private ObservableCollection<Peuplade> _cultists/*, _selectedPantCultists = new ObservableCollection<Peuplade>()*/, _selectedGodCultists = new ObservableCollection<Peuplade>();
        private ObservableCollection<Ville> _villes, _selectedPantVilles = new ObservableCollection<Ville>(), _selectedGodVilles = new ObservableCollection<Ville>();
        private RelayCommand _saving;

        public GodViewModel(RPGEntities15 rPGEntities15)
        {
            Bd = rPGEntities15;
            Alignements = Bd.Alignement.ToList();
            Pantheons = Bd.Pantheon.ToList();
            Cultists = new ObservableCollection<Peuplade>(Bd.Peuplade);
            Villes =  new ObservableCollection<Ville>(Bd.Ville);
            //SaveDivin.
        }

        public RPGEntities15 Bd { get => _bd; set { _bd = value; RaisePropertyChanged(); } }
        public bool PantAdd { get => _pantAdd; set { _pantAdd = value; RaisePropertyChanged(); } }
        public Pantheon SavePant { get => _savePant; set { _savePant = value; RaisePropertyChanged(); } }
        public Divinite SaveDivin { get => _saveDivin; set { _saveDivin = value; RaisePropertyChanged(); } }
        public List<Alignement> Alignements { get => _alignements; set { _alignements = value; RaisePropertyChanged(); } }
        public List<Pantheon> Pantheons { get => _pantheons; set { _pantheons = value; RaisePropertyChanged(); } }
        public ObservableCollection<Peuplade> Cultists { get => _cultists; set { _cultists = value; RaisePropertyChanged(); } }
        //public ObservableCollection<Peuplade> SelectedPantCultists { get => _selectedPantCultists; set { _selectedPantCultists = value; RaisePropertyChanged(); } }
        public ObservableCollection<Peuplade> SelectedGodCultists { get => _selectedGodCultists; set { _selectedGodCultists = value; RaisePropertyChanged(); } }

        public ObservableCollection<Ville> Villes { get => _villes; set { _villes = value; RaisePropertyChanged(); } }
        public ObservableCollection<Ville> SelectedPantVilles { get => _selectedPantVilles; set { _selectedPantVilles = value; RaisePropertyChanged(); }}
        public ObservableCollection<Ville> SelectedGodVilles { get => _selectedGodVilles; set { _selectedGodVilles = value; RaisePropertyChanged(); } }

        public RelayCommand Saving { get => _saving ?? (_saving = new RelayCommand(Save)); }
        public string PrintedText { get => _printedText; set { _printedText = value; RaisePropertyChanged(); } }

        private void Save()
        {
            try
            {
                if (PantAdd)
                {
                    //if (SelectedPantCultists.Count == 0) { PrintedText += "Veuillez choisir des popèulations culistes de ce Pantehon.\n"; return; }
                    if (SelectedPantVilles.Count == 0) { PrintedText += "Veuillez choisir desvilles dans lesquelles ce panthéon est vénéré.\n"; return; }
                    PrintedText += "Chargement des villes ou se trouvent les temples.\n";
                    SavePant.temples = String.Join(", ", SelectedPantVilles);
                    PrintedText += "Sauvegarde du Dieu";
                    Bd.Pantheon.Add(SavePant);
                    Bd.SaveChanges();
                    SavePant = new Pantheon();
                }
                else
                {
                    if (SelectedGodCultists.Count == 0) { PrintedText += "Veuillez choisir des populations culistes de ce Dieu.\n"; return; }
                    if (SelectedPantVilles.Count == 0) { PrintedText += "Veuillez choisir desvilles dans lesquelles ce dieu est vénéré.\n"; return; }
                    PrintedText += "Chargement des villes ou se trouvent les temples.\n";
                    SaveDivin.temples = String.Join(", ", SelectedGodVilles);
                    SaveDivin.temples = String.Join(", ", SelectedGodCultists);
                    Bd.Divinite.Add(SaveDivin);
                    Bd.SaveChanges();

                }
                PrintedText += (PantAdd ? "Panthéon" : "Dieu") + "  sauvegardé.";
            }
            catch { PrintedText += (PantAdd?"Panthéon":"Dieu") + " non sauvegardé."; }
        }
    }
}

using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Jahr_words.ViewModel
{
    class GodViewModel : ViewModelBase
    {
        private RPGEntities15 _bd;
        private bool _pantAdd = true;
        private Pantheon _savePant = new Pantheon();
        private Divinite _saveDivin = new Divinite();
        private List<Alignement> _alignements;
        private List<Pantheon> _pantheons;
        private List<Peuplade> _cultists, _selectedPantCultists = new List<Peuplade>(), _selectedGodCultists = new List<Peuplade>();
        private List<Ville> _villes, _selectedPantVilles = new List<Ville>(), _selectedGodVilles = new List<Ville>();

        public GodViewModel(RPGEntities15 rPGEntities15)
        {
            Bd = rPGEntities15;
            Alignements = Bd.Alignement.ToList();
            Pantheons = Bd.Pantheon.ToList();
            Cultists = Bd.Peuplade.ToList();
            Villes = Bd.Ville.ToList();
            //SaveDivin.
        }

        public RPGEntities15 Bd { get => _bd; set { _bd = value; RaisePropertyChanged(); } }
        public bool PantAdd { get => _pantAdd; set { _pantAdd = value; RaisePropertyChanged(); } }
        public Pantheon SavePant { get => _savePant; set { _savePant = value; RaisePropertyChanged(); } }
        public Divinite SaveDivin { get => _saveDivin; set { _saveDivin = value; RaisePropertyChanged(); } }
        public List<Alignement> Alignements { get => _alignements; set { _alignements = value; RaisePropertyChanged(); } }
        public List<Pantheon> Pantheons { get => _pantheons; set { _pantheons = value; RaisePropertyChanged(); } }
        public List<Peuplade> Cultists { get => _cultists; set { _cultists = value; RaisePropertyChanged(); } }
        public List<Peuplade> SelectedPantCultists { get => _selectedPantCultists; set { _selectedPantCultists = value; RaisePropertyChanged(); } }
        public List<Peuplade> SelectedGodCultists { get => _selectedGodCultists; set { _selectedGodCultists = value; RaisePropertyChanged(); } }

        public List<Ville> Villes { get => _villes; set { _villes = value; RaisePropertyChanged(); } }
        public List<Ville> SelectedPantVilles { get => _selectedPantVilles; set { _selectedPantVilles = value; RaisePropertyChanged(); }}
        public List<Ville> SelectedGodVilles { get => _selectedGodVilles; set { _selectedGodVilles = value; RaisePropertyChanged(); } }
    }
}

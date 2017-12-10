using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Jahr_words.ViewModel
{
    class BeastViewModel : ViewModelBase
    {
        private RPGEntities15 _bd;

        public BeastViewModel(RPGEntities15 bdd)
        {
            this.Bd = bdd;
        }

        public RPGEntities15 Bd { get => _bd; set => _bd = value; }
    }
}

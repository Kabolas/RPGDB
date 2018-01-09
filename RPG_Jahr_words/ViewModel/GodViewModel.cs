using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Jahr_words.ViewModel
{
    class GodViewModel
    {
        private RPGEntities15 _bd;
        private bool _pantAdd = true;
        private Pantheon _savePant;
        private Divinite _saveDivin;
        private List<Alignement> _alignements;
        private List<Pantheon> _pantheons;
    }
}

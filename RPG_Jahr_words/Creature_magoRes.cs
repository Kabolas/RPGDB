//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RPG_Jahr_words
{
    using System;
    using System.Collections.Generic;
    
    public partial class Creature_magoRes
    {
        public int Id { get; set; }
        public string crea { get; set; }
        public string magie { get; set; }
        public int maitrise { get; set; }
    
        public virtual Perso_Creature Perso_Creature { get; set; }
        public virtual Magie_type Magie_type { get; set; }
    }
}

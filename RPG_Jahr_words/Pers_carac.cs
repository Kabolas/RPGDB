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
    
    public partial class Pers_carac
    {
        public int Id { get; set; }
        public string stuff { get; set; }
        public string spells { get; set; }
        public string loot { get; set; }
        public string combos { get; set; }
        public string trais { get; set; }
        public Nullable<int> age { get; set; }
        public Nullable<int> masse { get; set; }
        public Nullable<int> taille { get; set; }
    
        public virtual Persos Persos { get; set; }
    }
}

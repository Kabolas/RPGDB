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
    
    public partial class ChampLexicAssoc
    {
        public int Id { get; set; }
        public string Chp { get; set; }
        public int Id_mot { get; set; }
    
        public virtual ChampLex ChampLex { get; set; }
        public virtual Mots Mots { get; set; }
    }
}

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
    
    public partial class Bijoux
    {
        public int Id_jewel { get; set; }
        public int Id { get; set; }
        public string emplacement { get; set; }
        public bool enchantable { get; set; }
        public string enchantements { get; set; }
    
        public virtual Bijoux_place Bijoux_place { get; set; }
        public virtual Items Items { get; set; }
    }
}

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
    
    public partial class CatWorldAssoc
    {
        public int Id { get; set; }
        public string Cat_monde { get; set; }
        public int Id_monde { get; set; }
    
        public virtual Categories_monde Categories_monde { get; set; }
        public virtual Monde Monde { get; set; }
    }
}

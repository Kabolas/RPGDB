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
    
    public partial class Beast_elem
    {
        public int Id { get; set; }
        public int Id_beast { get; set; }
        public string element { get; set; }
        public int maitrise { get; set; }
    
        public virtual Bestiaire_Beast Bestiaire_Beast { get; set; }
        public virtual Mag_element Mag_element { get; set; }
    }
}
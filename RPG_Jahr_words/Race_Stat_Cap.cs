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
    
    public partial class Race_Stat_Cap
    {
        public string Race_nom { get; set; }
        public Nullable<int> vitesse_sol { get; set; }
        public Nullable<int> vitesse_eau { get; set; }
        public Nullable<int> vitesse_vol { get; set; }
        public Nullable<int> force { get; set; }
        public Nullable<int> endurance { get; set; }
        public Nullable<int> resistance { get; set; }
        public Nullable<int> puissance { get; set; }
        public Nullable<int> defense { get; set; }
        public Nullable<int> discretion { get; set; }
        public Nullable<int> detection { get; set; }
        public Nullable<int> sagesse { get; set; }
        public Nullable<int> intelligence { get; set; }
        public Nullable<int> charisme { get; set; }
        public Nullable<int> dexterité { get; set; }
        public Nullable<int> agressivité { get; set; }
        public bool respiration_aquatique { get; set; }
        public bool vol { get; set; }
        public bool evolved { get; set; }
    
        public virtual Races Races { get; set; }
    }
}
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

    public partial class Pers_stats
    {
        public int Id_pers { get; set; }
        public int pv { get; set; }
        public int stamina { get; set; }
        public int magie_fuel { get; set; }
        public int force { get; set; }
        public int puissance { get; set; }
        public int defense { get; set; }
        public int resistance { get; set; }
        public int dexterite { get; set; }
        public int sagesse { get; set; }
        public int intelligence { get; set; }
        public int charisme { get; set; }
        public int discretion { get; set; }
        public int detection { get; set; }
        public Nullable<int> agressivite { get; set; }
        public int vitesse_sol { get; set; }
        public int vitesse_eau { get; set; }
        public Nullable<int> vitesse_vol { get; set; }

        public int Endurance { get => pv / 10; set { stamina = value * 12; pv = value * 10; } }
        public int Puissance { get => puissance; set { puissance = value; magie_fuel = value * 10; } }
        public virtual Persos Persos { get; set; }
    }
}

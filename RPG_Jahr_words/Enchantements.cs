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
    
    public partial class Enchantements
    {
        public int Id { get; set; }
        public string nom { get; set; }
        public string effects { get; set; }
        public string type { get; set; }
        public int cout_mana { get; set; }
        public bool power_on_craft { get; set; }
        public bool on_cac { get; set; }
        public bool on_dist { get; set; }
        public bool on_mag { get; set; }
        public string weapons_cac { get; set; }
        public string weapons_dist { get; set; }
        public string weapons_mag { get; set; }
        public bool on_armor { get; set; }
        public string armors { get; set; }
        public bool on_jewel { get; set; }
        public string jewels { get; set; }
        public bool unlockable { get; set; }
        public bool expert { get; set; }
        public bool lengendary { get; set; }
        public string origine { get; set; }
        public Nullable<decimal> prix_pose { get; set; }
        public Nullable<decimal> prix_aj { get; set; }
        public Nullable<int> puissance { get; set; }
        public Nullable<int> rapport { get; set; }
        public int niveau { get; set; }
        public string descr { get; set; }
        public string armors_cats { get; set; }
        public string requirements { get; set; }
    
        public virtual Enchant_Type Enchant_Type { get; set; }
        public virtual Monde_w Monde_w { get; set; }
    }
}

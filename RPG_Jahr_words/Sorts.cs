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
    
    public partial class Sorts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sorts()
        {
            this.Parchemins = new HashSet<Parchemins>();
        }
    
        public int Id { get; set; }
        public string nom { get; set; }
        public double puissance { get; set; }
        public int mana_cost { get; set; }
        public Nullable<int> stamina_cout { get; set; }
        public int niveau { get; set; }
        public string stat { get; set; }
        public string Cc { get; set; }
        public Nullable<int> Cc_chance { get; set; }
        public Nullable<double> Cc_duree { get; set; }
        public string buff { get; set; }
        public Nullable<int> buff_chance { get; set; }
        public Nullable<double> buff_duree { get; set; }
        public string element { get; set; }
        public Nullable<int> etat_chance { get; set; }
        public Nullable<double> etat_duree { get; set; }
        public int portee { get; set; }
        public int rayon { get; set; }
        public string ciblage { get; set; }
        public string ecole { get; set; }
        public double cooldown { get; set; }
        public string descr { get; set; }
        public bool perso_spell { get; set; }
        public bool beast_spell { get; set; }
        public bool creat_spell { get; set; }
    
        public virtual Buff Buff1 { get; set; }
        public virtual Ciblage Ciblage1 { get; set; }
        public virtual Crowd_control Crowd_control { get; set; }
        public virtual Mag_element Mag_element { get; set; }
        public virtual Magie_type Magie_type { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Parchemins> Parchemins { get; set; }
        public virtual Stat Stat1 { get; set; }
    }
}

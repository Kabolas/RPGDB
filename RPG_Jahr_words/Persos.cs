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
    
    public partial class Persos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Persos()
        {
            this.Pers_mago = new HashSet<Pers_mago>();
            this.Pers_magoRes = new HashSet<Pers_magoRes>();
            this.Perso_elem = new HashSet<Perso_elem>();
            this.Perso_elemRes = new HashSet<Perso_elemRes>();
            this.Perso_weap_Master = new HashSet<Perso_weap_Master>();
        }
    
        public int Id { get; set; }
        public string nom { get; set; }
        public string race { get; set; }
        public Nullable<int> Id_Beast { get; set; }
        public string nom_crea { get; set; }
        public string cat { get; set; }
        public string origine { get; set; }
        public bool evolve { get; set; }
        public string evol_nom { get; set; }
        public string alignement { get; set; }
        public string background { get; set; }
        public int lvl { get; set; }
    
        public virtual Alignement Alignement1 { get; set; }
        public virtual Bestiaire_Beast Bestiaire_Beast { get; set; }
        public virtual Monde_w Monde_w { get; set; }
        public virtual Pers_carac Pers_carac { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pers_mago> Pers_mago { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pers_magoRes> Pers_magoRes { get; set; }
        public virtual Pers_stats Pers_stats { get; set; }
        public virtual Perso_Creature Perso_Creature { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Perso_elem> Perso_elem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Perso_elemRes> Perso_elemRes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Perso_weap_Master> Perso_weap_Master { get; set; }
        public virtual PersoCategorie PersoCategorie { get; set; }
        public virtual Races Races { get; set; }
    }
}

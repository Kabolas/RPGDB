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
    
    public partial class Weaponry
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Weaponry()
        {
            this.Arme_distance = new HashSet<Arme_distance>();
            this.Armes_cac = new HashSet<Armes_cac>();
            this.Armes_magique = new HashSet<Armes_magique>();
        }
    
        public int id_weapon { get; set; }
        public int Id { get; set; }
        public string element_1 { get; set; }
        public Nullable<double> puissance_1 { get; set; }
        public Nullable<int> chance_1 { get; set; }
        public Nullable<double> duree_1 { get; set; }
        public string element_2 { get; set; }
        public Nullable<double> puissance_2 { get; set; }
        public Nullable<int> chance_2 { get; set; }
        public Nullable<double> duree_2 { get; set; }
        public int durabilite { get; set; }
        public bool enchantable { get; set; }
        public string enchantements { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Arme_distance> Arme_distance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Armes_cac> Armes_cac { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Armes_magique> Armes_magique { get; set; }
        public virtual Items Items { get; set; }
        public virtual Mag_element Mag_element { get; set; }
        public virtual Mag_element Mag_element1 { get; set; }
    }
}
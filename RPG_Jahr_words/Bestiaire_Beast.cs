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
    
    public partial class Bestiaire_Beast
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bestiaire_Beast()
        {
            this.Beast_cap_Assoc = new HashSet<Beast_cap_Assoc>();
            this.Bestiaire_Beast1 = new HashSet<Bestiaire_Beast>();
            this.Beast_elem = new HashSet<Beast_elem>();
            this.Beast_elemRes = new HashSet<Beast_elemRes>();
            this.Beast_mago = new HashSet<Beast_mago>();
            this.Beast_magoRes = new HashSet<Beast_magoRes>();
            this.Persos = new HashSet<Persos>();
        }
    
        public int Id { get; set; }
        public string nom { get; set; }
        public string origine { get; set; }
        public bool evolution { get; set; }
        public Nullable<int> sub_beast { get; set; }
        public bool petable { get; set; }
        public bool armorable { get; set; }
        public string cat { get; set; }
        public string comportement { get; set; }
        public string habitat { get; set; }
        public string loots { get; set; }
        public string descr { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Beast_cap_Assoc> Beast_cap_Assoc { get; set; }
        public virtual Best_cat Best_cat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bestiaire_Beast> Bestiaire_Beast1 { get; set; }
        public virtual Bestiaire_Beast Bestiaire_Beast2 { get; set; }
        public virtual Monde_w Monde_w { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Beast_elem> Beast_elem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Beast_elemRes> Beast_elemRes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Beast_mago> Beast_mago { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Beast_magoRes> Beast_magoRes { get; set; }
        public virtual Beast_Specs Beast_Specs { get; set; }
        public virtual Best_stats Best_stats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Persos> Persos { get; set; }
    }
}

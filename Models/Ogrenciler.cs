//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SysProgAnket3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ogrenciler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ogrenciler()
        {
            this.Ogrenci_Ders = new HashSet<Ogrenci_Ders>();
        }
    
        public int ogr_no { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public int bolum_kodu { get; set; }
    
        public virtual Bolumler Bolumler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ogrenci_Ders> Ogrenci_Ders { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace whatTheHack2.Models.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class PatientProfil
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PatientProfil()
        {
            this.Snoring = new HashSet<Snoring>();
        }
    
        public int id { get; set; }
        public string userId { get; set; }
        public string doctorUserId { get; set; }
        public Nullable<int> heartDisease { get; set; }
        public Nullable<int> asthma { get; set; }
        public Nullable<int> chronicCough { get; set; }
        public Nullable<int> obesity { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Snoring> Snoring { get; set; }
        public virtual AspNetUsers AspNetUsers1 { get; set; }
    }
}

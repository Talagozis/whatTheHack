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
    
    public partial class AspNetUsers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AspNetUsers()
        {
            this.PatientProfil = new HashSet<PatientProfil>();
            this.PatientProfil1 = new HashSet<PatientProfil>();
        }
    
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string forename { get; set; }
        public string surname { get; set; }
        public string fatherName { get; set; }
        public Nullable<System.DateTime> dob { get; set; }
        public Nullable<int> gender { get; set; }
        public Nullable<int> maritalStatusId { get; set; }
        public string job { get; set; }
        public string insurance { get; set; }
        public string amka { get; set; }
        public string mobileNumber { get; set; }
        public string addressLine { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientProfil> PatientProfil { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientProfil> PatientProfil1 { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineTechShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public partial class SalesExecutive
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalesExecutive()
        {
            this.Sales_Log = new HashSet<Sales_Log>();
        }

        public int Id { get; set; }
      
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string ProfilePic { get; set; }
    
        public string Password { get; set; }
        
        public string Email { get; set; }
      
        public string Phone { get; set; }
       
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public System.DateTime JoiningDate { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sales_Log> Sales_Log { get; set; }
    }
}

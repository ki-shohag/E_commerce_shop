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
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public partial class Sales_Log
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public int ProductId { get; set; }
        public Nullable<int> SalesExecutiveId { get; set; }
        public System.DateTime DateSold { get; set; }
        public int Quantity { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public Nullable<decimal> Profits { get; set; }
        [JsonIgnore]
        public virtual Customer Customer { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual SalesExecutive SalesExecutive { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace P_Art.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ArtSale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ArtSale()
        {
            this.Payments = new HashSet<Payment>();
        }
    
        public long SaleID { get; set; }
        public Nullable<System.DateTime> SoldDate { get; set; }
        public string DeliveryAddress { get; set; }
        public Nullable<long> BidID { get; set; }
        public string PaymentStatus { get; set; }
    
        public virtual Bid Bid { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
    }
}

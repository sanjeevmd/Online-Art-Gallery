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
    
    public partial class ArtSoldUnsold_Result
    {
        public long ArtID { get; set; }
        public string ArtTitle { get; set; }
        public string ArtDescription { get; set; }
        public int CategoryID { get; set; }
        public decimal MinimumBidAmount { get; set; }
        public Nullable<bool> IsSold { get; set; }
    }
}

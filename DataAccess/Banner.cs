//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Banner
    {
        public int BannerID { get; set; }
        public string Image { get; set; }
        public int ColumnCount { get; set; }
        public int CreatedByUserId { get; set; }
        public System.DateTime CreatedOnDate { get; set; }
        public int LasUpdatedByUserId { get; set; }
        public System.DateTime LastUpdatedOnDate { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual User User { get; set; }
    }
}

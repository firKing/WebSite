//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebSite.Models
{
    using System;
    using System.Collections.Generic;
   
    public partial class company
    {
        public company()
        {
            this.news = new HashSet<news>();
            this.purchases = new HashSet<purchase>();
        }
    
        public int companyId { get; set; }
        public int user_userId { get; set; }
    
        public virtual ICollection<news> news { get; set; }
        public virtual ICollection<purchase> purchases { get; set; }
        public virtual user user { get; set; }
    }
}

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
    
    public partial class news
    {
        public int newsId { get; set; }
        public int companyId { get; set; }
        public string news_title { get; set; }
        public string news_content { get; set; }
        public System.DateTime news_time { get; set; }
    
        public virtual company company { get; set; }
    }
}

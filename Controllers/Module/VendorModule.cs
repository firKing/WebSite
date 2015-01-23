using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Models;
using WebSite.Controllers.Common;
using System.Diagnostics.Contracts;
using System.Diagnostics.Debug;
using System;
namespace WebSite.Controllers.Module
{
    public class VendorModule
    {
        private VendorDBContext db = new VendorDBContext();
        public vendor GetVendorInfo(int vendorId)
        {
            return db.Vendors.Find(vendorId);
        }

        public List<vendor> GetVendorRecommendList(int countMax)
        {
            return db.Vendors.Take(countMax).ToList();
        }

        public bool VendorLogin(vendor info)
        {
            Assert(info != null);
            var query = from record in db.Vendors
                        where record.vendor_name == info.vendor_name
                        select new { name = record.vendor_name, id = record.vendorId };
            var result = query.Single();
            if (result == null)
            {
                return false;
            }
            else
            {
                HttpContext.Current.Session["user_name"] = result.name;
                HttpContext.Current.Session["user_id"] = result.id;
                HttpContext.Current.Session["user_type"] = UserType.Vendor;
                return true;
            }
        }

        public Pair<bool,int> VendorRegister(vendor info)
        {
            Assert(info != null);
            var id = db.Vendors.Add(info).vendorId;
            return new Pair<bool, int>(db.SaveChanges() > 0, id); ;
        }



        public bool DeleteVendor(int id)
        {
            var findResult = this.GetVendorInfo(id);
            if (findResult != null)
            {
                db.Vendors.Remove(findResult);
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public bool UpdateVendor(vendor record)
        {
            db.Entry<vendor>(record).State = System.Data.Entity.EntityState.Modified;

            return db.SaveChanges() > 0;
        }

        public List<vendor> ShowVendorList(int countMax)
        {

            return db.Vendors.Take(countMax).ToList();
        }

    }
}
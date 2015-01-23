using System.Collections.Generic;
using WebSite.Models;

namespace WebSite.Controllers.Module
{
    public class VendorModule
    {
        public vendor GetVendorInfo(int vendorId)
        {
            return new vendor { };
        }

        public bool VendorLogin(vendor info)
        {
            return false;
        }

        public bool VendorRegister(vendor info)
        {
            return false;
        }

        public bool CreatedVendor(vendor record)
        {
            return false;
        }

        public bool DeleteVendor(int id)
        {
            return false;
        }

        public bool UpdateVendor(vendor record)
        {
            return false;
        }

        public List<vendor> ShowVendorList(int countMax)
        {
            return new List<vendor> { };
        }


    }
}
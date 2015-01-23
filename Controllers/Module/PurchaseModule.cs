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
    public class PurchaseModule
    { 
        private PurchaseDBContext db = new PurchaseDBContext();
        public purchase GetPurchaseInfo(int purchaseId)
        {
            return db.Purchases.Find(purchaseId);
        }

        public List<purchase> GetPurchaseList(int countMax)
        {
            return db.Purchases.Take(countMax).ToList();
        }

        //获取企业发布的采购信息列表
        public List<purchase> GetPublishPurchaseInfoList(int companyId)
        {
            var query = from record in db.Purchases where record.companyId == companyId select record;
            return query.ToList();
        }

        public Pair<bool,int> CreatePurchase(purchase info)
        {
            Assert(info != null);
            var id = db.Purchases.Add(info).purchaseId;
            return new Pair<bool, int>(db.SaveChanges() > 0, id);
        }



        public bool DeletePurchase(int id)
        {
            var findResult = this.GetPurchaseInfo(id);
            if (findResult != null)
            {
                db.Purchases.Remove(findResult);
                return db.SaveChanges() > 0;
            }
            return false;
        }


    }
}
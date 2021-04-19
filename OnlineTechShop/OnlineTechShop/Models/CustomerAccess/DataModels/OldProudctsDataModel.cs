using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTechShop.Models.CustomerAccess.DataModels
{
    public class OldProudctsDataModel
    {
        TechShopDbEntities data = new TechShopDbEntities();
        public List<Models.Old_Products> GetPendingOldProductsByCustomerId(int id)
        {
            return data.Old_Products.Where(x => x.CutomerId == id && x.Status=="Pending").ToList();
        }
        public void InsertOldProudctForSale(Models.Old_Products oldProduct)
        {
            data.Old_Products.Add(oldProduct);
            data.SaveChanges();
        }
        public Models.Old_Products GetPostedProudctById(int id)
        {
            return data.Old_Products.Where(x => x.Id == id).FirstOrDefault();
        }
        public void UpdatePost(Models.Old_Products oldProduct)
        {
            data.Entry(oldProduct).State = System.Data.Entity.EntityState.Modified;
            data.SaveChanges();
        }
        public void DeletePost(int id)
        {
            data.Old_Products.Remove(data.Old_Products.Find(id));
            data.SaveChanges();
        }
        public bool CheckPostStatusByPostId(int id)
        {
            if (data.Old_Products.Where(x=>x.Id==id && x.Status=="Pending")!=null)
            {
                return false;
            }
            return true;
        }
        public List<Models.Old_Products> GetSoldProductsByCustomerId(int id)
        {
            return data.Old_Products.Where(x => x.CutomerId == id && x.Status == "Accepted").ToList();
        }
    }
}
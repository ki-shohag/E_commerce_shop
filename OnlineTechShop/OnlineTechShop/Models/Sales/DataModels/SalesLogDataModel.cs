
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTechShop.Models.Sales.DataModels
{
    public class SalesLogDataModel
    {
        TechShopDbEntities data = new TechShopDbEntities();

        public List<Sales_Log> GetAllSalesLog()
        {
            List<Sales_Log> SalesLog = data.Sales_Log.GroupBy(x =>x.DateSold).Select(grp => grp.FirstOrDefault()).ToList();
            return SalesLog;
        }
       
    }
}
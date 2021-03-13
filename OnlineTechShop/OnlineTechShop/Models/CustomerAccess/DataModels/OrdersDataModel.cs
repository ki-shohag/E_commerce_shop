using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTechShop.Models.CustomerAccess.ViewModels;

namespace OnlineTechShop.Models.CustomerAccess.DataModels
{
    public class OrdersDataModel
    {
        TechShopDbEntities data = new TechShopDbEntities();
        public Models.OrderData GetPaymentAndShippingDataByCustomerId(int id)
        {
            return data.OrderDatas.Where(x=>x.Customer_Id==id).FirstOrDefault();
        }
        public void AddOrderDataOnCustomerRegistration(string email)
        {
            Models.OrderData order = new OrderData();
            int id = (int)data.Customers.Where(x => x.Email == email).Select(x => x.Id).FirstOrDefault();
            order.Customer_Id = id;
            order.CardNumber = "";
            order.CardType = "";
            order.ExpirationMonth = 0;
            order.ExpirationYear = 0;
            order.ShippingAddress = "";
            order.ShippingMethod = "";
            data.OrderDatas.Add(order);
            data.SaveChanges();
        }
        public void UpdatePaymentAndShippingInfo(Models.OrderData order)
        {
            data.Entry(order).State = System.Data.Entity.EntityState.Modified;
            data.SaveChanges();
        }
        public int GetOrderIdByCustomerId(int id)
        {
            return (int)data.OrderDatas.Where(x => x.Customer_Id == id).Select(x => x.Id).FirstOrDefault();
        }
    }
}
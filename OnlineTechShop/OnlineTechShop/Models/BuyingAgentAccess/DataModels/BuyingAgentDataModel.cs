using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTechShop.Models;
using OnlineTechShop.Models.BuyingAgentAccess.ViewModels;

namespace OnlineTechShop.Models.BuyingAgentAccess.DataModels
{
    public class BuyingAgentDataModel
    {
        TechShopDbEntities data = new TechShopDbEntities();
        public List<BuyingAgent> GetAllBuyingAgent()
        {
            List<BuyingAgent> buyingAgent = data.BuyingAgents.OrderBy(x => x.Id).ToList();
            return buyingAgent;
        }
        public BuyingAgentViewModel ConvertToBuyingAgentViewModel(BuyingAgent buyingAgent)
        {
            BuyingAgentViewModel model = new BuyingAgentViewModel();
            model.Id = buyingAgent.Id;
            model.FullName = buyingAgent.FullName;
            model.Email = buyingAgent.Email;
            model.Address = buyingAgent.Address;
            model.JoiningDate = buyingAgent.JoiningDate;
            model.LastUpdated = (DateTime)buyingAgent.LastUpdated;
            model.Password = buyingAgent.Password;
            model.Profile_Pic = buyingAgent.ProfilePic;
            model.Salary = (decimal)buyingAgent.Salary;
            model.UserName = buyingAgent.UserName;
            return model;
        }
        public void InsertBuyingAgent(BuyingAgent buyingAgent)
        {
            buyingAgent.JoiningDate = DateTime.Now;
            buyingAgent.LastUpdated = DateTime.Now;
            data.BuyingAgents.Add(buyingAgent);
            data.SaveChanges();
        }
        public BuyingAgent GetValidBuyingAgent(string email, string password)
        {
            return data.BuyingAgents.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }
        public BuyingAgent GetBuyingAgentByEmail(string email)
        {
            return data.BuyingAgents.Where(x => x.Email == email).FirstOrDefault();
        }
        public BuyingAgent GetBuyingAgentByUserName(string userName)
        {
            return data.BuyingAgents.Where(x => x.UserName == userName).FirstOrDefault();
        }
        public BuyingAgent GetBuyingAgentById(int id)
        {
            return data.BuyingAgents.Find(id);
        }
        public void UpdateBuyingAgent(BuyingAgent buyingAgent)
        {
            data.Entry(buyingAgent).State = System.Data.Entity.EntityState.Modified;
            data.SaveChanges();
        }
    }
}
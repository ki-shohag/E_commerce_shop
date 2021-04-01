using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OnlineTechShop.Models.BuyingAgentAccess.DataModels;
namespace OnlineTechShop.Models.BuyingAgentAccess.ViewModels
{
    public class BuyingAgentViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Profile_Pic { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime LastUpdated { get; set; }

        BuyingAgentDataModel buyingAgentData = new BuyingAgentDataModel();
        public List<BuyingAgentViewModel> ShowAllBuyingAgents()
        {
            var buyingAgents = buyingAgentData.GetAllBuyingAgent();
            List<BuyingAgentViewModel> buyingAgentsList = new List<BuyingAgentViewModel>();
            foreach (var buyingAgent in buyingAgents)
            {
                buyingAgentsList.Add(buyingAgentData.ConvertToBuyingAgentViewModel(buyingAgent));
            }
            return buyingAgentsList;
        }
        public BuyingAgentViewModel ShowBuyingAgentByEmail(string email)
        {
            return buyingAgentData.ConvertToBuyingAgentViewModel(buyingAgentData.GetBuyingAgentByEmail(email));
        }
    }
}
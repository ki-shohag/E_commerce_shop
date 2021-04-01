using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTechShop.Models.CustomerAccess.ViewModels
{
    public class PurchaseDataViewModel
    {
        public int DatePurchased { get; set; }
        public int LaptopQuantity { get; set; }
        public int MonitorQuantity { get; set; }
        public int RamQuantity { get; set; }
        public int SSDQuantity { get; set; }
        public int GraphicsCardQuantity { get; set; }
        public int MotherboardQuantity { get; set; }
        public int CasingQuantity { get; set; }
        public int CPUQuantity { get; set; }
        public int CoolingFanQuantity { get; set; }
        public int KeyboardQuantity { get; set; }
        public int MouseQuantity { get; set; }

        public int year { get; set; }
        public DateTime PurchaseDate { get; internal set; }
    }
}
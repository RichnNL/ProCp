using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    class Price
    {
        private string cropName;
        private decimal sellPrice;
        private decimal buyPrice;

        public Price(string cropName, decimal sellPrice, decimal buyPrice)
        {
            this.cropName = cropName;
            this.sellPrice = sellPrice;
            this.buyPrice = buyPrice;
        }

        public decimal GetSellPrice() { return sellPrice; }
        public decimal GetBuyPrice() { return buyPrice; }
    }

}

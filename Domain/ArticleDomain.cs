using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ArticleDomain
    {
        public int idArticle { get; set; }

        public string barCode { get; set; }

        public double sellingPrice { get; set; }

        public string stateArticle { get; set; }

        public double customerProfit { get; set; }

        public double storeProfit { get; set; }

        public int idBelonging { get; set; }

        public double appraisalValue { get; set; }

        public string category { get; set; }

        public string description { get; set; }

        public string characteristics { get; set; }

        public double loanAmount { get; set; }

        public string serialNumber { get; set; }

        public int idSetAside { get; set; }

        public int idSale { get; set; }
    }
}

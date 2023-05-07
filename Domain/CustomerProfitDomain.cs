using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CustomerProfitDomain
    {
        public int idCustomer { get; set; }

        public string curp { get; set; }

        public string firstname { get; set; }

        public string lastName { get; set; }

        public float profitCustomer { get; set; }

        public string articlesProfit { get; set; }
    }
}

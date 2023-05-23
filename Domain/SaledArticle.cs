using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SaledArticle
    {

        public ArticleDomain article = new ArticleDomain();

        public DateTime saleDate {  get; set; }

        public int idSale { get; set; }

        public string customer { get; set; }
    }
}

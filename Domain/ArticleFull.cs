using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ArticleFull
    {
        
        public ArticleFull() { }

        //atributos de articulo
        public int idArticle { get; set; }
        public string barCode { get; set; }
        public float sellingPrice { get; set; }
        public string stateArticle { get; set; }
        public float customerProfit { get; set; }
        public float storeProfit { get; set; }     
        public DateTime creationDate { get; set; }

        //atributos de prenda
        public  int idBelonging { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public string serialNumber { get; set; }

        public int contract_idContract { get; set; }

        //atributos de contrato
        public int sale_idSale { get; set; }
    }
}

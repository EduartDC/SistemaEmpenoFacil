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

        public DateTime createDate { get; set; }

        public int idContract { get; set; }

        public byte[] imageOne { get; set; }

        public byte[] imageTwo { get; set; }

        public byte[] imageThree { get; set; }

        public byte[] imageFour { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ArticleDomain other = (ArticleDomain)obj;
            return idArticle == other.idArticle &&
                   barCode == other.barCode &&
                   description == other.description &&
                   serialNumber == other.serialNumber &&
                   sellingPrice == other.sellingPrice &&
                   appraisalValue == other.appraisalValue &&
                   category == other.category &&
                   characteristics == other.characteristics &&
                   idBelonging == other.idBelonging &&
                   loanAmount == other.loanAmount &&
                   stateArticle == other.stateArticle &&
                   createDate == other.createDate &&
                   idContract == other.idContract &&
                   imageOne == other.imageOne &&
                   imageTwo == other.imageTwo &&
                   imageThree == other.imageThree &&
                   imageFour == other.imageFour;
        }

    }
}

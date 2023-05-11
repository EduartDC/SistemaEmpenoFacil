using BusinessLogic.Utility;
using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class SaleDAO
    {
        public static List<Sale> GetSales(DateTime saleDate)
        {
            List<Sale> listSales = new List<Sale>();
            if (Utilities.VerifyConnection())
            {
                using(var connection = new ConnectionModel())
                {
                    listSales = connection.Sales.Where(sales => sales.saleDate == saleDate).ToList();
                }
            }
            else
            {
                throw new Exception("Error de conexion");
            }
            return listSales;
        }

        public static Sale GetSaleBySaleCode(int saleCode)
        {
            Sale sale = null;
            if(Utilities.VerifyConnection())
            {
                using(var connection = new ConnectionModel())
                {
                    sale = connection.Sales.Find(saleCode);
                }
            }
            else
            {
                throw new Exception("Error de conexion");
            }
            return sale;
        }
    }
}

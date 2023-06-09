﻿using BusinessLogic.Utility;
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
                    DateTime limitDate = saleDate.AddHours(23).AddMinutes(59);
                    listSales = connection.Sales.Where(sales => sales.saleDate > saleDate && sales.saleDate <= limitDate).ToList();
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

        public static (int, Sale) getSaleById(int idSale)
        {
            Sale sale = new Sale();
            int result = 0;
            if (Utilities.VerifyConnection())
            {
                try
                {
                    using (var connection = new ConnectionModel())
                    {
                        sale = connection.Sales.Where(a => a.idSale == idSale).FirstOrDefault();
                    }
                    if (sale != null)
                        result = MessageCode.SUCCESS;
                }
                catch(Exception)
                {
                    result = MessageCode.CONNECTION_ERROR;

                }
            }
            else
                result = MessageCode.CONNECTION_ERROR;

            return (result, sale);
        }

        public static (int, int) MakeSale(Sale sale)
        {

            int result = MessageCode.ERROR;
            int idSale = 0;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var addNewSale = connection.Sales.Add(new Sale()
                    {
                        total = sale.total,
                        subtotal = sale.subtotal,
                        saleDate = sale.saleDate,
                        discount = sale.discount,
                        Customer_idCustomer= sale.Customer_idCustomer,
                    });
                    connection.SaveChanges();
                    idSale = addNewSale.idSale;
                    result = MessageCode.SUCCESS;
                }
            }
            else
            {
                result = MessageCode.CONNECTION_ERROR;
            }
            return (result, idSale);
        }
    }
}

﻿using BusinessLogic.Utility;
using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class SetAsideDAO
    {
        //cide
        public static (int, int) CreateSetAside(SetAside newAside)
        {
            var result = MessageCode.ERROR;
            var idSetAside = 0;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    connection.SetAsides.Add(newAside);
                    result = connection.SaveChanges();
                    idSetAside = newAside.idSetAside;
                }
            }
            else
            {
                throw new Exception("Error de conexión");
            }
            return (idSetAside, result);
        }
        //cide
        public static int AddArticlesInSetAside(List<ArticlesSetAside> list)
        {
            var result = MessageCode.ERROR;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    foreach (var item in list)
                    {
                        connection.ArticlesSetAsides.Add(item);
                        result = connection.SaveChanges();
                    }
                }
            }
            else
            {
                throw new Exception("Error de conexión");
            }
            return result;
        }
        //cide
        public static int UpdateSetAsideState(string state, int idSetAside)
        {
            var result = MessageCode.ERROR;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var article = connection.SetAsides.Where(a => a.idSetAside == idSetAside).FirstOrDefault();
                    if (article != null)
                    {
                        article.stateAside = state;
                        result = connection.SaveChanges();
                    }
                }
            }
            else
            {
                throw new Exception("Error de conexión");
            }
            return result;
        }

        //Rafa
        public static int PayOffSetAside(string state, int idSetAside)
        {
            var result = MessageCode.ERROR;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var article = connection.SetAsides.Where(a => a.idSetAside == idSetAside).FirstOrDefault();
                    if (article != null)
                    {
                        article.stateAside = state;
                        article.reaminingAmount = 0;
                        
                        
                        result = connection.SaveChanges();
                    }
                }
            }
            else
            {
                throw new Exception("Error de conexión");
            }
            return result;
        }

        public static List<SetAside> GetSedAsidesByDate(DateTime startDate, DateTime endDate)
        {
            List<SetAside> setAsides = null;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    try
                    {
                        setAsides = new List<SetAside>();
                        setAsides = connection.SetAsides.Where(setAside => setAside.creationDate.CompareTo(startDate) >= 0 && setAside.creationDate.CompareTo(endDate) <= 0).ToList();
                    }catch(ArgumentNullException)
                    {
                        setAsides = null;
                    }

                }
            }
            else
            {
                throw new Exception("Error de conexión");
            }
            return setAsides;
        }

        public static (int, SetAside) GetAsideById(int id)
        {
            int result = 0;
            SetAside setAside = new SetAside();
            if (Utilities.VerifyConnection())
            {
                try
                {
                    using (var connection = new ConnectionModel())
                    {
                        setAside = connection.SetAsides.Where(a => a.idSetAside == id).FirstOrDefault();
                        if (setAside != null)
                            result = MessageCode.SUCCESS;
                    }
                }
                catch(Exception)
                {
                    result = MessageCode.CONNECTION_ERROR;
                }
            }
            else
                result = MessageCode.CONNECTION_ERROR;

            return (result, setAside);
        }

        //Rafa
        public static List<DataAcces.SetAside>GetSetAsidesByIdCustomer(int id)
        {
            List<DataAcces.SetAside>setAsides = new List<DataAcces.SetAside>();
            
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var result = connection.SetAsides.Where(setAside  =>setAside.Customer_idCustomer == id).ToList();
                    foreach(var item in result)
                         
                    {
                        DataAcces.SetAside setAside= new DataAcces.SetAside();
                        setAside.idSetAside= item.idSetAside;
                        setAside.deadlineDate = item.deadlineDate;
                        setAside.totalAmount = item.totalAmount;
                        setAside.reaminingAmount = item.reaminingAmount;
                        setAside.stateAside = item.stateAside;
                        setAsides.Add(setAside);
                    }
                }
            }
            else
            {
                throw new Exception("Error de conexion");
            }
            return setAsides;
        }
        
    }
}

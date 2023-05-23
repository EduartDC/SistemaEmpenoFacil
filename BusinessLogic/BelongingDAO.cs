using BusinessLogic.Utility;
using DataAcces;
using Domain.BelongingCreation;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations.Model;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BelongingDAO
    {
        public static int ReleaseBelongings()
        {
            var returl = MessageCode.ERROR;

            if (Utilities.VerifyConnection())
            {

            }
            return returl;
        }


        public static (int, List<int>) SaveBelongings(List<DataAcces.Belonging> belongingList)
        {
            List<int> idBelongings = new List<int>();


            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {

                    connection.Belongings.AddRange(belongingList);
                    connection.SaveChanges();
                    foreach (var util in belongingList)
                        idBelongings.Add(util.idBelonging);
                }
            }
            else
                return (MessageCode.CONNECTION_ERROR, idBelongings);

            return (MessageCode.SUCCESS, idBelongings);
        }

        public static (int, List<int>) SaveimagesBelongings(List<ImagesBelonging> imagesList)
        {
            List<int> idImages = new List<int>();
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    connection.ImagesBelongings.AddRange(imagesList);
                    connection.SaveChanges();
                    foreach (var util in imagesList)
                    {
                        idImages.Add(util.idImagenBelonging);
                    }
                }
            }
            else
                return (MessageCode.CONNECTION_ERROR, idImages);
            return (MessageCode.SUCCESS, idImages);

        }

        public static int DeletePendingBelongings(List<int> idBelongings)
        {
            int result = 0;
            if (Utilities.VerifyConnection())
            {
                try
                {
                    using (var connection = new ConnectionModel())
                    {
                        foreach (var id in idBelongings)
                        {
                           DataAcces.Belonging temp = connection.Belongings.Where(a => a.idBelonging == id).FirstOrDefault();
                            connection.Belongings.Remove(temp);
                        }
                        connection.SaveChanges();
                        result = MessageCode.SUCCESS;
                    }
                }
                catch (ArgumentException)
                {
                    result = MessageCode.ERROR_UPDATE;
                }
            }
            else
                result = MessageCode.CONNECTION_ERROR;

            return result;
        }

        public static int DeletePendingImages(List<int> idImages)
        {
            int result = 0;
            if (Utilities.VerifyConnection())
            {
                try
                {
                    using (var connection = new ConnectionModel())
                    {
                        foreach (int id in idImages)
                        {
                            ImagesBelonging temp = connection.ImagesBelongings.Where(a => a.idImagenBelonging == id).FirstOrDefault();
                            connection.ImagesBelongings.Remove(temp);
                        }
                        connection.SaveChanges();
                        result = MessageCode.SUCCESS;
                    }
                }
                catch (ArgumentException)
                {
                    result = MessageCode.ERROR_UPDATE;
                }
            }
            else
                result = MessageCode.CONNECTION_ERROR;

            return result;
        }

        public static  DataAcces.Belonging GetBelongingByID(int id)
        {
            DataAcces.Belonging belonging = null;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    belonging = connection.Belongings.Find(id);
                }
            }
            else
            {
                throw new Exception("Error de conexion");
            }
            return belonging;
        }

        public static List<Domain.BelongingCreation.Belonging> GetBelongingByIdContract(int id)
        {
            List<Domain.BelongingCreation.Belonging> belongins = new List<Domain.BelongingCreation.Belonging>();
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var resultSet = connection.Belongings.Where(belongin => belongin.Contract_idContract == id).ToList();
                    foreach (var element in resultSet)
                    {
                        Domain.BelongingCreation.Belonging belonging = new Domain.BelongingCreation.Belonging();
                        belonging.idBelonging = element.idBelonging;
                        belonging.Features = element.characteristics;
                        belonging.SerialNumber = element.serialNumber;
                        belonging.Category = element.category;
                        belonging.GenericDescription = element.description;
                        belonging.Model = element.model;
                        belonging.ApraisalAmount = element.appraisalValue;
                        belonging.LoanAmount = element.loanAmount;
                        Console.WriteLine(element.idBelonging);
                        var imageInfo = connection.ImagesBelongings.Where(util => util.Belonging_idBelonging == element.idBelonging).FirstOrDefault();
                        if (imageInfo != null)
                        {
                            belonging.image = imageInfo.imagen;
                        }
                        belongins.Add(belonging);
                    }
                }
            }
            else
            {
                throw new Exception("Error de conexion");
            }
            return belongins;
        }
        public static List<Domain.BelongingCreation.Belonging> GetAllBelonging()
        {
            List<Domain.BelongingCreation.Belonging> belongins = new List<Domain.BelongingCreation.Belonging>();
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var resultSet = connection.Belongings.ToList();
                    foreach (var element in resultSet)
                    {
                        Domain.BelongingCreation.Belonging belonging = new Domain.BelongingCreation.Belonging();
                        belonging.idBelonging = element.idBelonging;
                        belonging.Features = element.characteristics;
                        belonging.SerialNumber = element.serialNumber;
                        belonging.Category = element.category;
                        belonging.GenericDescription = element.description;
                        belonging.Model = element.model;
                        belonging.ApraisalAmount = element.appraisalValue;
                        belonging.LoanAmount = element.loanAmount;
                        belonging.DeadLine = element.Contract.deadlineDate;
                        
                        var imageInfo = connection.ImagesBelongings.Where(util => util.Belonging_idBelonging == element.idBelonging).FirstOrDefault();
                        if (imageInfo != null)
                        {
                            belonging.image = imageInfo.imagen;
                        }
                        belongins.Add(belonging);
                    }
                }
            }
            else
            {
                throw new Exception("Error de conexion");
            }
            return belongins;
        }
        public static List<Domain.BelongingCreation.Belonging> GetBelonging()
        {
            DateTime actualTime = DateTime.Now.AddDays(-15);
            List<Domain.BelongingCreation.Belonging> belongins = new List<Domain.BelongingCreation.Belonging>();
            List<Domain.BelongingCreation.Belonging> belongingList = new List<Domain.BelongingCreation.Belonging>();
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    
                    var resultSet = connection.Belongings.Where(b => b.Contract.deadlineDate < actualTime).ToList();
                    
                    foreach (var element in resultSet)
                    {

                        var verifyId = connection.Belongings_Articles.Where(a => a.idBelonging == element.idBelonging).FirstOrDefault();
                        var imageInfo = connection.ImagesBelongings.Where(util => util.Belonging_idBelonging == element.idBelonging).FirstOrDefault();
                        if (verifyId.idBelonging>0) 
                        {
                            
                        } else
                        {
                            Domain.BelongingCreation.Belonging belonging = new Domain.BelongingCreation.Belonging();
                            belonging.idBelonging = element.idBelonging;
                            belonging.Features = element.characteristics;
                            belonging.SerialNumber = element.serialNumber;
                            belonging.Category = element.category;
                            belonging.GenericDescription = element.description;
                            belonging.Model = element.model;
                            belonging.ApraisalAmount = element.appraisalValue;
                            belonging.LoanAmount = element.loanAmount;
                            belonging.DeadLine = element.Contract.deadlineDate;
                            belonging.State = element.Contract.stateContract;

                            
                            if (imageInfo != null)
                            {
                                belonging.image = imageInfo.imagen;
                            }
                            belongins.Add(belonging);
                        }
                        
                        
                        

                        

                        
                    }
                }
            }
            else
            {
                throw new Exception("Error de conexion");
            }
            return belongins;
        }
    }
}

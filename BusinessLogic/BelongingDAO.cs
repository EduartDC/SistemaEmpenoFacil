using BusinessLogic.Utility;
using DataAcces;
using Domain.BelongingCreation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations.Model;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public static Domain.BelongingCreation.Belonging GetBelongingByID(int id)
        {
            Domain.BelongingCreation.Belonging belonging = new Domain.BelongingCreation.Belonging();
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    DataAcces.Belonging resultQuery = connection.Belongings.Find(id);
                    belonging.idBelonging = resultQuery.idBelonging;
                    belonging.Features = resultQuery.characteristics;
                    belonging.SerialNumber = resultQuery.serialNumber;
                    belonging.Category = resultQuery.category;
                    belonging.GenericDescription = resultQuery.description;
                    belonging.Model = resultQuery.model;
                    belonging.ApraisalAmount = resultQuery.appraisalValue;
                    belonging.LoanAmount = resultQuery.loanAmount;
                    var imageInfo = connection.ImagesBelongings.Where(util => util.Belonging_idBelonging == resultQuery.idBelonging).FirstOrDefault();
                    if (imageInfo != null)
                    {
                        belonging.image = imageInfo.imagen;
                    }
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
                    try
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
                            var imageResult = connection.ImagesBelongings.Where(util => util.Belonging_idBelonging == element.idBelonging).FirstOrDefault();
                            if (imageResult != null)
                            {
                                belonging.image = imageResult.imagen;
                            }
                            Console.WriteLine(element.idBelonging);
                            var imageInfo = connection.ImagesBelongings.Where(util => util.Belonging_idBelonging == element.idBelonging).FirstOrDefault();
                            if (imageInfo != null)
                            {
                                belonging.image = imageInfo.imagen;
                            }
                            belongins.Add(belonging);
                        }
                    }
                    catch (ArgumentNullException)
                    {
                        belongins= null;
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
            DateTime actualTime = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            List<Domain.BelongingCreation.Belonging> belongins = new List<Domain.BelongingCreation.Belonging>();
           
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {

                    //var resultSet = connection.Belongings.Where(b => b.Contract.deadlineDate.AddDays(15) > actualTime).ToList();
                    var resultSet = connection.Belongings.Where(b => DbFunctions.AddDays(b.Contract.deadlineDate, 15) < actualTime).ToList();
                    foreach (var element in resultSet)
                    {

                        var verifyId = connection.Belongings_Articles.Where(a => a.idBelonging == element.idBelonging).FirstOrDefault();
                        if (verifyId != null && verifyId.idBelonging > 0)
                        {
                            Console.WriteLine(element.idBelonging);
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

                            
                            
                            belongins.Add(belonging);
                            Console.WriteLine("else"+belonging.DeadLine);
                        }
              
                    }
                    for (int i = 0; i < belongins.Count(); i++)
                    {
                        var element = belongins[i];
                        var image = connection.ImagesBelongings.Where(a => a.Belonging_idBelonging == element.idBelonging).FirstOrDefault();
                        if (image != null)
                        {
                            belongins[i].image = image.imagen;
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

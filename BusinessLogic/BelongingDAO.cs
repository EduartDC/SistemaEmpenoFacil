using BusinessLogic.Utility;
using DataAcces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations.Model;
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


        public static (int, List<int>) SaveBelongings(List<Belonging> belongingList)
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
                            Belonging temp = connection.Belongings.Where(a => a.idBelonging == id).FirstOrDefault();
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
            if(Utilities.VerifyConnection())
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
                }catch(ArgumentException)
                {
                    result = MessageCode.ERROR_UPDATE;
                }
            }else
                result = MessageCode.CONNECTION_ERROR;  

            return result;
        }
    }
}

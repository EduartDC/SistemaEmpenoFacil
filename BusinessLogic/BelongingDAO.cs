using DataAcces;
using System;
using System.Collections.Generic;
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

            if (Utilitys.VerifyConnection())
            {

            }
            return returl;
        }


        public static (int, List<int>) SaveBelongings(List<Belonging> belongingList)
        {
            List<int> idBelongings = new List<int>();


            if (Utilitys.VerifyConnection())
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

        public static int SaveimagesBelongings(List<ImagesBelonging> imagesList)
        {
            if (Utilitys.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    connection.ImagesBelongings.AddRange (imagesList);
                    connection.SaveChanges();
                }
            }
            else
                return MessageCode.CONNECTION_ERROR;
            return MessageCode.SUCCESS;

        }

        public static List<Belonging> GetAllBelongingsFromContract(int idContract)
        {
            List<Belonging> belongings = new List<Belonging>();
            if (Utilitys.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    belongings = connection.Belongings.Where(b => b.Contract_idContract == idContract).ToList();
                }
            }
            else
            {
                throw new Exception(MessageError.CONNECTION_ERROR);
            }
            return belongings;
        }
    }
}

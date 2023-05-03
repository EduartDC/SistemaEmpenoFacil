using BusinessLogic.Utility;
using DataAcces;
using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class OperationDAO
    {
        public static int AddOperation(Operation newOperation)
        {
            var result = MessageCode.ERROR;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    connection.Operations.Add(newOperation);
                    result = connection.SaveChanges();
                }
            }
            else
            {
                throw new Exception(MessageError.CONNECTION_ERROR);
            }
            return result;
        }

        public static async Task<List<OperationDomain>> GetAllOperationsByDate(DateTime date, int idStaff)
        {
            List<OperationDomain> operationsList = new List<OperationDomain>();
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var operations = await connection.Operations
                        .Where(e => DbFunctions.TruncateTime(e.operationDate) == date.Date && e.Staff_idStaff == idStaff)
                        .ToListAsync();
                    foreach (var operation in operations)
                    {
                        var operationDomain = new OperationDomain
                        {
                            idOperation = operation.idOperation,
                            operationDate = operation.operationDate,
                            concept = operation.concept,
                            paymentAmount = operation.paymentAmount,
                            changeAmount = operation.changeAmount,
                            receivedAmount = operation.receivedAmount,
                            staff = operation.Staff
                        };
                        operationsList.Add(operationDomain);
                    }
                }
            }
            else
            {
                throw new Exception(MessageError.CONNECTION_ERROR);
            }
            return operationsList;
        }

    }
}

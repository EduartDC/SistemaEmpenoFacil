using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OperationDomain
    {
        public int idOperation { get; set; }

        public DateTime operationDate { get; set; }

        public double paymentAmount { get; set; }

        public double receivedAmount { get; set; }

        public double changeAmount { get; set; }

        public int Staff_idStaff { get; set; }

        public int? SetAside_idSetAside { get; set; }

        public int? Contract_idContract { get; set; }

        public int? Sale_idSale { get; set; }

        public string concept { get; set; }

        public string fullNameStaff { get; set; }

        public virtual Staff staff { get; set; }
    }
}

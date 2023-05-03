using Domain.BelongingCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ContractDomain
    {
        public int idContract { get; set; }

        public double loanAmount { get; set; }

        public int? idContractPrevious { get; set; }

        public DateTime deadlineDate { get; set; }

        public DateTime creationDate { get; set; }

        public string stateContract { get; set; }

        public int iva { get; set; }

        public int? interestRate { get; set; }

        public double renewalFee { get; set; }

        public double settlementAmount { get; set; }

        public int duration { get; set; }

        public int Customer_idCustomer { get; set; }

        public string endorsementSettlementDates { get; set; }

        public string paymentsSettlement { get; set; }

        public string paymentsEndorsement { get; set; }

        public string loanProcentage { get; set; }

        public string totalAnnualCost { get; set; }

        public string annualInterestRate { get; set; }

        public List<DataAcces.Belonging> Belongings { get; set; }

        public virtual List<DataAcces.Operation> Operations { get; set; }

        public virtual DataAcces.Customer Customer { get; set; }
    }
}

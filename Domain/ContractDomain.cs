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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ContractDomain other = (ContractDomain)obj;

            return idContract == other.idContract &&
                   loanAmount == other.loanAmount &&
                   idContractPrevious == other.idContractPrevious &&
                   deadlineDate == other.deadlineDate &&
                   creationDate == other.creationDate &&
                   stateContract == other.stateContract &&
                   iva == other.iva &&
                   interestRate == other.interestRate &&
                   renewalFee == other.renewalFee &&
                   settlementAmount == other.settlementAmount &&
                   duration == other.duration &&
                   Customer_idCustomer == other.Customer_idCustomer &&
                   endorsementSettlementDates.SequenceEqual(other.endorsementSettlementDates) &&
                   paymentsSettlement.SequenceEqual(other.paymentsSettlement) &&
                   paymentsEndorsement.SequenceEqual(other.paymentsEndorsement) &&
                   loanProcentage == other.loanProcentage &&
                   totalAnnualCost == other.totalAnnualCost &&
                   annualInterestRate == other.annualInterestRate &&
                   Belongings.SequenceEqual(other.Belongings) &&
                   Operations.SequenceEqual(other.Operations) &&
                   Customer.Equals(other.Customer);
        }
    }
}

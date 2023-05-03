namespace DataAcces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contract
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contract()
        {
            Belongings = new HashSet<Belonging>();
            Operations = new HashSet<Operation>();
        }

        [Key]
        public int idContract { get; set; }

        public double loanAmount { get; set; }

        public int? idContractPrevious { get; set; }

        public DateTime deadlineDate { get; set; }

        public DateTime creationDate { get; set; }

        [Required]
        public string stateContract { get; set; }

        public int iva { get; set; }

        public int? interestRate { get; set; }

        public double renewalFee { get; set; }

        public double settlementAmount { get; set; }

        public int duration { get; set; }

        public int Customer_idCustomer { get; set; }

        public string endorsementSettlementDates { get; set; }

        [Required]
        public string paymentsSettlement { get; set; }

        [Required]
        public string paymentsEndorsement { get; set; }

        public string loanProcentage { get; set; }

        public string totalAnnualCost { get; set; }

        public string annualInterestRate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Belonging> Belongings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operation> Operations { get; set; }

        public virtual Customer Customer { get; set; }
    }
}

namespace DataAcces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Operation
    {
        [Key]
        public int idOperation { get; set; }

        public DateTime operationDate { get; set; }

        public double paymentAmount { get; set; }

        public double receivedAmount { get; set; }

        public double changeAmount { get; set; }

        public int Staff_idStaff { get; set; }

        public int? SetAside_idSetAside { get; set; }

        public int? Contract_idContract { get; set; }

        public int? Sale_idSale { get; set; }

        [Required]
        public string concept { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual Sale Sale { get; set; }

        public virtual SetAside SetAside { get; set; }

        public virtual Staff Staff { get; set; }
    }
}

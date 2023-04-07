namespace DataAcces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Metric
    {
        [Key]
        public int idMetrics { get; set; }

        [Required]
        public string interestRate { get; set; }

        [Required]
        public string IVA { get; set; }
    }
}

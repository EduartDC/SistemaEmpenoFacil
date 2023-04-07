namespace DataAcces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SetAside
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SetAside()
        {
            ArticlesSetAsides = new HashSet<ArticlesSetAside>();
            Operations = new HashSet<Operation>();
        }

        [Key]
        public int idSetAside { get; set; }

        public DateTime creationDate { get; set; }

        public DateTime deadlineDate { get; set; }

        public double totalAmount { get; set; }

        public double reaminingAmount { get; set; }

        [Required]
        public string percentage { get; set; }

        public int Customer_idCustomer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticlesSetAside> ArticlesSetAsides { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operation> Operations { get; set; }
    }
}

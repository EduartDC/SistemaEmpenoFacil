namespace DataAcces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            Operations = new HashSet<Operation>();
        }

        [Key]
        public int idStaff { get; set; }

        [Required]
        public string statusStaff { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string fisrtName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string userName { get; set; }

        [Required]
        public string rol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operation> Operations { get; set; }
    }
}

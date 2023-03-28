namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Belonging
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Belonging()
        {
            ImagesBelongings = new HashSet<ImagesBelonging>();
        }

        [Key]
        public int idBelonging { get; set; }

        public double appraisalValue { get; set; }

        [Required]
        public string category { get; set; }

        public string description { get; set; }

        [Required]
        public string characteristics { get; set; }

        public double loanAmount { get; set; }

        [Required]
        public string serialNumber { get; set; }

        public int Contract_idContract { get; set; }

        public virtual Belongings_Articles Belongings_Articles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImagesBelonging> ImagesBelongings { get; set; }

        public virtual Contract Contract { get; set; }
    }
}

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ImagesIdentification
    {
        [Key]
        public int idImagenIdentification { get; set; }

        [Required]
        public byte[] imagen { get; set; }

        public int Customer_idCustomer { get; set; }

        public virtual Customer Customer { get; set; }
    }
}

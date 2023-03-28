namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ImagesBelonging
    {
        [Key]
        public int idImagenBelonging { get; set; }

        [Required]
        public byte[] imagen { get; set; }

        public int Belonging_idBelonging { get; set; }

        public virtual Belonging Belonging { get; set; }
    }
}

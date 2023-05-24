namespace DataAcces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Belongings_Articles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Belongings_Articles()
        {
            ArticlesSetAsides = new HashSet<ArticlesSetAside>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int idArticle { get; set; }

        public string barCode { get; set; }

        public double sellingPrice { get; set; }

        [Required]
        public string stateArticle { get; set; }

        public double customerProfit { get; set; }

        public double storeProfit { get; set; }

        public DateTime creationDate { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idBelonging { get; set; }

        public int? Sale_idSale { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticlesSetAside> ArticlesSetAsides { get; set; }

        public virtual Belonging Belonging { get; set; }

        public virtual Sale Sale { get; set; }
    }
}

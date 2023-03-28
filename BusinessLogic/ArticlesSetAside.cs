namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ArticlesSetAside
    {
        [Key]
        public int idArticlesSetAside { get; set; }

        public int discount { get; set; }

        public int SetAside_idSetAside { get; set; }

        public int Article_idBelonging { get; set; }

        public virtual Belongings_Articles Belongings_Articles { get; set; }

        public virtual SetAside SetAside { get; set; }
    }
}

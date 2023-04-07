using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataAcces
{
    public partial class ConnectionModel : DbContext
    {
        public ConnectionModel()
            : base("name=ConnectionModel")
        {
        }

        public virtual DbSet<ArticlesSetAside> ArticlesSetAsides { get; set; }
        public virtual DbSet<Belonging> Belongings { get; set; }
        public virtual DbSet<Belongings_Articles> Belongings_Articles { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ImagesBelonging> ImagesBelongings { get; set; }
        public virtual DbSet<ImagesIdentification> ImagesIdentifications { get; set; }
        public virtual DbSet<Metric> Metrics { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SetAside> SetAsides { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Belonging>()
                .HasOptional(e => e.Belongings_Articles)
                .WithRequired(e => e.Belonging)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Belonging>()
                .HasMany(e => e.ImagesBelongings)
                .WithRequired(e => e.Belonging)
                .HasForeignKey(e => e.Belonging_idBelonging)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Belongings_Articles>()
                .HasMany(e => e.ArticlesSetAsides)
                .WithRequired(e => e.Belongings_Articles)
                .HasForeignKey(e => e.Article_idBelonging)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.Belongings)
                .WithRequired(e => e.Contract)
                .HasForeignKey(e => e.Contract_idContract)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.Operations)
                .WithOptional(e => e.Contract)
                .HasForeignKey(e => e.Contract_idContract);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Contracts)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.Customer_idCustomer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.ImagesIdentifications)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.Customer_idCustomer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Sales)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.Customer_idCustomer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.SetAsides)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.Customer_idCustomer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .HasMany(e => e.Belongings_Articles)
                .WithRequired(e => e.Sale)
                .HasForeignKey(e => e.Sale_idSale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .HasMany(e => e.Operations)
                .WithOptional(e => e.Sale)
                .HasForeignKey(e => e.Sale_idSale);

            modelBuilder.Entity<SetAside>()
                .HasMany(e => e.ArticlesSetAsides)
                .WithRequired(e => e.SetAside)
                .HasForeignKey(e => e.SetAside_idSetAside)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SetAside>()
                .HasMany(e => e.Operations)
                .WithOptional(e => e.SetAside)
                .HasForeignKey(e => e.SetAside_idSetAside);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Operations)
                .WithRequired(e => e.Staff)
                .HasForeignKey(e => e.Staff_idStaff)
                .WillCascadeOnDelete(false);
        }
    }
}

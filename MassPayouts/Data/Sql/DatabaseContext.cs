using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using MassPayouts.Model.Sql;

namespace MassPayouts.Data.Sql
{      
    public  class DatabaseContext : DbContext
    {
        public DatabaseContext(): base("name=DatabaseContext")
        {
        }

        public virtual DbSet<Contractors> Contractors { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<TypeOfContractor> TypeOfContractor { get; set; }
        public virtual DbSet<TypeOfTransaction> TypeOfTransaction { get; set; }
        public virtual DbSet<TypeOfWallet> TypeOfWallet { get; set; }
        public virtual DbSet<Wallets> Wallets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contractors>().HasKey(c=>c.Id).Property(p=>p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<TypeOfContractor>().HasKey(c=>c.Id).Property(p=>p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Transactions>().HasKey(c=>c.Id).Property(p=>p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<TypeOfTransaction>().HasKey(c=>c.Id).Property(p=>p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Wallets>().HasKey(c=>c.Id).Property(p=>p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<TypeOfWallet>().HasKey(c=>c.Id).Property(p=>p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Contractors>()
                .HasMany(e => e.Wallets)
                .WithRequired(e => e.Contractors)
                .HasForeignKey(e => e.OwnerId)
                .WillCascadeOnDelete(false);      

            modelBuilder.Entity<TypeOfContractor>()
                .HasMany(e => e.Contractors)
                .WithRequired(e => e.TypeOfContractor)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeOfContractor>()
                .HasOptional(e => e.TypeOfContractor1)
                .WithRequired(e => e.TypeOfContractor2);  

            modelBuilder.Entity<TypeOfTransaction>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.TypeOfTransaction)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);       

            modelBuilder.Entity<TypeOfWallet>()
                .HasMany(e => e.Wallets)
                .WithRequired(e => e.TypeOfWallet)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);    

            modelBuilder.Entity<Wallets>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Wallets)
                .HasForeignKey(e => e.WalletId)
                .WillCascadeOnDelete(false);
        }
    }
}

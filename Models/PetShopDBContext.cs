using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PetShopAPI.Models
{
    public partial class PetShopDBContext : DbContext
    {
        public PetShopDBContext()
        {
        }

        public PetShopDBContext(DbContextOptions<PetShopDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<CareCategory> CareCategories { get; set; }
        public virtual DbSet<CareSupply> CareSupplies { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<FoodKind> FoodKinds { get; set; }
        public virtual DbSet<Toy> Toys { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=HELENA\\SQLEXPRESS;Database=PetShopDB;UID=helena;PWD=123456789;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.Property(e => e.AnimalId).HasColumnName("ANIMAL_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<CareCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__CareCate__E7DA297C7C3BE433");

                entity.ToTable("CareCategory");

                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<CareSupply>(entity =>
            {
                entity.Property(e => e.CareSupplyId).HasColumnName("CARE_SUPPLY_ID");

                entity.Property(e => e.AnimalId).HasColumnName("ANIMAL_ID");

                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.CareSupplies)
                    .HasForeignKey(d => d.AnimalId)
                    .HasConstraintName("FK__CareSuppl__ANIMA__30F848ED");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CareSupplies)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__CareSuppl__CATEG__31EC6D26");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("Food");

                entity.Property(e => e.FoodId).HasColumnName("FOOD_ID");

                entity.Property(e => e.AnimalId).HasColumnName("ANIMAL_ID");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.EarliestExpirationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("EARLIEST_EXPIRATION_DATE");

                entity.Property(e => e.FoodKindId).HasColumnName("FOOD_KIND_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.NumberOfPackagesInStorage).HasColumnName("NUMBER_OF_PACKAGES_IN_STORAGE");

                entity.Property(e => e.PackageWeightInKg)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("PACKAGE_WEIGHT_IN_KG");

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.AnimalId)
                    .HasConstraintName("FK__Food__ANIMAL_ID__286302EC");

                entity.HasOne(d => d.FoodKind)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.FoodKindId)
                    .HasConstraintName("FK__Food__FOOD_KIND___29572725");
            });

            modelBuilder.Entity<FoodKind>(entity =>
            {
                entity.ToTable("FoodKind");

                entity.Property(e => e.FoodKindId).HasColumnName("FOOD_KIND_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Toy>(entity =>
            {
                entity.Property(e => e.ToyId).HasColumnName("TOY_ID");

                entity.Property(e => e.AnimalId).HasColumnName("ANIMAL_ID");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.Toys)
                    .HasForeignKey(d => d.AnimalId)
                    .HasConstraintName("FK__Toys__ANIMAL_ID__2C3393D0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

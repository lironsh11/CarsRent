using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project_4
{
    public partial class DbContext1 : DbContext
    {
        public DbContext1()
        {
        }

        public DbContext1(DbContextOptions<DbContext1> options)
            : base(options)
        {
        }

        public virtual DbSet<CarsInfo> CarsInfo { get; set; }
        public virtual DbSet<CarsType> CarsType { get; set; }
        public virtual DbSet<RentInfo> RentInfo { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Chaim\\source\\repos\\Project_4\\Project_4\\WebsiteDB.mdf;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarsInfo>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__CarsInfo__68A0340E0A96FE9B");

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedOnAdd();


                entity.Property(e => e.CarId)
                  .IsRequired();
                  

                entity.Property(e => e.AvailableForRent)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CarType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Picture).IsRequired();

                entity.Property(e => e.ProperForRent)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<CarsType>(entity =>
            {

                entity.HasKey(e => e.Id)
                .HasName("PK__CarsInfo__68A0340E0A96FE9B");

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedOnAdd();


                entity.Property(e => e.Model);

                entity.Property(e => e.Model).HasMaxLength(50);

                entity.Property(e => e.DayValue)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GearType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LateDayValue)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Manufacturer).IsRequired();
            });

            modelBuilder.Entity<RentInfo>(entity =>
            {
                entity.HasKey(e => e.RentId)
                    .HasName("PK__RentInfo__68A0340E670C254F");

                entity.Property(e => e.RentId)
                    .HasColumnName("RentId")
                    .ValueGeneratedOnAdd();
                
                entity.Property(e => e.CarId)
                           .IsRequired()
                           .HasMaxLength(50);

                entity.Property(e => e.RealReturnDate)
                    
                    .HasMaxLength(50);

                entity.Property(e => e.RentDate)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ReturnDate)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(10);

                entity.Property(e => e.DateOfBirth).HasMaxLength(50);

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Picture).IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

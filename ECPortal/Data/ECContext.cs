using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pk.Com.Jazz.ECP.Models;

namespace Pk.Com.Jazz.ECP.Data;

public class ECContext : IdentityDbContext<AppUser>
{
    public ECContext(DbContextOptions<ECContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppUser> AppUsers { get; set; }
    public virtual DbSet<AppUserToken> AppUserTokens { get; set; }
    public virtual DbSet<Employee> Employee { get; set; }
    public virtual DbSet<QualityScores> QualityScores { get; set; }

    public virtual DbSet<ManagersScores> ManagersScores { get; set; }

    public virtual DbSet<TrainingRequests> TrainingRequests { get; set; }

    public virtual DbSet<QuizScores> QuizScores { get; set; }

    public virtual DbSet<EC> ECs { get; set; }

    public virtual DbSet<ECRegions> ECRegions { get; set; }

    public virtual DbSet<ECAudits> ECAudits { get; set; }

    public virtual DbSet<ECGiveaways> ECGiveaways { get; set; }

    public virtual DbSet<ECStocks> ECStocks { get; set; }

    public virtual DbSet<ECTNA> ECTNAs { get; set; }

    public virtual DbSet<EmployeeCommission> EmployeeCommissions { get; set; }

    public virtual DbSet<EmployeeEDA> EmployeeEDAs { get; set; }

    public virtual DbSet<EmployeeFeedback> EmployeeFeedbacks { get; set; }

    public virtual DbSet<EmployeePerformance> EmployeePerformances { get; set; }

    public virtual DbSet<EmployeeRecognition> EmployeeRecognitions { get; set; }

    public virtual DbSet<EmployeeSales> EmployeeSales { get; set; }

    public virtual DbSet<EmployeeDeviceSale> EmployeeDeviceSales { get; set; }

    public virtual DbSet<EmployeeFourGSale> EmployeeFourGSales { get; set; }

    public virtual DbSet<EmployeeMWalletSale> EmployeeMWalletSales { get; set; }

    public virtual DbSet<EmployeePostpaidSale> EmployeePostpaidSales { get; set; }

    public virtual DbSet<EmployeePrepaidSale> EmployeePrepaidSales { get; set; }


    public virtual DbSet<ECRoxConversionSale> ECRoxConversionSales { get; set; }

    public virtual DbSet<ECRoxNewSale> ECRoxNewSales { get; set; }


    public virtual DbSet<ECSales> ECSales { get; set; }

    public virtual DbSet<ECDeviceSale> ECDeviceSales { get; set; }

    public virtual DbSet<ECFourGSale> ECFourGSales { get; set; }

    public virtual DbSet<ECMWalletSale> ECMWalletSales { get; set; }

    public virtual DbSet<ECPostpaidSale> ECPostpaidSales { get; set; }

    public virtual DbSet<ECPrepaidSale> ECPrepaidSales { get; set; }


    public virtual DbSet<EmployeeRoxConversionSale> EmployeeRoxConversionSales { get; set; }

    public virtual DbSet<EmployeeRoxNewSale> EmployeeRoxNewSales { get; set; }



    public virtual DbSet<EmployeeTargets> EmployeeTargets { get; set; }

    public virtual DbSet<ECTargets> ECTargets { get; set; }

    public virtual DbSet<EmployeeTrainings> EmployeeTrainings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=10.173.2.223;Database=ECPortal;user id=app_ecp; password=app_ecp123@");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppUser>(entity =>
        {
           /* entity.Property(e => e.EditBy)
                .IsRequired()
                .HasMaxLength(256);*/

            entity.Property(e => e.EntryDate).HasColumnType("datetime");

            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            /*entity.Property(e => e.Summary)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.Property(e => e.UserDisplayName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);*/



        });

        modelBuilder.Entity<EmployeeCommission>()
        .Property(e => e.CommissionAmount)
        .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<EC>()
                .HasOne(e => e.ECRegion)  // EC has one ECRegion
                .WithMany(r => r.ECs)     // ECRegion has many ECs
                .HasForeignKey(e => e.ECRegionID);

        modelBuilder.Entity<AppUserToken>(entity =>
        {
            entity.HasKey(e => e.AppUserTokenId);

            entity.Property(e => e.EntryDate).HasColumnType("datetime");

            entity.Property(e => e.UserAdLogin).HasMaxLength(256);

            entity.Property(e => e.TokenType).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.EmployeeNumber)
            .IsUnique();

        modelBuilder.Entity<EmployeeSales>()
            .HasOne(es => es.Employee)
            .WithMany(e => e.EmployeeSales)
            .HasForeignKey(es => es.EmployeeNumber);

       /* modelBuilder.Entity<EmployeeTargets>()
            .HasOne(et => et.Employee)
            .WithMany(e => e.EmployeeTargets)
            .HasForeignKey(et => et.EmployeeNumber);*/
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Volunteering.Data.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Campaign> Campaigns { get; set; } = null!;
        public virtual DbSet<CampaignPriority> CampaignPriorities { get; set; } = null!;
        public virtual DbSet<CampaignStatus> CampaignStatuses { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CategorySubcategory> CategorySubcategories { get; set; } = null!;
        public virtual DbSet<Donation> Donations { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<Payoff> Payoffs { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<ReportPhoto> ReportPhotos { get; set; } = null!;
        public virtual DbSet<ReportReportPhoto> ReportReportPhotos { get; set; } = null!;
        public virtual DbSet<Subcategory> Subcategories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserCampaign> UserCampaigns { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.ToTable("Campaign");

                entity.Property(e => e.Accumulated)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ApplianceDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignGoal).HasColumnType("money");

                entity.Property(e => e.CampaignName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.CampaignPriority)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.CampaignPriorityId)
                    .HasConstraintName("FK__Campaign__Campai__5FB337D6");

                entity.HasOne(d => d.CampaignStatus)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.CampaignStatusId)
                    .HasConstraintName("FK__Campaign__Campai__5EBF139D");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Campaign__Catego__5DCAEF64");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("FK__Campaign__Report__5CD6CB2B");
            });

            modelBuilder.Entity<CampaignPriority>(entity =>
            {
                entity.ToTable("CampaignPriority");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PriorityDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CampaignStatus>(entity =>
            {
                entity.ToTable("CampaignStatus");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CategorySubcategory>(entity =>
            {
                entity.ToTable("CategorySubcategory");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategorySubcategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__CategoryS__Categ__47DBAE45");

                entity.HasOne(d => d.Subcategory)
                    .WithMany(p => p.CategorySubcategories)
                    .HasForeignKey(d => d.SubcategoryId)
                    .HasConstraintName("FK__CategoryS__Subca__48CFD27E");
            });

            modelBuilder.Entity<Donation>(entity =>
            {
                entity.ToTable("Donation");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DonationValue).HasColumnType("money");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.Donations)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK__Donation__Campai__6A30C649");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Donations)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Donation__UserId__693CA210");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NewsText).IsUnicode(false);

                entity.Property(e => e.NewsTitle)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__News__UserId__3E52440B");
            });

            modelBuilder.Entity<Payoff>(entity =>
            {
                entity.ToTable("Payoff");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PayoffValue).HasColumnType("money");

                entity.Property(e => e.RecipientName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RecipientSurname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RecippientCardNumber)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.Payoffs)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payoff__Campaign__6E01572D");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ReportDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ReportName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ReportPhoto>(entity =>
            {
                entity.ToTable("ReportPhoto");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ReportReportPhoto>(entity =>
            {
                entity.ToTable("ReportReportPhoto");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportReportPhotos)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("FK__ReportRep__Repor__5535A963");

                entity.HasOne(d => d.ReportPhoto)
                    .WithMany(p => p.ReportReportPhotos)
                    .HasForeignKey(d => d.ReportPhotoId)
                    .HasConstraintName("FK__ReportRep__Repor__5629CD9C");
            });

            modelBuilder.Entity<Subcategory>(entity =>
            {
                entity.ToTable("Subcategory");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SubcategoryName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.CardNumber)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Organisation)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Speciality)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserSurname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRoleId)
                    .HasConstraintName("FK__User__UserRoleId__3A81B327");
            });

            modelBuilder.Entity<UserCampaign>(entity =>
            {
                entity.ToTable("UserCampaign");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.UserCampaigns)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK__UserCampa__Campa__656C112C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCampaigns)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserCampa__UserI__6477ECF3");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UserRoleName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

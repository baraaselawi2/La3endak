using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace La3endak.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AboutU> AboutUs { get; set; }

    public virtual DbSet<ContactU> ContactUs { get; set; }

    public virtual DbSet<Creditcard> Creditcards { get; set; }

    public virtual DbSet<EducationLevel> EducationLevels { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("DATA SOURCE=localhost:1521/orcl;USER ID=C##La3endak;PASSWORD=my_password;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##LA3ENDAK")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<AboutU>(entity =>
        {
            entity.HasKey(e => e.AboutId).HasName("SYS_C007984");

            entity.ToTable("ABOUT_US");

            entity.Property(e => e.AboutId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ABOUT_ID");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.Txt)
                .HasColumnType("CLOB")
                .HasColumnName("TXT");
        });

        modelBuilder.Entity<ContactU>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("SYS_C007978");

            entity.ToTable("CONTACT_US");

            entity.Property(e => e.ContactId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CONTACT_ID");
            entity.Property(e => e.ContactDate)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CONTACT_DATE");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FulluNmae)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FULLU_NMAE");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SUBJECT");
        });

        modelBuilder.Entity<Creditcard>(entity =>
        {
            entity.HasKey(e => e.CreditId).HasName("SYS_C007987");

            entity.ToTable("CREDITCARD");

            entity.Property(e => e.CreditId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CREDIT_ID");
            entity.Property(e => e.Balance)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("BALANCE");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CARD_NUMBER");
            entity.Property(e => e.Cvv)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CVV");
            entity.Property(e => e.ExpiredDate)
                .HasColumnType("DATE")
                .HasColumnName("EXPIRED_DATE");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Creditcards)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("SYS_C007988");
        });

        modelBuilder.Entity<EducationLevel>(entity =>
        {
            entity.HasKey(e => e.LevelId).HasName("SYS_C008018");

            entity.ToTable("EDUCATION_LEVELS");

            entity.Property(e => e.LevelId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("LEVEL_ID");
            entity.Property(e => e.Description)
                .HasColumnType("CLOB")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.LevelName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LEVEL_NAME");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("SYS_C007981");

            entity.ToTable("ORDERS");

            entity.Property(e => e.OrderId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ORDER_ID");
            entity.Property(e => e.AdditionalNotes)
                .HasColumnType("CLOB")
                .HasColumnName("ADDITIONAL_NOTES");
            entity.Property(e => e.RequestedDate)
                .HasColumnType("DATE")
                .HasColumnName("REQUESTED_DATE");
            entity.Property(e => e.RequestedTime)
                .HasPrecision(6)
                .HasColumnName("REQUESTED_TIME");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.SubjectId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SUBJECT_ID");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Subject).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("SYS_C007983");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("SYS_C007982");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("SYS_C007977");

            entity.ToTable("SUBJECTS");

            entity.Property(e => e.SubjectId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SUBJECT_ID");
            entity.Property(e => e.SubjectImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SUBJECT_IMAGE");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SUBJECT_NAME");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.TestimonialId).HasName("SYS_C007985");

            entity.ToTable("TESTIMONIALS");

            entity.Property(e => e.TestimonialId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TESTIMONIAL_ID");
            entity.Property(e => e.CreatedDate)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.CreatedTime)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_TIME");
            entity.Property(e => e.Rating)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RATING");
            entity.Property(e => e.Text)
                .HasColumnType("CLOB")
                .HasColumnName("TEXT");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Testimonials)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("SYS_C007986");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("SYS_C007975");

            entity.ToTable("USER_ACCOUNT");

            entity.HasIndex(e => e.Email, "SYS_C007976").IsUnique();

            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Address)
                .HasColumnType("CLOB")
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Bio)
                .HasColumnType("CLOB")
                .HasColumnName("BIO");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Experience)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EXPERIENCE");
            entity.Property(e => e.HourlyRate)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("HOURLY_RATE");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LOCATION");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PHONE");
            entity.Property(e => e.PreferredSubject)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PREFERRED_SUBJECT");
            entity.Property(e => e.StudentClass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STUDENT_CLASS");
            entity.Property(e => e.TeacherSubject)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TEACHER_SUBJECT");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("UPDATED_AT");
            entity.Property(e => e.UserImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("USER_IMAGE");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USER_NAME");
            entity.Property(e => e.UserRole)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER_ROLE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

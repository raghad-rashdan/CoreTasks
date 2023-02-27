using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace core__task.Models;

public partial class CoreTaskContext : DbContext
{
    public CoreTaskContext()
    {
    }

    public CoreTaskContext(DbContextOptions<CoreTaskContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Clinic> Clinics { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<P> Ps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=coreTask;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clinic>(entity =>
        {
            entity.HasKey(e => e.ClinicId).HasName("PK__Clinic__3347C2FD312829D2");

            entity.ToTable("Clinic");

            entity.Property(e => e.ClinicId).HasColumnName("ClinicID");
            entity.Property(e => e.ClinicDis)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ClinicImg)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ClinicName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctor__2DC00EDF73F6683B");

            entity.ToTable("Doctor");

            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.ClinicId).HasColumnName("ClinicID");
            entity.Property(e => e.DoctorEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DoctorImg)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DoctorName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Clinic).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.ClinicId)
                .HasConstraintName("FK__Doctor__ClinicID__5EBF139D");
        });

        modelBuilder.Entity<P>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__p__3213E83FAC1C38A8");

            entity.ToTable("p");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Sumofage).HasColumnName("sumofage");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

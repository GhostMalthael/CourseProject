using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace CourseProject.Model;

public partial class PaidPolyclinicContext : DbContext
{
    public PaidPolyclinicContext()
    {
    }

    public PaidPolyclinicContext(DbContextOptions<PaidPolyclinicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PolyclinicService> PolyclinicServices { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ServiceRenderedDuringVisit> ServiceRenderedDuringVisits { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<SpecializationsService> SpecializationsServices { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseMySql("server=localhost;user=root;password=1234;database=paid_polyclinic", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.2.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.IdAccount).HasName("PRIMARY");

            entity.ToTable("accounts");

            entity.HasIndex(e => e.RoleUser, "role_user");

            entity.Property(e => e.IdAccount)
                .ValueGeneratedNever()
                .HasColumnName("id_account");
            entity.Property(e => e.LoginAccount)
                .HasColumnType("text")
                .HasColumnName("login_account");
            entity.Property(e => e.PasswordAccount)
                .HasColumnType("text")
                .HasColumnName("password_account");
            entity.Property(e => e.RoleUser).HasColumnName("role_user");

            entity.HasOne(d => d.RoleUserNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleUser)
                .HasConstraintName("accounts_ibfk_1");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.IdDoctor).HasName("PRIMARY");

            entity.ToTable("doctors");

            entity.HasIndex(e => e.DoctorSpecialization, "doctor_specialization");

            entity.Property(e => e.IdDoctor)
                .ValueGeneratedNever()
                .HasColumnName("id_doctor");
            entity.Property(e => e.DoctorSpecialization).HasColumnName("doctor_specialization");
            entity.Property(e => e.FullNameDoctor)
                .HasColumnType("text")
                .HasColumnName("full_name_doctor");
            entity.Property(e => e.NumberOffice).HasColumnName("number_office");

            entity.HasOne(d => d.DoctorSpecializationNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.DoctorSpecialization)
                .HasConstraintName("doctors_ibfk_1");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.NumberMedicalCardPatient).HasName("PRIMARY");

            entity.ToTable("patients");

            entity.Property(e => e.NumberMedicalCardPatient)
                .ValueGeneratedNever()
                .HasColumnName("number_medical_card_patient");
            entity.Property(e => e.BirthDatePatient).HasColumnName("birth_date_patient");
            entity.Property(e => e.FullNamePatient)
                .HasMaxLength(100)
                .HasColumnName("full_name_patient");
            entity.Property(e => e.HomeAddressPatient)
                .HasMaxLength(200)
                .HasColumnName("home_address_patient");
            entity.Property(e => e.MedicalPolicyPatient)
                .HasMaxLength(16)
                .HasColumnName("medical_policy_patient");
            entity.Property(e => e.PassportDataPatient)
                .HasMaxLength(200)
                .HasColumnName("passport_data_patient");
            entity.Property(e => e.PhoneNumberPatient)
                .HasMaxLength(20)
                .HasColumnName("phone_number_patient");
        });

        modelBuilder.Entity<PolyclinicService>(entity =>
        {
            entity.HasKey(e => e.IdPolyclinicServices).HasName("PRIMARY");

            entity.ToTable("polyclinic_services");

            entity.Property(e => e.IdPolyclinicServices)
                .ValueGeneratedNever()
                .HasColumnName("id_polyclinic_services");
            entity.Property(e => e.NamePolyclinicService)
                .HasColumnType("text")
                .HasColumnName("name_polyclinic_service");
            entity.Property(e => e.PriceService).HasColumnName("price_service");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.IdRole)
                .ValueGeneratedNever()
                .HasColumnName("id_role");
            entity.Property(e => e.NameRole)
                .HasColumnType("text")
                .HasColumnName("name_role");
        });

        modelBuilder.Entity<ServiceRenderedDuringVisit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("service_rendered_during_visit");

            entity.HasIndex(e => e.IdService, "id_service");

            entity.HasIndex(e => e.IdVisited, "id_visited");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdService).HasColumnName("id_service");
            entity.Property(e => e.IdVisited).HasColumnName("id_visited");

            entity.HasOne(d => d.IdServiceNavigation).WithMany(p => p.ServiceRenderedDuringVisits)
                .HasForeignKey(d => d.IdService)
                .HasConstraintName("service_rendered_during_visit_ibfk_2");

            entity.HasOne(d => d.IdVisitedNavigation).WithMany(p => p.ServiceRenderedDuringVisits)
                .HasForeignKey(d => d.IdVisited)
                .HasConstraintName("service_rendered_during_visit_ibfk_1");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.IdSpecialization).HasName("PRIMARY");

            entity.ToTable("specializations");

            entity.Property(e => e.IdSpecialization)
                .ValueGeneratedNever()
                .HasColumnName("id_specialization");
            entity.Property(e => e.NameSpecialization)
                .HasColumnType("text")
                .HasColumnName("name_Specialization");
        });

        modelBuilder.Entity<SpecializationsService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("specializations_services");

            entity.HasIndex(e => e.IdService, "id_service");

            entity.HasIndex(e => e.IdSpecialization, "id_specialization");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdService).HasColumnName("id_service");
            entity.Property(e => e.IdSpecialization).HasColumnName("id_specialization");

            entity.HasOne(d => d.IdServiceNavigation).WithMany(p => p.SpecializationsServices)
                .HasForeignKey(d => d.IdService)
                .HasConstraintName("specializations_services_ibfk_2");

            entity.HasOne(d => d.IdSpecializationNavigation).WithMany(p => p.SpecializationsServices)
                .HasForeignKey(d => d.IdSpecialization)
                .HasConstraintName("specializations_services_ibfk_1");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.IdVisit).HasName("PRIMARY");

            entity.ToTable("visits");

            entity.HasIndex(e => e.DocId, "doc_id");

            entity.HasIndex(e => e.NumberMedicalCard, "number_medical_card");

            entity.Property(e => e.IdVisit).HasColumnName("id_visit");
            entity.Property(e => e.DateTimeVisit)
                .HasColumnType("datetime")
                .HasColumnName("date_time_visit");
            entity.Property(e => e.DocId).HasColumnName("doc_id");
            entity.Property(e => e.NumberMedicalCard).HasColumnName("number_medical_card");
            entity.Property(e => e.ServicesProvidedDuringVisit).HasColumnName("services_provided_during_visit");
            entity.Property(e => e.TotalCostVisit).HasColumnName("total_cost_visit");

            entity.HasOne(d => d.Doc).WithMany(p => p.Visits)
                .HasForeignKey(d => d.DocId)
                .HasConstraintName("visits_ibfk_1");

            entity.HasOne(d => d.NumberMedicalCardNavigation).WithMany(p => p.Visits)
                .HasForeignKey(d => d.NumberMedicalCard)
                .HasConstraintName("visits_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

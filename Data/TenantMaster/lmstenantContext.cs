using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DeviceManager.Api.data.tenantmaster
{
    public partial class lmstenantContext : DbContext
    {
        public lmstenantContext()
        {
        }

        public lmstenantContext(DbContextOptions<lmstenantContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Certificatemaster> Certificatemaster { get; set; }
        public virtual DbSet<Courseattachment> Courseattachment { get; set; }
        public virtual DbSet<Coursecertificatemapping> Coursecertificatemapping { get; set; }
        public virtual DbSet<Coursedetails> Coursedetails { get; set; }
        public virtual DbSet<Coursemaster> Coursemaster { get; set; }
        public virtual DbSet<Credentialdetails> Credentialdetails { get; set; }
        public virtual DbSet<Employeecoursemapping> Employeecoursemapping { get; set; }
        public virtual DbSet<Employeemaster> Employeemaster { get; set; }
        public virtual DbSet<Itemtypemaster> Itemtypemaster { get; set; }
        public virtual DbSet<Levelmaster> Levelmaster { get; set; }
        public virtual DbSet<Profilecoursemapping> Profilecoursemapping { get; set; }
        public virtual DbSet<Profilemaster> Profilemaster { get; set; }
        public virtual DbSet<Ratingmaster> Ratingmaster { get; set; }
        public virtual DbSet<Schedulemaster> Schedulemaster { get; set; }
        public virtual DbSet<Templatefieldmapping> Templatefieldmapping { get; set; }
        public virtual DbSet<Templatefields> Templatefields { get; set; }
        public virtual DbSet<Templatemaster> Templatemaster { get; set; }
        public virtual DbSet<Usercomments> Usercomments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseMySql("server=localhost;user=root;password=Jktech@1234;database=lmstenant", x => x.ServerVersion("8.0.27-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Certificatemaster>(entity =>
            {
                entity.ToTable("certificatemaster");

                entity.HasIndex(e => e.FkLevelId)
                    .HasName("Fk_Level_Certificate_idx");

                entity.HasIndex(e => e.FkTemplateId)
                    .HasName("Fk_Template_Certificate_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.FkLevel)
                    .WithMany(p => p.Certificatemaster)
                    .HasForeignKey(d => d.FkLevelId)
                    .HasConstraintName("fk_level_certificate");

                entity.HasOne(d => d.FkTemplate)
                    .WithMany(p => p.Certificatemaster)
                    .HasForeignKey(d => d.FkTemplateId)
                    .HasConstraintName("fk_Template_certificate");
            });

            modelBuilder.Entity<Courseattachment>(entity =>
            {
                entity.ToTable("courseattachment");

                entity.HasIndex(e => e.FkCourseId)
                    .HasName("Fk_Course_Attachment_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FileType)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MongoId)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.Courseattachment)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("fk_course_courseattachment");
            });

            modelBuilder.Entity<Coursecertificatemapping>(entity =>
            {
                entity.ToTable("coursecertificatemapping");

                entity.HasIndex(e => e.FkCertificateId)
                    .HasName("Fk_Certificate_Mapping_idx");

                entity.HasIndex(e => e.FkCourseId)
                    .HasName("Fk_Course_Id_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FkCertificate)
                    .WithMany(p => p.Coursecertificatemapping)
                    .HasForeignKey(d => d.FkCertificateId)
                    .HasConstraintName("fk_certificate_coursecertificate");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.Coursecertificatemapping)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("fk_course_coursecertificate");
            });

            modelBuilder.Entity<Coursedetails>(entity =>
            {
                entity.ToTable("coursedetails");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Tags)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Coursemaster>(entity =>
            {
                entity.ToTable("coursemaster");

                entity.HasIndex(e => e.FkCourseDetailsId)
                    .HasName("Fk_CourseDetail_Course_idx");

                entity.HasIndex(e => e.FkLevelId)
                    .HasName("Fk_Level_Course_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FkCourseDetails)
                    .WithMany(p => p.Coursemaster)
                    .HasForeignKey(d => d.FkCourseDetailsId)
                    .HasConstraintName("fk_coursedetails_coursemaster");

                entity.HasOne(d => d.FkLevel)
                    .WithMany(p => p.Coursemaster)
                    .HasForeignKey(d => d.FkLevelId)
                    .HasConstraintName("fk_level_coursemaster");
            });

            modelBuilder.Entity<Credentialdetails>(entity =>
            {
                entity.ToTable("credentialdetails");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SecretKey1)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SecretKey2)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SmtpPort)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SmtpServer)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserName)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Employeecoursemapping>(entity =>
            {
                entity.ToTable("employeecoursemapping");

                entity.HasIndex(e => e.FkCourseId)
                    .HasName("fk_course_employeecoursemapping_idx");

                entity.HasIndex(e => e.FkEmployeeId)
                    .HasName("fk_employee_employeecoursemapping_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.Employeecoursemapping)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("fk_course_employeecoursemapping");

                entity.HasOne(d => d.FkEmployee)
                    .WithMany(p => p.Employeecoursemapping)
                    .HasForeignKey(d => d.FkEmployeeId)
                    .HasConstraintName("fk_employee_employeecoursemapping");
            });

            modelBuilder.Entity<Employeemaster>(entity =>
            {
                entity.ToTable("employeemaster");

                entity.HasIndex(e => e.FkProfileId)
                    .HasName("fk_Profile_employee_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailId)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FirstName)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FullName)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("ImageURL")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastName)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastPassword)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PasswordExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.PasswordResetToken)
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TokenExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.UserMastercol)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserName)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.FkProfile)
                    .WithMany(p => p.Employeemaster)
                    .HasForeignKey(d => d.FkProfileId)
                    .HasConstraintName("fk_profile_employee");
            });

            modelBuilder.Entity<Itemtypemaster>(entity =>
            {
                entity.ToTable("itemtypemaster");

                entity.Property(e => e.Code)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Levelmaster>(entity =>
            {
                entity.ToTable("levelmaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Profilecoursemapping>(entity =>
            {
                entity.ToTable("profilecoursemapping");

                entity.HasIndex(e => e.FkCourseId)
                    .HasName("fk_course_profilecoursemapping_idx");

                entity.HasIndex(e => e.FkProfileId)
                    .HasName("fk_profile_profilecoursemapping_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.Profilecoursemapping)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("fk_course_profilecoursemapping");

                entity.HasOne(d => d.FkProfile)
                    .WithMany(p => p.Profilecoursemapping)
                    .HasForeignKey(d => d.FkProfileId)
                    .HasConstraintName("fk_profile_profilecoursemapping");
            });

            modelBuilder.Entity<Profilemaster>(entity =>
            {
                entity.ToTable("profilemaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Ratingmaster>(entity =>
            {
                entity.ToTable("ratingmaster");

                entity.HasIndex(e => e.FkCourseId)
                    .HasName("Fk_Course_Rating_idx");

                entity.HasIndex(e => e.FkEmployeeId)
                    .HasName("Fk_User_Rating_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Rating).HasColumnType("decimal(18,2)");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.Ratingmaster)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("fk_course_ratingmaster");

                entity.HasOne(d => d.FkEmployee)
                    .WithMany(p => p.Ratingmaster)
                    .HasForeignKey(d => d.FkEmployeeId)
                    .HasConstraintName("fk_employee_ratingmaster");
            });

            modelBuilder.Entity<Schedulemaster>(entity =>
            {
                entity.ToTable("schedulemaster");

                entity.Property(e => e.Code)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Templatefieldmapping>(entity =>
            {
                entity.ToTable("templatefieldmapping");

                entity.HasIndex(e => e.FkFieldId)
                    .HasName("Fk_Field_TemplateField_idx");

                entity.HasIndex(e => e.FkTemplateId)
                    .HasName("Fk_Template_TemplateField_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Templatefields>(entity =>
            {
                entity.ToTable("templatefields");

                entity.Property(e => e.Code)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ContentToReplace)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FieldDatabase)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FieldProperty)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FieldTable)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FieldType)
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.WhereFieldProperty)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Templatemaster>(entity =>
            {
                entity.ToTable("templatemaster");

                entity.HasIndex(e => e.FkItemTypeId)
                    .HasName("Fk_ItemType_Template_idx");

                entity.HasIndex(e => e.FkcreditionalDetailId)
                    .HasName("Fk_Credential_Template_idx");

                entity.Property(e => e.Bccaddress)
                    .HasColumnName("BCCAddress")
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Body)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Ccaddress)
                    .HasColumnName("CCAddress")
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FkcreditionalDetailId).HasColumnName("FKCreditionalDetailId");

                entity.Property(e => e.Footer)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FooterFrame)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.From)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FromName)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.HeaderFrame)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IsHtmlbody).HasColumnName("IsHTMLBody");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Regards)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Salutation)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Subject)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ToAddress)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.FkItemType)
                    .WithMany(p => p.Templatemaster)
                    .HasForeignKey(d => d.FkItemTypeId)
                    .HasConstraintName("fk_itemtype_templatemaster");

                entity.HasOne(d => d.FkcreditionalDetail)
                    .WithMany(p => p.Templatemaster)
                    .HasForeignKey(d => d.FkcreditionalDetailId)
                    .HasConstraintName("fk_credentials_templatemaster");
            });

            modelBuilder.Entity<Usercomments>(entity =>
            {
                entity.ToTable("usercomments");

                entity.HasIndex(e => e.FkCourseId)
                    .HasName("Fk_Course_UserComments_idx");

                entity.HasIndex(e => e.FkEmployeeId)
                    .HasName("Fk_User_UserComments_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(5000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.Usercomments)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("fk_course_UserComments");

                entity.HasOne(d => d.FkEmployee)
                    .WithMany(p => p.Usercomments)
                    .HasForeignKey(d => d.FkEmployeeId)
                    .HasConstraintName("fk_employee_UserComments");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

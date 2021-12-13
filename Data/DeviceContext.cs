using DeviceManager.Api.Database;
using DeviceManager.Api.Data.Views;
using Microsoft.EntityFrameworkCore;

namespace DeviceManager.Api.Data
{
    /// <summary>
    /// The device DB (entity framework's) context
    /// </summary>
    public class DeviceContext : DbContext, IDbContext
    {
        private string tenantDbConnectionString { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public DeviceContext(DbContextOptions<DeviceContext> options)
            : base(options)
        {
            // TODO: Comment below this if you are running migrations commands
            // TODO: uncomment below line of you are running the application for the first time
            //this.Database.EnsureCreated();
        }

        public DeviceContext(string connection)
        {
            tenantDbConnectionString = connection;
        }

        public virtual DbSet<CertificateMaster> CertificateMaster { get; set; }
        public virtual DbSet<CourseAttachment> CourseAttachment { get; set; }
        public virtual DbSet<CourseCertificateMapping> CourseCertificateMapping { get; set; }
        public virtual DbSet<CourseDetails> CourseDetails { get; set; }
        public virtual DbSet<CourseMaster> CourseMaster { get; set; }
        public virtual DbSet<CredentialDetails> CredentialDetails { get; set; }
        public virtual DbSet<EmployeeCourseMapping> EmployeeCourseMapping { get; set; }
        public virtual DbSet<EmployeeMaster> EmployeeMaster { get; set; }
        public virtual DbSet<ItemTypeMaster> ItemTypeMaster { get; set; }
        public virtual DbSet<LevelMaster> LevelMaster { get; set; }
        public virtual DbSet<ProfileCourseMapping> ProfileCourseMapping { get; set; }
        public virtual DbSet<ProfileMaster> ProfileMaster { get; set; }
        public virtual DbSet<RatingMaster> RatingMaster { get; set; }
        public virtual DbSet<ScheduleMaster> ScheduleMaster { get; set; }
        public virtual DbSet<TemplateFieldMapping> TemplateFieldMapping { get; set; }
        public virtual DbSet<TemplateFields> TemplateFields { get; set; }
        public virtual DbSet<TemplateMaster> TemplateMaster { get; set; }
        public virtual DbSet<UserComments> UserComments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CertificateMaster>(entity =>
            {
                entity.ToTable("CertificateMaster");

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
                    .WithMany(p => p.CertificateMaster)
                    .HasForeignKey(d => d.FkLevelId)
                    .HasConstraintName("fk_level_certificate");

                entity.HasOne(d => d.FkTemplate)
                    .WithMany(p => p.CertificateMaster)
                    .HasForeignKey(d => d.FkTemplateId)
                    .HasConstraintName("fk_Template_certificate");
            });

            modelBuilder.Entity<CourseAttachment>(entity =>
            {
                entity.ToTable("CourseAttachment");

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
                    .WithMany(p => p.CourseAttachment)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("fk_course_CourseAttachment");
            });

            modelBuilder.Entity<CourseCertificateMapping>(entity =>
            {
                entity.ToTable("CourseCertificateMapping");

                entity.HasIndex(e => e.FkCertificateId)
                    .HasName("Fk_Certificate_Mapping_idx");

                entity.HasIndex(e => e.FkCourseId)
                    .HasName("Fk_Course_Id_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FkCertificate)
                    .WithMany(p => p.CourseCertificateMapping)
                    .HasForeignKey(d => d.FkCertificateId)
                    .HasConstraintName("fk_certificate_coursecertificate");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.CourseCertificateMapping)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("fk_course_coursecertificate");
            });

            modelBuilder.Entity<CourseDetails>(entity =>
            {
                entity.ToTable("CourseDetails");

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

            modelBuilder.Entity<CourseMaster>(entity =>
            {
                entity.ToTable("CourseMaster");

                entity.HasIndex(e => e.FkCourseDetailsId)
                    .HasName("Fk_CourseDetail_Course_idx");

                entity.HasIndex(e => e.FkLevelId)
                    .HasName("Fk_Level_Course_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FkCourseDetails)
                    .WithMany(p => p.CourseMaster)
                    .HasForeignKey(d => d.FkCourseDetailsId)
                    .HasConstraintName("fk_CourseDetails_CourseMaster");

                entity.HasOne(d => d.FkLevel)
                    .WithMany(p => p.CourseMaster)
                    .HasForeignKey(d => d.FkLevelId)
                    .HasConstraintName("fk_level_CourseMaster");
            });

            modelBuilder.Entity<CredentialDetails>(entity =>
            {
                entity.ToTable("CredentialDetails");

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

            modelBuilder.Entity<EmployeeCourseMapping>(entity =>
            {
                entity.ToTable("EmployeeCourseMapping");

                entity.HasIndex(e => e.FkCourseId)
                    .HasName("fk_course_EmployeeCourseMapping_idx");

                entity.HasIndex(e => e.FkEmployeeId)
                    .HasName("fk_employee_EmployeeCourseMapping_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.EmployeeCourseMapping)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("fk_course_EmployeeCourseMapping");

                entity.HasOne(d => d.FkEmployee)
                    .WithMany(p => p.EmployeeCourseMapping)
                    .HasForeignKey(d => d.FkEmployeeId)
                    .HasConstraintName("fk_employee_EmployeeCourseMapping");
            });

            modelBuilder.Entity<EmployeeMaster>(entity =>
            {
                entity.ToTable("EmployeeMaster");

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
                    .WithMany(p => p.EmployeeMaster)
                    .HasForeignKey(d => d.FkProfileId)
                    .HasConstraintName("fk_profile_employee");
            });

            modelBuilder.Entity<ItemTypeMaster>(entity =>
            {
                entity.ToTable("ItemTypeMaster");

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

            modelBuilder.Entity<LevelMaster>(entity =>
            {
                entity.ToTable("LevelMaster");

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

            modelBuilder.Entity<ProfileCourseMapping>(entity =>
            {
                entity.ToTable("ProfileCourseMapping");

                entity.HasIndex(e => e.FkCourseId)
                    .HasName("fk_course_ProfileCourseMapping_idx");

                entity.HasIndex(e => e.FkProfileId)
                    .HasName("fk_profile_ProfileCourseMapping_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.ProfileCourseMapping)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("fk_course_ProfileCourseMapping");

                entity.HasOne(d => d.FkProfile)
                    .WithMany(p => p.ProfileCourseMapping)
                    .HasForeignKey(d => d.FkProfileId)
                    .HasConstraintName("fk_profile_ProfileCourseMapping");
            });

            modelBuilder.Entity<ProfileMaster>(entity =>
            {
                entity.ToTable("ProfileMaster");

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

            modelBuilder.Entity<RatingMaster>(entity =>
            {
                entity.ToTable("RatingMaster");

                entity.HasIndex(e => e.FkCourseId)
                    .HasName("Fk_Course_Rating_idx");

                entity.HasIndex(e => e.FkEmployeeId)
                    .HasName("Fk_User_Rating_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Rating).HasColumnType("decimal(18,2)");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.RatingMaster)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("fk_course_RatingMaster");

                entity.HasOne(d => d.FkEmployee)
                    .WithMany(p => p.RatingMaster)
                    .HasForeignKey(d => d.FkEmployeeId)
                    .HasConstraintName("fk_employee_RatingMaster");
            });

            modelBuilder.Entity<ScheduleMaster>(entity =>
            {
                entity.ToTable("ScheduleMaster");

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

            modelBuilder.Entity<TemplateFieldMapping>(entity =>
            {
                entity.ToTable("TemplateFieldMapping");

                entity.HasIndex(e => e.FkFieldId)
                    .HasName("Fk_Field_TemplateField_idx");

                entity.HasIndex(e => e.FkTemplateId)
                    .HasName("Fk_Template_TemplateField_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TemplateFields>(entity =>
            {
                entity.ToTable("TemplateFields");

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

            modelBuilder.Entity<TemplateMaster>(entity =>
            {
                entity.ToTable("TemplateMaster");

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
                    .WithMany(p => p.TemplateMaster)
                    .HasForeignKey(d => d.FkItemTypeId)
                    .HasConstraintName("fk_itemtype_TemplateMaster");

                entity.HasOne(d => d.FkcreditionalDetail)
                    .WithMany(p => p.TemplateMaster)
                    .HasForeignKey(d => d.FkcreditionalDetailId)
                    .HasConstraintName("fk_credentials_TemplateMaster");
            });

            modelBuilder.Entity<UserComments>(entity =>
            {
                entity.ToTable("UserComments");

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
                    .WithMany(p => p.UserComments)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("fk_course_UserComments");

                entity.HasOne(d => d.FkEmployee)
                    .WithMany(p => p.UserComments)
                    .HasForeignKey(d => d.FkEmployeeId)
                    .HasConstraintName("fk_employee_UserComments");
            });
        }
    }   
}

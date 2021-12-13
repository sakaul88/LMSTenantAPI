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
        public virtual DbSet<CoursePricing> CoursePricing { get; set; }
        public virtual DbSet<CredentialDetails> CredentialDetails { get; set; }
        public virtual DbSet<DetailStructureInput> DetailStructureInput { get; set; }
        public virtual DbSet<FormGridMaster> FormGridMaster { get; set; }
        public virtual DbSet<FormMaster> FormMaster { get; set; }
        public virtual DbSet<ItemTypeMaster> ItemTypeMaster { get; set; }
        public virtual DbSet<LevelMaster> LevelMaster { get; set; }
        public virtual DbSet<MastersStructureInput> MastersStructureInput { get; set; }
        public virtual DbSet<PlanCourseMapping> PlanCourseMapping { get; set; }
        public virtual DbSet<PlanMaster> PlanMaster { get; set; }
        public virtual DbSet<ProfileFormMaintenance> ProfileFormMaintenance { get; set; }
        public virtual DbSet<ProfileMaster> ProfileMaster { get; set; }
        public virtual DbSet<RatingMaster> RatingMaster { get; set; }
        public virtual DbSet<ScheduleMaster> ScheduleMaster { get; set; }
        public virtual DbSet<TabFormProfileMaintenanance> TabFormProfileMaintenanance { get; set; }
        public virtual DbSet<TemplateFieldMapping> TemplateFieldMapping { get; set; }
        public virtual DbSet<TemplateFields> TemplateFields { get; set; }
        public virtual DbSet<TemplateMaster> TemplateMaster { get; set; }
        public virtual DbSet<TenantMaster> TenantMaster { get; set; }
        public virtual DbSet<UserComments> UserComments { get; set; }
        public virtual DbSet<UserGridConfiguration> UserGridConfiguration { get; set; }
        public virtual DbSet<UserMaster> UserMaster { get; set; }
        public virtual DbSet<UserPlanMapping> UserPlanMapping { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(tenantDbConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CertificateMaster>(entity =>
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
                    .WithMany(p => p.CertificateMaster)
                    .HasForeignKey(d => d.FkLevelId)
                    .HasConstraintName("Fk_Level_Certificate");

                entity.HasOne(d => d.FkTemplate)
                    .WithMany(p => p.CertificateMaster)
                    .HasForeignKey(d => d.FkTemplateId)
                    .HasConstraintName("Fk_Template_Certificate");
            });

            modelBuilder.Entity<CourseAttachment>(entity =>
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
                    .WithMany(p => p.CourseAttachment)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("Fk_Course_Attachment");
            });

            modelBuilder.Entity<CourseCertificateMapping>(entity =>
            {
                entity.ToTable("coursecertificatemapping");

                entity.HasIndex(e => e.FkCertificateId)
                    .HasName("Fk_Certificate_Mapping_idx");

                entity.HasIndex(e => e.FkCourseId)
                    .HasName("Fk_Course_Id_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FkCertificate)
                    .WithMany(p => p.CourseCertificateMapping)
                    .HasForeignKey(d => d.FkCertificateId)
                    .HasConstraintName("Fk_Certificate_Mapping");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.CourseCertificateMapping)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("Fk_Course_Mapping");
            });

            modelBuilder.Entity<CourseDetails>(entity =>
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

            modelBuilder.Entity<CourseMaster>(entity =>
            {
                entity.ToTable("coursemaster");

                entity.HasIndex(e => e.FkCourseDetailsId)
                    .HasName("Fk_CourseDetail_Course_idx");

                entity.HasIndex(e => e.FkCoursePricingId)
                    .HasName("Fk_CoursePricing_Course_idx");

                entity.HasIndex(e => e.FkLevelId)
                    .HasName("Fk_Level_Course_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FkCourseDetails)
                    .WithMany(p => p.Coursemaster)
                    .HasForeignKey(d => d.FkCourseDetailsId)
                    .HasConstraintName("Fk_CourseDetail_Course");

                entity.HasOne(d => d.FkCoursePricing)
                    .WithMany(p => p.CourseMaster)
                    .HasForeignKey(d => d.FkCoursePricingId)
                    .HasConstraintName("Fk_CoursePricing_Course");

                entity.HasOne(d => d.FkLevel)
                    .WithMany(p => p.CourseMaster)
                    .HasForeignKey(d => d.FkLevelId)
                    .HasConstraintName("Fk_Level_Course");
            });

            modelBuilder.Entity<CoursePricing>(entity =>
            {
                entity.ToTable("coursepricing");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DiscountAllocated).HasColumnType("decimal(18,2)");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<CredentialDetails>(entity =>
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

            modelBuilder.Entity<DetailStructureInput>(entity =>
            {
                entity.ToTable("detailstructureinput");

                entity.HasIndex(e => e.FkMasterStructureId)
                    .HasName("Fk_MasterStructure_DetailStructure_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Approver)
                    .HasColumnName("approver")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.BreadCrumbAddUpdate)
                    .HasColumnName("breadCrumbAddUpdate")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Breadcrumb)
                    .HasColumnName("breadcrumb")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FkMasterStructureId).HasColumnName("fkMasterStructureId");

                entity.Property(e => e.Header)
                    .HasColumnName("header")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.HeaderTitle)
                    .HasColumnName("headerTitle")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Subheader)
                    .HasColumnName("subheader")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SubheaderTitle)
                    .HasColumnName("subheaderTitle")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp1)
                    .HasColumnName("temp1")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp10)
                    .HasColumnName("temp10")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp11)
                    .HasColumnName("temp11")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp12)
                    .HasColumnName("temp12")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp13)
                    .HasColumnName("temp13")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp14)
                    .HasColumnName("temp14")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp15)
                    .HasColumnName("temp15")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp16)
                    .HasColumnName("temp16")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp17)
                    .HasColumnName("temp17")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp18)
                    .HasColumnName("temp18")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp19)
                    .HasColumnName("temp19")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp2)
                    .HasColumnName("temp2")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp20)
                    .HasColumnName("temp20")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp21)
                    .HasColumnName("temp21")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp22)
                    .HasColumnName("temp22")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp23)
                    .HasColumnName("temp23")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp24)
                    .HasColumnName("temp24")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp25)
                    .HasColumnName("temp25")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp3)
                    .HasColumnName("temp3")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp4)
                    .HasColumnName("temp4")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp5)
                    .HasColumnName("temp5")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp6)
                    .HasColumnName("temp6")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp7)
                    .HasColumnName("temp7")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp8)
                    .HasColumnName("temp8")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp9)
                    .HasColumnName("temp9")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.FkMasterStructure)
                    .WithMany(p => p.DetailStructureInput)
                    .HasForeignKey(d => d.FkMasterStructureId)
                    .HasConstraintName("Fk_MasterStructure_DetailStructure");
            });

            modelBuilder.Entity<FormGridMaster>(entity =>
            {
                entity.ToTable("formgridmaster");

                entity.HasIndex(e => e.FkFormId)
                    .HasName("fkFormId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApiEndPoint)
                    .HasColumnName("apiEndPoint")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ButtonLabel)
                    .HasColumnName("buttonLabel")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FkFormId).HasColumnName("fkFormId");

                entity.Property(e => e.GridId)
                    .IsRequired()
                    .HasColumnName("gridId")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.HeaderTitle)
                    .HasColumnName("headerTitle")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.MappedColumns)
                    .HasColumnName("mappedColumns")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Parameter)
                    .HasColumnName("parameter")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.FkForm)
                    .WithMany(p => p.FormGridMaster)
                    .HasForeignKey(d => d.FkFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("formgridmaster_ibfk_1");
            });

            modelBuilder.Entity<FormMaster>(entity =>
            {
                entity.ToTable("formmaster");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FormDescription)
                    .HasColumnName("formDescription")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Icon)
                    .HasColumnName("icon")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.MenuId).HasColumnName("menuId");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModuleName)
                    .HasColumnName("moduleName")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SortOrder).HasColumnName("sortOrder");

                entity.Property(e => e.SubMenuId).HasColumnName("subMenuId");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<ItemTypeMaster>(entity =>
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

            modelBuilder.Entity<LevelMaster>(entity =>
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

            modelBuilder.Entity<MastersStructureInput>(entity =>
            {
                entity.ToTable("mastersstructureinput");

                entity.HasIndex(e => e.FkFormId)
                    .HasName("Fk_Form_MasterStructure_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BreadCrumb)
                    .HasColumnName("breadCrumb")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FkFormId).HasColumnName("fkFormId");

                entity.Property(e => e.Header)
                    .HasColumnName("header")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.SubHeader)
                    .HasColumnName("subHeader")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp1)
                    .HasColumnName("temp1")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp10)
                    .HasColumnName("temp10")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp11)
                    .HasColumnName("temp11")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp12)
                    .HasColumnName("temp12")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp13)
                    .HasColumnName("temp13")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp14)
                    .HasColumnName("temp14")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp15)
                    .HasColumnName("temp15")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp16)
                    .HasColumnName("temp16")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp17)
                    .HasColumnName("temp17")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp18)
                    .HasColumnName("temp18")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp19)
                    .HasColumnName("temp19")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp2)
                    .HasColumnName("temp2")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp20)
                    .HasColumnName("temp20")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp21)
                    .HasColumnName("temp21")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp22)
                    .HasColumnName("temp22")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp23)
                    .HasColumnName("temp23")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp24)
                    .HasColumnName("temp24")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp25)
                    .HasColumnName("temp25")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp3)
                    .HasColumnName("temp3")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp4)
                    .HasColumnName("temp4")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp5)
                    .HasColumnName("temp5")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp6)
                    .HasColumnName("temp6")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp7)
                    .HasColumnName("temp7")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp8)
                    .HasColumnName("temp8")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Temp9)
                    .HasColumnName("temp9")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.FkForm)
                    .WithMany(p => p.MastersStructureInput)
                    .HasForeignKey(d => d.FkFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Form_MasterStructure");
            });

            modelBuilder.Entity<PlanCourseMapping>(entity =>
            {
                entity.ToTable("plancoursemapping");

                entity.HasIndex(e => e.FkCourseId)
                    .HasName("Fk_Course_PlanMapping_idx");

                entity.HasIndex(e => e.FkPlanId)
                    .HasName("Fk_Plan_PlanMapping_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.PlanCourseMapping)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("Fk_Course_PlanMapping");

                entity.HasOne(d => d.FkPlan)
                    .WithMany(p => p.PlanCourseMapping)
                    .HasForeignKey(d => d.FkPlanId)
                    .HasConstraintName("Fk_Plan_PlanMapping");
            });

            modelBuilder.Entity<PlanMaster>(entity =>
            {
                entity.ToTable("planmaster");

                entity.HasIndex(e => e.FkScheduleId)
                    .HasName("Fk_Schedule_Plan_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.DiscountProvided).HasColumnType("decimal(18,2)");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.OriginalCost).HasColumnType("decimal(18,2)");

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18,2)");

                entity.HasOne(d => d.FkSchedule)
                    .WithMany(p => p.PlanMaster)
                    .HasForeignKey(d => d.FkScheduleId)
                    .HasConstraintName("Fk_Schedule_Plan");
            });

            modelBuilder.Entity<ProfileFormMaintenance>(entity =>
            {
                entity.ToTable("profileformmaintenance");

                entity.HasIndex(e => e.FkFormId)
                    .HasName("Fk_Form_ProfileMaintenance_idx");

                entity.HasIndex(e => e.FkProfileId)
                    .HasName("Fk_Profile_ProfileMaintenance_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FkFormId).HasColumnName("fkFormId");

                entity.Property(e => e.FkProfileId).HasColumnName("fkProfileId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.FkForm)
                    .WithMany(p => p.ProfileFormMaintenance)
                    .HasForeignKey(d => d.FkFormId)
                    .HasConstraintName("Fk_Form_ProfileMaintenance");

                entity.HasOne(d => d.FkProfile)
                    .WithMany(p => p.ProfileFormMaintenance)
                    .HasForeignKey(d => d.FkProfileId)
                    .HasConstraintName("Fk_Profile_ProfileMaintenance");
            });

            modelBuilder.Entity<ProfileMaster>(entity =>
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

            modelBuilder.Entity<RatingMaster>(entity =>
            {
                entity.ToTable("ratingmaster");

                entity.HasIndex(e => e.FkCourseId)
                    .HasName("Fk_Course_Rating_idx");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("Fk_User_Rating_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Rating).HasColumnType("decimal(18,2)");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.RatingMaster)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("Fk_Course_Rating");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.RatingMaster)
                    .HasForeignKey(d => d.FkUserId)
                    .HasConstraintName("Fk_User_Rating");
            });

            modelBuilder.Entity<ScheduleMaster>(entity =>
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

            modelBuilder.Entity<TabFormProfileMaintenanance>(entity =>
            {
                entity.ToTable("tabformprofilemaintenanance");

                entity.HasIndex(e => e.FkFormId)
                    .HasName("Fk_Form_TabProfile_idx");

                entity.HasIndex(e => e.FkProfileId)
                    .HasName("Fk_Profile_TabProfile_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FkFormId).HasColumnName("fkFormId");

                entity.Property(e => e.FkProfileId).HasColumnName("fkProfileId");

                entity.Property(e => e.FkSubFormId).HasColumnName("fkSubFormId");

                entity.Property(e => e.FkTabId).HasColumnName("fkTabId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.TabNames)
                    .IsRequired()
                    .HasColumnName("tabNames")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TabTitles)
                    .HasColumnName("tabTitles")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TabUrl)
                    .HasColumnName("tabUrl")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.FkForm)
                    .WithMany(p => p.TabFormProfileMaintenanance)
                    .HasForeignKey(d => d.FkFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Form_TabProfile");

                entity.HasOne(d => d.FkProfile)
                    .WithMany(p => p.TabFormProfileMaintenanance)
                    .HasForeignKey(d => d.FkProfileId)
                    .HasConstraintName("Fk_Profile_TabProfile");
            });

            modelBuilder.Entity<TemplateFieldMapping>(entity =>
            {
                entity.ToTable("templatefieldmapping");

                entity.HasIndex(e => e.FkFieldId)
                    .HasName("Fk_Field_TemplateField_idx");

                entity.HasIndex(e => e.FkTemplateId)
                    .HasName("Fk_Template_TemplateField_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FkField)
                    .WithMany(p => p.TemplateFieldMapping)
                    .HasForeignKey(d => d.FkFieldId)
                    .HasConstraintName("Fk_Field_TemplateField");

                entity.HasOne(d => d.FkTemplate)
                    .WithMany(p => p.TemplateFieldMapping)
                    .HasForeignKey(d => d.FkTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Template_TemplateField");
            });

            modelBuilder.Entity<TemplateFields>(entity =>
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

            modelBuilder.Entity<TemplateMaster>(entity =>
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
                    .WithMany(p => p.TemplateMaster)
                    .HasForeignKey(d => d.FkItemTypeId)
                    .HasConstraintName("Fk_ItemType_Template");

                entity.HasOne(d => d.FkcreditionalDetail)
                    .WithMany(p => p.TemplateMaster)
                    .HasForeignKey(d => d.FkcreditionalDetailId)
                    .HasConstraintName("Fk_Credential_Template");
            });

            modelBuilder.Entity<TenantMaster>(entity =>
            {
                entity.ToTable("tenantmaster");

                entity.HasIndex(e => e.FkuserId)
                    .HasName("fk_user_id");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FileSchema)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FkuserId).HasColumnName("FKUserId");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Schema)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Fkuser)
                    .WithMany(p => p.TenantMaster)
                    .HasForeignKey(d => d.FkuserId)
                    .HasConstraintName("fk_user_id");
            });

            modelBuilder.Entity<UserComments>(entity =>
            {
                entity.ToTable("usercomments");

                entity.HasIndex(e => e.FkCourseId)
                    .HasName("Fk_Course_UserComments_idx");

                entity.HasIndex(e => e.FkUserId)
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
                    .HasConstraintName("Fk_Course_UserComments");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.UserComments)
                    .HasForeignKey(d => d.FkUserId)
                    .HasConstraintName("Fk_User_UserComments");
            });

            modelBuilder.Entity<UserGridConfiguration>(entity =>
            {
                entity.ToTable("usergridconfiguration");

                entity.HasIndex(e => e.FkGridId)
                    .HasName("fkGridId");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("Fk_User_UserGridConfig_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.DefaultColumns)
                    .HasColumnName("defaultColumns")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FkGridId).HasColumnName("fkGridId");

                entity.Property(e => e.FkUserId).HasColumnName("fkUserId");

                entity.Property(e => e.GridId)
                    .HasColumnName("gridId")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.UnmappedColumns)
                    .HasColumnName("unmappedColumns")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.FkGrid)
                    .WithMany(p => p.UserGridConfiguration)
                    .HasForeignKey(d => d.FkGridId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usergridconfiguration_ibfk_1");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.UserGridConfiguration)
                    .HasForeignKey(d => d.FkUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_User_UserGridConfig");
            });

            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.ToTable("usermaster");

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

                entity.Property(e => e.IsTenant).HasDefaultValueSql("'0'");

                entity.Property(e => e.LastName)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PasswordExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserMastercol)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserName)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PasswordResetToken).HasColumnType("text");
            });

            modelBuilder.Entity<UserPlanMapping>(entity =>
            {
                entity.ToTable("userplanmapping");

                entity.HasIndex(e => e.FkPlanId)
                    .HasName("Fk_Plan_PlanMapping_idx");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("Fk_User_PlanMapping_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.Property(e => e.ValidTill).HasColumnType("datetime");

                entity.HasOne(d => d.FkPlan)
                    .WithMany(p => p.UserPlanMapping)
                    .HasForeignKey(d => d.FkPlanId)
                    .HasConstraintName("Fk_Plan_UserPlanMapping");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.UserPlanMapping)
                    .HasForeignKey(d => d.FkUserId)
                    .HasConstraintName("Fk_User_UserPlanMapping");
            });
        }
    }   
}

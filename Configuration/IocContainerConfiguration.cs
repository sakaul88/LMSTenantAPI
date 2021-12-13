using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Data.Views;
using DeviceManager.Api.Repository;
using DeviceManager.Api.Services;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.Services.Implementation;
using DeviceManager.Api.Services.Interfaces;
using DeviceManager.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeviceManager.Api.Configuration
{
    /// <summary>
    /// IOC contaner start-up configuration
    /// </summary>
    public static class IocContainerConfiguration
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void ConfigureService(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IDataBaseManager, DataBaseManager>();
            services.AddTransient<IContextFactory, ContextFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            services.AddScoped(typeof(IGenericService<ProfileMasterViewModel>), typeof(ProfileMasterService<ProfileMasterViewModel>));
            
            services.AddScoped<IUserManagerService, UserManagerService>();

            //LMS
            services.AddScoped(typeof(IGenericService<CertificateMasterViewModel>), typeof(CertificateMasterService<CertificateMasterViewModel>));
            services.AddScoped(typeof(IGenericService<CourseAttachmentViewModel>), typeof(CourseAttachmentService<CourseAttachmentViewModel>));
            services.AddScoped(typeof(IGenericService<CourseCertificateMappingViewModel>), typeof(CourseCertificateMappingService<CourseCertificateMappingViewModel>));
            services.AddScoped(typeof(IGenericService<CourseDetailsViewModel>), typeof(CourseDetailsService<CourseDetailsViewModel>));
            services.AddScoped(typeof(IGenericService<CourseMasterViewModel>), typeof(CourseMasterService<CourseMasterViewModel>));
            services.AddScoped(typeof(IGenericService<CredentialDetailsViewModel>), typeof(CredentialDetailsService<CredentialDetailsViewModel>));
            services.AddScoped(typeof(IGenericService<ItemTypeMasterViewModel>), typeof(ItemTypeMasterService<ItemTypeMasterViewModel>));
            services.AddScoped(typeof(IGenericService<LevelMasterViewModel>), typeof(LevelMasterService<LevelMasterViewModel>));
            services.AddScoped(typeof(IGenericService<RatingMasterViewModel>), typeof(RatingMasterService<RatingMasterViewModel>));
            services.AddScoped(typeof(IGenericService<ScheduleMasterViewModel>), typeof(ScheduleMasterService<ScheduleMasterViewModel>));
            services.AddScoped(typeof(IGenericService<TemplateFieldMappingViewModel>), typeof(TemplateFieldMappingService<TemplateFieldMappingViewModel>));
            services.AddScoped(typeof(IGenericService<TemplateFieldsViewModel>), typeof(TemplateFieldsService<TemplateFieldsViewModel>));
            services.AddScoped(typeof(IGenericService<TemplateMasterViewModel>), typeof(TemplateMasterService<TemplateMasterViewModel>));
            services.AddScoped(typeof(IGenericService<UserCommentsViewModel>), typeof(UserCommentsService<UserCommentsViewModel>));

            //Token Service
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
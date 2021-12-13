using AutoMapper.Configuration;
using DeviceManager.Api.Data.Views;
using DeviceManager.Api.Database;
//using DeviceManager.Api.Databases;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Mappings
{
    /// <summary>
    /// Contains objects mapping
    /// </summary>
    /// <seealso cref="MapperConfigurationExpression" />
    public class MapsProfile : MapperConfigurationExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapsProfile"/> class
        /// </summary>
        public MapsProfile()
        {
            // DeviceViewModel To Device and its reverse from Device To DeviceViewModel
            //CreateMap<DeviceViewModel, Device>()
            //    .ForMember(dest => dest.DeviceTitle, opt => opt.MapFrom(src => src.Title))
            //    .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => Guid.NewGuid()))
            //    .ForMember(x => x.DeviceDetails, opt => opt.Ignore()).ReverseMap();


            // DeviceViewModel To Device and its reverse from Device To DeviceViewModel
            CreateMap<ProfileMasterViewModel, ProfileMaster>().ReverseMap();

            //LMS
            CreateMap<CertificateMasterViewModel, CertificateMaster>().ReverseMap();
            CreateMap<CourseAttachmentViewModel, CourseAttachment>().ReverseMap();
            CreateMap<CourseCertificateMappingViewModel, CourseCertificateMapping>().ReverseMap();
            CreateMap<CourseDetailsViewModel, CourseDetails>().ReverseMap();
            CreateMap<CourseMasterViewModel, CourseMaster>().ReverseMap();
            CreateMap<CredentialDetailsViewModel, CredentialDetails>().ReverseMap();
            CreateMap<ItemTypeMasterViewModel, ItemTypeMaster>().ReverseMap();
            CreateMap<LevelMasterViewModel, LevelMaster>().ReverseMap();
            CreateMap<RatingMasterViewModel, RatingMaster>().ReverseMap();
            CreateMap<ScheduleMasterViewModel, ScheduleMaster>().ReverseMap();
            CreateMap<TemplateFieldMappingViewModel, TemplateFieldMapping>().ReverseMap();
            CreateMap<TemplateFieldsViewModel, TemplateFields>().ReverseMap();
            CreateMap<TemplateMasterViewModel, TemplateMaster>().ReverseMap();
            CreateMap<UserCommentsViewModel, UserComments>().ReverseMap();

        }
    }
}

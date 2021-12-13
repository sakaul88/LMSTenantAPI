using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Database;
using DeviceManager.Api.Repository;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.Utilities;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Services
{
    /// <inheritdoc />
    public class ProfileFormMaintenanceService<T> : IProfileFormMaintenanceService<ProfileFormMaintenanceViewModel>, IGenericService<ProfileFormMaintenanceViewModel>
    {
        private IGenericRepository<ProfileFormMaintenance> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public ProfileFormMaintenanceService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<ProfileFormMaintenance> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<ProfileFormMaintenanceViewModel>> IGenericService<ProfileFormMaintenanceViewModel>.GetAll()
        {
            IList<ProfileFormMaintenanceViewModel> models = new List<ProfileFormMaintenanceViewModel>();
            var companies = await GenericRepository.GetAll();
            foreach (var company in companies)
            {
               models.Add(mapper.Map<ProfileFormMaintenance, ProfileFormMaintenanceViewModel>(company));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<ProfileFormMaintenanceViewModel>> GetAll(int page, int pageSize)
        {
            IList<ProfileFormMaintenanceViewModel> models = new List<ProfileFormMaintenanceViewModel>();
            var users = await GenericRepository.GetAll(page, pageSize);
            foreach (var user in users)
            {
                models.Add(mapper.Map<ProfileFormMaintenance, ProfileFormMaintenanceViewModel>(user));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<ProfileFormMaintenanceViewModel>> GetAll(string include)
        {
            IList<ProfileFormMaintenanceViewModel> models = new List<ProfileFormMaintenanceViewModel>();
            var users = await GenericRepository.GetAll();
            foreach (var user in users)
            {
               models.Add(mapper.Map<ProfileFormMaintenance, ProfileFormMaintenanceViewModel>(user));
            }
            return models.AsEnumerable();
        }

        public async Task<ProfileFormMaintenanceViewModel> GetById(int id)
        {
            var device = await GenericRepository.GetById(id);
            var model = mapper.Map<ProfileFormMaintenance, ProfileFormMaintenanceViewModel>(device);
            return model;
        }

        public ProfileFormMaintenanceViewModel Create(ProfileFormMaintenanceViewModel model)
        {
            var exists = GenericRepository.GetAll("FkForm", "FkProfile")
                .Result
                .Where(i => i.FkFormId == model.FkFormId && i.FkProfileId == model.FkProfileId)
                .FirstOrDefault();
            if (exists != null)
                throw new DomainException(HttpStatusCode.Found, "User077",
                    string.Format("Form '{0}' already exists for profile '{1}'.", 
                    exists.FkForm.Name, exists.FkProfile.Name));

            var device = mapper.Map<ProfileFormMaintenanceViewModel, ProfileFormMaintenance>(model);
            var entity = GenericRepository.Create(device);
            return mapper.Map<ProfileFormMaintenance, ProfileFormMaintenanceViewModel>(entity);

        }

        public void Update(int id, ProfileFormMaintenanceViewModel model)
        {
            var device = mapper.Map<ProfileFormMaintenanceViewModel, ProfileFormMaintenance>(model);
            GenericRepository.Update(id, device);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            //deleteModel.IsActive = false;
            //deleteModel.IsDeleted = true;
            GenericRepository.Delete(deleteModel);
        }

        public Task<IEnumerable<ProfileFormMaintenanceViewModel>> GetAllWithData(int LoggedInUserId)
        {
            throw new System.NotImplementedException();
        }

        Task<IEnumerable<ProfileFormMaintenanceViewModel>> IGenericService<ProfileFormMaintenanceViewModel>.GetByAny(int value)
        {

            throw new System.NotImplementedException();
        }
    }
}

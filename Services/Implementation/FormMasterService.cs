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
    public class FormMasterService<T> : IFormMasterService<FormMasterViewModel>, IGenericService<FormMasterViewModel>
    {
        private IGenericRepository<FormMaster> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public FormMasterService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<FormMaster> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<FormMasterViewModel>> IGenericService<FormMasterViewModel>.GetAll()
        {
            IList<FormMasterViewModel> models = new List<FormMasterViewModel>();
            var companies = await GenericRepository.GetAll();
            foreach (var company in companies)
            {

               models.Add(mapper.Map<FormMaster, FormMasterViewModel>(company));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<FormMasterViewModel>> GetAll(int page, int pageSize)
        {
            IList<FormMasterViewModel> models = new List<FormMasterViewModel>();
            var users = await GenericRepository.GetAll(page, pageSize);
            foreach (var user in users)
            {
                models.Add(mapper.Map<FormMaster, FormMasterViewModel>(user));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<FormMasterViewModel>> GetAll(string include)
        {
            IList<FormMasterViewModel> models = new List<FormMasterViewModel>();
            var users = await GenericRepository.GetAll();
            foreach (var user in users)
            {
               models.Add(mapper.Map<FormMaster, FormMasterViewModel>(user));
            }
            return models.AsEnumerable();
        }

        public async Task<FormMasterViewModel> GetById(int id)
        {
            var device = await GenericRepository.GetById(id);
            var model = mapper.Map<FormMaster, FormMasterViewModel>(device);
            return model;
        }

        public FormMasterViewModel Create(FormMasterViewModel model)
        {
            var exists = GenericRepository.FindBy(i => i.Url == model.Url).Result.FirstOrDefault();
            if (exists != null)
                throw new DomainException(HttpStatusCode.Found, "User077",
                    string.Format("Form with URL {0} already exists!!", model.Url));

            var device = mapper.Map<FormMasterViewModel, FormMaster>(model);
            var entity = GenericRepository.Create(device);
            return mapper.Map<FormMaster, FormMasterViewModel>(entity);
        }

        public void Update(int id, FormMasterViewModel model)
        {
            var exists = GenericRepository
                .FindBy(i => i.Url == model.Url && i.Id != id).Result.FirstOrDefault();
            if (exists != null)
                throw new DomainException(HttpStatusCode.Found, "User077",
                    string.Format("Form with URL {0} already exists!!", model.Url));

            var device = mapper.Map<FormMasterViewModel, FormMaster>(model);
            GenericRepository.Update(id, device);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public Task<IEnumerable<FormMasterViewModel>> GetAllWithData(int LoggedInUserId)
        {
            throw new System.NotImplementedException();
        }

        Task<IEnumerable<FormMasterViewModel>> IGenericService<FormMasterViewModel>.GetByAny(int value)
        {

            throw new System.NotImplementedException();
        }
    }
}

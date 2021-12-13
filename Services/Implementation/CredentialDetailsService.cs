using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Database;
using DeviceManager.Api.Repository;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Services
{
    public class CredentialDetailsService<T> : ICredentialDetailsService<CredentialDetailsViewModel>, IGenericService<CredentialDetailsViewModel>
    {
        private IGenericRepository<CredentialDetails> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public CredentialDetailsService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<CredentialDetails> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<CredentialDetailsViewModel>> IGenericService<CredentialDetailsViewModel>.GetAll()
        {
            IList<CredentialDetailsViewModel> models = new List<CredentialDetailsViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<CredentialDetails, CredentialDetailsViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<CredentialDetailsViewModel>> GetAll(int page, int pageSize)
        {
            IList<CredentialDetailsViewModel> models = new List<CredentialDetailsViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<CredentialDetails, CredentialDetailsViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<CredentialDetailsViewModel>> GetAll(string include)
        {
            IList<CredentialDetailsViewModel> models = new List<CredentialDetailsViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<CredentialDetails, CredentialDetailsViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<CredentialDetailsViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<CredentialDetails, CredentialDetailsViewModel>(getId);
            return model;
        }

        public CredentialDetailsViewModel Create(CredentialDetailsViewModel model)
        {

            var insertModel = mapper.Map<CredentialDetailsViewModel, CredentialDetails>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<CredentialDetails, CredentialDetailsViewModel>(modelI);

        }

        public void Update(int id, CredentialDetailsViewModel model)
        {
            var updateModel = mapper.Map<CredentialDetailsViewModel, CredentialDetails>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            //deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<CredentialDetailsViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<CredentialDetailsViewModel> models = new List<CredentialDetailsViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<CredentialDetails, CredentialDetailsViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<CredentialDetailsViewModel>> IGenericService<CredentialDetailsViewModel>.GetByAny(int value)
        {
            IList<CredentialDetailsViewModel> models = new List<CredentialDetailsViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<CredentialDetails, CredentialDetailsViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}

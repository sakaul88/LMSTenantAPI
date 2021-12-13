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
    public class CertificateMasterService<T> : ICertificateMasterService<CertificateMasterViewModel>, IGenericService<CertificateMasterViewModel>
    {
        private IGenericRepository<CertificateMaster> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public CertificateMasterService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<CertificateMaster> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<CertificateMasterViewModel>> IGenericService<CertificateMasterViewModel>.GetAll()
        {
            IList<CertificateMasterViewModel> models = new List<CertificateMasterViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<CertificateMaster, CertificateMasterViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<CertificateMasterViewModel>> GetAll(int page, int pageSize)
        {
            IList<CertificateMasterViewModel> models = new List<CertificateMasterViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<CertificateMaster, CertificateMasterViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<CertificateMasterViewModel>> GetAll(string include)
        {
            IList<CertificateMasterViewModel> models = new List<CertificateMasterViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<CertificateMaster, CertificateMasterViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<CertificateMasterViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<CertificateMaster, CertificateMasterViewModel>(getId);
            return model;
        }

        public CertificateMasterViewModel Create(CertificateMasterViewModel model)
        {

            var insertModel = mapper.Map<CertificateMasterViewModel, CertificateMaster>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<CertificateMaster, CertificateMasterViewModel>(modelI);

        }

        public void Update(int id, CertificateMasterViewModel model)
        {
            var updateModel = mapper.Map<CertificateMasterViewModel, CertificateMaster>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<CertificateMasterViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<CertificateMasterViewModel> models = new List<CertificateMasterViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<CertificateMaster, CertificateMasterViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<CertificateMasterViewModel>> IGenericService<CertificateMasterViewModel>.GetByAny(int value)
        {
            IList<CertificateMasterViewModel> models = new List<CertificateMasterViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.FkLevelId == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<CertificateMaster, CertificateMasterViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}

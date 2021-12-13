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
    /// <inheritdoc />
    public class MastersStructureInputService<T> : IMastersStructureInputService<MastersStructureInputViewModel>, IGenericService<MastersStructureInputViewModel>
    {
        private IGenericRepository<MastersStructureInput> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public MastersStructureInputService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<MastersStructureInput> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<MastersStructureInputViewModel>> IGenericService<MastersStructureInputViewModel>.GetAll()
        {
            IList<MastersStructureInputViewModel> models = new List<MastersStructureInputViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<MastersStructureInput, MastersStructureInputViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<MastersStructureInputViewModel>> GetAll(int page, int pageSize)
        {
            IList<MastersStructureInputViewModel> models = new List<MastersStructureInputViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<MastersStructureInput, MastersStructureInputViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<MastersStructureInputViewModel>> GetAll(string include)
        {
            IList<MastersStructureInputViewModel> models = new List<MastersStructureInputViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<MastersStructureInput, MastersStructureInputViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<MastersStructureInputViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<MastersStructureInput, MastersStructureInputViewModel>(getId);
            return model;
        }

        public MastersStructureInputViewModel Create(MastersStructureInputViewModel model)
        {

            var insertModel = mapper.Map<MastersStructureInputViewModel, MastersStructureInput>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<MastersStructureInput, MastersStructureInputViewModel>(modelI);

        }

        public void Update(int id, MastersStructureInputViewModel model)
        {
            var updateModel = mapper.Map<MastersStructureInputViewModel, MastersStructureInput>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<MastersStructureInputViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<MastersStructureInputViewModel> models = new List<MastersStructureInputViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<MastersStructureInput, MastersStructureInputViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<MastersStructureInputViewModel>> IGenericService<MastersStructureInputViewModel>.GetByAny(int value)
        {
            IList<MastersStructureInputViewModel> models = new List<MastersStructureInputViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.FkFormId == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<MastersStructureInput, MastersStructureInputViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}

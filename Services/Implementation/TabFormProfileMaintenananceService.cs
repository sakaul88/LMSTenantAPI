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
    public class TabFormProfileMaintenananceService<T> : ITabFormProfileMaintenananceService<TabFormProfileMaintenananceViewModel>, IGenericService<TabFormProfileMaintenananceViewModel>
    {
        private IGenericRepository<TabFormProfileMaintenanance> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public TabFormProfileMaintenananceService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<TabFormProfileMaintenanance> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<TabFormProfileMaintenananceViewModel>> IGenericService<TabFormProfileMaintenananceViewModel>.GetAll()
        {
            IList<TabFormProfileMaintenananceViewModel> models = new List<TabFormProfileMaintenananceViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<TabFormProfileMaintenanance, TabFormProfileMaintenananceViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<TabFormProfileMaintenananceViewModel>> GetAll(int page, int pageSize)
        {
            IList<TabFormProfileMaintenananceViewModel> models = new List<TabFormProfileMaintenananceViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<TabFormProfileMaintenanance, TabFormProfileMaintenananceViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<TabFormProfileMaintenananceViewModel>> GetAll(string include)
        {
            IList<TabFormProfileMaintenananceViewModel> models = new List<TabFormProfileMaintenananceViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<TabFormProfileMaintenanance, TabFormProfileMaintenananceViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<TabFormProfileMaintenananceViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<TabFormProfileMaintenanance, TabFormProfileMaintenananceViewModel>(getId);
            return model;
        }

        public TabFormProfileMaintenananceViewModel Create(TabFormProfileMaintenananceViewModel model)
        {

            var insertModel = mapper.Map<TabFormProfileMaintenananceViewModel, TabFormProfileMaintenanance>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<TabFormProfileMaintenanance, TabFormProfileMaintenananceViewModel>(modelI);

        }

        public void Update(int id, TabFormProfileMaintenananceViewModel model)
        {
            var updateModel = mapper.Map<TabFormProfileMaintenananceViewModel, TabFormProfileMaintenanance>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<TabFormProfileMaintenananceViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<TabFormProfileMaintenananceViewModel> models = new List<TabFormProfileMaintenananceViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<TabFormProfileMaintenanance, TabFormProfileMaintenananceViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<TabFormProfileMaintenananceViewModel>> IGenericService<TabFormProfileMaintenananceViewModel>.GetByAny(int value)
        {
            IList<TabFormProfileMaintenananceViewModel> models = new List<TabFormProfileMaintenananceViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.FkProfileId == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<TabFormProfileMaintenanance, TabFormProfileMaintenananceViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}

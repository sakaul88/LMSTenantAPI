using System;
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
    public class RatingMasterService<T> : IRatingMasterService<RatingMasterViewModel>, IGenericService<RatingMasterViewModel>
    {
        private IGenericRepository<RatingMaster> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public RatingMasterService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<RatingMaster> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<RatingMasterViewModel>> IGenericService<RatingMasterViewModel>.GetAll()
        {
            IList<RatingMasterViewModel> models = new List<RatingMasterViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<RatingMaster, RatingMasterViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<RatingMasterViewModel>> GetAll(int page, int pageSize)
        {
            IList<RatingMasterViewModel> models = new List<RatingMasterViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<RatingMaster, RatingMasterViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<RatingMasterViewModel>> GetAll(string include)
        {
            IList<RatingMasterViewModel> models = new List<RatingMasterViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<RatingMaster, RatingMasterViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<RatingMasterViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<RatingMaster, RatingMasterViewModel>(getId);
            return model;
        }

        public RatingMasterViewModel Create(RatingMasterViewModel model)
        {

            var insertModel = mapper.Map<RatingMasterViewModel, RatingMaster>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<RatingMaster, RatingMasterViewModel>(modelI);

        }

        public void Update(int id, RatingMasterViewModel model)
        {
            var updateModel = mapper.Map<RatingMasterViewModel, RatingMaster>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<RatingMasterViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<RatingMasterViewModel> models = new List<RatingMasterViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<RatingMaster, RatingMasterViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<RatingMasterViewModel>> IGenericService<RatingMasterViewModel>.GetByAny(int value)
        {
            IList<RatingMasterViewModel> models = new List<RatingMasterViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<RatingMaster, RatingMasterViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}

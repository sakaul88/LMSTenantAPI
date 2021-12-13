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
    public class EmployeeMasterService<T> : IEmployeeMasterService<EmployeeMasterViewModel>, IGenericService<EmployeeMasterViewModel>
    {
        private IGenericRepository<EmployeeMaster> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public EmployeeMasterService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<EmployeeMaster> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<EmployeeMasterViewModel>> IGenericService<EmployeeMasterViewModel>.GetAll()
        {
            IList<EmployeeMasterViewModel> models = new List<EmployeeMasterViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<EmployeeMaster, EmployeeMasterViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<EmployeeMasterViewModel>> GetAll(int page, int pageSize)
        {
            IList<EmployeeMasterViewModel> models = new List<EmployeeMasterViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<EmployeeMaster, EmployeeMasterViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<EmployeeMasterViewModel>> GetAll(string include)
        {
            IList<EmployeeMasterViewModel> models = new List<EmployeeMasterViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<EmployeeMaster, EmployeeMasterViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<EmployeeMasterViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<EmployeeMaster, EmployeeMasterViewModel>(getId);
            return model;
        }

        public EmployeeMasterViewModel Create(EmployeeMasterViewModel model)
        {

            var insertModel = mapper.Map<EmployeeMasterViewModel, EmployeeMaster>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<EmployeeMaster, EmployeeMasterViewModel>(modelI);

        }

        public void Update(int id, EmployeeMasterViewModel model)
        {
            var updateModel = mapper.Map<EmployeeMasterViewModel, EmployeeMaster>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<EmployeeMasterViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<EmployeeMasterViewModel> models = new List<EmployeeMasterViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<EmployeeMaster, EmployeeMasterViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<EmployeeMasterViewModel>> IGenericService<EmployeeMasterViewModel>.GetByAny(int value)
        {
            IList<EmployeeMasterViewModel> models = new List<EmployeeMasterViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<EmployeeMaster, EmployeeMasterViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}
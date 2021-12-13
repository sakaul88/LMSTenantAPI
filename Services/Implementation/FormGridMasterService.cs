using AutoMapper;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Database;
using DeviceManager.Api.Repository;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.Utilities;
using DeviceManager.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeviceManager.Api.Services.Implementation
{
    public class FormGridMasterService<T> : IFormGridMasterService<FormGridMasterViewModel>, IGenericService<FormGridMasterViewModel>
    {

        private IGenericRepository<FormGridMaster> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public FormGridMasterService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<FormGridMaster> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<FormGridMasterViewModel>> IGenericService<FormGridMasterViewModel>.GetAll()
        {
            IList<FormGridMasterViewModel> models = new List<FormGridMasterViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<FormGridMaster, FormGridMasterViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<FormGridMasterViewModel>> GetAll(int page, int pageSize)
        {
            IList<FormGridMasterViewModel> models = new List<FormGridMasterViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<FormGridMaster, FormGridMasterViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<FormGridMasterViewModel>> GetAll(string include)
        {
            IList<FormGridMasterViewModel> models = new List<FormGridMasterViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<FormGridMaster, FormGridMasterViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<FormGridMasterViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<FormGridMaster, FormGridMasterViewModel>(getId);
            return model;
        }

        public FormGridMasterViewModel Create(FormGridMasterViewModel model)
        {
            var exists = GenericRepository
                .FindBy(i => i.FkFormId == model.FkFormId && i.GridId == model.GridId)
                .Result
                .FirstOrDefault();
            if (exists != null)
                throw new DomainException(HttpStatusCode.Found, "User077",
                    string.Format("Grid with name {0} already exists against the form!!", model.GridId));

            var insertModel = mapper.Map<FormGridMasterViewModel, FormGridMaster>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<FormGridMaster, FormGridMasterViewModel>(modelI);

        }

        public void Update(int id, FormGridMasterViewModel model)
        {
            var exists = GenericRepository
                .FindBy(i => i.FkFormId == model.FkFormId && i.GridId == model.GridId && i.Id != id)
                .Result
                .FirstOrDefault();
            if (exists != null)
                throw new DomainException(HttpStatusCode.Found, "User077",
                    string.Format("Grid with name {0} already exists against the form!!", model.GridId));

            var updateModel = mapper.Map<FormGridMasterViewModel, FormGridMaster>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<FormGridMasterViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<FormGridMasterViewModel> models = new List<FormGridMasterViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<FormGridMaster, FormGridMasterViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<FormGridMasterViewModel>> IGenericService<FormGridMasterViewModel>.GetByAny(int value)
        {
            IList<FormGridMasterViewModel> models = new List<FormGridMasterViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.FkFormId == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<FormGridMaster, FormGridMasterViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}

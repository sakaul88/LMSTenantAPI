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
    public class TemplateFieldsService<T> : ITemplateFieldsService<TemplateFieldsViewModel>, IGenericService<TemplateFieldsViewModel>
    {
        private IGenericRepository<TemplateFields> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public TemplateFieldsService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<TemplateFields> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<TemplateFieldsViewModel>> IGenericService<TemplateFieldsViewModel>.GetAll()
        {
            IList<TemplateFieldsViewModel> models = new List<TemplateFieldsViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<TemplateFields, TemplateFieldsViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<TemplateFieldsViewModel>> GetAll(int page, int pageSize)
        {
            IList<TemplateFieldsViewModel> models = new List<TemplateFieldsViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<TemplateFields, TemplateFieldsViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<TemplateFieldsViewModel>> GetAll(string include)
        {
            IList<TemplateFieldsViewModel> models = new List<TemplateFieldsViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<TemplateFields, TemplateFieldsViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<TemplateFieldsViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<TemplateFields, TemplateFieldsViewModel>(getId);
            return model;
        }

        public TemplateFieldsViewModel Create(TemplateFieldsViewModel model)
        {

            var insertModel = mapper.Map<TemplateFieldsViewModel, TemplateFields>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<TemplateFields, TemplateFieldsViewModel>(modelI);

        }

        public void Update(int id, TemplateFieldsViewModel model)
        {
            var updateModel = mapper.Map<TemplateFieldsViewModel, TemplateFields>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDelete = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<TemplateFieldsViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<TemplateFieldsViewModel> models = new List<TemplateFieldsViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<TemplateFields, TemplateFieldsViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<TemplateFieldsViewModel>> IGenericService<TemplateFieldsViewModel>.GetByAny(int value)
        {
            IList<TemplateFieldsViewModel> models = new List<TemplateFieldsViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<TemplateFields, TemplateFieldsViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}

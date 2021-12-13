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
    public class TemplateFieldMappingService<T> : ITemplateFieldMappingService<TemplateFieldMappingViewModel>, IGenericService<TemplateFieldMappingViewModel>
    {
        private IGenericRepository<TemplateFieldMapping> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public TemplateFieldMappingService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<TemplateFieldMapping> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<TemplateFieldMappingViewModel>> IGenericService<TemplateFieldMappingViewModel>.GetAll()
        {
            IList<TemplateFieldMappingViewModel> models = new List<TemplateFieldMappingViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<TemplateFieldMapping, TemplateFieldMappingViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<TemplateFieldMappingViewModel>> GetAll(int page, int pageSize)
        {
            IList<TemplateFieldMappingViewModel> models = new List<TemplateFieldMappingViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<TemplateFieldMapping, TemplateFieldMappingViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<TemplateFieldMappingViewModel>> GetAll(string include)
        {
            IList<TemplateFieldMappingViewModel> models = new List<TemplateFieldMappingViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<TemplateFieldMapping, TemplateFieldMappingViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<TemplateFieldMappingViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<TemplateFieldMapping, TemplateFieldMappingViewModel>(getId);
            return model;
        }

        public TemplateFieldMappingViewModel Create(TemplateFieldMappingViewModel model)
        {

            var insertModel = mapper.Map<TemplateFieldMappingViewModel, TemplateFieldMapping>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<TemplateFieldMapping, TemplateFieldMappingViewModel>(modelI);

        }

        public void Update(int id, TemplateFieldMappingViewModel model)
        {
            var updateModel = mapper.Map<TemplateFieldMappingViewModel, TemplateFieldMapping>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<TemplateFieldMappingViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<TemplateFieldMappingViewModel> models = new List<TemplateFieldMappingViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<TemplateFieldMapping, TemplateFieldMappingViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<TemplateFieldMappingViewModel>> IGenericService<TemplateFieldMappingViewModel>.GetByAny(int value)
        {
            IList<TemplateFieldMappingViewModel> models = new List<TemplateFieldMappingViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<TemplateFieldMapping, TemplateFieldMappingViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}

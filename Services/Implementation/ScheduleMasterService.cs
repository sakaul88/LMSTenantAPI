﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Database;
using DeviceManager.Api.Repository;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;


namespace DeviceManager.Api.Services.Implementation
{
    public class ScheduleMasterService<T> : IScheduleMasterService<ScheduleMasterViewModel>, IGenericService<ScheduleMasterViewModel>
    {
        private IGenericRepository<ScheduleMaster> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public ScheduleMasterService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<ScheduleMaster> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<ScheduleMasterViewModel>> IGenericService<ScheduleMasterViewModel>.GetAll()
        {
            IList<ScheduleMasterViewModel> models = new List<ScheduleMasterViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<ScheduleMaster, ScheduleMasterViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<ScheduleMasterViewModel>> GetAll(int page, int pageSize)
        {
            IList<ScheduleMasterViewModel> models = new List<ScheduleMasterViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<ScheduleMaster, ScheduleMasterViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<ScheduleMasterViewModel>> GetAll(string include)
        {
            IList<ScheduleMasterViewModel> models = new List<ScheduleMasterViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<ScheduleMaster, ScheduleMasterViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<ScheduleMasterViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<ScheduleMaster, ScheduleMasterViewModel>(getId);
            return model;
        }

        public ScheduleMasterViewModel Create(ScheduleMasterViewModel model)
        {

            var insertModel = mapper.Map<ScheduleMasterViewModel, ScheduleMaster>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<ScheduleMaster, ScheduleMasterViewModel>(modelI);

        }

        public void Update(int id, ScheduleMasterViewModel model)
        {
            var updateModel = mapper.Map<ScheduleMasterViewModel, ScheduleMaster>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<ScheduleMasterViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<ScheduleMasterViewModel> models = new List<ScheduleMasterViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<ScheduleMaster, ScheduleMasterViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<ScheduleMasterViewModel>> IGenericService<ScheduleMasterViewModel>.GetByAny(int value)
        {
            IList<ScheduleMasterViewModel> models = new List<ScheduleMasterViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<ScheduleMaster, ScheduleMasterViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}

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
    public class UserCommentsService<T> : IUserCommentsService<UserCommentsViewModel>, IGenericService<UserCommentsViewModel>
    {
        private IGenericRepository<UserComments> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public UserCommentsService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<UserComments> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<UserCommentsViewModel>> IGenericService<UserCommentsViewModel>.GetAll()
        {
            IList<UserCommentsViewModel> models = new List<UserCommentsViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<UserComments, UserCommentsViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<UserCommentsViewModel>> GetAll(int page, int pageSize)
        {
            IList<UserCommentsViewModel> models = new List<UserCommentsViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<UserComments, UserCommentsViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<UserCommentsViewModel>> GetAll(string include)
        {
            IList<UserCommentsViewModel> models = new List<UserCommentsViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<UserComments, UserCommentsViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<UserCommentsViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<UserComments, UserCommentsViewModel>(getId);
            return model;
        }

        public UserCommentsViewModel Create(UserCommentsViewModel model)
        {

            var insertModel = mapper.Map<UserCommentsViewModel, UserComments>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<UserComments, UserCommentsViewModel>(modelI);

        }

        public void Update(int id, UserCommentsViewModel model)
        {
            var updateModel = mapper.Map<UserCommentsViewModel, UserComments>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<UserCommentsViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<UserCommentsViewModel> models = new List<UserCommentsViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<UserComments, UserCommentsViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<UserCommentsViewModel>> IGenericService<UserCommentsViewModel>.GetByAny(int value)
        {
            IList<UserCommentsViewModel> models = new List<UserCommentsViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<UserComments, UserCommentsViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}

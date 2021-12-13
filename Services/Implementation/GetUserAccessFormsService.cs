using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Data.Views;
using DeviceManager.Api.Repository;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.Services.Interfaces;

namespace DeviceManager.Api.Services
{
    /// <inheritdoc />
    public class GetUserAccessFormsService<T> : IGetUserAccessFormsService<GetUserAccessFormsViewModel>, IGenericService<GetUserAccessFormsViewModel>
    {
        private IGenericRepository<GetUserAccessForms> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public GetUserAccessFormsService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<GetUserAccessForms> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<GetUserAccessFormsViewModel>> IGenericService<GetUserAccessFormsViewModel>.GetAll()
        {
            IList<GetUserAccessFormsViewModel> models = new List<GetUserAccessFormsViewModel>();
            var companies = await GenericRepository.GetAll();
            foreach (var company in companies)
            {
               models.Add(mapper.Map<GetUserAccessForms, GetUserAccessFormsViewModel>(company));
            }
            return models.OrderBy(i => i.Id).AsEnumerable();
        }

        public async Task<IEnumerable<GetUserAccessFormsViewModel>> GetAll(int page, int pageSize)
        {
            IList<GetUserAccessFormsViewModel> models = new List<GetUserAccessFormsViewModel>();
            var users = await GenericRepository.GetAll(page, pageSize);
            foreach (var user in users)
            {
                models.Add(mapper.Map<GetUserAccessForms, GetUserAccessFormsViewModel>(user));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<GetUserAccessFormsViewModel>> GetAll(string include)
        {
            IList<GetUserAccessFormsViewModel> models = new List<GetUserAccessFormsViewModel>();
            var users = await GenericRepository.GetAll();
            foreach (var user in users)
            {
               models.Add(mapper.Map<GetUserAccessForms, GetUserAccessFormsViewModel>(user));
            }
            return models.AsEnumerable();
        }

        public  async Task<GetUserAccessFormsViewModel> GetById(int id)
        {
            var device = await GenericRepository.GetById(id);
            var model = mapper.Map<GetUserAccessForms, GetUserAccessFormsViewModel>(device);
            return model;
        }

        public GetUserAccessFormsViewModel Create(GetUserAccessFormsViewModel model)
        {
          
            var device = mapper.Map<GetUserAccessFormsViewModel, GetUserAccessForms>(model);
            var entity = GenericRepository.Create(device);
            return mapper.Map<GetUserAccessForms, GetUserAccessFormsViewModel>(entity);
        }

        public void Update(int id, GetUserAccessFormsViewModel model)
        {
            var device = mapper.Map<GetUserAccessFormsViewModel, GetUserAccessForms>(model);
            GenericRepository.Update(id, device);
        }

        public void Delete(int id)
        {
            var device = Task.Run(() => GenericRepository.GetById(id)).Result;
            GenericRepository.Delete(device);
        }

        public Task<IEnumerable<GetUserAccessFormsViewModel>> GetAllWithData(int LoggedInUserId)
        {
            throw new System.NotImplementedException();
        }

        Task<IEnumerable<GetUserAccessFormsViewModel>> IGenericService<GetUserAccessFormsViewModel>.GetByAny(int value)
        {

            throw new System.NotImplementedException();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using DeviceManager.Api.Common;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Data.Views;
using DeviceManager.Api.Repository;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.Services.Interfaces;

namespace DeviceManager.Api.Services.Implementation
{
    public class GetFormDetailsService<T> : IGetFormDetails<GetFormDetailsViewModel>, IGenericService<GetFormDetailsViewModel>
    {
        private IGenericRepository<GetFormDetails> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IUserManagerService UserManagerService;
        private IPrincipal Principal;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public GetFormDetailsService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<GetFormDetails> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<GetFormDetailsViewModel>> IGenericService<GetFormDetailsViewModel>.GetAll()
        {
            IList<GetFormDetailsViewModel> models = new List<GetFormDetailsViewModel>();
            var companies = await GenericRepository.GetAll();
            foreach (var company in companies)
            {
                models.Add(mapper.Map<GetFormDetails, GetFormDetailsViewModel>(company));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<GetFormDetailsViewModel>> GetAll(int page, int pageSize)
        {
            IList<GetFormDetailsViewModel> models = new List<GetFormDetailsViewModel>();
            var users = await GenericRepository.GetAll(page, pageSize);
            foreach (var user in users)
            {
                models.Add(mapper.Map<GetFormDetails, GetFormDetailsViewModel>(user));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<GetFormDetailsViewModel>> GetAll(string include)
        {
            IList<GetFormDetailsViewModel> models = new List<GetFormDetailsViewModel>();
            var users = await GenericRepository.GetAll();
            foreach (var user in users)
            {
                models.Add(mapper.Map<GetFormDetails, GetFormDetailsViewModel>(user));
            }
            return models.AsEnumerable();
        }

        public async Task<GetFormDetailsViewModel> GetById(int id)
        {
            var device = await GenericRepository.GetAll();
            var formDetail = device.Where(i => i.Id == id).FirstOrDefault();
            var model = mapper.Map<GetFormDetails, GetFormDetailsViewModel>(formDetail);
            return model;
        }

        public GetFormDetailsViewModel Create(GetFormDetailsViewModel model)
        {

            var device = mapper.Map<GetFormDetailsViewModel, GetFormDetails>(model);
            var entity = GenericRepository.Create(device);
            return mapper.Map<GetFormDetails, GetFormDetailsViewModel>(entity);
        }

        public void Update(int id, GetFormDetailsViewModel model)
        {
            var device = mapper.Map<GetFormDetailsViewModel, GetFormDetails>(model);
            GenericRepository.Update(id, device);
        }

        public void Delete(int id)
        {
            var device = Task.Run(() => GenericRepository.GetById(id)).Result;
            GenericRepository.Delete(device);
        }

        public Task<IEnumerable<GetFormDetailsViewModel>> GetAllWithData(int LoggedInUserId)
        {
            throw new System.NotImplementedException();
        }

        async Task<IEnumerable<GetFormDetailsViewModel>> IGenericService<GetFormDetailsViewModel>.GetByAny(int value)
        {
            IList<GetFormDetailsViewModel> models = new List<GetFormDetailsViewModel>();
            var companies = await GenericRepository.FindBy(x => x.FkFormId == value);
            foreach (var company in companies)
            {
                models.Add(mapper.Map<GetFormDetails, GetFormDetailsViewModel>(company));
            }
            return models;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DeviceManager.Api.Common.DTO.LoginApproval;
using DeviceManager.Api.Common.Enum;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Database;
using DeviceManager.Api.Repository;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.Utilities;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Services
{
    /// <inheritdoc />
    public class UserMasterService<T> : IUserMasterService<UserMasterViewModel>, IGenericService<UserMasterViewModel>
    {
        private readonly IGenericRepository<UserMaster> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <inheritdoc />
        public UserMasterService(
            IUnitOfWork unitOfWork,            
            IMapper mapper, IGenericRepository<UserMaster> genericRepository)
        {
            this.unitOfWork = unitOfWork;         
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<UserMasterViewModel>> IGenericService<UserMasterViewModel>.GetAll()
        {
            IList<UserMasterViewModel> models = new List<UserMasterViewModel>();
            var users = await GenericRepository.GetAll();
            foreach (var user in users)
            {
                var model = mapper.Map<UserMaster, UserMasterViewModel>(user);
                models.Add(PopulateUserType(model));
            }
            return models.AsEnumerable();
        }

        private UserMasterViewModel PopulateUserType(UserMasterViewModel userMasterViewModel)
        {
           
            if (userMasterViewModel.isLocked!=null && userMasterViewModel.isLocked.Value)
            {
               
                userMasterViewModel.UserType = UserStatus.locked;
            }
            else if (userMasterViewModel.IsActive.Value && userMasterViewModel.PasswordExpiryDate != null && userMasterViewModel.PasswordExpiryDate <= DateTime.Now)
            {
                userMasterViewModel.UserType = UserStatus.PasswordExpired;
            }
            else if (!string.IsNullOrEmpty(userMasterViewModel.PasswordResetToken))
            {
                userMasterViewModel.UserType = UserStatus.ResetPassword;
            }
            else if ( userMasterViewModel.IsActive.Value)
            {                
                userMasterViewModel.UserType = UserStatus.Approved;
            }
            else if (userMasterViewModel.IsDeleted.Value)
            {
               
                userMasterViewModel.UserType = UserStatus.Rejected;
            }           
            else if (userMasterViewModel.isLocked != null && !userMasterViewModel.isLocked.Value && !userMasterViewModel.IsActive.Value)
            {               
                userMasterViewModel.UserType = UserStatus.Pending;
            }
           
            return userMasterViewModel;
        }


        async Task<IEnumerable<UserMasterViewModel>> IGenericService<UserMasterViewModel>.GetByAny(int value)
        {
            IList<UserMasterViewModel> models = new List<UserMasterViewModel>();
            var users = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var user in users)
           
            {               
                var model = mapper.Map<UserMaster, UserMasterViewModel>(user);
                models.Add(PopulateUserType(model));
            }
            return models.AsEnumerable();
        }


        public async Task<IEnumerable<UserMasterViewModel>> GetAll(int page, int pageSize)
        {
            IList<UserMasterViewModel> models = new List<UserMasterViewModel>();
            var users = await GenericRepository.GetAll(page, pageSize);
            foreach (var user in users)
            {
               
                var model = mapper.Map<UserMaster, UserMasterViewModel>(user);
                models.Add(PopulateUserType(model));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<UserMasterViewModel>> GetAll(string include)
        {
            IList<UserMasterViewModel> models = new List<UserMasterViewModel>();
            var users = await GenericRepository.FindBy(x => x.UserName == include);
            foreach (var user in users)
            
            {             
                var model = mapper.Map<UserMaster, UserMasterViewModel>(user);
                models.Add(PopulateUserType(model));
            }
            return models.AsEnumerable();
        }
           
        public async Task<UserMasterViewModel> GetById(int id)
        {
            var device = GenericRepository.FindBy(x => x.Id == id).Result.First();
            device.Password = PasswordManager.DecryptPwd(device.Password);
            var model = mapper.Map<UserMaster, UserMasterViewModel>(device);
          
            return PopulateUserType(model);
        }

        public UserMasterViewModel Create(UserMasterViewModel model)
        {
            var exists = GenericRepository.FindBy(i => i.UserName == model.Email).Result.FirstOrDefault();
            if (exists != null)
                throw new DomainException(HttpStatusCode.Conflict, "User077",
                    string.Format("User with email '{0}' already exists!!", model.Email));

            var device = mapper.Map<UserMasterViewModel, UserMaster>(model);
            device.Password = PasswordManager.EncryptPassword(device.Password);
            var entity = GenericRepository.Create(device);
            var userModel= mapper.Map<UserMaster, UserMasterViewModel>(entity);
            return PopulateUserType(userModel);

        }

        public void Update(int id, UserMasterViewModel model)
        {
            var device = mapper.Map<UserMasterViewModel, UserMaster>(model);
            device.Password = PasswordManager.EncryptPassword(device.Password);
            GenericRepository.Update(id, device);
            // GenericRepository.Update(id, entity);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public Task<IEnumerable<UserMasterViewModel>> GetAllWithData(int LoggedInUserId)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateLoginRequests(LoginUpdateRequest loginApprovalReq)
        {
            IEnumerable<UserMaster> userMasters = GenericRepository.FindBy(x => loginApprovalReq.UserMasterIds.Contains(x.Id)).Result;

            if (userMasters == null || !userMasters.Any() || userMasters.Count() != loginApprovalReq.UserMasterIds.Count())
            {
                var idsNotFound = string.Join(','
                    , loginApprovalReq.UserMasterIds.Where(requestedId => userMasters.All(existedIds => existedIds.Id != requestedId)));

                throw new DomainException(HttpStatusCode.NotFound, "User009", string.Format("User Ids {0} not found to update!!", idsNotFound));
            }

            switch (loginApprovalReq.RequestStatus)
            {
                case LoginUpdateRequestType.Approve:
                    userMasters.ToList().ForEach(c =>
                    {
                        c.IsDeleted = false;
                        c.IsActive = true;
                    });
                    break;
                case LoginUpdateRequestType.Reject:
                    userMasters.ToList().ForEach(c =>
                    {
                        c.IsDeleted = true;
                        c.IsActive = false;
                    });
                    break;
                case LoginUpdateRequestType.Unlock:
                    userMasters.ToList().ForEach(c =>
                    {
                        c.IsDeleted = false;
                        c.IsLocked = false;
                        c.LoginFailureCount = 0;
                        c.IsActive = true;
                    });
                    break;
            }

            GenericRepository.BulkUpdate(userMasters);
            return true;
        }

    }
}

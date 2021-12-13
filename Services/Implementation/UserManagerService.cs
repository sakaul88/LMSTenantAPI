using DeviceManager.Api.Common;
using DeviceManager.Api.Data;
using DeviceManager.Api.Data.Views;
using DeviceManager.Api.Database;
using DeviceManager.Api.Services.Interfaces;
using DeviceManager.Api.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using DeviceManager.Api.Utilities;
using System.Net;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.Repository;
using AutoMapper;

namespace DeviceManager.Api.Services.Implementation
{

    public class UserManagerService : IUserManagerService
    {
        private readonly IGenericRepository<UserMaster> UserRepository;
        private readonly IGenericRepository<GetUserAccessForms> UserAccessFormRepository;
        private readonly IGenericRepository<GetFormDetails> FormDetailsRepository;
        private readonly IGenericRepository<FormGridMaster> FormGridMasterRepository;
        private readonly IGenericRepository<TabFormProfileMaintenanance> TabFormProfileMaintenananceRepository;
        private readonly IGenericRepository<UserGridConfiguration> UserGridRepository;
        private readonly IGenericRepository<TenantMaster> TenantMasterRepository;
        private readonly IMapper mapper;
        private readonly IDbContext Context;

        public UserManagerService(
        IMapper mapper,
        IGenericRepository<UserMaster> userRepository,
        IGenericRepository<GetUserAccessForms> userAccessFormRepository,
        IGenericRepository<GetFormDetails> formDetailsRepository,
        IGenericRepository<FormGridMaster> formGridMasterRepository,
        IGenericRepository<TabFormProfileMaintenanance> tabFormProfileMaintenananceRepository,
        IGenericRepository<UserGridConfiguration> userGridRepository,
        IGenericRepository<TenantMaster> tenantMasterRepository,
        IDbContext context)
        {
            UserAccessFormRepository = userAccessFormRepository;
            FormDetailsRepository = formDetailsRepository;
            this.mapper = mapper;
            UserRepository = userRepository;
            TenantMasterRepository = tenantMasterRepository;
            FormGridMasterRepository = formGridMasterRepository;
            TabFormProfileMaintenananceRepository = tabFormProfileMaintenananceRepository;
            UserGridRepository = userGridRepository;
            Context = context;
        }

        public async Task<AuthUser> AuthenticateAndGetUserDetails(UserLoginModel model)
        {
            AuthUser authenticatedUser = new AuthUser();

            var userEntity = await UserRepository.GetAll();
            var user = userEntity.FirstOrDefault(x => x.UserName == model.EmailAddress);
            if (user == null)
            {
                throw new DomainException(HttpStatusCode.Unauthorized, "User01", "user does not exists.");
            }
            if (user.IsLocked == true)
            {
                throw new DomainException(HttpStatusCode.NotAcceptable, "User02", "your account has been locked please cordinate with system admin.");
            }
            if (user.IsActive == false)
            {
                throw new DomainException(HttpStatusCode.NotAcceptable, "User03", "your account has been not active so please cordinate with system admin.");
            }
            if (user.PasswordExpiryDate <= DateTime.UtcNow)
            {
                throw new DomainException(HttpStatusCode.Unauthorized, "User04", "your password has been expired.");
            }
            if (user.Password != PasswordManager.EncryptPassword(model.Password))
            {
                if (user.LoginFailureCount == 4)
                {
                    user.IsLocked = true;
                    user.IsActive = false;
                    user.LoginFailureCount++;
                }
                else
                    user.LoginFailureCount = user.LoginFailureCount != null ? (user.LoginFailureCount + 1) : 1;

                UserRepository.Update(user.Id, user);
                throw new DomainException(HttpStatusCode.Unauthorized, "User05", "invalid password try again.");
            }

            if (user != null)
            {
                if (user.LoginFailureCount > 0)
                {
                    user.LoginFailureCount = 0;
                    UserRepository.Update(user.Id, user);
                }

                var tenant = await  TenantMasterRepository.FindBy(i => i.FkuserId == (user.Id));

                authenticatedUser.uid = user.Id;
                authenticatedUser.email = user.UserName;
                authenticatedUser.firstName = user.FirstName;
                authenticatedUser.isapprover = true;
                authenticatedUser.isAuth = true;
                authenticatedUser.lastName = user.LastName;
                authenticatedUser.status = true;
                authenticatedUser.ulevel = 1;
                authenticatedUser.usr_level = 2;
                authenticatedUser.roles = new List<string> { "role1", "role2" };
                authenticatedUser.imgurl = user.ImageUrl;
                authenticatedUser.FkProfileId = user.FkProfileId;

                if (tenant != null && tenant.FirstOrDefault() != null)
                {
                    authenticatedUser.cmpid = tenant.FirstOrDefault().Id;
                    //authenticatedUser.countryName = company.CountryName;
                    authenticatedUser.DbName = tenant.FirstOrDefault().Schema;
                }

                //if (employee != null && employee.Count() > 0)
                //{
                //    var employeeModel = employee.FirstOrDefault();
                //    //authenticatedUser.roleId = int.Parse(employeeModel.FkRoleId.ToString());
                    
                    
                //    //authenticatedUser.uid = employeeModel.Id;
                //    authenticatedUser.fkEmpId = authenticatedUser.cmpid;

                //    if (employeeModel.FkLandingPage != null)
                //    {
                //        authenticatedUser.FkLandingPageId = employeeModel.FkLandingPage.Id;
                //        authenticatedUser.landingPageURL = employeeModel.FkLandingPage.Url;
                //    }

                //    if (employeeModel.FkDepartmentNavigation != null)
                //    {
                //        authenticatedUser.FkDepartmentId = employeeModel.FkDepartmentNavigation.Id;
                //        authenticatedUser.departmentName = employeeModel.FkDepartmentNavigation.Name;
                //    }

                //    if (employeeModel.FkLocationMasterId != null)
                //    {
                //        var location = await LocationRepository.GetById(employeeModel.FkLocationMasterId.Value);
                //        authenticatedUser.location = location.Name;
                //    }
                //    else
                //        authenticatedUser.location = "None";
                //}
            }
            return authenticatedUser;
        }

        public async Task<IEnumerable<GetUserAccessForms>> GetUserAccessForms(int id)
        {
            return await Context.Set<GetUserAccessForms>().Where(x => x.FkProfileId == id).ToListAsync();
        }

        public async Task<IEnumerable<UserGridViewModel>> GetUserGridConfigurationByAny(int userId, int formId)
        {
            var userGridConfigurations = await FormGridMasterRepository.GetAll();
            var result = userGridConfigurations
                .Where(x => x.FkFormId == formId && x.IsActive == true)
                .Select(i => UserGrid(i, userId).Result)
                .ToList();
            return result;
        }

        public async Task<IEnumerable<TabFormProfileMaintenanance>> GetUserTabConfigurationByForm(int profileId, int formId)
        {
            return await Context.Set<TabFormProfileMaintenanance>().Where(x => x.FkProfileId == profileId && x.FkFormId == formId && x.IsActive == true).ToListAsync();
        }

        public async Task<string> ForgotPassword(string email)
        {
            var token = PasswordManager.GeneratePasswordResetToken();
            try
            {
                var userEntity = await UserRepository.GetAll();
                var user = userEntity.FirstOrDefault(x => x.UserName == email);
                if (user == null)
                {
                    throw new DomainException(HttpStatusCode.NotFound, "User01", "user does not exists.");
                }
                if (user.IsLocked == true)
                {
                    throw new DomainException(HttpStatusCode.OK, "User02", "your account has been locked please cordinate with system admin.");
                }
                if (user.IsActive == false)
                {
                    throw new DomainException(HttpStatusCode.OK, "User03", "your account has been not active so please cordinate with system admin.");
                }

                if (user != null)
                {
                    user.PasswordResetToken = token;
                    user.TokenExpiryDate = DateTime.Now.AddHours(12);
                    UserRepository.Update(user.Id, user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return token;
        }

        public async Task<string> ResetPassword(ResetPasswordModel model)
        {
            if(string.IsNullOrEmpty(model.ResetPasswordToken))
                throw new DomainException(HttpStatusCode.Unauthorized, "User07", "Password reset token does not exist.");

            if (model.Password != model.ConfirmPassword)
                throw new DomainException(HttpStatusCode.NotAcceptable, "User06", "Password doesn't match with confirm password.");

            var userEntity = await UserRepository.GetAll();
            var user = userEntity.Where(i => i.PasswordResetToken == model.ResetPasswordToken).FirstOrDefault();
            if (user == null)
                throw new DomainException(HttpStatusCode.NotFound, "User08", "User does not exists.");

            if (user.TokenExpiryDate < DateTime.Now)
                throw new DomainException(HttpStatusCode.Forbidden, "User09", "Reset password token has been Expired.");

            user.TokenExpiryDate = null;
            user.PasswordResetToken = null;
            user.LastPassword = user.Password;
            user.Password = PasswordManager.EncryptPassword(model.Password);

            UserRepository.Update(user.Id, user);

            return "The password has been reset";
        }

        private async Task<UserGridViewModel> UserGrid(FormGridMaster model, int userId)
        {
            string unmappedCol = "";
            int? userConfigId = null;
            var userGridConfig = await UserGridRepository.FindBy(i => i.FkUserId == userId && i.FkGridId == model.Id);
            var configs = userGridConfig.FirstOrDefault();
            if (configs != null)
            {
                unmappedCol = configs.UnmappedColumns;
                userConfigId = configs.Id;
            }

            return new UserGridViewModel()
            {
                Id = model.Id,
                FkFormId = model.FkFormId,
                GridId = model.GridId,
                MappedColumns = model.MappedColumns,
                unmappedColumns = unmappedCol,
                apiEndPoint = model.ApiEndPoint,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                HeaderTitle = model.HeaderTitle,
                ButtonLabel = model.ButtonLabel,
                parameter = model.Parameter,
                UserGridConfigurationId = userConfigId
            };
        }
    }
}

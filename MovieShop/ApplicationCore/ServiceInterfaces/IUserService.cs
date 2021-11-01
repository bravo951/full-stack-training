using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<int> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel user);
    }
}

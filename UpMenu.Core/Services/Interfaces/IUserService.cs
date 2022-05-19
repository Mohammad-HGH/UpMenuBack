using System;
using System.Threading.Tasks;
using UpMenu.Core.DTOs.Account;
using UpMenu.DataLayer.Entities.Account;

namespace UpMenu.Core.Services.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<RegisterUserResult> RegisterUser(RegisterUserDTO register);
        Task<LoginUserResult> LoginUser(LoginUserDTO login);
        Task<User> GetUserByEmail(string email);
    }
}

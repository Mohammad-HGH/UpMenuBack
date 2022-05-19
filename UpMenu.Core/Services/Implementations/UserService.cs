using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UpMenu.Core.DTOs.Account;
using UpMenu.Core.Security;
using UpMenu.Core.Services.Interfaces;
using UpMenu.Core.Utilities.Convertors;
using UpMenu.DataLayer.Entities.Account;
using UpMenu.DataLayer.Repository;

namespace UpMenu.Core.Services.Implementations
{
    public class UserService : IUserService
    {
        private IGenericRepository<User> _userRepository;
        private IPasswordHelper passwordHelper;
        private IMailSender mailSender;
        private IViewRenderService renderView;
        public UserService(IGenericRepository<User> userRepository, IPasswordHelper passwordHelper, IMailSender mailSender, IViewRenderService renderView)
        {
            _userRepository = userRepository;
            this.passwordHelper = passwordHelper;
            this.mailSender = mailSender;
            this.renderView = renderView;
        }

        public async Task<RegisterUserResult> RegisterUser(RegisterUserDTO register)
        {
            if (IsUserExistsByEmail(register.Email))
            {
                return RegisterUserResult.EmailExists;
            }
            else
            {
                var user = new User
                {
                    Email = register.Email.SanitizeText(),
                    Address = register.Address.SanitizeText(),
                    FirstName = register.FirstName.SanitizeText(),
                    LastName = register.LastName.SanitizeText(),
                    EmailActiveCode = Guid.NewGuid().ToString(),
                    Password = passwordHelper.EncodePasswordMd5(register.Password),
                    Phone = register.Phone,
                    IsActivated = false,
                    IsDelete = false
                };
                await _userRepository.AddEntity(user);
                await _userRepository.SaveChanges();
                //var body = await renderView.RenderToStringAsync("Email/ActivateAccount", user);
                //mailSender.Send("adsalnet@gmail.com", "تست فعال سازی", body);
                return RegisterUserResult.Success;
            }
        }

        public bool IsUserExistsByEmail(string email)
        {
            return _userRepository.GetEntitiesQuery().Any(s => s.Email == email.ToLower().Trim());
        }

        public async Task<LoginUserResult> LoginUser(LoginUserDTO login)
        {
            var password = passwordHelper.EncodePasswordMd5(login.Password);
            var user = await _userRepository.GetEntitiesQuery().SingleOrDefaultAsync(s => s.Email == login.Email.ToLower().Trim() && s.Password == password);
            if (user == null)
            {
                return LoginUserResult.IncorrectData;
            }
            if (!user.IsActivated)
            {
                return LoginUserResult.NotActivated;
            }
            return LoginUserResult.Success;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetEntitiesQuery().SingleOrDefaultAsync(s => s.Email == email.ToLower().Trim());
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
        }
    }
}

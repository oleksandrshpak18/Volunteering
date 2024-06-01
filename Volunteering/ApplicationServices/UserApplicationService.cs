using System.Linq;
using Volunteering.Data.DomainServices;
using Volunteering.Data.Interfaces;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;
using Volunteering.Helpers;

namespace Volunteering.ApplicationServices
{
    public class UserApplicationService 
    {
        private UserDomainService _domainService;
        public UserApplicationService(UserDomainService domainService)
        {
            _domainService = domainService;
        }

        public AuthResult Register (UserRegisterRequest vm)
        {
            if (_domainService.FindByEmail(vm.Email) != null)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Електронна адреса вже використовується"
                    }
                };
            } 

            try
            {
                User user = _domainService.Add(vm);

                if (user != null)
                {
                    string token = _domainService.GenerateJwtToken(user);

                    return new AuthResult()
                    {
                        Result = true,
                        Token = token
                    };
                }
            }
            catch (Exception ex)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Внутрішня помилка сервера при реєстрації.",
                        ex.Message
                    }
                };
            }

            return new AuthResult()
            {
                Result = false,
                Errors = new List<string>()
                    {
                        "Помилка сервера при реєстрації.",
                    }
            };
        }


        public AuthResult Login(UserLoginRequestVM vm)
        {
            var existingUser = _domainService.FindByEmail(vm.Email);
            if ( existingUser == null)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Неправильна пошта або пароль"
                    }
                };
            }

            var isPasswordCorrect = _domainService.VerifyPassword(existingUser, vm.Password);

            if (!isPasswordCorrect)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Неправильна пошта або пароль" // no more info for security reasons
                    }
                };
            }

            var jwtToken = _domainService.GenerateJwtToken(existingUser);

            return new AuthResult()
            {
                Token = jwtToken,
                Result = true
            };

        }
        public IEnumerable<UserVM> GetAll()
        {
            return _domainService.ModelToVm(_domainService.GetAll());
        }

        public IEnumerable<UserShortInfoVM> GetTop(int count = 7)
        {
            return _domainService.GetTop(count);
        }

        public bool ?IsInfoFilled(Guid userId)
        {
            return _domainService.IsInfoFilled(userId);
        }
        
        public UserVM Update(Guid userId, UserDetailsVM user)
        {
            user.UserId = userId;
            var res = _domainService.Update(user);
            if (res == null)
            {
                return null;
            }

            return _domainService.ModelToVm(res);
        }

        public UserShortInfoVM GetShortInfo(Guid userId)
        {
            return _domainService.GetShortInfo(userId);
        }

        public UserVM GetById(Guid userId)
        {
            return _domainService.ModelToVm(_domainService.GetById(userId));
        }

        
        public UserPublicInfoVM GetPublicById(Guid userId)
        {
            return _domainService.ModelToPublicVm(_domainService.GetById(userId));
        }
    }
}

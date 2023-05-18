using Microsoft.AspNetCore.Identity;
using PasswordManagerWEBAPP.ViewModels;
using PasswordManagerWEBAPP.Models;

namespace PasswordManagerWEBAPP.Data.Repositories
{
    public interface IAccountRepository
    {
        Task<bool> SignUpUserAsync(RegisterUserViewModel user);
        Task<string> SignInUserAsync(LoginUserViewModel loginUserViewModel);
    }
}

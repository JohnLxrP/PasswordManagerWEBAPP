using PasswordManagerWEBAPP.Models;

namespace PasswordManagerWEBAPP.Data.Repositories;

public interface IPasswordmngrsRepository
{
    Task<List<Passwordmngr>> GetAllPasswordmngrs(string token);
    Task<Passwordmngr?> GetPasswordmngrById(int PasswordmngrId);
    Task<Passwordmngr?> CreatePasswordmngr(Passwordmngr newPasswordmngr);
    Task DeletePasswordmngr(int passwordmngrId, string token);
    Task<Passwordmngr?> UpdatePasswordmngr(int passwordmngrId, Passwordmngr updatedPasswordmngr);
    //Task GetAllPasswordmngrs(Passwordmngr newPasswordmngr);
}
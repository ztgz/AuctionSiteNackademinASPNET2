using System.Threading.Tasks;
using Models.ViewModels;

namespace Services.Interfaces
{
    public interface IAccountService
    {
        Task<_UsersRolesManage> GetUsersRoles();
        Task<bool>              SetUserToRole(string userEmail, string role);
    }
}

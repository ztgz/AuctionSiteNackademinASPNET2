using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Models.IdentityModels;
using Models.ViewModels;
using Services.Interfaces;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountService (UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<_UsersRolesManage> GetUsersRoles()
        {
            try
            {
                var admins   = (await _userManager.GetUsersInRoleAsync(AppUser.ROLE_ADMIN)).ToList();
                var regulars = (await _userManager.GetUsersInRoleAsync(AppUser.ROLE_REGULAR)).ToList();
                _UsersRolesManage result = new _UsersRolesManage
                {
                    AdminUsers   = admins,
                    RegularUsers = regulars 
                };
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> SetUserToRole(string userEmail, string role)
        {
            try
            {
                //Get the user
                var user = await _userManager.FindByEmailAsync(userEmail);
                //Remove him from all roles
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                //Add to the specified role
                var result = await _userManager.AddToRoleAsync(user, role);
                //TODO CHECK THERE NEEDS TO BE ATLEAST ONE ADMIN


                return result.Succeeded;
            }
            catch
            {
                return false;
            }
        }
    }
}

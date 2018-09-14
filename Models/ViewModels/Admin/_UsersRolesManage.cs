using System.Collections.Generic;
using Models.IdentityModels;

namespace Models.ViewModels
{
    public class _UsersRolesManage
    {
        public IList<AppUser> AdminUsers   { get; set; }
        public IList<AppUser> RegularUsers { get; set; }
    }
}

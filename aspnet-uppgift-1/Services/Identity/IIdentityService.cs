using aspnet_uppgift_1.Data;
using aspnet_uppgift_1.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_uppgift_1.Services.Identity
{
    public interface IIdentityService
    {
        Task CreateRootAccountAsync();

        Task<IdentityResult> CreateNewRoleAsync(string roleName);

        Task<IdentityResult> CreateNewUserAsync(ApplicationUser user, string password);

        Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string roleName);

        Task<IdentityResult> DeleteUserAsync(string id);

        IEnumerable<ApplicationUser> GetAllUsers();

        IEnumerable<IdentityRole> GetAllRoles();

        Task<IEnumerable<UserViewModel>> GetAllUserViewModelsAsync();
    }
}

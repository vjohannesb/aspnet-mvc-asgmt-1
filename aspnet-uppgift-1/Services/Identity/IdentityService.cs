using aspnet_uppgift_1.Data;
using aspnet_uppgift_1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_uppgift_1.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly ILogger<IdentityService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, 
            ILogger<IdentityService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task CreateRootAccountAsync()
        {
            var _admins = await _userManager.GetUsersInRoleAsync("Admin");
            if (!_admins.Any())
            {
                _logger.LogInformation("No admin account found - creating one.");

                var user = new ApplicationUser
                {
                    FirstName = "Admin",
                    LastName = "Account",
                    UserName = "admin@domain.com",
                    Email = "admin@domain.com"
                };

                var result = await CreateNewUserAsync(user, "BytMig123!");

                if (result.Succeeded)
                {
                    if (!_roleManager.Roles.Any())
                    {
                        await CreateNewRoleAsync("Admin");
                        await CreateNewRoleAsync("Teacher");
                        await CreateNewRoleAsync("Student");
                    }

                    await AddUserToRoleAsync(user, "Admin");
                }
            }
        }

        public async Task<IdentityResult> CreateNewRoleAsync(string roleName)
            => await _roleManager.CreateAsync(new IdentityRole(roleName));

        public async Task<IdentityResult> CreateNewUserAsync(ApplicationUser user, string password)
            => await _userManager.CreateAsync(user, password);

        public async Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string roleName)
            => await _userManager.AddToRoleAsync(user, roleName);

        public async Task<IdentityResult> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            try
            {
                return await _userManager.DeleteAsync(user);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<IdentityRole> GetAllRoles() => _roleManager.Roles;

        public IEnumerable<ApplicationUser> GetAllUsers() => _userManager.Users;

        public async Task<IEnumerable<UserViewModel>> GetAllUserViewModelsAsync()
        {
            var users = GetAllUsers();
            var userList = new List<UserViewModel>();

            foreach(var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();

                userList.Add(new UserViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ImageURI = user.ImageURI,
                    Role = role
                });
            }
            return userList;
        }
    }
}

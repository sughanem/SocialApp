using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using SocialApp.Database;

namespace SocialAppService.Infrastructure
{
    public static class UserManagerExtensions
    {
        public static async Task<IEnumerable<User>> SearchByNameAsync(this UserManager<User> userManager, string name)
        {   
            var usersList = userManager.Users
            .Where(user => user.FirstName.Contains(name) || user.LastName.Contains(name))
            .OrderBy(user => user.FirstName)
            .Take(10).ToList();
            return await Task.FromResult(usersList);
        }
    }
}
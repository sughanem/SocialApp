using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialApp.Database;
using SocialAppService.Repositories;

namespace SocialAppService.Infrastructure
{
    public static class EntityRepositoryUserPostsExtensions
    {

        public static async Task<IEnumerable<UserPost>> getUserPosts(this IEntityRepository<UserPost> userPostRepo, int userId)
        {   
            var postsList = userPostRepo.getDbContext().UserPosts
            .Where(post => post.UserID == userId)
            .OrderByDescending(post => post.DOC)
            .Take(10).ToList();
            return await Task.FromResult(postsList);
        }
    }
}
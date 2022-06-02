using System.Collections.Generic;
using System.Threading.Tasks;
using SocialAppService.Models.BindingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SocialAppService.Repositories;
using SocialAppService.Infrastructure;
using System;

namespace SocialAppService.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : ControllerBase
    {
        private readonly IEntityRepository<UserPost> repo;

        public PostsController(IEntityRepository<UserPost> repo) 
        {
            this.repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IEnumerable<UserPost>> GetPosts()
        {
            return await this.repo.RetrieveAllAsync();
        }

        [HttpGet("{UserId}", Name = nameof(GetUserPosts))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<UserPost>>> GetUserPosts(int UserId)
        {
            var post = await this.repo.getUserPosts(UserId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost("create")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<string>> Create([FromBody] CreatePostBindingModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var post = new UserPost() 
            {
                UserID = model.UserID,
                Content = model.Content,
                DOC = DateTime.Now,
                Visibility = model.Visibility,
                Likes = model.Likes
            };
            var added = await this.repo.CreateAsync(post);
            return CreatedAtRoute(
                    routeName: "GetUserPosts", 
                    routeValues: new {UserId = post.UserID},
                    value: model);
        }

        [HttpPut("{postId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int postId, [FromBody] CreatePostBindingModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existing =  repo.ifExisting(postId);
            if (existing.Equals(false))
            {
                return NotFound();
            }

            var updatedPost = new UserPost() 
            {
                UserID = model.UserID,
                Content = model.Content,
                DOC = DateTime.Now,
                Visibility = model.Visibility,
                Likes = model.Likes
            };
            await repo.UpdateAsync(postId, updatedPost);
            return new NoContentResult();
        }

        [HttpDelete("{postId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int postId)
        {
            var existing = await repo.RetrieveAsync(postId);
            if (existing == null)
            {
                return NotFound();
            }
            bool? deleted = await repo.DeleteAsync(postId);
            if (deleted.HasValue && deleted.Value)
            {
                return new NoContentResult();
            }
            else
            {
                return BadRequest();
            }
        }

        // [HttpGet("{UserId}", Name = nameof(Get))]
        // [ProducesResponseType(200)]
        // [ProducesResponseType(400)]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // public async Task<ActionResult<UserPost>> Get(int postId)
        // {
        //     var post = await this.repo.RetrieveAsync(postId);
        //     if (post == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(post);
        // }
    }
}
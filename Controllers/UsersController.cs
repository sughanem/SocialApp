using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialAppService.Models.BindingModels;
using SocialAppService.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Database;
using SocialAppService.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Text.RegularExpressions;

namespace SocialAppService.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly TokenGenerator tokenGenerator;
        private readonly Random random;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager,
        TokenGenerator tokenGenerator) 
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenGenerator = tokenGenerator;
            this.random = new Random();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var users = userManager.Users.Select( obj => new UserDTO(obj.Id, obj.FirstName, obj.LastName, 
            obj.UserName, obj.Email, obj.DOB, obj.Gender, obj.Photos, obj.About));
            return await Task.FromResult(users);
        }

        [HttpGet("{UserName}", Name = nameof(GetUser))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserDTO>> GetUser(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var UserDTO = new UserDTO(user.Id, user.FirstName, user.LastName, user.UserName,
                user.Email, user.DOB, user.Gender, user.Photos, user.About);
                return Ok(UserDTO);
            }
            return NotFound();
        }

        [HttpGet("search/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Search(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var usersList = await userManager.SearchByNameAsync(name);
                if (usersList.Any())
                {
                    var usersDTOList = new List<UserDTO>();
                    foreach (var user in usersList)
                    {
                        usersDTOList.Add( new UserDTO(user.Id, user.FirstName, user.LastName, user.UserName,
                        user.Email, user.DOB, user.Gender, user.Photos, user.About));
                    }
                    return Ok(usersDTOList);
                }
            }

            return NotFound();
        }

        [HttpPost("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<string>> Register([FromBody] SignupBindingModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await userManager.FindByEmailAsync(model.Email) != null)
            {
                return Conflict("This email is linked to an existing account!");
            }
            var user = new User() 
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = Regex.Replace(model.FirstName.ToLower() + "-" +
                 model.LastName.ToLower() + "-" + random.Next(), @"\s+", ""),
                Email = model.Email,
                DOB = model.DOB,
                Gender = model.Gender
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                    var existingUser = new UserDTO(user.Id, user.FirstName, user.LastName, user.UserName,
                    user.Email, user.DOB, user.Gender, user.Photos, user.About);
                    existingUser.Token = tokenGenerator.GenerateToken(user);
                    return Ok(existingUser);
            }
            else
            {
                return BadRequest(Task.FromResult(string.Join(",", result.Errors.Select(err => err.Description).ToArray())));  
            }
     
        }

        [HttpPost("login")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<string>> Login([FromBody] LoginBindingModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);

                if (result.Succeeded)
                {
                    var existingUser = new UserDTO(user.Id, user.FirstName, user.LastName, user.UserName,
                        user.Email, user.DOB, user.Gender, user.Photos, user.About);
                    existingUser.Token = tokenGenerator.GenerateToken(user);
                    return Ok(existingUser);
                }
            }

             return BadRequest("You've entered an invalid email or password combination!");
        }

        // [HttpPut("{id}")]
        // [ProducesResponseType(204)]
        // [ProducesResponseType(400)]
        // [ProducesResponseType(404)]
        // public async Task<IActionResult> Update(int id, [FromBody] User user)
        // {
        //     if (user == null)
        //     {
        //         return BadRequest();
        //     }
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //     var existing =  repo.ifExisting(id);
        //     if (existing.Equals(false))
        //     {
        //         return NotFound();
        //     }
        //     await repo.UpdateAsync(id, user);
        //     return new NoContentResult();
        // }

        // [HttpDelete("{id}")]
        // [ProducesResponseType(204)]
        // [ProducesResponseType(400)]
        // [ProducesResponseType(404)]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     var existing = await repo.RetrieveAsync(id);
        //     if (existing == null)
        //     {
        //         return NotFound();
        //     }
        //     bool? deleted = await repo.DeleteAsync(id);
        //     if (deleted.HasValue && deleted.Value)
        //     {
        //         return new NoContentResult();
        //     }
        //     else
        //     {
        //         return BadRequest();
        //     }
        // }
    }
}
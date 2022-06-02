using System.ComponentModel.DataAnnotations;

namespace SocialAppService.Models.BindingModels
{
    public class LoginBindingModel
    {
        [Required]
        public string Email {set; get;}

        [Required]
        public string Password {set; get;}
    }

}
using System;
using System.ComponentModel.DataAnnotations;

namespace SocialAppService.Models.BindingModels
{
    public class SignupBindingModel
    {
        [Required]
        public string FirstName {set; get;}

        [Required]
        public string LastName {set; get;}
        [Required]
        public string Email {set; get;}

        [Required]
        public string Password {set; get;}

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public char Gender { get; set; }
    }

}
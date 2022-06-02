using System;

namespace SocialAppService.Models.DTOs
{
    public class UserDTO{
        public UserDTO(int id, string firstName, string lastName, string userName, string email, DateTime dOB, char gender
        , byte[] photos, string about)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserName = userName;
            this.Email = email;
            this.DOB = dOB;
            this.Gender = gender;
            this.Photos = photos;
            this.About = about;
        }
        
        public int Id {get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public char Gender { get; set; }
        public byte[] Photos { get; set; }
        public string About { get; set; }
        public string Token { get; set; }
    }
}
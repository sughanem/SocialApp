using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SocialApp.Database
{
    [Index(nameof(Email), Name = "UniqueEmail_Users", IsUnique = true)]
    public partial class User : IdentityUser<int>, EntityBase
    {

        [Key]
        override    
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        override
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        override
        public string Email { get; set; }

        [Required]
        override
        public string PasswordHash { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DOB { get; set; }

        [Required]
        public char Gender { get; set; }

        public byte[] Photos { get; set; }

        [StringLength(1000)]
        public string About { get; set; }


        [InverseProperty(nameof(UserPage.User))]
        public virtual ICollection<UserPage> Pages { get; set; }
        
        [InverseProperty(nameof(UserPost.User))]
        public virtual ICollection<UserPost> Posts { get; set; }

    }
}

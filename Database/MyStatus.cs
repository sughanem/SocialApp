using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


#nullable disable

namespace SocialApp.Database
{

    [Index(nameof(Name), Name = "UniqueName", IsUnique = true)]
    public partial class MyStatus : EntityBase
    {
        [Key]
        [Required]
        public char StatusCode { set; get; }

        [Required]
        [StringLength(30)]
        public string Name { set; get; }

        [NotMapped]
        public int Id { get => Id; set => Id = Convert.ToInt32(StatusCode); }


    }
}

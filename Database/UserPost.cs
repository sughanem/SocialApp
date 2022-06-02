using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


#nullable disable

namespace SocialApp.Database
{
    public partial class UserPost : EntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [StringLength(2000)]
        public string Content { get; set; }

        [Column( TypeName = "date")]
        [Required]
        public DateTime DOC { get; set; }

        [Required]
        public byte Visibility { get; set; }

        public int? Likes { get; set; }

        [ForeignKey(nameof(UserID))]
        [InverseProperty("Posts")]
        public virtual User User { get; set; }
    }
}

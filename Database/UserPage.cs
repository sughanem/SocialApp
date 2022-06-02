using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


#nullable disable

namespace SocialApp.Database
{
    public partial class UserPage : EntityBase
    {
        [Key]
        public int Id { get; set;}

        [Required]
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string PageName { get; set; }

        [Column(TypeName = "date")]
        public DateTime DOC { get; set; }

        public byte[] Photos { get; set; }

        [StringLength(1000)]
        public string About { get; set; }

        public int? TotalFollowers { get; set; }


        [ForeignKey(nameof(UserID))]
        [InverseProperty("Pages")]
        public virtual User User { get; set; }
    }
}

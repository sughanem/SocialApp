using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


#nullable disable

namespace SocialApp.Database
{

    public partial class FriendshipStatus : EntityBase
    {

        [Required]
        public int RequesterId { get; }

        [Required]
        public int AddresseeId { get; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime SpecifiedDateTime { get; set; }

        [Required]
        public char StatusCode { get; set; }

        [Required]
        public int SpecifierId { get; set; }

        [NotMapped]
        public int Id { get => RequesterId; set => Id = RequesterId; }

        [ForeignKey("RequesterId, AddresseeId")]
        public virtual Friendship Friendship { get; set; }

        [ForeignKey("SpecifierId")]
        public virtual User Specifier { get; set; }

        [ForeignKey("StatusCode")]
        public virtual MyStatus Status { get; set; }
        
    
    }
}

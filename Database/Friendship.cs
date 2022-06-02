using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


#nullable disable

namespace SocialApp.Database
{

    public partial class Friendship : EntityBase
    {

        [Required]
        public int RequesterId { get; set;}

        [Required]
        public int AddresseeId { get; set;}

        [Required]
        [Column(TypeName = "date")]
        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public int Id { get => RequesterId; set => Id = RequesterId; }

        public virtual User Requster { get; set; }

        public virtual User Addressee { get; set; }

        public virtual ICollection<FriendshipStatus> FriendshipStatuses { get; set; }
    }
}

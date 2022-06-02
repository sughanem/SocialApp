using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SocialApp.Database
{
    [Keyless]
    [Table("Chat")]
    public partial class Chat
    {
        public int Sender { get; set; }
        public int Receiver { get; set; }
        [StringLength(1000)]
        public string ChatMessage { get; set; }
        [Required]
        [Column("Time_")]
        public byte[] Time { get; set; }

        [ForeignKey(nameof(Sender))]
        public virtual User SenderNavigation { get; set; }
        [ForeignKey(nameof(Receiver))]
        public virtual User ReceiverNavigation { get; set; }
    }
}

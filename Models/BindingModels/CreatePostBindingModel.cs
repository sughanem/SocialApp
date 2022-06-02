using System.ComponentModel.DataAnnotations;

namespace SocialAppService.Models.BindingModels
{
    public class CreatePostBindingModel
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        [StringLength(2000)]
        public string Content { get; set; }

        [Required]
        public byte Visibility { get; set; }

        public int? Likes { get; set; }
    }

}
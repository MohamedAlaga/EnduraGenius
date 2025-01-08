using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    public class RequestInbodyDTO
    {
        [Required]
        public string name { get; set; }
        [Required]
        [Range(0, 4)]
        public int ActivityLevel { get; set; }
    }
}

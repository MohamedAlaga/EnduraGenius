using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage = "maximum username size is 50 characters")]
        public string UserName { get; set; }

        [Required]
        [Range(40,300)]
        public float WeightInKg { get; set; }
        [Required]
        public int TallInCm { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public bool IsMale { get; set; }
    }
}

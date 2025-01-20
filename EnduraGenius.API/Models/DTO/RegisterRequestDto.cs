using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the data needed to register a new user
    /// </summary>
    public class RegisterRequestDto
    {
        /// <summary>
        /// the email of the user
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// the password of the user
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// the user name of the user account
        /// </summary>
        [Required]
        [MaxLength(50,ErrorMessage = "maximum username size is 50 characters")]
        public string UserName { get; set; }

        /// <summary>
        /// the weight of the user in kg
        /// </summary>
        [Required]
        [Range(40,300)]
        public float WeightInKg { get; set; }

        /// <summary>
        /// the height of the user in cm
        /// </summary>
        [Required]
        public int TallInCm { get; set; }

        /// <summary>
        /// the age of the user
        /// </summary>
        [Required]
        public int Age { get; set; }

        /// <summary>
        /// set true if the user is male and false if the user is female
        /// </summary>
        [Required]
        public bool IsMale { get; set; }
    }
}

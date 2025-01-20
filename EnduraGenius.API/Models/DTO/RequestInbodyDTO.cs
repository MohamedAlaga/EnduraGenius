using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the data needed to send the inbody request
    /// </summary>
    public class RequestInbodyDTO
    {
        /// <summary>
        /// the name of the user
        /// </summary>
        [Required]
        public string name { get; set; }

        /// <summary>
        /// the activity level of the user
        /// </summary>
        [Required]
        [Range(0, 4)]
        public int ActivityLevel { get; set; }
    }
}

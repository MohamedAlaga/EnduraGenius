using System.ComponentModel.DataAnnotations;

namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the data needed to send the reset pass email
    /// </summary>
    public class LeaderBoardResponseDTO
    {
        /// <summary>
        /// the user name
        /// </summary>
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// the user current points
        /// </summary>
        [Required]
        public int Points { get; set; }
    }
}

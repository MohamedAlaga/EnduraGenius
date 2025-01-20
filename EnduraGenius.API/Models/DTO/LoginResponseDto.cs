namespace EnduraGenius.API.Models.DTO
{
    /// <summary>
    /// DTO to handle the login response
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// user JWT token
        /// </summary>
        public string JwtToken { get; set; }
    }
}

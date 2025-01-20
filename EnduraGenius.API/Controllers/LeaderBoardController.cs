using EnduraGenius.API.Repositories.UserRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnduraGenius.API.Controllers
{
    /// <summary>
    /// LeaderBoard Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderBoardController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// Constructor for LeaderBoardController
        /// </summary>
        public LeaderBoardController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        /// <summary>
        /// Get the LeaderBoard of the users
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response contains LeaderBoard .
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> getUsersLeaderBoard()
        {
           var LeaderBoard =  await this._userRepository.LeaderBoard();
            return Ok(LeaderBoard);
        }
    }
}

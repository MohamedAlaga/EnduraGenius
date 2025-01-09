using EnduraGenius.API.Repositories.UserRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnduraGenius.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderBoardController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public LeaderBoardController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> getUsersLeaderBoard()
        {
           var LeaderBoard =  await this._userRepository.LeaderBoard();
            return Ok(LeaderBoard);
        }
    }
}

using Chat.Business.src.Abstraction;
using Chat.Business.src.Dto.User.Create;
using Chat.Business.src.Dto.User.Get;
using Chat.Business.src.Managers;
using Chat.Domain.src.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace RealTimeChat.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class AuthController(IAuthService _authService, IUserRepository _userRepository, JwtManager _jwtManager) : ControllerBase
	{
		[HttpPost("login")]
		[EnableRateLimiting("MessageLimiter")]

		public async Task<IActionResult> Login(UserCredentials request)
		{
			var result = await _authService.AuthenticateUserAsync(request);

			return Ok(new { AccessToken = result.AccessToken, RefreshToken = result.RefreshToken });
		}
		

		[HttpPost("Regester")]
		public async Task<IActionResult> CreateAccount(CreateUserRequests requests)
		{


			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var result = await _authService.CreateUserAsync(requests);
			return Ok(result);
		}
	}
}

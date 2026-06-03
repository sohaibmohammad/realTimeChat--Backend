using AutoMapper;
using Chat.Business.src.Abstraction;
using Chat.Business.src.Dto.User.Create;
using Chat.Business.src.Dto.User.Get;
using Chat.Business.src.Dto.User.Shared;
using Chat.Business.src.Managers;
using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Implementation
{
	public class AuthService : IAuthService

	{
 		private readonly IUserRepository _userRepository;

		private readonly JwtManager _jwtManager;

		 
 
		private readonly IRefreshTokenRepository _refreshTokenRepository;

		public AuthService(IRefreshTokenRepository refreshTokenRepository,   IUserRepository userRepository, JwtManager jwtManager)
		{
			_userRepository = userRepository;
			_jwtManager = jwtManager;
			 
			_refreshTokenRepository = refreshTokenRepository;
			
		}

		public async Task<AuthResultDto> AuthenticateUserAsync(UserCredentials userCredentials)
		{
			var user = await _userRepository.GetUserByEmailAsync(userCredentials.Email) ?? throw new ("Invalid login credentials.");
			if (!user.IsEmailVerified)
			{
				throw new ("The email is not verified");
			}
			if (user.IsDeleted)
			{
				throw new ("The account is not found");
			}
			var isAuthenticated = PassswordService.VerifyPassword(user.PasswordHash, userCredentials.Password);
			if (!isAuthenticated)
			{
				throw new ("Invalid login credentials");
			}
			var subject = "مرحباً بك مجدداً في منصة دروب 🚀";

 
 			string token = _jwtManager.GenerateAccessToken(user);
			var refreshToken = await _jwtManager.GenerateRefreshTokenAsync(user);
			return new AuthResultDto
			{
				AccessToken = token,
				RefreshToken = refreshToken
			};
		}


		public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequests requests)
		{

 			var IsValidEmail = Validator.IsValidEmail(requests.Email);
			if (!IsValidEmail)
			{
				throw new ArgumentException("Invalid Email address.");
			}

			var existingUser = await _userRepository.GetUserByEmailAsync(requests.Email);
			if (existingUser is not null)
			{
				if (!existingUser.IsEmailVerified)
				{

					existingUser.UserName = requests.UserName;
 			 

					existingUser.PasswordHash = PassswordService.HashPassword(requests.Password);
 
					await _userRepository.SaveChangesAsync();
					throw new ("Verification code resent. Please verify your email.");
				}
				throw new ("A user with this email alredy exist.");
			}

			try
			{
				var userEntity = new User
				{
				UserName=requests.UserName,
					Email = requests.Email,
					PasswordHash = PassswordService.HashPassword(requests.Password),
				
					Active = true,
					IsDeleted = false,

				};

				await _userRepository.AddAsync(userEntity);
				await _userRepository.SaveChangesAsync();
				return new CreateUserResponse
				(
					 // أو المعرف الخاص بك حسب تسميته في الـ Entity
					userEntity.UserName,
					 userEntity.Email,
					 DateTime.UtcNow
				);

			}
			catch (Exception ex)
			{
				throw new Exception("Error in register process");
			}


		}


		public Task<bool> ForgotPasswordAsync(string email)
		{
			throw new NotImplementedException();
		}

		public async Task<AuthResultDto> RefreshTokenAsync(string refreshToken)
		{
			var storedToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

			if (storedToken == null || storedToken.IsRevoked || storedToken.ExpiryDate <= DateTime.UtcNow)
			{
				throw new ("Invalid or expired refresh token.");
			}

			var user = await _userRepository.GetByIdAsync(storedToken.UserId)
			   ?? throw new ("User not found.");

			storedToken.IsRevoked = true;
			await _refreshTokenRepository.DeleteRefreshTokenAsync(refreshToken);

			string newAccessToken = _jwtManager.GenerateAccessToken(user);
			string newRefreshToken = await _jwtManager.GenerateRefreshTokenAsync(user);

			return new AuthResultDto
			{
				AccessToken = newAccessToken,
				RefreshToken = newRefreshToken
			};
		}

		public Task<User> ResetPasswordAsync(string email, string code, string newPassword)
		{
			throw new NotImplementedException();
		}

		public Task SendVerificationCodeAsync(string email)
		{
			throw new NotImplementedException();
		}

		public Task<User> VerifyEmailAsync(string email, string code)
		{
			throw new NotImplementedException();
		}

		public Task<bool> VerifyResetCodeAsync(string email, string code)
		{
			throw new NotImplementedException();
		}
	}
}

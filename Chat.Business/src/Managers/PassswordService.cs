namespace Chat.Business.src.Managers
{
	public static class PassswordService
	{
		public static string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password);
		}
		public static bool VerifyPassword(string hashedPassword, string providedPassword)
		{
			return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
		}
	}

}

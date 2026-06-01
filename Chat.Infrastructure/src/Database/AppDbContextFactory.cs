using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.src.Database
{
	public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
	{
		public AppDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
			// حط هون نفس الكونكشن سترينج تبعك للتجربة فقط
			optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=RealTimeChat;Trusted_Connection=True;TrustServerCertificate=True");

			return new AppDbContext(optionsBuilder.Options);
		}
	}
}

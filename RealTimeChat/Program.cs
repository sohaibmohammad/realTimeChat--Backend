using Chat.Business.src.Abstraction;
using Chat.Business.src.Hubs;
using Chat.Business.src.Implementation;
using Chat.Business.src.Managers;
using Chat.Business.src.Messages.Commands;
using Chat.Domain.src.Abstraction;
using Chat.Infrastructure.src.Database;
using Chat.Infrastructure.src.Messaging;
using Chat.Infrastructure.src.Repository;
using Chat.Infrastructure.src.Workers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. تسجيل الـ Repositories والـ Services (Dependency Injection)
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IParticipantRepository,ParticipantRepository>();
builder.Services.AddScoped<JwtManager>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();	
builder.Services.AddScoped<IConversationService,ConversationService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IChatNotifier, ChatNotifier>();
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMessageQueue, MessageQueue>();
builder.Services.AddHostedService<MessageWorker>();

builder.Services.AddMediatR(cfg => {
	cfg.RegisterServicesFromAssembly(typeof(SendMessageCommandHandler).Assembly);
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		var jwtSection = builder.Configuration.GetSection("JwtOptions");
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = jwtSection["Issuer"],
			ValidAudience = jwtSection["Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(jwtSection["SecretKey"])
				)
		};
		options.Events = new JwtBearerEvents
		{
			OnAuthenticationFailed = context =>
			{
				Console.WriteLine("Jwt Auth Failed" + context.Exception.Message);
				return Task.CompletedTask;
			},
			OnMessageReceived = context =>
			{
				var accessToken = context.Request.Query["access_token"];

				var path = context.HttpContext.Request.Path;
				if (!string.IsNullOrWhiteSpace(accessToken) && path.StartsWithSegments("/chathub"))
				{
					context.Token = accessToken;
				}
				return Task.CompletedTask;
			}
		};
	});


// 🔥 [تعديل 1]: تسجيل سياسة الـ CORS وبنائها (ضروري جداً عشان الـ React والـ SignalR)
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowReactApp", policy =>
	{
		policy.WithOrigins("http://localhost:3000", "http://localhost:5173") // روابط الفرونت إند المتوقعة
			  .AllowAnyHeader()
			  .AllowAnyMethod()
			  .AllowCredentials(); // الـ SignalR مستحيل يشتغل بدون هاد السطر
	});
});

builder.Services.AddRateLimiter(options => {
	options.AddFixedWindowLimiter("MessageLimiter", limiterOptions => {
		limiterOptions.PermitLimit = 5;
		limiterOptions.Window = TimeSpan.FromMinutes(1);
		limiterOptions.QueueLimit = 0;
	});

});


// 2. تسجيل خدمات الـ SignalR
builder.Services.AddSignalR();

// 3. تسجيل الـ DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🔥 [تعديل 2]: ترتيب الـ Middleware الصحيح
app.UseCors("AllowReactApp"); // 1. الـ CORS أولاً

app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
// 3. الـ Endpoints والمخارج أخيرًا
app.MapControllers();
app.MapHub<ChatHub>("/chathub");

app.Run();
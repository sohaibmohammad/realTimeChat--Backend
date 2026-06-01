using Chat.Business.src.Abstraction;
using Chat.Business.src.Hubs;
using Chat.Business.src.Implementation;
using Chat.Domain.src.Abstraction;
using Chat.Infrastructure.src.Database;
using Chat.Infrastructure.src.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. تسجيل الـ Repositories والـ Services (Dependency Injection)
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<ChatHub>();
builder.Services.AddScoped<IParticipantRepository,ParticipantRepository>(); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();       // 2. الـ Authorization ثانياً

// 3. الـ Endpoints والمخارج أخيرًا
app.MapControllers();
app.MapHub<ChatHub>("/chathub");

app.Run();
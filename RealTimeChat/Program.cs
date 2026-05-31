using Chat.Domain.src.Abstraction;
using Chat.Infrastructure.src.Database;
using Chat.Infrastructure.src.Repository;
using Microsoft.EntityFrameworkCore;
 
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

 builder.Services.AddSignalR();

 builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

  
var app = builder.Build();

 if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

 app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();
 

app.Run();
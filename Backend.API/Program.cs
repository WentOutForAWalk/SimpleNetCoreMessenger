using Backend.API.Data;
using Backend.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Swagger ..
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddTransient<ChannelService>();
builder.Services.AddTransient<MessageService>();

// Controllers
builder.Services.AddControllers();

builder.BackendDb();

var app = builder.Build();

app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MigrateDb();

app.Run();

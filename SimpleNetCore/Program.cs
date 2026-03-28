using SimpleNetCore.Data;
using SimpleNetCore.Endpoints;
using SimpleNetCore.Services;

var builder = WebApplication.CreateBuilder(args);

// Swagger ..
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddSingleton<MemoryStorageService>();
builder.Services.AddTransient<ChannelService>();
builder.Services.AddTransient<MessageService>();

// Controllers
builder.Services.AddControllers();

builder.AddSimpleNetCoreDb();

var app = builder.Build();

app.MapControllers();




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapChannelsEndpoints();

app.MigrateDb();

app.Run();

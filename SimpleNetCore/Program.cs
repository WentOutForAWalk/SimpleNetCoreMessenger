using SimpleNetCore.Data;
using SimpleNetCore.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddSimpleNetCoreDb();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapChannelsEndpoints();

app.MigrateDb();

app.Run();

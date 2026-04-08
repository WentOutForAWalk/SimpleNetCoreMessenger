using Backend.API.Extensions;
using Backend.API.Middleware;
using Backend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Swagger ..
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controllers
builder.Services.AddControllers();

// Services and dbcontext
builder.Services.AddInfrastructure(builder.Configuration);

// Identity
builder.Services.AddAuthorization();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "dotnet";
});



var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MigrateDb();

app.Run();

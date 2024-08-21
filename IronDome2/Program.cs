using IronDome2.Middlewares.attack;
using IronDome2.Middlewares.Global;
using IronDome2.Models;
using IronDome2.services;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DbConnection>(options => options.UseSqlServer(connectionString));
// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseMiddleware<GlobalLoginMiddleWare>();

//app.UseWhen(
//    context =>
//        context.Request.Path.StartsWithSegments("/api/attacks"),
//    appBuilder =>
//    {
//        appBuilder.UseMiddleware<JwtValidationMiddleware>();
//        appBuilder.UseMiddleware<AttackLogInMiddleWare>();
//    });




app.MapControllers();

app.Run();


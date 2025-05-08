using capAPI.Helpers;
using capAPI.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBCapstoneContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



EmailHelper.Init(builder.Configuration);

var app = builder.Build();




if (app.Environment.IsDevelopment())
{


    app.UseSwagger();




    app.UseSwaggerUI();
}


//app.UseSwagger();

//app.UseSwaggerUI(o =>
//{
//    o.SwaggerEndpoint("/swagger/v1/swagger.json", "Capaston v1");
//    o.RoutePrefix = string.Empty;
//});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

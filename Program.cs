using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MobileStoreAPI.Data;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
#region CORS setting for API
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
    policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    }

    );
});
#endregion
#region CORS setting for API
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
    policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowAnyMethod();
    }
 
    );
});
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version())));

//builder.Services.AddCors(); 






var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.WebRootPath, "images")),
    RequestPath = "/wwwroot/images"
});

app.UseHttpsRedirection();

app.UseCors("_myAllowSpecificOrigins");




//app.UseCors(options => options.WithOrigins("http://localhost:3000")
//    .AllowAnyMethod()
//    .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

//app.UseCors("myAppCors");



app.Run();

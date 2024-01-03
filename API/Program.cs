using API.Helper;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add Configuration to access appsettings.json
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Configure services
builder.Services.AddDbContext<StoreContext>(options =>
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString);
    });


//Mapper
builder.Services.AddAutoMapper(typeof(MappingProfiles));

//product repository service
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//Generic repository service
builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));


var app = builder.Build();



//check if no database and seed data
using (var scope = app.Services.CreateScope())
{
    var services=scope.ServiceProvider;
    var loggerFactory=services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex) {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "an error occured during migration ");
    }
    
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();

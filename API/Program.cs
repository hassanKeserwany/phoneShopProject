using API.Helper;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//swager config
builder.Services.AddSwaggerGen(c =>
{
    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "JWT Auth Bearer Schema",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type=SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer",
        }
    };
    c.AddSecurityDefinition("Bearer", securitySchema);
    var securityRequirment=new OpenApiSecurityRequirement{ { securitySchema, new[] { "Bearer" } } };
    c.AddSecurityRequirement(securityRequirment);
});


// Add Configuration to access appsettings.json
var Configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

//Mapper
builder.Services.AddAutoMapper(typeof(MappingProfiles));


// Configure services sql for identity

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    var connectionString = Configuration.GetConnectionString("IdentityConnection");
    options.UseSqlServer(connectionString);
});
/////////////////////////////////////////////////////////////////////////
///add services for jwt and usermanager and sign in 
builder.Services.AddIdentityCore<Core.Entities.Identity.AppUser>()
        .AddEntityFrameworkStores<AppIdentityDbContext>();
   //   .AddDefaultTokenProviders();


builder.Services.AddScoped<UserManager<Core.Entities.Identity.AppUser>>();
builder.Services.AddScoped<SignInManager<Core.Entities.Identity.AppUser>>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<Core.Entities.Identity.AppUser>,
    UserClaimsPrincipalFactory<Core.Entities.Identity.AppUser>>();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
            ValidIssuer = Configuration["Token:Issuer"],
            ValidateIssuer = true,
            ValidateAudience = false,
        };
    });
/////////////////////////////////////////////////////////////////////////
builder.Services.AddScoped<ITokenService,TokenService>();

// Configure services sql for system
builder.Services.AddDbContext<StoreContext>(options =>
    {
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString);
    });

//add redis
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var configuration = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"), true);
    return ConnectionMultiplexer.Connect(configuration);
});


//Generic repository service
builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

// Registration of IBasketRepository service
builder.Services.AddScoped<IBasketRepository, BasketRepository>();



//allow orign header method
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});



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

// Enable CORS middleware
app.UseCors();

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

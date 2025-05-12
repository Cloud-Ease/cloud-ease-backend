using CloudEase.API.Data;
using CloudEase.API.Middleware;
using CloudEase.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IProfileService, ProfileService>();

// Swagger ve Bearer token desteði
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CloudEase API",
        Version = "v1"
    });

    // Bearer token güvenlik tanýmý
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Firebase'den alýnan JWT token'ý buraya girin. Örnek: Bearer eyJhbGciOi..."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Dependency Injection
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddSingleton<FirebaseAuthService>();

// Database baðlantýsý
try
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}

var app = builder.Build();

// HTTP pipeline ayarlarý
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Custom Middleware
app.UseMiddleware<FirebaseAuthMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

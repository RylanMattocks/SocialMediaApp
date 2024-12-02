using DropTablesSocial.Data;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using DropTablesSocial.API;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Remove explicit Kestrel configuration
// builder.WebHost.ConfigureKestrel(serverOptions =>
// {
//     serverOptions.ListenAnyIP(80);
// });

// Add services to the container.
builder.Services.AddDbContext<DropTablesContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DropTablesDb"),
    sqlOptions => sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IPostRepo, PostRepo>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DropTables API",
        Version = "v1",
        Description = "An ASP.NET Core Web API for managing DropTables functionality.",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact"),
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license"),
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DropTables API V1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
    });
}
else
{
    // Production configuration
    app.UseExceptionHandler("/Error");
}

app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

// Add a simple health check endpoint
app.MapGet("/", () => "all good 🙃");

app.Run();
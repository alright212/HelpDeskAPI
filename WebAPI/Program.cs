using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("WebAPI")));

builder.Services.AddScoped<IAppeals, Appeals>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    // Include XML comments if available
    // c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "YourXmlDocumentationFileName.xml"));
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("SpecificOriginPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3001")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Only include this if you need to handle credentials
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}
app.UseHttpsRedirection();

// Apply CORS policy to all requests
app.UseCors("SpecificOriginPolicy");

app.MapControllers();
app.Run();
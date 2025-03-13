using CustomerSupport.Application.Mapping;
using CustomerSupport.Application.Services.Implementations;
using CustomerSupport.Application.Services.Interfaces;
using CustomerSupport.Infrastructure.DependencyInjections;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Add your Angular app URL
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Enables access to the current HTTP context for retrieving user details.  
builder.Services.AddHttpContextAccessor();

// Register Services
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


// Register Auto mapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Register Infrastructure dependencies
builder.Services.AddInfrastructure(builder.Configuration);

// Allow files size up to 100 MB
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
});


var app = builder.Build();

// Apply database seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await services.SeedDatabaseAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use the CORS policy
app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

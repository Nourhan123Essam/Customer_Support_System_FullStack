using CustomerSupport.Application.Mapping;
using CustomerSupport.Application.Services.Implementations;
using CustomerSupport.Application.Services.Interfaces;
using CustomerSupport.Infrastructure.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Services
builder.Services.AddScoped<ITicketService, TicketService>();


// Register Auto mapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Register Infrastructure dependencies
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

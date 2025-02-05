using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura o Entity Framework com PostgreSQL
 builder.Services.AddDbContext<AppDbContext>(options => {
     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));});
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
// Adiciona serviços da API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configura o HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

app.UseAuthorization();
app.MapControllers();
app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
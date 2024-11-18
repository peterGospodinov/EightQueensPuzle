using Microsoft.EntityFrameworkCore;
using NQueensPuzzle.Web.Services;
using QueensPuzle.Web.Data;
using QueensPuzle.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ResultContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection").ToString());
builder.Services.AddScoped<DbMigrationService>();

builder.Services.AddHostedService<ResultProcessingService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbMigrationService = scope.ServiceProvider.GetRequiredService<DbMigrationService>();
    dbMigrationService.ApplyMigrations();  // Assuming you have a method to apply migrations
}


app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

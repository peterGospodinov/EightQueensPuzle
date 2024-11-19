using Microsoft.EntityFrameworkCore;
using NQueensPuzzle.Web.Services;
using QueensPuzle.Web.Data;
using QueensPuzle.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ResultContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
    dbMigrationService.ApplyMigrations();  
}


app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseRouting();
app.Run();
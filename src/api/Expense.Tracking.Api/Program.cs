using Expense.Tracking.Api.Infrastrucure.Database;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Expense.Tracking.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            })
            .AddOData(options =>
            {
                options.EnableQueryFeatures();
            });

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(corsBuilder => corsBuilder
                .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Value.Split(","))
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ExpenseContext>(options =>
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = Path.Join(path, "expenses.db");

            options.UseMySQL(builder.Configuration.GetConnectionString("Expenses"));
            //options.UseSqlite($"Data Source={dbPath}");
        });

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCors();

        app.MapControllers();

        // Ensure the database is created and seeded with data
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ExpenseContext>();
            context.Database.EnsureCreated();
        }

        app.Run();
    }
}

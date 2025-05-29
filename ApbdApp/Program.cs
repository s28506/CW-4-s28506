using ApbdApp.DAL;
using ApbdApp.Services;
using Microsoft.EntityFrameworkCore;

namespace ApbdApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        
        builder.Services.AddControllers();
        builder.Services.AddDbContext<MyDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddScoped<IPatientService, PatientService>();
        builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
        builder.Services.AddScoped<IMyDbContext, MyDbContext>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PH01___C__tutorial;
using PH01___C__tutorial.UniversityContexts;

class Program
{
    static void Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddDbContextFactory<UniversityDbContext>(options =>
                {
                    options.UseNpgsql("Host=127.0.0.1;Port=5432;Database=university;User Id=postgres;Password=myPassword;");
                });
                services.AddScoped<IUniversityDbContextFactory, UniversityDbContextFactory>();
                services.AddScoped<IAverageCalculator, AverageCalculator>();
                services.AddScoped<IStudentFinder, StudentFinder>();
            })
            .Build();
        var averageByStudent = host.Services.GetRequiredService<IAverageCalculator>();
        var studentFinder = host.Services.GetRequiredService<IStudentFinder>();

        var top3Students = averageByStudent.CalculateAverageTop3();

        foreach (var entry in top3Students)
        {
            var student = studentFinder.FindStudentById(entry.StudentNumber);
            Console.WriteLine($"{student.FirstName} {student.LastName}: {entry.AverageScore:F5}");
        }
    }
}
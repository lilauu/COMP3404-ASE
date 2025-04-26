using COMP3404_Server.Database;
using COMP3404_Server.Repositories;
using Microsoft.EntityFrameworkCore;

namespace COMP3404_Server;

public class Startup
{
    public IConfigurationRoot Configuration { get; }
    public Startup(IConfigurationRoot configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IWebHostEnvironment environment, IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        
        services.AddHttpClient();

        services.AddScoped<IUserAccountRepository, Repository>();
        services.AddScoped<IChatRepository, Repository>();
    }
}

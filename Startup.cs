using eTickets.Data;

namespace eTickets
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add serices to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //DbContextConfiguration
            services.AddDbContext<AppDbContext>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {

        }
    }
}

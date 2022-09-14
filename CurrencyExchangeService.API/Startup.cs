using CurrencyExchangeService.Business.Services.Abstract;
using CurrencyExchangeService.Business.Services.Concrete;
using CurrencyExchangeService.Data.Core;
using CurrencyExchangeService.Data.EF;
using CurrencyExchangeService.Data.Repositories.Abstract;
using CurrencyExchangeService.Data.Repositories.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CurrencyExchangeService.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            configuration.GetSection("BusinessConfig").Bind(StaticConfig.BusinessConfig);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CurrencyExchangeDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("CurrencyExchangeDBContext")));
            services.AddControllers();
            services.AddDistributedMemoryCache();
            services.AddScoped<ICurrencyExchangeService, Business.Services.Concrete.CurrencyExchangeService>();
            services.AddScoped<ICurrencyExchangeRepository, CurrencyExchangeRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IRestrictionService, RestrictionService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Currency Exchange API", Version = "v1" });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Currency Exchange API");
                c.RoutePrefix = "swagger";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

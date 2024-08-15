
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsHub.API.Controllers;
using SportsHub.Data;
using SportsHub.Data.Account;
using SportsHub.Data.ExcelUpload;
using SportsHub.Model;
using SportsHub.Service.Account;
using SportsHub.Service.ExcelUpload;

namespace SportsHub.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            /*services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            });*/
            services.AddSingleton(Configuration.GetSection("AppSettings").Get<AppSettings>());
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            }).AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            });
            services.AddAuthentication();
            services.PostConfigure<ConnectionStrings>(myOptions =>
            {
                myOptions.DefaultConnection = connectionString;
            });
            services.ConfigureJwtAuthentication(Configuration.GetSection("AppSettings").Get<AppSettings>());
            services.AddSingleton<PostgreSQLConnectionProvider, PostgreSQLConnectionProvider>();

            services.AddSingleton<IExcelUploadRepository, ExcelUploadRepository>();
            services.AddSingleton<IExcelUploadService, ExcelUploadService>();
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<IAccountService, AccountService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

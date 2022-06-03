using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SocialApp.Database;
using SocialAppService.Infrastructure;
using SocialAppService.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SocialAppService.Models;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace SocialAppService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment env { get; }
        private readonly string MyAllowPoliciy = "_MyAllowPoliciy";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var server = Configuration["DBServer"] ?? "localhost";
            var port = Configuration["DBPort"] ?? "1433";
            var userId = Configuration["DBUser"] ?? "sa";
            var password = Configuration["DBPassword"] ?? "Pa@@w0rd";
           
        //    Configuration["ConnectionStrings:DefaultConnection"]
        // $"Server={server},{port};Initial Catalog=SocialApp;User Id={userId};Password={password}"

        // $"Server={server},{port};Initial Catalog=SocialAppDB;User Id={userId};Password={password}"

            services.AddDbContext<SocialAppDatabase>(options => 
              options.UseSqlServer("workstation id=SocialAppDB.mssql.somee.com;packet size=4096;user id=suleiman_SQLLogin_1;pwd=ujz2xi97v7;data source=SocialAppDB.mssql.somee.com;persist security info=False;initial catalog=SocialAppDB"));

            services.AddIdentity<User, IdentityRole<int>>(options => {
                options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<SocialAppDatabase>();
            
            services.Configure<JWTConfig>(Configuration.GetSection("JWTConfig"));

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });
            
            if (env.IsDevelopment())
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddDistributedMemoryCache();
                // services.AddStackExchangeRedisCache(options =>
                // {
                //     options.Configuration = _config["MyRedisConStr"];
                //     options.InstanceName = "SampleInstance";
                // });
            }

            addJwtAuth(services);

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SocialAppService", Version = "v1" });
            });

            services.AddScoped<ICacheProvider, CacheProvider>();
            services.AddScoped(typeof(TokenGenerator));
            services.AddScoped(typeof(CacheRepository<>));
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>)); 

            services.AddCors(options => 
            {
                options.AddPolicy(name: MyAllowPoliciy,
                                builder => 
                                {
                                    builder.WithOrigins("http://localhost:4200")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();

                                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialAppService v1"));
                app.UseCors(MyAllowPoliciy);
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            PerpDB.perpPopulation(app);
       
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
 
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "SocialAppClient";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private void addJwtAuth(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt => 
            {
                var key = Encoding.ASCII.GetBytes(Configuration["JWTConfig:Key"]);
                var issuer = Configuration["JWTConfig:Issuer"];
                var audience = Configuration["JWTConfig:Audience"];
                opt.TokenValidationParameters = new TokenValidationParameters(){
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                };
            });
        }
    }
}

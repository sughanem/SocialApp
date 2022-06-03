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
        private string AuthKey;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
            this.AuthKey = Configuration["Key"];
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<SocialAppDatabase>(options => 
              options.UseSqlServer(Configuration["ConnectionStrings"] ?? Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddIdentity<User, IdentityRole<int>>(options => {
                options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<SocialAppDatabase>();
            
            services.Configure<JWTConfig>(Configuration.GetSection("JWTConfig"));

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "SocialAppClient/dist";
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialAppService v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // PerpDB.perpPopulation(app);

            app.UseCors(builder => builder
                .WithOrigins(Configuration["SiteUrl"] ?? "https://localhost") 
                .WithMethods("GET, POST", "OPTIONS")  
                .WithHeaders("Origin", "Authorization") 
            );
       
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
                var key = Encoding.ASCII.GetBytes(this.AuthKey ?? Configuration["JWTConfig:Key"]);
                var issuer = Configuration["Issuer"] ?? Configuration["JWTConfig:Issuer"];
                var audience = Configuration["Audience"] ?? Configuration["JWTConfig:Audience"];
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

using HowLongApi.Infrastructure;
using HowLongApi.Infrastructure.Data;
using HowLongApi.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using Pomelo.EntityFrameworkCore.MySql;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HowLongApi.Businesses;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.Net.Http.Headers;
using HowLongApi.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HowLongApi
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
            var connectionString = System.Environment.GetEnvironmentVariable("DB_CONNECTION_STRING", EnvironmentVariableTarget.Process);
            services.AddDbContext<ApiContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0)))
            );

            services.AddDbContext<AuthDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("AUTH_DB"),
                new MySqlServerVersion(new Version(8, 0))));

            services.AddCors(options =>
            {
                options.AddPolicy("Query",
                    builder => builder.WithOrigins("http://localhost:3000"));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("s18239411cz/adaq3dsahnanjkjoei#d!@#pds$c!#$!#@")),
                    ClockSkew = TimeSpan.FromMinutes(30),
                    ValidIssuer = "HowLong",
                    ValidAudience = "Users",
                };
            });

            services.AddScoped(typeof(ITeamRepository<>), typeof(TeamRepository<>));
            services.AddTransient(typeof(ITeamBusiness), typeof(TeamBusiness));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<AuthDbContext>();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Usuario/Login");

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HowLongApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HowLongApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints
                    .MapControllers();
            });
        }
    }
}

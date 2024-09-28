using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
namespace BookStore
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
            services.AddControllers();
            services.AddTransient<IBookStoreRepository, BookStoreRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddDbContext<BookStoreContext>(options=>options.UseNpgsql(Configuration.GetConnectionString("BookStoreConnectionString")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(
                options =>{
                            options.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme =JwtBearerDefaults.AuthenticationScheme;
                            options.DefaultScheme =JwtBearerDefaults.AuthenticationScheme;
                        }).AddJwtBearer(
                            opt=>{
                            opt.SaveToken =true;
                            opt.RequireHttpsMetadata = false;
                            opt.TokenValidationParameters = new TokenValidationParameters(){
                                    ValidateIssuer =true,
                                    ValidateAudience = true,
                                    ValidIssuer =Configuration["Jwt:ValidIssuer"],
                                    ValidAudience=Configuration["Jwt:ValidAudiance"],
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"]))
                            };
                        });
            services.AddSwaggerGen(conf =>
            {
                conf.SwaggerDoc("v1", new OpenApiInfo { Title = "Bookstore api", Version = "v1" });
                conf.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
                    In =ParameterLocation.Header,
                    Description= "Please Enter JWT token here",
                    Name = "Authorization",
                    Type= SecuritySchemeType.ApiKey
                });
                conf.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type =ReferenceType.SecurityScheme,
                                Id ="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Book store web api");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger();
            });
        }
    }
}

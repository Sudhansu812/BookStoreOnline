using BookSharingOnlineApi.Repository;
using BookSharingOnlineApi.Repository.Interfaces;
using BookSharingOnlineApi.Services;
using BookSharingOnlineApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;

namespace BookSharingOnlineApi
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
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
                });
            });

            // Dependency injection --- Services
            services.AddTransient<ITransactionManagementService, TransactionManagementService>();
            services.AddTransient<IAccountManagementService, AccountManagementService>();
            services.AddTransient<IEncryptionService, EncryptionService>();

            // Dependency injection --- Repository
            services.AddTransient<IBookRepo, BookRepo>();
            services.AddTransient<ICartRepo, CartRepo>();
            services.AddTransient<IOrderRepo, OrderRepo>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IRatedBookRepo, RatedBookRepo>();

            // Dependency injection --- Database Context
            services.AddDbContext<BookContext>(option => option.UseSqlServer(Configuration.GetConnectionString("BookSharingOnline")));
            services.AddDbContext<CartContext>(option => option.UseSqlServer(Configuration.GetConnectionString("BookSharingOnline")));
            services.AddDbContext<OrderContext>(option => option.UseSqlServer(Configuration.GetConnectionString("BookSharingOnline")));
            services.AddDbContext<UserContext>(option => option.UseSqlServer(Configuration.GetConnectionString("BookSharingOnline")));
            services.AddDbContext<RatedBookContext>(option => option.UseSqlServer(Configuration.GetConnectionString("BookSharingOnline")));

            // Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Jwt Authentication
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "http://localhost:5001",
                ValidAudience = "http://localhost:5001",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("verySuperSecretKey@123"))
                };
            });

            // Swagger
            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("version1", new OpenApiInfo { Title = "Book Sharing Online API", Version = "version1" });
            //});
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("version1", new OpenApiInfo
                {
                    Title = "Book Sharing Online API",
                    Version = "version1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                 new OpenApiSecurityScheme
                 {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                 },
                 new string[] { }
                }
              });
            });

            // output as Camel Case in json
            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("EnableCORS");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/version1/swagger.json", "Book Sharing Online API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
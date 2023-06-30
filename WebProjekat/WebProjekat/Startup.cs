using AutoMapper;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebProjekat.DTO.UserDTO;
using WebProjekat.Infrastructure;
using WebProjekat.Interfaces;
using WebProjekat.Mapping;
using WebProjekat.Repository;
using WebProjekat.Repository.Interfaces;
using WebProjekat.Services;
using WebProjekat.Validators.UserValidators;
using WebProjekat.DTO.ProductDTO;
using WebProjekat.Validators.ProductValidators;
using WebProjekat.DTO.OrderDTO;
using WebProjekat.Validators.OrderValidators;

namespace WebProjekat
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebProjekat", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters //Podesavamo parametre za validaciju pristiglih tokena
                {
                    ValidateIssuer = true, //Validira izdavaoca tokena
                    ValidateAudience = false, //Kazemo da ne validira primaoce tokena
                    ValidateLifetime = true,//Validira trajanje tokena
                    ValidateIssuerSigningKey = true, //validira potpis token, ovo je jako vazno!
                    ValidIssuer = "https://localhost:44365", //odredjujemo koji server je validni izdavalac
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))//navodimo privatni kljuc kojim su potpisani nasi tokeni
                };
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc();
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IValidator<RegisterUserDTO>, RegisterUserDTOValidator>();
            services.AddScoped<IValidator<GoogleLogInDTO>, GoogleLogInDTOValidator>();
            services.AddScoped<IValidator<LogInUserDTO>, LogInUserDTOValidator>();
            services.AddScoped<IValidator<UpdateUserDTO>, UpdateUserDTOValidator>();
            services.AddScoped<IValidator<ChangePasswordDTO>, ChangePasswordDTOValidator>();
            services.AddScoped<IValidator<ProductDTO>, ProductDTOValidator>();
            services.AddScoped<IValidator<OrderItemDTO>, OrderItemDTOValidator>();
            services.AddScoped<IValidator<OrderDTO>, OrderDTOValidator>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddHostedService<DeliveryTimeService>();

            services.AddDbContext<DbContextWP>(options => options.UseSqlServer(Configuration.GetConnectionString("WebProjekatDB")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebProjekat v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

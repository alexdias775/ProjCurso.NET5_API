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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuarioApi.Data.Dtos;
using UsuarioApi.Models;
using UsuarioApi.Services;

namespace UsuarioApi
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
            services.AddDbContext<UserDbContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("UsuarioConnection"))//definindo a conex�o com o banco
            );
            services.AddIdentity<CustomIdentityUser, IdentityRole<int>>(opt =>
                {
                    opt.SignIn.RequireConfirmedEmail = true;
                })
                //Definindo para usar Identity
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders(); //para garer c�digos de confirma��o
            //Utilizando uma FrameworkStore para armazenar dados que est�o sendo usado para identifica��o
            services.AddScoped<CadastroService, CadastroService>();
            services.AddScoped<EmailService, EmailService>();
            services.AddScoped<LoginService, LoginService>();
            services.AddScoped<TokenService, TokenService>();
            services.AddScoped<LogoutService, LogoutService>();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

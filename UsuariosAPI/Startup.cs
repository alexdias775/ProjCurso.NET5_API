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
using UsuariosAPI.Data;
using UsuariosAPI.Services;

namespace UsuariosAPI
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
                options.UseMySQL(Configuration.GetConnectionString("UsuarioConnection")));
            services
                .AddIdentity<IdentityUser<int>, IdentityRole<int>>(opt =>
                {
                    opt.SignIn.RequireConfirmedEmail = true;
                /*Nesse ponto se fala que se tem uma opção e nessa opção tem SignIn
                que requer que o campo email seja um campo obrigatório*/
                })
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();
            /*TokenProvider do próprio Identity usado para gerar tokens, resetar senhas, alterar email 
              e alterar telefone */

            services.AddScoped<CadastroService, CadastroService>();
            services.AddScoped<LogoutService, LogoutService>();
            services.AddScoped<LoginService, LoginService>();
            services.AddScoped<TokenService, TokenService>();
            services.AddScoped<EmailService, EmailService>();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
        /*Como dito anteriormente, é possível definir quais são os requisitos de uma senha utilizando o Identity. 
         * Por padrão, as senhas devem conter um caractere maiúsculo, um minúsculo, um dígito e um caractere não 
         * alfanumérico, além de seis caracteres no mínimo.Manipulando a nossa classe Startup em nosso método Con-
         * figureServices(), é possível alterar este comportamento padrão, por exemplo:
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
            });
            Mais exemplos e definições podem ser consultadas através da
            https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-5.0#password
         * oficial.
         */

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

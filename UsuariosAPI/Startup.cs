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
                /*Nesse ponto se fala que se tem uma op��o e nessa op��o tem SignIn
                que requer que o campo email seja um campo obrigat�rio*/
                })
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();
            /*TokenProvider do pr�prio Identity usado para gerar tokens, resetar senhas, alterar email 
              e alterar telefone */

            services.AddScoped<CadastroService, CadastroService>();
            services.AddScoped<LogoutService, LogoutService>();
            services.AddScoped<LoginService, LoginService>();
            services.AddScoped<TokenService, TokenService>();
            services.AddScoped<EmailService, EmailService>();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
        /*Como dito anteriormente, � poss�vel definir quais s�o os requisitos de uma senha utilizando o Identity. 
         * Por padr�o, as senhas devem conter um caractere mai�sculo, um min�sculo, um d�gito e um caractere n�o 
         * alfanum�rico, al�m de seis caracteres no m�nimo.Manipulando a nossa classe Startup em nosso m�todo Con-
         * figureServices(), � poss�vel alterar este comportamento padr�o, por exemplo:
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
            });
            Mais exemplos e defini��es podem ser consultadas atrav�s da
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

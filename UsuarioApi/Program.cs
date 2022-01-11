using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuarioApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureAppConfiguration((context, builder) => 
            builder.AddUserSecrets<Program>());
        /*Se eu quero alterar o fluxo da criação do meu programa, que classe eu posso 
         * fazer isso? Temos uma classe com o nome de programa, program, no caso em inglês. 
         * Parece bem intuitivo. Então dentro dela o que eu vou fazer? Eu vou simplesmente 
         * falar que eu quero configurar a minha .ConfigureAppConfiguration, parece um nome 
         * um tanto quanto redundante, mas ele vai ter um contexto e um builder, (contexto, 
         * builder) =>.

         E qual vai ser o papel desse builder? Eu vou simplesmente falar que esse builder vai 
         adicionar o UserSecrets ao meu programa, builder .AddUserSecretsProgram().
         */
    }
}

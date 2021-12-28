using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UsuariosAPI.Data
{
    public class UserDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    /* O que esse classe vai fazer? 
         Primeiro precisa-se definir qual contexto que será extendido ou seja, definir qual classe será
     herdada.
    Precisa-se definir o que está sendo utilizado com o IdentityDbContext, está sendo utilizado o Iden-
    tityUser que vai possuir como identificador um inteiro e dentro do sistema o mesmo vai ter um 
    papelUma (Role)  IdentityRole que também terá como identificador um inteiro. E no final a chave utili-
    za para identificar esse cara no sistema será um inteiro.
     */
    {
        public UserDbContext(DbContextOptions<UserDbContext> opt) : base(opt)
        //
        {

        }

    }
}
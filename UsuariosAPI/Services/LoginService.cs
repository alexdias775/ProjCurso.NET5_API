using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService = null)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogaUsuario(LoginRequest request)
        {
            var resultadoIdentity = _signInManager
                .PasswordSignInAsync(request.Username, request.Password, false, false);
            //  o _signInManager busca fazer autenticação via PasswordSignInAsync
            if (resultadoIdentity.Result.Succeeded) 
                /*Já com a lógica de login feita,e agora também já com a garantia do login feito 
                 * e a operação de login retornando um Sucessed, é preciso gerar um token 
                 e retornar para o usuário que fez o login, logo é nessário da lógica abaixo*/
            {
                /*Obs importante: o CreateToken recebe IdentityUser<int> usuario, mas o LogaUsuario
                 acima está recebendo um request do tipo LoginRequest.
                - Então como recupera um IdentityUser usuario para passar para o CreateToken?
                 */

                var identityUser = _signInManager
                    .UserManager //dentro do _signInManager tem-se o UserManager
                    .Users 
                    /*E através do UserManager possui-se acesso ao Users
                     que é propriedade dos usuários*/
                    .FirstOrDefault 
                    /*Através da propriedade Users quer se recuperar um usuário,
                     o primeiro encontrado ou Default(que será um null caso não encontre nenhum)
                    */
                    (usuario => usuario.NormalizedUserName == request.Username.ToUpper());
                /*recuperando um usuario, tal que, o nome desse usuario normalizado tem que ser 
                 * igual ao usuario que está tentando fazer o login(resquest.Username). Coloca-se ToUpper 
                 para deixar as letras em maiúscula, e assim deixando a compara~ção padronizada, para que 
                 caso o usuario tenha feito o nome com alguma letra maiscula, minuscula, deixe tudo em   
                 maisculo*/
               Token token = _tokenService.CreateToken(identityUser);
                /*com o a identityUser recuperado, passa-se o usuario recuperado para o tokenService
                 e o mesmo vai retornar um token */
               return Result.Ok().WithSuccess(token.Value); 
            }
            return Result.Fail("Login falhou");
        }
    }
}
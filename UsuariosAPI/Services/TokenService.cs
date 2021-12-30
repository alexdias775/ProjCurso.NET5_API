using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class TokenService
    {
        /*Qual usuario está se recebendo? Ou seja qual Iden-
         tityUser que está se recebendo para que seja criado 
         um Token para o mesmo*/
        
        public Token CreateToken(IdentityUser<int> usuario)
        {
            Claim[] direitosUsuario = new Claim[]
            /*Gerando um array de coisa que estão 
             sendo reclamadas, no sentido de querendo
            através de direito*/
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString())
            };

            /*precisa-se gerar uma chave para criptografar*/
            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn")
                );

            /*gerando crendenciais a partir de um SigningCredentials, que é uma classe 
             * especializada para gerar credenciais a partir de chave e através de um
             algoritmo de criptografia*/
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            /*Gerando o token e definindo quais as informações que se quer colocar dentro do 
             mesmo*/
            var token = new JwtSecurityToken(
                claims: direitosUsuario, //apontando quais são as claims
                signingCredentials: credenciais, //definindo quais são as partes de segurança
                expires: DateTime.UtcNow.AddHours(1) 
                /*e definindo em quanto tempo esse token vai expirar, nesse caso foi definido
                 que o mesmo será expirado em uma hora*/
                );

            /* É necessário transformar o trecho de código anterior em uma string para que seja 
             * possível coloca-lo dentro do Token e assim pode-lo armazenar*/
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);
        }
    }
}

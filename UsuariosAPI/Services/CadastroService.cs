using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;

        public CadastroService(IMapper mapper,
            UserManager<IdentityUser<int>> userManager, 
            EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public Result CadastroUsuario(CreateUsuarioDto createDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
            Task<IdentityResult> resultadoIdentity = _userManager
                .CreateAsync(usuarioIdentity, createDto.Password);
            /*a partir de _userManager criando de maneira assicrona o usuário que acabou de ser ma-
             * peado - usuarioIdentity - o mesmo vai ter uma senha, e onde está a senha? Na requi-
             * sição, no  createDto.Password */
            if (resultadoIdentity.Result.Succeeded) 
            {
                //gerando código de confirmação de cadastro
                string code = _userManager
                    .GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;

                //Lógica para enviar email para o usuario que acabou de ser cadastrado
                _emailService.EnviarEmail(new[] { usuarioIdentity.Email }, //parâmetros necessários para quando se quer enviar um email 
                    "Link de Ativação", usuarioIdentity.Id, code);
  
                return Result.Ok()
                    .WithSuccess(code);
            }
            return Result.Fail("Falha ao cadastrar usuário");

            /* De forma resumida resultadoIdentity vai ser uma tarefa do IdentityResult. Quando é executado o CreateAsync
             * (Criação Assicrona) a mesma executará uma tarefa e essa tarefa possuirá um resultado que pode ou ter sucedido
             * bem ou não.*/
        }

        public Result AtivaContaUsuario(AtivaContaRequest request)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(u => u.Id == request.UsuarioId);
            var identityResult = _userManager
                .ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao).Result;
            if (identityResult.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Falha ao ativar conta de usuário"); 
        }
    }
}

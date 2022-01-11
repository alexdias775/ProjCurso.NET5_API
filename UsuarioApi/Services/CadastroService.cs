using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsuarioApi.Data.Dtos;
using UsuarioApi.Data.Request;
using UsuarioApi.Models;

namespace UsuarioApi.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        //private UserManager<IdentityUser<int>> _userManager;
        private UserManager<CustomIdentityUser> _userManager;
        private EmailService _emailService;
        
        //private RoleManager<IdentityRole<int>> _roleManager;

        public CadastroService(IMapper mapper, 
            UserManager<CustomIdentityUser> userManager, 
            EmailService emailService, 
            RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            //_roleManager = roleManager;
        }

        public Result CadastraUsuario(CreateUsuarioDto createDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);
            Task<IdentityResult> resultadoIdentity = _userManager
                .CreateAsync(usuarioIdentity, createDto.Password);
            _userManager.AddToRoleAsync(usuarioIdentity, "regular");
            //var createRoleResult = _roleManager
            //    .CreateAsync(new IdentityRole<int>("admin")).Result;
            //var usuarioRoleResult = _userManager
            //    .AddToRoleAsync(usuarioIdentity, "admin").Result;
            if (resultadoIdentity.Result.Succeeded)
            {
                var codigoAtivacao = _userManager
                    .GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                var encondeCode = HttpUtility.UrlEncode(codigoAtivacao);

                _emailService.EnviarEmail(new[] { usuarioIdentity.Email }, 
                    "Link de ativação", usuarioIdentity.Id, encondeCode);

                return Result.Ok().WithSuccess(codigoAtivacao);
            }
            return Result.Fail("Falha ao cadastrar usuário");
        }

        public Result AtivaContaUsuario(AtivaContaUsuario request)
        {
            var usuarioIdentity = _userManager
                .Users
                .FirstOrDefault(u => u.Id == request.UsuarioId);
            var resultado = _userManager.ConfirmEmailAsync(usuarioIdentity, request.CodigoDeAtivacao).Result;
            if (resultado.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Falha ao ativar conta de usuário");
        }
    }
}

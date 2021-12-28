using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public Result CadastroUsuario(CreateUsuarioDto createDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
            Task<IdentityResult> resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, createDto.Password);
            /*a partir de _userManager criaado de maneira assicrona o usuário que acabou de ser ma-
             * peado - usuarioIdentity - e o mesmo vai ter uma senha, e onde está a senha? Na requi-
             * sição, no  createDto.Password */
            if (resultadoIdentity.Result.Succeeded) return Result.Ok();
            return Result.Fail("Falha ao cadastrar usuário");

            /* De forma resumida resultadoIdentity vai ser uma tarefa do IdentityResult. Quando é executado o CreateAsync
             * (Criação Assicrona) a mesma executará uma tarefa e essa tarefa possuirá um resultado que pode ou ter sucedido
             * bem ou não.*/
        }
    }
}

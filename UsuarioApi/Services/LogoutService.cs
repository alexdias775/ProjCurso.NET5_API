using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using UsuarioApi.Models;

namespace UsuarioApi.Services
{
    public class LogoutService
    {
        private SignInManager<CustomIdentityUser> _signInManager;

        public LogoutService(SignInManager<CustomIdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result DeslogaUsuario()
        {
            var resultadoIdentity = _signInManager.SignOutAsync();
            if (resultadoIdentity.IsCompleted) return Result.Ok();
            return Result.Fail("O Logout falhou");
        }
    }
}

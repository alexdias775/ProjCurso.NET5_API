using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuarioApi.Data.Dtos;
using UsuarioApi.Data.Request;
using UsuarioApi.Services;

namespace UsuarioApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public IActionResult CadastraUsuario(CreateUsuarioDto createDto)
        {
            Result resultado = _cadastroService.CadastraUsuario(createDto);
            if(resultado.IsFailed) return StatusCode(500);
            return Ok(resultado.Successes);
        }

        [HttpGet("/ativa")]
        public IActionResult AtivaContaUsuario([FromQuery] AtivaContaUsuario request)
        {
            Result resultado = _cadastroService.AtivaContaUsuario(request);
            if (resultado.IsFailed) return StatusCode(500);
            return Ok(resultado.Successes);
        }
    }
}

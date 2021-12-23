using AutoMapper;
using FilmesAPI.Models;
using FilmesAPI.Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmesAPI.Data;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private AppDbContext _context;
        //um campo do tipo FilmeContext, inicializando
        //esse campo com _context;
        private IMapper _mapper;


        //inicializando _context e _mapper via construtor
        public EnderecoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
            //convertendo um filmeDto(do tipo CreateFilmeDto) para um Filme e guardando na váriavel filme.

            /*Filme filme = new Filme 
            //criação de um objeto com um construtor implicito

            //Abaixo está se passando para o  "filme", as propriedades
            //do filmeDto

            {
                Titulo = filmeDto.Titulo,
                Genero = filmeDto.Genero,
                Duracao = filmeDto.Duracao,
                Diretor = filmeDto.Diretor
            };*/

            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaEnderecosPorId), new { Id = endereco.Id }, endereco);
        }

        [HttpGet]
        public IEnumerable<Endereco> RecuperaEnderecos()
        {
            return _context.Enderecos;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaEnderecosPorId(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco != null)
            {
                ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);

                /*ReadFilmeDto filmeDto = new ReadFilmeDto
                {
                    Titulo = filme.Titulo,
                    Diretor = filme.Diretor,
                    Duracao = filme.Duracao,
                    Id = filme.Id,
                    Genero = filme.Genero,
                    HoraDaConsulta = DateTime.Now
                };*/

                return Ok(enderecoDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")] //Put - verbo especifico para atualização
        public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
            /*Como atualizar esse filme? Para isso preciso receber informaões 
             novas para atualizar esse filme. Então a partir do corpo da requi-
            sição, deseja-se receber um "filmeNovo"  
            a*/
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if(endereco == null)
            {
                return NotFound(); //error 404 pela arquitetura REST
            }

            _mapper.Map(enderecoDto, endereco);
            /*pegando as informações do filmeDto e passando para o filme
            Ou seja, está se sobreescrevendo o filme com as informações
            do filmeDto*/

            /*(nesse caso não se quer converter um objeto para outro tipo
             * mas sim dois objetos entre si.)
            filme.Titulo = filmeDto.Titulo;
            filme.Genero = filmeDto.Genero;
            filme.Duracao = filmeDto.Duracao;
            filme.Diretor = filmeDto.Diretor;*/
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaEndereco(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }
            _context.Remove(endereco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
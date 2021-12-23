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

namespace FilmesAPi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private AppDbContext _context;
        //um campo do tipo AppDbContext, inicializando
        //esse campo com _context;
        private IMapper _mapper;


        //inicializando _context e _mapper via construtor
        public FilmeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        
        {
           // List<Filme> filme = _mapper.Map<List<Filme>>(filmeDto);
            //convertendo um filmeDto(do tipo CreateFilmeDto) para um Filme e guardando na váriavel filme.

            /*Filme filme = new Filme 
            criação de um objeto com um construtor implicito

            Abaixo está se passando para o  "filme", as propriedades
            do filmeDto

            {
                Titulo = filmeDto.Titulo,
                Genero = filmeDto.Genero,
                Duracao = filmeDto.Duracao,
                Diretor = filmeDto.Diretor
            };*/

            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new { Id = filme.Id }, filme);         
        }

        [HttpGet]
        public IActionResult RecuperaFilmes([FromQuery] int? classificacaoEtaria = null)
            /*parâmetro de consulta [FromQuery] */
        {
            List<Filme> filmes;
            if(classificacaoEtaria == null)
            {
                filmes = _context.Filmes.ToList();
            }
            else
            {
                filmes = _context
                .Filmes.Where(filme => filme.ClassificacaoEtaria <= classificacaoEtaria).ToList();
            }
            
            if(filmes != null)
            {
                List<ReadFilmeDto> readDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
                return Ok(readDto);
            }
            return NotFound();
            //return _context.Filmes;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmesPorId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);

                /*ReadFilmeDto filmeDto = new ReadFilmeDto
                {
                    Titulo = filme.Titulo,
                    Diretor = filme.Diretor,
                    Duracao = filme.Duracao,
                    Id = filme.Id,
                    Genero = filme.Genero,
                    HoraDaConsulta = DateTime.Now
                };*/

                return Ok(filmeDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")] //Put - verbo especifico para atualização
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
            /*Como atualizar esse filme? Para isso preciso receber informaões 
             novas para atualizar esse filme. Então a partir do corpo da requi-
            sição, deseja-se receber um "filmeNovo"  
            a*/
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if(filme == null)
            {
                return NotFound(); //error 404 pela arquitetura REST
            }

            _mapper.Map(filmeDto, filme);
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
        public IActionResult DeletaFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound();
            }
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
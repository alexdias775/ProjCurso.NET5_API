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
    public class CinemaController : ControllerBase
    {
        private AppDbContext _context;
        //um campo do tipo FilmeContext, inicializando
        //esse campo com _context;
        private IMapper _mapper;


        //inicializando _context e _mapper via construtor
        public CinemaController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
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

            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = cinema.Id }, cinema);
        }

        [HttpGet]
        public IEnumerable<Cinema> RecuperaCinemas()
        {
            return _context.Cinemas;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaCinemasPorId(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);

                /*ReadFilmeDto filmeDto = new ReadFilmeDto
                {
                    Titulo = filme.Titulo,
                    Diretor = filme.Diretor,
                    Duracao = filme.Duracao,
                    Id = filme.Id,
                    Genero = filme.Genero,
                    HoraDaConsulta = DateTime.Now
                };*/

                return Ok(cinemaDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")] //Put - verbo especifico para atualização
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
            /*Como atualizar esse filme? Para isso preciso receber informaões 
             novas para atualizar esse filme. Então a partir do corpo da requi-
            sição, deseja-se receber um "filmeNovo"  
            a*/
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if(cinema == null)
            {
                return NotFound(); //error 404 pela arquitetura REST
            }

            _mapper.Map(cinemaDto, cinema);
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
        public IActionResult DeletaCinema(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }
            _context.Remove(cinema);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
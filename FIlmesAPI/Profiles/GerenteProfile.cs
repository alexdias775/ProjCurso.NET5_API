using AutoMapper;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using System.Linq;

namespace FilmesAPI.Profiles
{
    public class GerenteProfile : Profile
    {
        public GerenteProfile()
        {
            CreateMap<CreateGerenteDto, Gerente>();
            CreateMap<Gerente, ReadGerenteDto>()
                .ForMember(gerente => gerente.Cinemas, opts => opts
                .MapFrom(gerente => gerente.Cinemas.Select
                (c => new {c.Id, c.Nome, c.Endereco, c.EnderecoId})));
            /*Está se mapeando de gerente para ReadGerenteDto. 
            E para o membro de Cinemas dentro do gerente, para esta pro-
            priedade, está se definindo as seguintes opções: 
            mapei do gerente e selecione e retorne os seguintes: 
            Id, Nome, Endereço, EndereoId do (c => new ....) */
        }
    }
}

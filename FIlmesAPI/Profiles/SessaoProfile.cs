using AutoMapper;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class SessaoProfile : Profile
    {
        public SessaoProfile()
        {
            CreateMap<CreateSessaoDto, Sessao>();
            CreateMap<Sessao, ReadSessaoDto>()
                .ForMember(dto => dto.HorarioDeInicio, opts => opts
                .MapFrom(dto =>
                dto.HorarioDeEncerramento.AddMinutes(dto.Filme.Duracao * (-1))));
            /*Para um membro do dto, que será o dto.HorarioDeInicio, quer se 
             calcular o mesmo através das opções previamente definidas a seguir: 
            Mapeando a partir do dto também, através do horárioDeEncerramento
            e a partir desse HorarioDeEncerramento e utilizado o metódo 
            AddMinutes para somar o minutos de duração do filme, subtraisse 
            multiplicando -1
            
             EM RESUMO: se tem o horário de encerramento e se tem o horário total
            do filme, logo para achar o horário inicial, só pegar horário final
            menos o tempo de duração do filme*/

        }
    }
}

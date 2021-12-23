using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FilmesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
            /*explicitando com o "OnModelCreating" que se deseja fa-
             zer algumas definições na hora da criação dos modelos*/

            /*Abaixo informa-se como os modelos serão criados*/
        {
            builder.Entity<Endereco>()
                /*está se construindo que, uma entidade do tipo
                Endereco tem um Cinema(na primeira linha abaixo) */
                .HasOne(endereco => endereco.Cinema)
                /*se define abaixo também que, o Cinema da linha
                acima possui um endereco*/
                .WithOne(cinema => cinema.Endereco)
                /*abaixo se define que a chave estrangeira está 
                alojada em Cinema e é o EnderecoId*/
                .HasForeignKey<Cinema>(cinema => cinema.EnderecoId);

            builder.Entity<Cinema>() //um cinema
                .HasOne(cinema => cinema.Gerente) //vai ter um Gerente
                .WithMany(gerente => gerente.Cinemas) //e um Gerente vai ter muitos Cinemas
                .HasForeignKey(cinema => cinema.GerenteId).IsRequired(false); //chave estrangeira liga as tabelas
                /*Não precisa especificar <Cinema> pois se começou com Cinema e terminou com cinema.
                 ficou Implicito */

                /*1° opção - .OnDelete(DeleteBehavior.Restrict); caso não se queira que
                 * ao deletar gerente se delete cinema, tem que restrigir e depois
                 * fazer Add-Migration 
                 *2° opção - Colocar ".IsRequired(false)" isso possibilita que o cinema seja criado
                 *com uma chave de gerente null
                 */

            builder.Entity<Sessao>()
                .HasOne(sessao => sessao.Filme)
                .WithMany(filme => filme.Sessoes)
                .HasForeignKey(sessao => sessao.FilmeId);

            builder.Entity<Sessao>()
                .HasOne(sessao => sessao.Cinema)
                .WithMany(cinema => cinema.Sessoes)
                .HasForeignKey(sessao => sessao.CinemaId);
        }
        /*Chave estrangeira (foreign key) é o campo que estabelece o relacionamento entre duas tabe-
         * las. Assim, uma coluna corresponde à mesma coluna que é a chave primária de outra tabela.
         * Dessa forma, deve-se especificar na tabela que contém a chave estrangeira quais são essas 
         * colunas e à qual tabela está relacionada. */

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Gerente> Gerentes { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }
    }
}

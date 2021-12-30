using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace UsuariosAPI.Models
{
    public class Mensagem
    {
        /*O destinatário não será puramente uma string, o mesmo precisará ser um
         tipo especial, que identifica um endereço de email, que é MailboxAddress*/
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; } 
        
        public Mensagem(IEnumerable<string> destinatario, string assunto,
            int usuarioId, string codigo)
        {
            /* Abaixo está se instanciando uma lista de MailboxAddress*/
            Destinatario = new List<MailboxAddress>();
            /*E a esta lista que está sendo estanciada, está se adicionando a mesma
             um novo elemento, que é um destinatário(d),ou seja, uma string que está
            na lista de destinatario recebida por parâmetro e é necessário converter 
            para MailboxAddress(d)*/
            Destinatario.AddRange(destinatario.Select(d => new MailboxAddress(d)));
            /**/
            Assunto = assunto;
            Conteudo = $"http://localhost:6000/ativa?UsuarioId={usuarioId}&CodigoDeAtivacao={codigo}";      
        }
    }
}

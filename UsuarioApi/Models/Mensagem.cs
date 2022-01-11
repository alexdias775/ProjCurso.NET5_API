using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace UsuarioApi.Models
{
    public class Mensagem
    {
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<string> destinatario, 
            string assunto, int usuarioId, string codigoDeAtivacao)
        {
            Destinatario = new List<MailboxAddress>();
            Destinatario.AddRange(destinatario.Select(d => new MailboxAddress(d)));
            //convertendo destinatario que está sendo recebido como parametro via msg e convertendo para um tipo MailboxAddress
            Assunto = assunto;
            Conteudo = $"http://localhost:6000/ativa?UsuarioId={usuarioId}&CodigoDeAtivacao={codigoDeAtivacao}";
        }
    }
}

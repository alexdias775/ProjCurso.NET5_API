using MimeKit;
using System;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class EmailService
    {
        public void EnviarEmail(string[] destinatario, string assunto,
            int usuarioId, string code)
        {
            /* A partir do momento em que se quer enviar um email, precisa-se
            compor uma mensagem*/
            Mensagem mensagem = new Mensagem(destinatario,
                assunto, usuarioId, code);

            /*
             - É necessário converter essa mensagem em realmente uma mensagem de email 
             utilizando os artificioes do pacote do Mailkit e MimeKit.
             */
            var mensagemDeEmail = CriaCorpoDoEmail(mensagem);
            Enviar(mensagemDeEmail);
        }

        private void Enviar(object mensagemDeEmail)
        {
            //throw new NotImplementedException();
        }

        private object CriaCorpoDoEmail(Mensagem mensagem)
        {
            var mensagemDeEmail = new MimeMessage();
            mensagemDeEmail.From.Add(new MailboxAddress("ADICIONAR O REMETENTE"));
            mensagemDeEmail.To.AddRange(mensagem.Destinatario);
            mensagemDeEmail.Subject = mensagem.Assunto;
            mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = mensagem.Conteudo
            };

            return mensagemDeEmail;
        }
    }
}

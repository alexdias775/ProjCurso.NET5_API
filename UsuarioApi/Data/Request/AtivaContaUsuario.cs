using System.ComponentModel.DataAnnotations;

namespace UsuarioApi.Data.Request
{
    public class AtivaContaUsuario
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public string CodigoDeAtivacao { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;

namespace UsuarioApi.Data.Request
{
    public class SolicitaResetRequest
    {
        [Required]
        public string Email { get; set; }
    }
}

using System;

namespace GesDoc.Models
{
    public class UsuarioGrupoCliente
    {
        public int codUsuario { get; set; }
        public int codCliente { get; set; }
        public int codGrupo { get; set; }
        public string nomeCliente { get; set; }
        public int usuarioCadastro { get; set; }
        public DateTime? dataCadastro { get; set; }
        public bool consultaGrupo { get; set; }
    }       
}

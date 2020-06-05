using System;

namespace GesDoc.Models
{
    public class UsuarioOnLine
    {
        public int CodUsuario { get; set; }
        public string USLogin { get; set; }
        public string IdSessao { get; set; }
        public DateTime? HorarioLogin { get; set; }
    }
}
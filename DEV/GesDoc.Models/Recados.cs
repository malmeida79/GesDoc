using System;

namespace GesDoc.Models
{
    public class Recados
    {
        public int CodRecado { get; set; }
        public string Recado { get; set; }
        public int CodTipoRecado { get; set; }
        public DateTime? DataRecado { get; set; }
        public int CodUsuarioRecado { get; set; }
        public bool Ativo { get; set; }
    }
}
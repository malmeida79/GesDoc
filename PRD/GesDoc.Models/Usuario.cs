using System;

namespace GesDoc.Models
{
    public class Usuario
    {
        public int codUsuario { get; set; }
        public string nomeUsuario { get; set; }
        public string sobreNome { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string email { get; set; }
        public DateTime? dataCadastro { get; set; }
        public DateTime? dataUltimoAcesso { get; set; }
        public DateTime? dataAlteracao { get; set; }
        public bool Bloqueado { get; set; }
        public bool Ativo { get; set; }
        public bool TipoCliente{get;set;}
        public bool AssinaDocumento { get; set; }
        public bool LiberaDocumento { get; set; }
        public int codCliente { get; set; }
        public string docsPastaRaiz = string.Empty;
        public bool deletado { get; set; }
    }
}
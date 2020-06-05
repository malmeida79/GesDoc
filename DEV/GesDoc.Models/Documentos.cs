using System;

namespace GesDoc.Models
{
    public class Documentos : TipoDocumento
    {
        public int CodDocumento { get; set; }
        public int CodCliente { get; set; }
        public string NomeCliente { get; set; }
        public int CodEquipamento { get; set; }
        public string NomeDocumento { get; set; }
        public string HashCode { get; set; }
        public string HashCodeAposAssinado { get; set; }
        public int CodUsuarioGeracao { get; set; }
        public int CodTipoServico { get; set; }
        public DateTime? DataGeracao { get; set; }
        public bool Assinado { get; set; }
        public int CodUsuarioAssinatura { get; set; }
        public DateTime? DataAssinatura { get; set; }
        public bool Liberado { get; set; }
        public int CodUsuarioLiberacao { get; set; }
        public DateTime? DataLiberacao { get; set; }
        public bool ClienteNotificado { get; set; }
        public string EmailNotificacao { get; set; }
        public DateTime? DataNotificacao { get; set; }
        public string UsuarioGeracao { get; set; }
        public string UsuarioAssinatura { get; set; }
        public string UsuarioLiberacao { get; set; }
    }
}

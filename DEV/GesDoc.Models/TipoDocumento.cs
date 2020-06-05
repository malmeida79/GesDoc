namespace GesDoc.Models
{
    public class TipoDocumento
    {
        public int CodTipoDocumento { get; set; }
        public string DescricaoTipoDocumento { get; set; }
        public bool TipoGeral { get; set; }
        public bool TipoCliente { get; set; }
        public bool ExigeLiberacao { get; set; }
        public bool ClassificadoTipoServico { get; set; }
    }
}
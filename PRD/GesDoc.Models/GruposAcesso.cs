namespace GesDoc.Models
{
    public class GruposAcesso:Grupos
    {
        public string InfoGrupo { get; set; }
        public bool GrupoPadrao { get; set; }
        public bool GrupoPadraoCliente { get; set; }
    }
}
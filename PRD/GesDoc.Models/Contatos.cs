namespace GesDoc.Models
{
    public class Contatos
    {
        public int CodContato { get; set; }
        public int CodCliente { get; set; }
        public string Nome { get; set; }
        public string CodDDD { get; set; }
        public string Telefone { get; set; }
        public string Ramal { get; set; }
        public string Email { get; set; }
        public int CodTipoContato { get; set; }
        public string TipoContato { get; set; }
    }
}
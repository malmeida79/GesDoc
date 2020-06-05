namespace GesDoc.Models
{
    public class Cliente :GruposClientes
    {
        public int CodCliente { get; set; }
        public string NomeCliente { get; set; }
        public string CpfCnpjCliente { get; set; }
        public string RazaoSocialCliente { get; set; }
        public bool Status { get; set; }
        public bool Deletado { get; set; }
    }
}
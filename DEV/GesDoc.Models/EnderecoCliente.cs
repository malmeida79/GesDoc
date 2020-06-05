namespace GesDoc.Models
{
    public class EnderecoCliente : TipoEndereco
    {
        public int CodEnderecoCliente { get; set; }
        public int CodCliente { get; set; }
        public int CodEndereco { get; set; }
        public string DescricaoLogradouro { get; set; }
        public string DescricaoEndereco { get; set; }
        public string CepEndereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Referencia { get; set; }
        public string DescricaoBairro { get; set; }
        public string DescricaoCidade { get; set; }
        public string DescricaoEstado { get; set; }
        public string Descricaopais { get; set; }
        public string UFEstado { get; set; }
    }
}

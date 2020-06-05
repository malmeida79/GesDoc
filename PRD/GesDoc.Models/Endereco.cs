namespace GesDoc.Models
{
    public class Endereco:Bairro
    {
        public int CodLogradouro { get; set; }
        public string DescricaoLogradouro { get; set; }
        public int CodEndereco { get; set; }
        public string DescricaoEndereco { get; set; }
        public string CepEndereco { get; set; }
    }
}

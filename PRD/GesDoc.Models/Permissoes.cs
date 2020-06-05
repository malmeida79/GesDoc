namespace GesDoc.Models
{
    public class Permissoes
    {
        string Pagina { get; set; }
        public bool Leitura { get; set; }
        public bool Gravacao { get; set; }
        public bool Excluir { get; set; }
        public bool AssinaDocumento = false;
        public bool LiberaDocumento = false;
        public bool EhCliente = false;
    }
}
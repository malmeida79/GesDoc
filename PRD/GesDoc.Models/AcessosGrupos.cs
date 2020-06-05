namespace GesDoc.Models
{
    public class AcessosGrupos : Funcionalidades
    {
        public int CodGrupo { get; set; }
        public string NomeGrupo { get; set; }
        public string DescricaoAcesso { get; set; }
        public string UrlAcesso { get; set; }
        public bool Acesso { get; set; }
        public bool JahExistia { get; set; }
        public bool Leitura { get; set; }
        public bool Gravacao { get; set; }
        public bool Excluir { get; set; }
        public bool ErroAcesso { get; set; }
    }
}

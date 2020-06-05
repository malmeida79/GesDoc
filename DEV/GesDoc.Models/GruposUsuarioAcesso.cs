namespace GesDoc.Models
{
    public class GruposUsuarioAcesso : GruposAcesso
    {
        public int CodUsuario { get; set; }
        public bool Acesso { get; set; }
        public bool JahExistia { get; set; }
    }
}
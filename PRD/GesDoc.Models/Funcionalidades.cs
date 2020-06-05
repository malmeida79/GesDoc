namespace GesDoc.Models
{
    public class Funcionalidades:Departamentos
    {
        public int CodFuncionalidade { get; set; }
        public string DescricaoFuncionalidade { get; set; }
        public string UrlFuncionalidade { get; set; }
        public bool ExibeMenu { get; set; }
        public bool FuncionalidadePadrao { get; set; }
    }       
}

using System.Collections.Generic;
using System.Drawing;

namespace GesDoc.Models
{
    /// <summary>
    /// Classe para tratar os dados de conexao e acesso do usuário.
    /// </summary>
    public class UsuarioLogado : Usuario
    {
        public int Tentativas = 0;
        public List<AcessosGrupoUsuario> GETFuncionalidesAcessos = null;
        public List<GruposUsuarioAcesso> GETGruposAcessos = null;
        public List<int> GETCodGruposAcesso = null;
        public List<int> GETClientesAcesso = null;
        public List<UsuarioGrupoCliente> GETDadosAcessosGrupoClientes = null;
        public List<AcessosGrupoUsuario> GETMenuUsuario = null;
        public string TextoLabel = string.Empty;
        public Color CorLabel = Color.White;
        public int codtipoRecado = 0;
    }

}

using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class clonadorAcessosGrupo : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        GruposAcessoController CtrlGrp;
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Clonador de acessos ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            if (!Page.IsPostBack)
            {
                CtrlGrp = new GruposAcessoController();

                CarregaGrid();
            }

        }

        protected void gdvGrupos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvGrupos.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvGrupos_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            var lista = CtrlGrp.GetAll();

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<GruposAcesso>(SortExp, Sortdir);

            CarregaGrid(lista);

        }

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            Session["acessoGrupoEditar"] = string.Empty;
            Server.Transfer("listaControleAcesso.aspx");
        }

        protected void gdvGrupos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = int.Parse((string)e.CommandArgument);

            string codigoGrupoOrigem = gdvGrupos.Rows[index].Cells[0].Text;

            if (e.CommandName == "clone")
            {
                AcessosGruposController CtrlClone = new AcessosGruposController();
                if (CtrlClone.ClonarAcessosGrupo(Convert.ToInt32(codigoGrupoOrigem), Convert.ToInt32((string)Session["acessoGrupoEditar"])))
                {
                    Mensagens.Alerta("Acessos clonados com sucesso!");
                    CtrlClone = null;
                    return;
                }
                else
                {
                    Mensagens.Alerta($"Ocorreu um erro:{Mensagens.MsgErro}");
                    CtrlClone = null;
                    return;
                }
            }
        }

        #endregion

        #region "Metodos" 

        protected void CarregaGrid(List<GruposAcesso> lista = null)
        {
            if (lista == null)
            {
                lista = new List<GruposAcesso>();
                lista = CtrlGrp.GetAll();
            }

            gdvGrupos.Preencher<GruposAcesso>(lista);
        }

        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        
        #endregion


    }
}
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
    public partial class clonadorAcessos : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        UsuarioController CtrlUsr;
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
                CtrlUsr = new UsuarioController();

                CarregaGrid();
                Session["FiltroUsuario"] = string.Empty;
                Session["selecaoEmail"] = false;
                Session["selecaoNome"] = false;
                rdpesquisaEmail.Checked = false;
                rdPesquisanome.Checked = false;
            }

        }

        protected void gdvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvUsuarios.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvUsuarios_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            var lista = CtrlUsr.GetAll();

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<Usuario>(SortExp, Sortdir);

            CarregaGrid(lista);

        }

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            Session["usuarioEditar"] = string.Empty;
            Server.Transfer("listaControleAcesso.aspx");
        }

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {

            if (rdpesquisaEmail.Checked)
            {
                List<Usuario> lista = CtrlUsr.PesquisarLista(null, txtParPesquisa.Text, true);
                CarregaGrid(lista);
            }
            else if (rdPesquisanome.Checked)
            {
                List<Usuario> lista = CtrlUsr.PesquisarLista(null, txtParPesquisa.Text);
                CarregaGrid(lista);
            }
            else
            {
                Mensagens.Alerta("Informe um tipo de pesquisa");
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            Session["FiltroUsuario"] = string.Empty;
            Session["selecaoEmail"] = false;
            Session["selecaoNome"] = false;
            txtParPesquisa.Text = string.Empty;
            rdpesquisaEmail.Checked = false;
            rdPesquisanome.Checked = false;
        }
        #endregion

        #region "Metodos" 

        protected void CarregaGrid(List<Usuario> lista = null)
        {
            if (lista == null)
            {
                lista = new List<Usuario>();
                lista = CtrlUsr.GetAll();
            }

            gdvUsuarios.Preencher<Usuario>(lista);
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

        protected void gdvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                bool bloqueado = Convert.ToBoolean(e.Row.Cells[5].Text);

                // Tratamento para true/false sair como ativo/inativo
                if (bloqueado)
                {
                    e.Row.Cells[5].Text = "Sim";
                }
                else
                {
                    e.Row.Cells[5].Text = "Não";
                }


                bool status = Convert.ToBoolean(e.Row.Cells[6].Text);

                // Tratamento para true/false sair como ativo/inativo
                if (status)
                {
                    e.Row.Cells[6].Text = "Ativo";
                }
                else
                {
                    e.Row.Cells[6].Text = "Inativo";
                }

                bool statusUsr = Convert.ToBoolean(e.Row.Cells[7].Text);

                // Tratamento para true/false sair como ativo/inativo
                if (statusUsr)
                {
                    e.Row.Cells[7].Text = "Deletado";
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }
                else
                {
                    e.Row.Cells[7].Text = "";
                }
            }
        }

        protected void gdvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = int.Parse((string)e.CommandArgument);

            string codigoUsuarioOrigem = gdvUsuarios.Rows[index].Cells[0].Text;

            if (e.CommandName == "clone")
            {
                UsuarioController CtrlClone = new UsuarioController();
                if (CtrlClone.ClonarAcessosUsuario(Convert.ToInt32(codigoUsuarioOrigem), Convert.ToInt32((string)Session["usuarioEditar"])))
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
    }
}
using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class listaUsuarios : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        UsuarioController CtrlUsr;
        Permissoes permissoes;

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Lista de usuários ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            CtrlUsr = new UsuarioController();

            ButtonBar.NovoClick += new EventHandler(btnNovo_Click);
            ButtonBar.PesquisaClick += new EventHandler(btnPesquisa_Click);
            ButtonBar.ExportCsvClick += new EventHandler(ExportToCsv_Click);
            ButtonBar.ExportExcelClick += new EventHandler(ExportToExcel_Click);
            ButtonBar.ExportTxtClick += new EventHandler(ExportToTxt_Click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultListBar(permissoes);
                ButtonBar.DisableExports(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Novo, texto: @"<span class="" glyphicon glyphicon-plus""></span> Novo Usuário");

                CarregaGrid();

                Session["FiltroUsuario"] = string.Empty;
                Session["selecaoEmail"] = false;
                Session["selecaoNome"] = false;
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

            var lista = CtrlUsr.ListarCompleta();

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<Usuario>(SortExp, Sortdir);

            CarregaGrid(lista);

        }

        protected void gdvUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["usuarioEditar"] = gdvUsuarios.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadusuarios.aspx");
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Session["usuarioEditar"] = string.Empty;
            Server.Transfer("cadusuarios.aspx");
        }

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (txtParPesquisa.Text.Contains("@"))
            {
                List<Usuario> lista = CtrlUsr.PesquisarLista(null, txtParPesquisa.Text, true);
                CarregaGrid(lista);
            }
            else
            {
                List<Usuario> lista = CtrlUsr.PesquisarLista(null, txtParPesquisa.Text);
                CarregaGrid(lista);
            }

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            Session["FiltroUsuario"] = string.Empty;
            Session["selecaoEmail"] = false;
            Session["selecaoNome"] = false;
            txtParPesquisa.Text = string.Empty;
            gdvUsuarios.DataSource = null;
            chkDeletados.Checked = false;
        }

        protected void gdvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LinkButton addButton = (LinkButton)e.Row.Cells[0].Controls[0];
                    addButton.Text = "Selecionar";
                }

                bool bloqueado = Convert.ToBoolean(e.Row.Cells[6].Text);

                // Tratamento para true/false sair como ativo/inativo
                if (bloqueado)
                {
                    e.Row.Cells[6].Text = "Sim";
                }
                else
                {
                    e.Row.Cells[6].Text = "Não";
                }


                bool status = Convert.ToBoolean(e.Row.Cells[7].Text);

                // Tratamento para true/false sair como ativo/inativo
                if (status)
                {
                    e.Row.Cells[7].Text = "Ativo";
                }
                else
                {
                    e.Row.Cells[7].Text = "Inativo";
                }

                bool statusUsr = Convert.ToBoolean(e.Row.Cells[8].Text);

                // Tratamento para true/false sair como ativo/inativo
                if (statusUsr)
                {
                    e.Row.Cells[8].Text = "Deletado";
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }
                else
                {
                    e.Row.Cells[8].Text = "";
                }
            }
        }

        protected void ExportToCsv_Click(Object sender, EventArgs e)
        {
            List<Usuario> lista = new List<Usuario>();
            lista = CtrlUsr.ListarCompleta();

            if (!chkDeletados.Checked)
            {
                lista = lista.Where(x => x.deletado == false).ToList();
            }

            Exports.ListToCSV<Usuario>(lista, "Usuario");
        }

        protected void ExportToTxt_Click(Object sender, EventArgs e)
        {
            List<Usuario> lista = new List<Usuario>();
            lista = CtrlUsr.ListarCompleta();

            if (!chkDeletados.Checked)
            {
                lista = lista.Where(x => x.deletado == false).ToList();
            }

            Exports.ListToTXT<Usuario>(lista, "Usuarios");
        }

        protected void ExportToExcel_Click(Object sender, EventArgs e)
        {
            List<Usuario> lista = new List<Usuario>();
            lista = CtrlUsr.ListarCompleta();

            if (!chkDeletados.Checked)
            {
                lista = lista.Where(x => x.deletado == false).ToList();
            }

            Exports.ListToExcel<Usuario>(lista, "Usuarios");
        }

        #endregion

        #region "Metodos" 

        protected void CarregaGrid(List<Usuario> lista = null)
        {
            if (lista == null)
            {
                lista = new List<Usuario>();
                lista = CtrlUsr.ListarCompleta();
            }

            if (!chkDeletados.Checked)
            {
                lista = lista.Where(x => x.deletado == false).ToList();
            }

            gdvUsuarios.Preencher<Usuario>(lista);
            ButtonBar.EnableExports(permissoes);
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

        protected void chkDeletados_CheckedChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }
    }
}
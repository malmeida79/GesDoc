using GesDoc.Web.Controllers;
using GesDoc.Web.Services;
using GesDoc.Models;
using System;

namespace GesDoc.Web.Controls
{
    public partial class DropTipoServico : System.Web.UI.UserControl
    {
        public event EventHandler SelectedIndexChanged;
        public string valorSelecionado = null;

        public Int32 GetSelectedValue()
        {
            return cboTipoServico.SelectedValue.RecuperarValor<Int32>();
        }

        public Int32 GetSelectedIndex()
        {
            return cboTipoServico.SelectedIndex.RecuperarValor<Int32>();
        }

        public string GetSelectedText()
        {
            return cboTipoServico.SelectedItem.Text.RecuperarValor<string>();
        }
        public Int32 GetItemCount()
        {
            return cboTipoServico.Items.Count;
        }

        public void SetEnable(bool enable)
        {
            cboTipoServico.Enabled = enable;
        }

        public void SetSelectedValue(string selecionado)
        {
            cboTipoServico.SetSelectedValue(selecionado);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(sender, e);
            }
        }

        protected void cboTipoServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedIndexChanged(sender, e);
        }

        public void CarregaTipoServico(string selecionado = null)
        {
            TipoServicoController CtrltpSrv = new TipoServicoController();
            cboTipoServico.Preencher<TipoServico>(CtrltpSrv.GetAll(), "descricaoTipoServico", "codigoTipoServico", true, "Selecione", selecionado);
            CtrltpSrv = null;
        }

        public void Descarrega()
        {
            cboTipoServico.Descarregar();
        }
    }
}
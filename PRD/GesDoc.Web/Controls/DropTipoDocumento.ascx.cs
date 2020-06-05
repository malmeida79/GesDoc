using GesDoc.Web.Controllers;
using GesDoc.Web.Services;
using GesDoc.Models;
using System;

namespace GesDoc.Web.Controls
{
    public partial class DropTipoDocumento : System.Web.UI.UserControl
    {
        public event EventHandler SelectedIndexChanged;
        public string valorSelecionado = null;

        public Int32 GetSelectedValue()
        {
            return cboTipoDocumento.SelectedValue.RecuperarValor<Int32>();
        }

        public Int32 GetSelectedIndex()
        {
            return cboTipoDocumento.SelectedIndex.RecuperarValor<Int32>();
        }

        public string GetSelectedText()
        {
            return cboTipoDocumento.SelectedItem.Text.RecuperarValor<string>();
        }
        public Int32 GetItemCount()
        {
            return cboTipoDocumento.Items.Count;
        }

        public void SetEnable(bool enable)
        {
            cboTipoDocumento.Enabled = enable;
        }

        public void SetSelectedValue(string selecionado)
        {
            cboTipoDocumento.SetSelectedValue(selecionado);
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

        protected void cboTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedIndexChanged(sender, e);
        }

        public void CarregaTipoDocumento(string selecionado = null)
        {
            TipoDocumentoController Ctrltpdoc = new TipoDocumentoController();
            cboTipoDocumento.Preencher<TipoDocumento>(Ctrltpdoc.GetAll(), "DescricaoTipoDocumento", "codTipoDocumento", true, "Selecione", selecionado);
            Ctrltpdoc = null;
        }

        public void Descarrega()
        {
            cboTipoDocumento.Descarregar();
        }
    }
}
using GesDoc.Web.Controllers;
using GesDoc.Web.Services;
using GesDoc.Models;
using System;

namespace GesDoc.Web.Controls
{
    public partial class DropClientes : System.Web.UI.UserControl
    {
        public event EventHandler SelectedIndexChanged;
        public string valorSelecionado = null;

        public Int32 GetSelectedValue() {
            return cboClientes.SelectedValue.RecuperarValor<Int32>();
        }

        public Int32 GetSelectedIndex()
        {
            return cboClientes.SelectedIndex.RecuperarValor<Int32>();
        }

        public string GetSelectedText()
        {
            return cboClientes.SelectedItem.Text.RecuperarValor<string>();
        }

        public Int32 GetItemCount()
        {
            return cboClientes.Items.Count;
        }

        public void SetEnable(bool enable) {
            cboClientes.Enabled = enable;
        }

        public void SetSelectedValue(string selecionado) {
            cboClientes.SetSelectedValue(selecionado);
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

        protected void cboClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedIndexChanged(sender, e);
        }

        public void CarregaClientes(string selecionado = null)
        {
            ClientesController CtrlCli = new ClientesController();
            cboClientes.Preencher<Cliente>(CtrlCli.GetAll(), "nomeCliente", "codCliente", true, "Selecione", selecionado);
            CtrlCli = null;
        }

        public void Descarrega()
        {
            cboClientes.Descarregar();
        }
    }
}
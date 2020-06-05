using GesDoc.Web.Controllers;
using GesDoc.Web.Services;
using GesDoc.Models;
using System;

namespace GesDoc.Web.Controls
{
    public partial class DropEquipamentos : System.Web.UI.UserControl
    {
        public event EventHandler SelectedIndexChanged;
        public string valorSelecionado = null;
        public Int32 ClienteReferencia = 0;

        public Int32 GetSelectedValue()
        {
            return cboEquipamentos.SelectedValue.RecuperarValor<Int32>();
        }

        public Int32 GetSelectedIndex()
        {
            return cboEquipamentos.SelectedIndex.RecuperarValor<Int32>();
        }

        public string GetSelectedText()
        {
            return cboEquipamentos.SelectedItem.Text.RecuperarValor<string>();
        }

        public Int32 GetItemCount()
        {
            return cboEquipamentos.Items.Count;
        }

        public void SetEnable(bool enable)
        {
            cboEquipamentos.Enabled = enable;
        }

        public void SetSelectedValue(string selecionado)
        {
            cboEquipamentos.SetSelectedValue(selecionado);
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

        protected void cboEquipamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedIndexChanged(sender, e);
        }

        public void CarregaEquipamentos(string selecionado = null)
        {
            EquipamentosController CtrlEquip = new EquipamentosController();
            cboEquipamentos.Preencher<Equipamento>(CtrlEquip.PesquisarPorCodigoClienteComTipo(Convert.ToInt32(ClienteReferencia)), "descricaoEquipamento", "codEquipamento", true, "Selecione", selecionado);
            CtrlEquip = null;
        }

        public void Descarrega()
        {
            cboEquipamentos.Descarregar();
        }
    }
}
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DropTipoServico.ascx.cs" Inherits="GesDoc.Web.Controls.DropTipoServico" %>

<div class="form-group">
    <asp:Label ID="lblTipoServico" runat="server" Text="Selecione o Tipo de serviço:"></asp:Label>
    &nbsp;
        <asp:DropDownList ID="cboTipoServico" runat="server" AutoPostBack="True" Height="22px" OnSelectedIndexChanged="cboTipoServico_SelectedIndexChanged" Width="488px">
        </asp:DropDownList>

</div>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DropClientes.ascx.cs" Inherits="GesDoc.Web.Controls.DropClientes" %>

<div class="form-group">
    <asp:Label ID="lblClientes" runat="server" Text="Selecione um cliente:"></asp:Label>
    &nbsp;
        <asp:DropDownList ID="cboClientes" runat="server" AutoPostBack="True" Height="22px" OnSelectedIndexChanged="cboClientes_SelectedIndexChanged" Width="488px">
        </asp:DropDownList>

</div>

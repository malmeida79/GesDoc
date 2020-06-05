<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DropTipoDocumento.ascx.cs" Inherits="GesDoc.Web.Controls.DropTipoDocumento" %>

<div class="form-group">
    <asp:Label ID="lblTipoDocumento" runat="server" Text="Selecione o Tipo de documento:"></asp:Label>
    &nbsp;
        <asp:DropDownList ID="cboTipoDocumento" runat="server" AutoPostBack="True" Height="22px" OnSelectedIndexChanged="cboTipoDocumento_SelectedIndexChanged" Width="488px">
        </asp:DropDownList>

</div>

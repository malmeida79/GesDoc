<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DropEquipamentos.ascx.cs" Inherits="GesDoc.Web.Controls.DropEquipamentos" %>

<div class="form-group">
    <asp:Label ID="lblEquipamentos" runat="server" Text="Selecione o equipamento:"></asp:Label>
    &nbsp;
        <asp:DropDownList ID="cboEquipamentos" runat="server" AutoPostBack="True" Height="22px" OnSelectedIndexChanged="cboEquipamentos_SelectedIndexChanged" Width="488px">
        </asp:DropDownList>

</div>

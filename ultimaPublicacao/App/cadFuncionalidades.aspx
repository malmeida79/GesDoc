<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadFuncionalidades.aspx.cs" Inherits="GesDoc.Web.App.cadFuncionalidades" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        <br />
        <asp:HiddenField ID="hdnCodFuncionalidade" runat="server" />
        <table class="tblConteudoForms">
            <tr>
                <td class="cellRight">Departamentos:</td>
                <td class="cellLeft">
                    <asp:DropDownList ID="cboDepartamento" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Selecione o departamento." ValidationGroup="cadastro" ControlToValidate="cboDepartamento" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="cellRight">Nome Funcionalidade:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtNomeFuncionalidade" runat="server" Width="177px" TabIndex="1"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o nome da Funcionalidade." ValidationGroup="cadastro" ControlToValidate="txtNomeFuncionalidade" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="cellRight">Url Funcionalidade:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtUrlFuncionalidade" runat="server" Width="177px" TabIndex="2"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Informe a url da Funcionalidade." ValidationGroup="cadastro" ControlToValidate="txtUrlFuncionalidade" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="cellLeft">
                    <asp:CheckBox ID="chkExibeMenu" runat="server" Text="Exibe menu" TabIndex="3"></asp:CheckBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="cellLeft">
                    <asp:CheckBox ID="chkMenuItemPadrao" runat="server" Text="Item padrão para usuários novos (Libera acesso de forma automática)"></asp:CheckBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>

        <uc1:ButtonBar runat="server" id="ButtonBar" />

    </div>
</asp:Content>

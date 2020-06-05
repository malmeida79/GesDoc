<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadDepartamentos.aspx.cs" Inherits="GesDoc.Web.App.cadDepartamentos" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        <br />
        <asp:HiddenField ID="hdnCodDepartamento" runat="server" />

        <table class="tblConteudoForms">
            <tr>
                <td class="cellRight">Nome Departamento:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtNomeDepartamento" runat="server" Width="177px"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o nome do departamento." ValidationGroup="cadastro" ControlToValidate="txtNomeDepartamento" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="cellLeft">
                    <asp:CheckBox ID="chkMenuPadrao" runat="server" Text="Item padrão para usuários novos (Libera acesso de forma automática)"></asp:CheckBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <uc1:ButtonBar runat="server" id="ButtonBar" />
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadEstados.aspx.cs" Inherits="GesDoc.Web.App.cadEstados" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        <br />
        <asp:HiddenField ID="hdnCodEstado" runat="server" />
        <table class="tblConteudoForms">
            <tr>
                <td class="cellRight">Pais:</td>
                <td class="cellLeft">
                    <asp:DropDownList ID="cboPais" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Selecione o Pais." ValidationGroup="cadastro" ControlToValidate="cboPais" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="cellRight">Nome Estado:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtNomeEstado" runat="server" Width="177px" TabIndex="1"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o nome da Estado." ValidationGroup="cadastro" ControlToValidate="txtNomeEstado" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="cellRight">Uf Estado:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtUfEstado" runat="server" Width="177px" TabIndex="2"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Informe a uf do Estado." ValidationGroup="cadastro" ControlToValidate="txtUfEstado" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>

        <uc1:ButtonBar runat="server" id="ButtonBar" />

    </div>
</asp:Content>

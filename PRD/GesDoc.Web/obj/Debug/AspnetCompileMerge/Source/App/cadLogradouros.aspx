<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadLogradouros.aspx.cs" Inherits="GesDoc.Web.App.cadLogradouros" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">

        <asp:HiddenField ID="hdnCodLogradouro" runat="server" />

        <table class="tblConteudoForms">
            <tr>
                <td class="cellRight">Nome Logradouro:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtNomeLogradouro" runat="server" Width="177px"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o nome do Logradouro." ValidationGroup="cadastro" ControlToValidate="txtNomeLogradouro" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <uc1:ButtonBar runat="server" id="ButtonBar" />
        </table>
    </div>
</asp:Content>

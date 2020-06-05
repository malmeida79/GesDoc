<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadTipoServico.aspx.cs" Inherits="GesDoc.Web.App.cadTipoServico" %>

<%@ Register src="../Controls/ButtonBar.ascx" tagname="ButtonBar" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">

        <asp:HiddenField ID="hdnCodTipoServico" runat="server" />
        <table class="tblConteudoForms">
            <tr>
                <td class="cellRight">Nome Tipo de Servico:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtNomeTipoServico" runat="server" Width="377px"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o nome do Tipo de Servico." ValidationGroup="cadastro" ControlToValidate="txtNomeTipoServico" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="cellCenter"colspan="3">
                    <uc1:ButtonBar ID="ButtonBar" runat="server" />
                </td>
            </tr>
        </table>

    </div>
</asp:Content>

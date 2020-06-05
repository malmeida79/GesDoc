<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadGruposAcesso.aspx.cs" Inherits="GesDoc.Web.App.cadGruposAcesso" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        <br />
        <asp:HiddenField ID="hdnCodGrupo" runat="server" />

        <table class="tblConteudoForms">
            <tr>
                <td class="cellRight">Nome Grupo:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtNomeGrupo" runat="server" Width="177px"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o nome do Grupo." ValidationGroup="cadastro" ControlToValidate="txtNomeGrupo" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="cellRight">Informações do Grupo:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtInfoGrupo" runat="server" Width="177px" Height="53px" TextMode="MultiLine"></asp:TextBox>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="cellRight">Grupo padrão:</td>
                <td class="cellLeft">
                    <asp:CheckBox ID="chkPadrao" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="cellRight">Grupo padrão para clientes:</td>
                <td class="cellLeft">
                    <asp:CheckBox ID="ChkPadraoCliente" runat="server" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="cellCenter" colspan="3">
                    <uc1:ButtonBar runat="server" id="ButtonBar" />
                </td>
            </tr>
                     <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" class="cellCenter">* Grupos padrão serão sempre atribuidos a novos usuários
                    <br />
                    no momento do cadastro destes de forma automática. (Caso, sinalizado padrão cliente apenas usuários desse tipo receberão esse grupo de forma automática).</td>
            </tr>
        </table>

    </div>
</asp:Content>

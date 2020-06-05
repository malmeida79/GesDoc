<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="rastreioDocumento.aspx.cs" Inherits="GesDoc.Web.App.rastreioDocumento" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        <b>Informe o HASH code para o documento a ser pesquisado.</b><br />
        <asp:TextBox ID="txtParPesquisahash" runat="server" Width="578px"></asp:TextBox>
        &nbsp;<br />
        <br />
        &nbsp;&nbsp;<br />
        <uc1:ButtonBar runat="server" ID="ButtonBar" />
        <br />
        <br />
        <asp:Panel ID="pnlResultado" runat="server" CssClass="cosnsultadocumentos">

            <table class="tblConteudoForms">
                <tr>
                    <td class="cellCenter" colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="cellCenter" colspan="2">:: Dados do Documento ::
                    </td>
                </tr>
                <tr>
                    <td class="cellCenter" colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="cellLeft">Código Documento:</td>
                    <td>
                        <asp:Label ID="CodDocumento" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="cellLeft">Arquivo:</td>
                    <td>
                        <asp:Label ID="NomeDocumento" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="cellLeft">Hash Code:</td>
                    <td>
                        <asp:Label ID="HashCode" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="cellLeft">Hash Code Assinado:</td>
                    <td>
                        <asp:Label ID="HashCodeAposAssinado" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="cellLeft">Usuario upload:</td>
                    <td>
                        <asp:Label ID="usuarioGeracao" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="cellLeft">Data upload:</td>
                    <td>
                        <asp:Label ID="dataGeracao" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="cellLeft">Assinado:</td>
                    <td>
                        <asp:Label ID="assinado" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="cellLeft">Usuário Assinatura: </td>
                    <td>
                        <asp:Label ID="usuarioAssinatura" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="cellLeft">Data Assinatura: </td>
                    <td>
                        <asp:Label ID="dataAssinatura" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="cellLeft">Liberado: </td>
                    <td>
                        <asp:Label ID="liberado" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="cellLeft">Usuário Liberação: </td>
                    <td>
                        <asp:Label ID="usuarioLiberacao" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="cellLeft">Data Liberação: </td>
                    <td>
                        <asp:Label ID="dataLiberacao" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="cellLeft">Cliente Notificado: </td>
                    <td>
                        <asp:Label ID="clienteNotificado" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="cellLeft">E-mail Notificação: </td>
                    <td>
                        <asp:Label ID="emailNotificacao" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="cellLeft">Data Notificação: </td>
                    <td>
                        <asp:Label ID="dataNotificacao" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>

            <br />

        </asp:Panel>

    </div>
</asp:Content>

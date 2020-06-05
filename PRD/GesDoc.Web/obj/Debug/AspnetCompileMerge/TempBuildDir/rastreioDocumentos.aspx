<%@ Page Title="Rastreio de documentos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rastreioDocumentos.aspx.cs" Inherits="GesDoc.Web.rastreioDocumentos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        document.getElementById('fxTela').style.display = 'block';
        document.getElementById('fxTela').innerHTML = 'Rastreio de documentos';
    </script>
    <div class="corpoPaginas">
        <br />

        <br />
        <div class="row">
            <div class="col-md-4">&nbsp;</div>
            <div class="col-md-4">
                <b>Informe o HASH code para o documento a ser pesquisado.</b><br />
                 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:TextBox ID="txtParPesquisahash" runat="server" Width="90%"></asp:TextBox>
               <br />
                <br />
                 <br />
                &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;<asp:Button ID="btnPesquisa" BackColor="Gray" ForeColor="White" runat="server" Text="Consultar" ValidationGroup="pesquisa" OnClick="btnPesquisa_Click" TabIndex="4"></asp:Button>
                &nbsp;<asp:Button ID="btnLimpar" BackColor="Gray" ForeColor="White" runat="server" Text="Limpar" OnClick="btnLimpar_Click" TabIndex="5"></asp:Button>
                <br />
                <br />
            </div>
            <div class="col-md-4">&nbsp;</div>
        </div>
        <asp:Panel ID="pnlResultado" runat="server" CssClass="cosnsultadocumentos">

            <table class="tblConteudoForms">
                <tr>
                    <td class="cellCenter" colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="cellCenter" colspan="2">:: Dados do documento ::
                    </td>
                </tr>
                <tr>
                    <td class="cellCenter" colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="cellLeft">Código documento:</td>
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
        <br />
        <br />

        <br />
        <br />
    </div>
</asp:Content>

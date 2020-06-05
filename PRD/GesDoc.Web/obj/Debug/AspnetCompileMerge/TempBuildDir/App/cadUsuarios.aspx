<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadUsuarios.aspx.cs" Inherits="GesDoc.Web.App.cadUsuarios" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 50%;
            text-align: center;
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">

        <table class="tblConteudoForms">
            <tr>
                <td class="cellRight">Usuário é do tipo cliente?:
                </td>
                <td class="cellLeft">
                    <asp:HiddenField ID="hdnCodUsuario" runat="server" />
                    <asp:CheckBox ID="chkTipoCliente" runat="server" Text="" TabIndex="1" AutoPostBack="True" OnCheckedChanged="chkTipoCliente_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td class="cellRight">Nome Usuario:
                </td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtNomeUsuario" runat="server" Width="177px" TabIndex="2"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o nome." ValidationGroup="cadastro" ControlToValidate="txtNomeUsuario" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="cellRight">Sobre Nome:
                </td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtSobreNome" runat="server" Width="177px" TabIndex="3"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Necessário informar sobrenome." ValidationGroup="cadastro" ControlToValidate="txtSobreNome" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="cellRight">Login:
                </td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtLogin" runat="server" Width="177px" TabIndex="4"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Informe o login." ValidationGroup="cadastro" ControlToValidate="txtLogin" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="cellRight">Senha:
                </td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" Width="177px" TabIndex="5"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Informe a senha." ValidationGroup="cadastro" ControlToValidate="txtSenha" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="cellRight">Confirmação de senha:
                </td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtCSenha" runat="server" TextMode="Password" Width="177px" TabIndex="6"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Confirme a senha" ValidationGroup="cadastro" ControlToValidate="txtCSenha" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="cellRight">E-mail:
                </td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtEmail" runat="server" Width="177px" TabIndex="7"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Informe o nome e-mail." ValidationGroup="cadastro" ControlToValidate="txtEmail" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="cellRight">Situação do usuário:</td>
                <td class="cellLeft">
                    <asp:CheckBox ID="chkBloqueado" runat="server" Text="Bloqueado" TabIndex="7" />
                    <asp:CheckBox ID="chkAtivo" runat="server" Text="Ativo" TabIndex="8" />
                </td>
            </tr>
            <tr>
                <td class="cellCenter" colspan="2">:: Permissões Especiais ::</td>
            </tr>
            <tr>
                <td class="cellCenter" colspan="2">
                    <asp:CheckBox ID="chkAssinaDocumento" runat="server" Text="Assina Documento" TabIndex="9" /> &nbsp;&nbsp;
                    <asp:CheckBox ID="chkLiberaDocumento" runat="server" Text="Libera Documento" TabIndex="10" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1" colspan="2">
                    <uc1:ButtonBar runat="server" id="ButtonBar" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="cellCenter" colspan="2">:: Configurações de Acessos ::</td>
            </tr>
            <tr>
                <td class="cellCenter" colspan="2">
                    <asp:Button ID="btnAcessoClientes" runat="server" Text="Clientes do usuário" BackColor="Gray" ForeColor="White" OnClick="btnAcessoClientes_Click" Enabled="False" TabIndex="15"></asp:Button>
                    <asp:Button ID="btnAcessoGrupos" runat="server" Text="Grupos do usuário" BackColor="#00CC00" ForeColor="White" OnClick="btnAcessoGrupos_Click" TabIndex="16"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

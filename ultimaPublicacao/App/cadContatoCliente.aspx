<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadContatoCliente.aspx.cs" Inherits="GesDoc.Web.App.cadContatoCliente" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">

        <asp:HiddenField ID="hdnCodContato" runat="server" />
        <asp:HiddenField ID="hdnCodCliente" runat="server" />
        <table class="tblConteudoForms">
            <tr>
                <td>

                    <table class="tblConteudoForms">
                        <tr>
                            <td class="cellRight">Nome:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtNome" runat="server" Width="177px"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o nome do usuário." ValidationGroup="cadastro" ControlToValidate="txtNome" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">DDD:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtDDD" runat="server" Width="177px" TabIndex="1"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">Telefone:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtTelefone" runat="server" Width="177px" TabIndex="2"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">Ramal:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtRamal" runat="server" Width="177px" TabIndex="3"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">Tipo de Contato:</td>
                            <td class="cellLeft">
                                <asp:DropDownList ID="cboTipoContato" runat="server" TabIndex="4">
                                </asp:DropDownList>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">E-mail:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtEmail" runat="server" Width="177px" TabIndex="5"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="cellCenter">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="cellCenter">
                                <uc1:ButtonBar runat="server" ID="ButtonBar" />

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

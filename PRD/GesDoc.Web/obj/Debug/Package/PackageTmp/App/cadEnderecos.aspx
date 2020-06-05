<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadEnderecos.aspx.cs" Inherits="GesDoc.Web.App.cadEnderecos" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">

        <asp:HiddenField ID="hdnCodEndereco" runat="server" />
        <table class="tblConteudoForms">

            <tr>
                <td class="cellRight">Pais:</td>
                <td class="cellLeft">
                    <asp:DropDownList ID="cboPais" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboPais_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Selecione o país." ValidationGroup="cadastro" ControlToValidate="cboPais" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="cellRight">Estado:</td>
                <td class="cellLeft">
                    <asp:DropDownList ID="cboEstado" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboEstado_SelectedIndexChanged" TabIndex="1">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Selecione o estado." ValidationGroup="cadastro" ControlToValidate="cboEstado" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="cellRight">Cidades:</td>
                <td class="cellLeft">
                    <asp:DropDownList ID="cboCidade" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboCidade_SelectedIndexChanged" TabIndex="2">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Selecione a cidade." ValidationGroup="cadastro" ControlToValidate="cboCidade" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="cellRight">Bairro:</td>
                <td class="cellLeft">
                    <asp:DropDownList ID="CboBairro" runat="server" AutoPostBack="True" TabIndex="3">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Selecione a bairro." ValidationGroup="cadastro" ControlToValidate="CboBairro" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="cellRight">Logradouro:</td>
                <td class="cellLeft">
                    <asp:DropDownList ID="cboLogradouro" runat="server" TabIndex="4">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Selecione o logradouro." ValidationGroup="cadastro" ControlToValidate="cboLogradouro" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="cellRight">Endereco:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtNomeEndereco" runat="server" Width="177px" TabIndex="5"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o Endereco." ValidationGroup="cadastro" ControlToValidate="txtNomeEndereco" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="cellRight">Cep:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtCep" runat="server" Width="177px" TabIndex="6"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Informe o cep." ValidationGroup="cadastro" ControlToValidate="txtCep" ForeColor="Red"></asp:RequiredFieldValidator>
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

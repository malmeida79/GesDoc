<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadCidades.aspx.cs" Inherits="GesDoc.Web.App.cadCidades" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">

        <asp:HiddenField ID="hdnCodCidade" runat="server" />

        <table class="tblConteudoForms">
            <tr>
                <td class="cellRight">Paises:</td>
                <td class="cellLeft">
                    <asp:DropDownList ID="cboPais" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboPais_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="cellRight">Estados:</td>
                <td class="cellLeft">
                    <asp:DropDownList ID="cboEstado" runat="server" AutoPostBack="True" TabIndex="1">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="cellRight">Nome Cidade:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtNomeCidade" runat="server" Width="177px" TabIndex="2"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o nome da Cidade." ValidationGroup="cadastro" ControlToValidate="txtNomeCidade" ForeColor="Red"></asp:RequiredFieldValidator>
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

<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadRecados.aspx.cs" Inherits="GesDoc.Web.App.cadRecados" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">

        <asp:HiddenField ID="hdnCodRecado" runat="server" />
        <table class="tblConteudoForms">
            <tr>
                <td class="cellRight">Tipo Recado:</td>
                <td class="cellLeft">
                    <asp:DropDownList ID="cboTipoRecado" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Selecione o Tipo." ValidationGroup="cadastro" ControlToValidate="cboTipoRecado" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="cellRight">&nbsp;</td>
                <td class="cellLeft">&nbsp;</td>

            </tr>
            <tr>
                <td class="cellRight">Recado:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtRecado" runat="server" Width="311px" Height="84px" TextMode="MultiLine" TabIndex="1"></asp:TextBox>&nbsp;
                <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o nome da Recado." ValidationGroup="cadastro" ControlToValidate="txtRecado" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>&nbsp;</td>
                <td class="cellLeft">
                    <asp:CheckBox ID="chkAtivo" runat="server" Text="Ativo" TabIndex="2" />
                </td>
            </tr>


            <tr>
                <td class="cellCenter" colspan="2">

                    <uc1:ButtonBar runat="server" id="ButtonBar" />

                </td>
            </tr>
        </table>
    </div>
</asp:Content>

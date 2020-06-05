<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadSalaCliente.aspx.cs" Inherits="GesDoc.Web.App.cadSalaCliente" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">

        <asp:HiddenField ID="hdnCodSala" runat="server" />
        <asp:HiddenField ID="hdnCodCliente" runat="server" />
        <table class="tblConteudoForms">
            <tr>
                <td>
                    <table class="tblConteudoForms">
                        <tr>
                            <td class="cellRight">Nome Sala:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtNomeSala" runat="server" Width="177px"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o nome da sala." ValidationGroup="cadastro" ControlToValidate="txtNomeSala" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">Responsável Sala:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtRespSala" runat="server" Width="177px" TabIndex="1"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">Setor:</td>
                            <td class="cellLeft">
                                <asp:DropDownList ID="cboSetor" runat="server" TabIndex="2">
                                </asp:DropDownList>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <uc1:ButtonBar runat="server" id="ButtonBar" />
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

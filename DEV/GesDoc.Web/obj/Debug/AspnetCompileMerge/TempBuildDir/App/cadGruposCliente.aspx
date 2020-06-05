<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadGruposCliente.aspx.cs" Inherits="GesDoc.Web.App.cadGruposCliente" %>

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
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="cellCenter" colspan="3">
                                <uc1:ButtonBar runat="server" id="ButtonBar" />
                            </td>
                        </tr>
                    </table>

    </div>
</asp:Content>

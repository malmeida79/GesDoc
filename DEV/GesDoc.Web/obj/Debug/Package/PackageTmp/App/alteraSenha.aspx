<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="alteraSenha.aspx.cs" Inherits="GesDoc.Web.App.alteraSenha" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        <table class="tblConteudoForms">
            <tr>
                <td class="cellRight">Senha Atual:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtSenha" runat="server" Width="177px" TabIndex="2" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="cellRight">Nova Senha:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtNovaSenha" runat="server" Width="177px" TabIndex="2" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="cellRight">Confirma Nova Senha:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtConfNovaSenha" runat="server" Width="177px" TabIndex="2" TextMode="Password"></asp:TextBox>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <uc1:ButtonBar runat="server" id="ButtonBar" />
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="acessoCliente.aspx.cs" Inherits="GesDoc.Web.App.acessoCliente" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        <asp:HiddenField ID="hdnCodCliente" runat="server" />
        Equipamentos:
        <asp:DropDownList ID="cboEquipamentos" runat="server" AutoPostBack="True" Height="32px" OnSelectedIndexChanged="cboEquipamentos_SelectedIndexChanged" Width="506px" TabIndex="1">
        </asp:DropDownList>
        <br />
        <br />
        <iframe id="downloader" width="0" height="0" style="display: none;"></iframe>
        <ul class="file-tree" id="listaArquivos" runat="server">
        </ul>
 
        <br />

        <uc1:ButtonBar runat="server" ID="ButtonBar" />

        <ul id="contextMenu" class="dropdown-menu" role="menu" style="display: none">
            <li><a tabindex="-1" href="#"><span class="glyphicon glyphicon-download-alt" aria-hidden="true">&nbsp;Download</span></a></li>
        </ul>
    </div>
</asp:Content>

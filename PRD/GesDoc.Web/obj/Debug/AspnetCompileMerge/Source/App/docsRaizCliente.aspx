<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="docsRaizCliente.aspx.cs" Inherits="GesDoc.Web.App.docsRaizCliente" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        <div id="userCliente" runat="server">
            Cliente:   
        <asp:DropDownList ID="cboCliente" runat="server" AutoPostBack="true" TabIndex="4" OnSelectedIndexChanged="cboCliente_SelectedIndexChanged">
        </asp:DropDownList>
            <br />
            <br />

        </div>
        <iframe id="downloader" width="0" height="0" style="display: none;"></iframe>
        <ul class="file-tree" id="listaArquivos" runat="server">
        </ul>
        <br />
        <uc1:ButtonBar runat="server" ID="ButtonBar" />

        <ul id="contextMenu" class="dropdown-menu" role="menu" style="display: none">
            <li id="liAbrir" runat="server"><a tabindex="-1" href="#"><span class="glyphicon glyphicon-file" aria-hidden="true">&nbsp;Abrir</span></a></li>
            <li id="liExcluir" runat="server"><a tabindex="-1" href="#"><span class="glyphicon glyphicon-trash" aria-hidden="true">&nbsp;Excluir</span></a></li>
            <li id="liDownload" runat="server"><a tabindex="-1" href="#"><span class="glyphicon glyphicon-download-alt" aria-hidden="true">&nbsp;Download</span></a></li>
        </ul>
        
        <ul id="contextMenuUsername" class="dropdown-menu" role="menu" style="display: none">
        </ul>

    </div>
</asp:Content>

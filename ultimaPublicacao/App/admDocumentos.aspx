<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="admDocumentos.aspx.cs" Inherits="GesDoc.Web.App.admDocumentos" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>

<%@ Register Src="../Controls/DropEquipamentos.ascx" TagName="DropEquipamentos" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        <iframe id="downloader" width="0" height="0" style="display: none;"></iframe>
        <asp:HiddenField ID="hdnCodCliente" runat="server" />
        <uc2:DropEquipamentos ID="DropEquipamentos" runat="server" />
        <br />

        <ul class="file-tree" id="listaArquivos" runat="server">
        </ul>

        <br />
        <uc1:ButtonBar runat="server" ID="ButtonBar" />

        <ul id="contextMenu" class="dropdown-menu" role="menu" style="display: none">
            <li id="liAbrir" runat="server"><a tabindex="-1" href="#"><span class="glyphicon glyphicon-file" aria-hidden="true">&nbsp;Abrir</span></a></li>
            <li id="liAssinar" runat="server"><a tabindex="-1" href="#"><span class="glyphicon glyphicon-pencil" aria-hidden="true">&nbsp;Assinar</span></a></li>
            <li id="liLiberar" runat="server"><a tabindex="-1" href="#"><span class="glyphicon glyphicon-ok" aria-hidden="true">&nbsp;Liberar</span></a></li>
            <li id="liExcluir" runat="server"><a tabindex="-1" href="#"><span class="glyphicon glyphicon-trash" aria-hidden="true">&nbsp;Excluir</span></a></li>
            <li class="divider"></li>
            <li id="liDownload" runat="server"><a tabindex="-1" href="#"><span class="glyphicon glyphicon-download-alt" aria-hidden="true">&nbsp;Download</span></a></li>
        </ul>
    </div>

    <ul id="contextMenuUsername" class="dropdown-menu" role="menu" style="display: none">
    </ul>

</asp:Content>

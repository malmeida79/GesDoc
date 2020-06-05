<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="adicionaDocumento.aspx.cs" Inherits="GesDoc.Web.App.adicionaDocumento" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagName="ButtonBar" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/DropTipoServico.ascx" TagName="DropTipoServico" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/DropClientes.ascx" TagPrefix="uc1" TagName="DropClientes" %>
<%@ Register Src="~/Controls/DropTipoDocumento.ascx" TagPrefix="uc3" TagName="DropTipoDocumento" %>
<%@ Register Src="~/Controls/DropEquipamentos.ascx" TagPrefix="uc4" TagName="DropEquipamentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="corpoPagina">
        <uc3:DropTipoDocumento runat="server" ID="DropTipoDocumento" />
        <uc1:DropClientes runat="server" ID="DropClientes" />
        <uc4:DropEquipamentos runat="server" ID="DropEquipamentos" />
        <uc2:DropTipoServico ID="DropTipoServico" runat="server" />
        <br />

        <label class="file-upload">
            <span><strong>
                <asp:Label ID="lblUpload" runat="server" Text="Selecione um arquivo:"></asp:Label></strong></span>
            <asp:FileUpload ID="FileUpload" runat="server" BorderWidth="1px" Width="400px"></asp:FileUpload>
        </label>

        <br />
        <br />
        <asp:Label ID="lblRetorno" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <uc1:ButtonBar runat="server" ID="ButtonBar" />
    </div>
</asp:Content>

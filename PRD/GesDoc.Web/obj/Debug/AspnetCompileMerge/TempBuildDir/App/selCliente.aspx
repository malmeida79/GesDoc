<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="selCliente.aspx.cs" Inherits="GesDoc.Web.App.selCliente" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">

        <br />
        <br />
        <br />
        <br />
        <div class="selecionaCliente">
            <div class="col-md-12 text-center">
                <asp:Label ID="lblgrupo" runat="server" Text="Grupos:" ForeColor="Black"></asp:Label>
                <asp:DropDownList ID="cboGruposAcesso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboGruposAcesso_SelectedIndexChanged" Width="352px"></asp:DropDownList><br />
                <br />
                <asp:Label ID="lblCliente" runat="server" Text="Clientes:" ForeColor="Black"></asp:Label>
                <asp:DropDownList ID="cboClientesAcesso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboClientesAcesso_SelectedIndexChanged" Width="352px"></asp:DropDownList>
                <br />
                <br />
                <uc1:ButtonBar runat="server" id="ButtonBar" />
            </div>
        </div>
    </div>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="listaEstados.aspx.cs" Inherits="GesDoc.Web.App.listaEstados" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        Selecione um Pais:&nbsp;
            <asp:DropDownList ID="cboPais" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboPais_SelectedIndexChanged">
            </asp:DropDownList>
        <br />
        <br />
        <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvEstados" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" Width="50%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gdvEstados_PageIndexChanging" AllowSorting="True" AutoGenerateEditButton="True" OnRowEditing="gdvEstados_RowEditing" OnSorting="gdvEstados_Sorting" OnRowDataBound="gdvEstados_RowDataBound" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="codEstado" HeaderText="Código" SortExpression="codEstado" />
                <asp:BoundField DataField="descricaoEstado" HeaderText="Estado" SortExpression="descricaoEstado" />
                <asp:BoundField DataField="ufEstado" HeaderText="UF" SortExpression="ufEstado" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="False" BorderStyle="Groove" ForeColor="White" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" />
            <PagerSettings FirstPageText="&amp;lt;&amp;lt;Inicio" LastPageText="&amp;gt;&amp;gt;Fim" NextPageText="&amp;gt;Proxima" PreviousPageText="&amp;lt;Anterior" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" BorderStyle="Groove" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" BorderStyle="Groove" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
        <br />
        <br />
        <uc1:ButtonBar runat="server" id="ButtonBar" />

    </div>
</asp:Content>

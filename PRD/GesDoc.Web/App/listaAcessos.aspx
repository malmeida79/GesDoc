<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="listaAcessos.aspx.cs" Inherits="GesDoc.Web.App.listaAcessos" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        <br />
        <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvAcessos" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" Width="50%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gdvAcessos_PageIndexChanging" AllowSorting="True" OnSorting="gdvAcessos_Sorting" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="contagem" HeaderText="Contagem" SortExpression="contagem" />
                <asp:BoundField DataField="codUsuario" HeaderText="Código" SortExpression="codUsuario" />
                <asp:BoundField DataField="ultimaData" HeaderText="Data" SortExpression="ultimaData" />
                <asp:BoundField DataField="nomeusuario" HeaderText="Nome" SortExpression="nomeUsuario" />
                <asp:BoundField DataField="login" HeaderText="Login" SortExpression="login" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="False" BorderStyle="Groove" ForeColor="White" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" BorderStyle="Groove" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" BorderStyle="Groove" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
        <br />
        <uc1:ButtonBar runat="server" ID="ButtonBar" />
    </div>
</asp:Content>

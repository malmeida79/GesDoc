<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="listaTipoRecado.aspx.cs" Inherits="GesDoc.Web.App.listaTipoRecado" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvTipoRecado" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" Width="50%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gdvTipoRecado_PageIndexChanging" AllowSorting="True" AutoGenerateEditButton="True" OnRowEditing="gdvTipoRecado_RowEditing" OnSorting="gdvTipoRecado_Sorting" OnRowDataBound="gdvTipoRecado_RowDataBound" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="codTipoRecado" HeaderText="Código" SortExpression="codTipoRecado" />
                <asp:BoundField DataField="descricaoTipoRecado" HeaderText="Tipo de Recado" SortExpression="descricaoTipoRecado" />
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
        <br />


        <uc1:ButtonBar runat="server" id="ButtonBar" />


    </div>
</asp:Content>

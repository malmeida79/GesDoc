<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="listaTipoServico.aspx.cs" Inherits="GesDoc.Web.App.listaTipoServico" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">

        <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvTipoServico" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" Width="50%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gdvTipoServico_PageIndexChanging" AllowSorting="True" AutoGenerateEditButton="True" OnRowEditing="gdvTipoServico_RowEditing" OnSorting="gdvTipoServico_Sorting" OnRowDataBound="gdvTipoServico_RowDataBound" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="codigoTipoServico" HeaderText="Código" SortExpression="codigoTipoServico" />
                <asp:BoundField DataField="descricaoTipoServico" HeaderText="Tipo de Serviço" SortExpression="descricaoTipoServico" />
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

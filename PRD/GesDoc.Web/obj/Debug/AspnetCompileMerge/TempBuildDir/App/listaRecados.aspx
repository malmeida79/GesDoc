<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="listaRecados.aspx.cs" Inherits="GesDoc.Web.App.listaRecados" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        Selecione Tipo de Recado:&nbsp;
            <asp:DropDownList ID="cboTpRec" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTpRec_SelectedIndexChanged">
            </asp:DropDownList>
        <br />
        <br />
        <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvRecados" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" Width="50%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gdvRecados_PageIndexChanging" AllowSorting="True" AutoGenerateEditButton="True" OnRowEditing="gdvRecados_RowEditing" OnSorting="gdvRecados_Sorting" OnRowDataBound="gdvRecados_RowDataBound" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="codRecado" HeaderText="Código" SortExpression="codRecado" />
                <asp:BoundField DataField="Recado" HeaderText="Recado" SortExpression="recado" />
                <asp:BoundField DataField="dataRecado" HeaderText="Data" SortExpression="dataRecado" />
                <asp:BoundField DataField="ativo" HeaderText="Status" SortExpression="ativo" />
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

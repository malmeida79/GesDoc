<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="listaBairros.aspx.cs" Inherits="GesDoc.Web.App.listaBairros" %>

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
        Selecione um Estado:&nbsp;
            <asp:DropDownList ID="cboEstado" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboEstado_SelectedIndexChanged" TabIndex="1">
            </asp:DropDownList>
        <br />
        <br />
        Selecione uma Cidade:&nbsp;
            <asp:DropDownList ID="cboCidade" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboCidade_SelectedIndexChanged" TabIndex="2">
            </asp:DropDownList>
        <br />
        <br />
        <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvBairros" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" Width="50%" AllowPaging="True" OnPageIndexChanging="gdvBairros_PageIndexChanging" AllowSorting="True" AutoGenerateEditButton="True" OnRowEditing="gdvBairros_RowEditing" OnSorting="gdvBairros_Sorting" AutoGenerateColumns="False" OnRowDataBound="gdvBairros_RowDataBound" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="codbairro" HeaderText="Código" SortExpression="codBairro" />
                <asp:BoundField DataField="descricaoBairro" HeaderText="Bairro" SortExpression="descricaoBairro" />
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

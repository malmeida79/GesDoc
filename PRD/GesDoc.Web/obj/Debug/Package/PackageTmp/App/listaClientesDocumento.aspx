<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="listaClientesDocumento.aspx.cs" Inherits="GesDoc.Web.App.listaClientesDocumento" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">

        <b>Informe o nome do cliente, cpnpj ou grupo:</b><br />
        <br />
        <asp:TextBox ID="txtParPesquisa" runat="server" Width="197px"></asp:TextBox>
        &nbsp;<br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe um parâmetro para consulta." ValidationGroup="pesquisa" ControlToValidate="txtParPesquisa" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <br />
        &nbsp;<asp:RadioButton ID="rdPesquisanome" runat="server" Checked="True" GroupName="tipoPesquisa" Text="Pesquisa por Nome" TabIndex="1" />
        <asp:RadioButton ID="rdpesquisaCnpj" runat="server" GroupName="tipoPesquisa" Text="Pesquisa por Cpf/Cnpj" TabIndex="2" />
        &nbsp;<asp:RadioButton ID="rdpesquisaGrupo" runat="server" GroupName="tipoPesquisa" Text="Pesquisa por Grupo" TabIndex="3" />
        <br />
        <uc1:ButtonBar runat="server" id="ButtonBar" />
        <br />
        <br />
        <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvClientes" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" Width="50%" AllowPaging="True" OnPageIndexChanging="gdvClientes_PageIndexChanging" AllowSorting="True" AutoGenerateEditButton="True" OnRowEditing="gdvClientes_RowEditing" OnSorting="gdvClientes_Sorting" AutoGenerateColumns="False" OnRowDataBound="gdvClientes_RowDataBound" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="codCliente" HeaderText="Código cliente" SortExpression="codCliente" />
                <asp:BoundField DataField="nomeCliente" HeaderText="Nome cliente" SortExpression="nomeCliente" />
                <asp:BoundField DataField="cpfCnpjCliente" HeaderText="CPF / CNPJ" SortExpression="cpfCnpjCliente" />
                <asp:BoundField DataField="status" HeaderText="Ativo" SortExpression="status" />
                <asp:BoundField DataField="nomeGrupo" HeaderText="Grupo" SortExpression="nomeGrupo" />
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
    </div>
</asp:Content>

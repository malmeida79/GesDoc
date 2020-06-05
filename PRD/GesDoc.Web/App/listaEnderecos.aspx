<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="listaEnderecos.aspx.cs" Inherits="GesDoc.Web.App.listaEnderecos" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        <b>Informe um CEP ou Parte do endereço</b><br />
        <br />
        <asp:TextBox ID="txtParPesquisa" runat="server" Width="197px"></asp:TextBox>
        &nbsp;<br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe um parâmetro para consulta." ValidationGroup="pesquisa" ControlToValidate="txtParPesquisa" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <br />
        &nbsp;<asp:RadioButton ID="rdPesquisaCep" runat="server" Checked="True" GroupName="tipoPesquisa" Text="Pesquisa por CEP" TabIndex="1" />
        <asp:RadioButton ID="rdpesquisaEndereco" runat="server" GroupName="tipoPesquisa" Text="Pesquisa por Endereço" TabIndex="2" />
        &nbsp;<br />
        <uc1:ButtonBar runat="server" id="ButtonBar" />
        <br />
        <br />
        <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvEnderecos" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" Width="50%" AllowPaging="True" OnPageIndexChanging="gdvEnderecos_PageIndexChanging" AllowSorting="True" AutoGenerateEditButton="True" OnRowEditing="gdvEnderecos_RowEditing" OnSorting="gdvEnderecos_Sorting" AutoGenerateColumns="False" OnRowDataBound="gdvEnderecos_RowDataBound" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="codEndereco" HeaderText="Código endereço" SortExpression="codEndereco" />
                <asp:BoundField DataField="descricaoLogradouro" HeaderText="Logradouro" SortExpression="descricaoLogradouro" />
                <asp:BoundField DataField="descricaoEndereco" HeaderText="Endereço" SortExpression="descricaoEndereco" />
                <asp:BoundField DataField="cepEndereco" HeaderText="Cep" SortExpression="cepEndereco" />
                <asp:BoundField DataField="descricaoBairro" HeaderText="Bairro" SortExpression="descricaoBairro" />
                <asp:BoundField DataField="descricaoCidade" HeaderText="Cidade" SortExpression="descricaoCidade" />
                <asp:BoundField DataField="descricaoEstado" HeaderText="Estado" SortExpression="descricaoEstado" />
                <asp:BoundField DataField="descricaoPais" HeaderText="País" SortExpression="descricaoPais" />
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

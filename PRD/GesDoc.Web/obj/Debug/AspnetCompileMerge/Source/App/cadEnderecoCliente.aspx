<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadEnderecoCliente.aspx.cs" Inherits="GesDoc.Web.App.cadEnderecoCliente" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">

        <asp:HiddenField ID="hdnCodCliente" runat="server" />
        <asp:HiddenField ID="hdnCodEndCliente" runat="server" />
        <table class="tblConteudoForms">
            <tr>
                <td class="cellRight">Numero:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtNumero" runat="server" Width="177px"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Informe o numero." ValidationGroup="cadastro" ControlToValidate="txtNumero" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="cellRight">Complemento:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtComplemento" runat="server" Width="177px" TabIndex="1"></asp:TextBox>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="cellRight">Referência:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtReferencia" runat="server" Width="177px" TabIndex="3"></asp:TextBox>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="cellRight">Tipo de endereço: </td>
                <td class="cellLeft">
                    <asp:DropDownList ID="cboTipoEndereco" runat="server" TabIndex="4"></asp:DropDownList>
                </td>
            </tr>

        </table>

        <br />

        Obs: É necessário um endereço ser selecionado na listagem abaixo, para alterar o mesmo basta realizar uma busca.<br />
        Para manter o atual nenhuma atuação se faz necessária.<br />
        <br />
        <asp:TextBox ID="txtParPesquisa" runat="server" Width="197px" TabIndex="5"></asp:TextBox>
        &nbsp;<br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe um parâmetro para consulta." ValidationGroup="pesquisa" ControlToValidate="txtParPesquisa" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <br />
        &nbsp;<asp:RadioButton ID="rdPesquisaCep" runat="server" Checked="True" GroupName="tipoPesquisa" Text="Pesquisa por CEP" TabIndex="6" />
        <asp:RadioButton ID="rdpesquisaEndereco" runat="server" GroupName="tipoPesquisa" Text="Pesquisa por Endereço" TabIndex="7" />
        <br />
        &nbsp;<br />
        <asp:Button ID="btnPesquisa" BackColor="Gray" ForeColor="White" runat="server" Text="Buscar" ValidationGroup="pesquisa" OnClick="btnPesquisa_Click" TabIndex="8"></asp:Button>
        &nbsp;<asp:Button ID="btnLimpar" BackColor="Gray" ForeColor="White" runat="server" Text="Restaurar" OnClick="btnLimpar_Click" TabIndex="9"></asp:Button>
        <br />
        <br />
        <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvEnderecos" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" Width="50%" AllowPaging="True" OnPageIndexChanging="gdvEnderecos_PageIndexChanging" AllowSorting="True" OnSorting="gdvEnderecos_Sorting" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="codEndereco" HeaderText="Código endereço" SortExpression="codEndereco" />
                <asp:BoundField DataField="descricaoLogradouro" HeaderText="Logradouro" SortExpression="descricaoLogradouro" />
                <asp:BoundField DataField="descricaoEndereco" HeaderText="Endereço" SortExpression="descricaoEndereco" />
                <asp:BoundField DataField="cepEndereco" HeaderText="Cep" SortExpression="cepEndereco" />
                <asp:BoundField DataField="descricaoBairro" HeaderText="Bairro" SortExpression="descricaoBairro" />
                <asp:BoundField DataField="descricaoCidade" HeaderText="Cidade" SortExpression="descricaoCidade" />
                <asp:BoundField DataField="descricaoEstado" HeaderText="Estado" SortExpression="descricaoEstado" />
                <asp:BoundField DataField="descricaoPais" HeaderText="País" SortExpression="descricaoPais" />
                <asp:CommandField ButtonType="Button" SelectText="Selecionar" ShowSelectButton="True">
                    <ControlStyle BackColor="Gray" ForeColor="White" />
                </asp:CommandField>
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
        <uc1:ButtonBar runat="server" id="ButtonBar" />
 
    </div>
</asp:Content>

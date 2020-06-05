<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="listaUsuarios.aspx.cs" Inherits="GesDoc.Web.App.listaUsuarios" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        <table class="tblConteudoForms">
            <tr>
                <td class="cellCenter">** Para pesquisar de usuário você pode informar um nome ou um e-mail, em branco listará todos.</td>
            </tr>
            <tr>
                <td class="cellCenter">
                    <asp:CheckBox ID="chkDeletados" runat="server" Text=" Incluir usuários deletados" TabIndex="2" AutoPostBack="True" OnCheckedChanged="chkDeletados_CheckedChanged" />
                    <asp:TextBox ID="txtParPesquisa" runat="server" Width="197px"></asp:TextBox><br />
                    <uc1:ButtonBar runat="server" ID="ButtonBar" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvUsuarios" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" Width="90%" AllowPaging="True" Height="264px" OnPageIndexChanging="gdvUsuarios_PageIndexChanging" AllowSorting="True" AutoGenerateEditButton="True" OnRowEditing="gdvUsuarios_RowEditing" OnSorting="gdvUsuarios_Sorting" AutoGenerateColumns="False" OnRowDataBound="gdvUsuarios_RowDataBound" ForeColor="Black" GridLines="Vertical" PageSize="20">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="codUsuario" HeaderText="Código usuário" SortExpression="codUsuario" />
                <asp:BoundField DataField="nomeUsuario" HeaderText="Nome Usuário" SortExpression="nomeUsuario" />
                <asp:BoundField DataField="sobreNome" HeaderText="Sobre Nome" SortExpression="sobreNome" />
                <asp:BoundField DataField="login" HeaderText="Login" SortExpression="login" />
                <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                <asp:BoundField DataField="Bloqueado" HeaderText="Bloqueado" SortExpression="Bloqueado" />
                <asp:BoundField DataField="Ativo" HeaderText="Ativo" SortExpression="ativo" />
                <asp:BoundField DataField="deletado" HeaderText="Deletado" SortExpression="deletado" />
                <asp:BoundField DataField="dataCadastro" HeaderText="Data Cadastro" SortExpression="dataCadastro" />
                <asp:BoundField DataField="dataultimoAcesso" HeaderText="Ultimo Acesso" SortExpression="dataUltimoAcesso" />
                <asp:BoundField DataField="dataAlteracao" HeaderText="Ultimo Alteração" SortExpression="dataAlteracao" />
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

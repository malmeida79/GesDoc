<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="clonadorAcessos.aspx.cs" Inherits="GesDoc.Web.App.clonadorAcessos" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        <b>Informe o nome ou email do usuário:</b><br />
        <br />
        <asp:TextBox ID="txtParPesquisa" runat="server" Width="197px"></asp:TextBox>
        &nbsp;<br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe um parâmetro para consulta." ValidationGroup="pesquisa" ControlToValidate="txtParPesquisa" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <br />
        &nbsp;<asp:RadioButton ID="rdPesquisanome" runat="server" GroupName="tipoPesquisa" Text="Pesquisa por Nome" TabIndex="1" />
        &nbsp;<asp:RadioButton ID="rdpesquisaEmail" runat="server" GroupName="tipoPesquisa" Text="Pesquisa por e-mail" TabIndex="2" />
        <br />
        <uc1:ButtonBar runat="server" id="ButtonBar" />

        <br />
        <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvUsuarios" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" Width="90%" AllowPaging="True" Height="264px" OnPageIndexChanging="gdvUsuarios_PageIndexChanging" AllowSorting="True" OnSorting="gdvUsuarios_Sorting" AutoGenerateColumns="False" OnRowDataBound="gdvUsuarios_RowDataBound" ForeColor="Black" GridLines="Vertical" OnRowCommand="gdvUsuarios_RowCommand">
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
                <asp:ButtonField ButtonType="Button" CommandName="clone" Text="Clonar Acessos" />
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

    </div>
</asp:Content>

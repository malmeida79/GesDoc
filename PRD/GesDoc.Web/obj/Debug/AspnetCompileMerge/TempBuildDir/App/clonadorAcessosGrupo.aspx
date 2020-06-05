<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="clonadorAcessosGrupo.aspx.cs" Inherits="GesDoc.Web.App.clonadorAcessosGrupo" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">

        <table class="tblConteudoForms">
            <tr>
                <td class="cellCenter">
                    <b>Selecione um grupo:</b><br />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvGrupos" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" Width="40%" AllowPaging="True" Height="264px" OnPageIndexChanging="gdvGrupos_PageIndexChanging" AllowSorting="True" OnSorting="gdvGrupos_Sorting" AutoGenerateColumns="False" OnRowCommand="gdvGrupos_RowCommand" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="codGrupo" HeaderText="Código grupo" SortExpression="codGrupo" />
                <asp:BoundField DataField="nomeGrupo" HeaderText="Nome grupo" SortExpression="nomeGrupo" />
                <asp:BoundField DataField="infoGrupo" HeaderText="Informações de grupo" SortExpression="infoGrupo" />
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
        <table class="tblConteudoForms">
            <tr>
                <td class="cellCenter">
                    <uc1:ButtonBar runat="server" id="ButtonBar" />
                </td>
            </tr>
        </table>

    </div>
</asp:Content>

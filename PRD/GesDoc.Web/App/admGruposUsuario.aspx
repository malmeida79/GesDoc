<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="admGruposUsuario.aspx.cs" Inherits="GesDoc.Web.App.admGruposUsuario" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">

        <asp:HiddenField ID="hdnCodusuario" runat="server" />

        <table class="tblConteudoForms">
            <tr>
                <td colspan="2" class="cellCenter">
                    <uc1:ButtonBar runat="server" id="ButtonBar" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvGruposAcesso" runat="server" AutoGenerateColumns="False" BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gdvGruposAcesso_PageIndexChanging" OnRowCancelingEdit="gdvGruposAcesso_RowCancelingEdit" OnRowEditing="gdvGruposAcesso_RowEditing" OnRowUpdating="gdvGruposAcesso_RowUpdating">
            <Columns>
                <asp:BoundField DataField="codGrupo" HeaderText="Código" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="nomeGrupo" HeaderText="Grupo" ReadOnly="True" />
                <asp:BoundField DataField="infoGrupo" HeaderText="Informações" ReadOnly="True" />
                <asp:CheckBoxField DataField="acesso" HeaderText="Permitido" />
                <asp:CommandField ButtonType="Button" CancelText="Cancelar" EditText="Modificar" HeaderText="Ações" ShowEditButton="True" UpdateText="Gravar">
                    <ControlStyle BackColor="Gray" ForeColor="White" />
                </asp:CommandField>
            </Columns>
            <EditRowStyle BackColor="#FFCC66" />
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

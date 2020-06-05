<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ButtonBar.ascx.cs" Inherits="GesDoc.Web.Controls.ButtonBar" %>
<div id="ContainerBarraBotoes">
    <div id="botoesLista">
        <asp:LinkButton ID="btnPesquisa" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnPesquisa_Click" TabIndex="4"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton>
        <asp:LinkButton ID="btnNovo" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnNovo_Click" TabIndex="1"><span class="glyphicon glyphicon-plus"></span>Novo</asp:LinkButton>
        <asp:LinkButton ID="btnAcao" runat="server" CssClass="btn btn-success btn-sm" OnClick="btnAcao_Click" TabIndex="2"><span class="glyphicon glyphicon-ok"></span>Cadastrar</asp:LinkButton>
        <asp:LinkButton ID="btnLimpar" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnLimpar_Click" TabIndex="5"><span class="glyphicon glyphicon-repeat"></span>Limpar</asp:LinkButton>
        <asp:LinkButton ID="btnRecuperar" runat="server" CssClass="btn btn-success btn-sm" OnClick="btnRecuperar_Click" TabIndex="6"><span class="glyphicon glyphicon-check"></span>Recuperar</asp:LinkButton>
        <asp:LinkButton ID="btnExcluir" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnExcluir_Click" TabIndex="6"><span class="glyphicon glyphicon-trash"></span>Excluir</asp:LinkButton>
        <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnCancelar_Click" TabIndex="7"><span class="glyphicon glyphicon-ban-circle"></span>Cancelar</asp:LinkButton>
        <br />
        <br />
        <asp:LinkButton ID="btnExportCsv" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btnExportCsv_Click" TabIndex="8"><span class="glyphicon glyphicon-download"></span>Exportar CSV</asp:LinkButton>
        <asp:LinkButton ID="btnExportExcel" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btnExportExcel_Click" TabIndex="9"><span class="glyphicon glyphicon-download"></span>Exportar Excel</asp:LinkButton>
        <asp:LinkButton ID="btnExportTxt" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btnExportTxt_Click" TabIndex="10"><span class="glyphicon glyphicon-download"></span>Exportar TXT</asp:LinkButton>
        <asp:Button ID="btnAcaoJQuery" runat="server" CssClass="finalizarAcao" OnClick="btnAcaoJQuery_click" style="display:none;" />
    </div>
    <div id="avisoPermissao">
        <asp:Label ID="lblPermissaoAviso" runat="server" Text=""></asp:Label>
    </div>
</div>

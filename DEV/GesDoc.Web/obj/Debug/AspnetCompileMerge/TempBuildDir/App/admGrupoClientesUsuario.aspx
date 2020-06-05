<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="admGrupoClientesUsuario.aspx.cs" Inherits="GesDoc.Web.App.admGrupoClientesUsuario" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        <asp:HiddenField ID="hdnCodUsuario" runat="server" />

        <table class="tblConteudoForms">
            <tr>
                <td colspan="2" class="cellCenter">
                    <asp:Label ID="lblGrupos0" runat="server">Grupos:</asp:Label>
                    <asp:Label ID="lblGrupos" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="cellCenter">Selecione um grupo:
                    <asp:DropDownList ID="cboGrupo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboGrupo_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="cellCenter">
                    <asp:CheckBox ID="chkPesquisaGrupo" runat="server" Text="Pesquisa por Grupo?" AutoPostBack="True" OnCheckedChanged="chkPesquisaGrupo_CheckedChanged" TabIndex="1" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="cellCenter">
                    <table class="tblConteudoChkList">
                        <tr>
                            <td class="cellLeft">
                                <asp:CheckBoxList ID="chkGrpClientes" runat="server" RepeatColumns="3" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="chkGrpClientes_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <uc1:ButtonBar runat="server" id="ButtonBar" />
    </div>
</asp:Content>

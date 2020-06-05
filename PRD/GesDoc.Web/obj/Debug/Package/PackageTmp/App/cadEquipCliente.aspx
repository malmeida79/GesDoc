<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadEquipCliente.aspx.cs" Inherits="GesDoc.Web.App.cadEquipCliente" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">

        <asp:HiddenField ID="hdnCodEquipamento" runat="server" />
        <asp:HiddenField ID="hdnCodCliente" runat="server" />
        <table class="tblConteudoForms">
            <tr>
                <td>

                    <table class="tblConteudoForms">
                        <tr>
                            <td class="cellRight">Setores:</td>
                            <td class="cellLeft">
                                <asp:DropDownList ID="cboSetor" runat="server" Height="20px" Width="185px" AutoPostBack="True" OnSelectedIndexChanged="cboSetor_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">Sala:</td>
                            <td class="cellLeft">
                                <asp:DropDownList ID="cboSala" runat="server" Height="20px" Width="185px" TabIndex="1">
                                </asp:DropDownList>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">Tipo:</td>
                            <td class="cellLeft">
                                <asp:DropDownList ID="cboTipo" runat="server" Height="20px" Width="185px" TabIndex="2">
                                </asp:DropDownList>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">Marca Equipamento:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtMarca" runat="server" Width="177px" TabIndex="3"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe a marca do Equipamento." ValidationGroup="cadastro" ControlToValidate="txtMarca" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">Modelo Equipamento:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtModelo" runat="server" Width="177px" TabIndex="4"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Informe o modelo do equipamento" ValidationGroup="cadastro" ControlToValidate="txtModelo" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">Numero série:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtNumSerie" runat="server" Width="177px" TabIndex="5"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">Numero Patrimonio:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtNumPat" runat="server" Width="177px" TabIndex="6"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">Registro Anvisa:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtRegAnvisa" runat="server" Width="177px" TabIndex="7"></asp:TextBox>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">Ano Fabricação:</td>
                            <td class="cellLeft">
                                <asp:DropDownList ID="cboAnoFab" runat="server" Height="20px" Width="185px" TabIndex="8">
                                </asp:DropDownList>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="cellRight">Status:</td>
                            <td class="cellLeft">
                                <asp:DropDownList ID="cboStatus" runat="server" Height="20px" Width="185px" TabIndex="9">
                                </asp:DropDownList>
                                &nbsp;
                            </td>
                        </tr>

                        <tr>
                            <td class="cellRight">&nbsp;</td>
                            <td class="cellLeft">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">

                                <uc1:ButtonBar runat="server" id="ButtonBar" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

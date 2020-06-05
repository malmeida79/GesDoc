<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadSetorCliente.aspx.cs" Inherits="GesDoc.Web.App.cadSetorCliente" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">

        <asp:HiddenField ID="hdnCodSetor" runat="server" />
        <asp:HiddenField ID="hdnCodCliente" runat="server" />
        <table class="tblConteudoForms">
            <tr>
                <td>


                    <table class="tblConteudoForms">
                        <tr>
                            <td class="cellRight">Nome Setor:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtNomeSetor" runat="server" Width="177px"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o nome do setor." ValidationGroup="cadastro" ControlToValidate="txtNomeSetor" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">Responsável Técnico:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtRspTecnico" runat="server" Width="177px" TabIndex="1"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Informe o nome do responsável técnico" ValidationGroup="cadastro" ControlToValidate="txtRspTecnico" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">CRM Responsável Técnico:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtCRMResp" runat="server" Width="177px" TabIndex="2"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Informe o CRM do responsável técnico." ValidationGroup="cadastro" ControlToValidate="txtCRMResp" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">Nome Responsável Legal:</td>
                            <td class="cellLeft">&nbsp;<asp:TextBox ID="txtNomeResponsavel" runat="server" Width="177px" TabIndex="3"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td class="cellRight">CRM Responsável Legal:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtCrmResponsavel" runat="server" Width="177px" TabIndex="4"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">Nome supervisor de proteção radiológica:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtSupervisor" runat="server" Width="177px" TabIndex="5"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">CPF supervisor de proteção radiológica:</td>
                            <td class="cellLeft">
                                <asp:TextBox ID="txtCRMSup" runat="server" Width="177px" TabIndex="6"></asp:TextBox>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="cellRight">&nbsp;</td>
                            <td class="cellLeft">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="cellCenter">
                                <uc1:ButtonBar runat="server" ID="ButtonBar" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

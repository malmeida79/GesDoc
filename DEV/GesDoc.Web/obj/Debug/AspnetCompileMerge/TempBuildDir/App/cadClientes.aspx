<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="cadClientes.aspx.cs" Inherits="GesDoc.Web.App.cadClientes" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="corpoPagina">
        <br />
        <asp:HiddenField ID="hdnCodCliente" runat="server" />
        <asp:HiddenField ID="hdnCodGrupo" runat="server" />

        <table class="tblConteudoForms">
            <tr>
                <td class="cellRight">Grupo Cliente:</td>
                <td class="cellLeft">
                    <asp:DropDownList ID="cboGrupo" runat="server" AutoPostBack="false" Width="352px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="cellRight">Nome Cliente (Conhecido na RAD):</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtNomeCliente" runat="server" Width="343px" TabIndex="1"></asp:TextBox></td>
            </tr>

            <tr>
                <td class="cellRight">Razão Social:</td>
                <td class="cellLeft">
                    <asp:TextBox ID="txtRazaoSocial" runat="server" Width="343px" TabIndex="2"></asp:TextBox></td>
            </tr>

            <tr>
                <td class="cellRight">CPF/CNPJ:</td>
                <td class="cellLeft">
                    <asp:TextBox runat="server" Width="343px" TabIndex="3" ID="txtCpfCnpj"></asp:TextBox></td>
            </tr>

            <tr>
                <td class="cellRight">Status:</td>
                <td class="cellLeft">
                    <asp:RadioButton ID="rdAtivo" runat="server" GroupName="status" Text="Ativo" TabIndex="4" />
                    <asp:RadioButton ID="rdInativo" runat="server" GroupName="status" Text="Inativo" TabIndex="5" />
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td class="cellCenter" colspan="2">
                    <uc1:ButtonBar runat="server" id="ButtonBar" />
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td class="cellCenter" colspan="2">
                    <table class="tblConteudoForms">
                        <tr>
                            <td>
                                <asp:Button Text="Endereços" BorderStyle="None" ID="Tab1" CssClass="tabInitial" runat="server" OnClick="Tab1_Click" TabIndex="7" />
                                <asp:Button Text="Contatos" BorderStyle="None" ID="Tab2" CssClass="tabInitial" runat="server" OnClick="Tab2_Click" TabIndex="9" />
                                <asp:Button Text="Setores" BorderStyle="None" ID="Tab3" CssClass="tabInitial" runat="server" OnClick="Tab3_Click" TabIndex="10" />
                                <asp:Button Text="Salas" BorderStyle="None" ID="Tab4" CssClass="tabInitial" runat="server" OnClick="Tab4_Click" TabIndex="11" />
                                <asp:Button Text="Equipamentos" BorderStyle="None" ID="Tab5" CssClass="tabInitial" runat="server" OnClick="Tab5_Click" TabIndex="12" />

                                <asp:MultiView ID="MainView" runat="server">

                                    <asp:View ID="View1" runat="server">
                                        <table class="tblConteudoTab">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvEnderecos" runat="server" AllowPaging="True" AllowSorting="True" BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" Width="100%" AutoGenerateColumns="False" OnPageIndexChanging="gdvEnderecos_PageIndexChanging" OnRowDeleting="gdvEnderecos_RowDeleting" OnRowEditing="gdvEnderecos_RowEditing" OnSorting="gdvEnderecos_Sorting">
                                                        <Columns>
                                                            <asp:BoundField DataField="codEnderecoCliente" HeaderText="codEnderecoCliente" SortExpression="codEnderecoCliente" Visible="False" />
                                                            <asp:BoundField DataField="codcliente" HeaderText="codcliente" SortExpression="codCliente" Visible="False" />
                                                            <asp:BoundField DataField="codEndereco" HeaderText="codEndereco" SortExpression="codEndereco" ReadOnly="True" Visible="False" />
                                                            <asp:BoundField DataField="descricaoLogradouro" HeaderText="Logradouro" SortExpression="descricaoLogradouro" />
                                                            <asp:BoundField DataField="descricaoendereco" HeaderText="Endereço" SortExpression="descricaoEndereco" />
                                                            <asp:BoundField DataField="numero" HeaderText="Numero" SortExpression="numero" />
                                                            <asp:BoundField DataField="complemento" HeaderText="Complemento" SortExpression="complemento" />
                                                            <asp:BoundField DataField="referencia" HeaderText="Referência" SortExpression="referencia" />
                                                            <asp:BoundField DataField="cepEndereco" HeaderText="CEP" SortExpression="cepEndereco" />
                                                            <asp:BoundField DataField="descricaoBairro" HeaderText="Bairro" SortExpression="descricaoBairro" />
                                                            <asp:BoundField DataField="descricaoCidade" HeaderText="Cidade" SortExpression="descricaoCidade" />
                                                            <asp:BoundField DataField="descricaoEstado" HeaderText="Estado" SortExpression="descricaoEstado" />
                                                            <asp:BoundField DataField="descricaopais" HeaderText="Pais" SortExpression="descricaoPais" />
                                                            <asp:BoundField DataField="ufestado" HeaderText="UF" SortExpression="ufEstado" />
                                                            <asp:BoundField DataField="descricaoTipoEndereco" HeaderText="Tipo de Endereço" SortExpression="descricaoTipoEndereco" />
                                                            <asp:CommandField ShowEditButton="True" ButtonType="Button" CancelText="Cancelar" DeleteText="Excluir" EditText="Modificar" NewText="Novo" ShowDeleteButton="True" UpdateText="Gravar">
                                                                <ControlStyle BackColor="Gray" ForeColor="White" Width="70px" />
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: center;">
                                                    <asp:Button ID="btnAddEnd" runat="server" Text="Cadastrar Novo Endereço" BackColor="Gray" ForeColor="White" OnClick="btnAddEnd_Click" ValidationGroup="cadastro" Enabled="False" TabIndex="13"></asp:Button></td>
                                            </tr>

                                        </table>
                                    </asp:View>

                                    <asp:View ID="View2" runat="server">
                                        <table class="tblConteudoTab">

                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvContatos" runat="server" AllowPaging="True" AllowSorting="True" BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" Width="100%" AutoGenerateColumns="false" OnPageIndexChanging="gdvContatos_PageIndexChanging" OnRowDeleting="gdvContatos_RowDeleting" OnRowEditing="gdvContatos_RowEditing" OnSorting="gdvContatos_Sorting">
                                                        <Columns>
                                                            <asp:BoundField DataField="codContato" HeaderText="codContato" SortExpression="codContato" Visible="False" />
                                                            <asp:BoundField DataField="codcliente" HeaderText="codcliente" SortExpression="codcliente" Visible="False" />
                                                            <asp:BoundField DataField="nome" HeaderText="Nome" SortExpression="nome" />
                                                            <asp:BoundField DataField="codddd" HeaderText="DDD" SortExpression="codDdd" />
                                                            <asp:BoundField DataField="telefone" HeaderText="Telefone" SortExpression="telefone" />
                                                            <asp:BoundField DataField="ramal" HeaderText="Ramal" SortExpression="ramal" />
                                                            <asp:BoundField DataField="email" HeaderText="E-Mail" SortExpression="email" />
                                                            <asp:BoundField DataField="codTipoContato" HeaderText="codTipoContato" SortExpression="codTipoContato" Visible="false" />
                                                            <asp:BoundField DataField="tipoContato" HeaderText="Tipo" SortExpression="tipoContato" />
                                                            <asp:CommandField ShowEditButton="True" ButtonType="Button" CancelText="Cancelar" DeleteText="Excluir" EditText="Modificar" NewText="Novo" ShowDeleteButton="True" UpdateText="Gravar">
                                                                <ControlStyle BackColor="Gray" ForeColor="White" Width="70px" />
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: center;">
                                                    <asp:Button ID="btnAddCt" runat="server" Text="Cadastrar Novo Contato" BackColor="Gray" ForeColor="White" OnClick="btnAddCt_Click" ValidationGroup="cadastro" Enabled="False" TabIndex="14"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>

                                    <asp:View ID="View3" runat="server">
                                        <table class="tblConteudoTab">

                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvSetores" runat="server" AllowPaging="True" AutoGenerateColumns="false" AllowSorting="True" BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" Width="100%" OnPageIndexChanging="gdvSetores_PageIndexChanging" OnRowDeleting="gdvSetores_RowDeleting" OnRowEditing="gdvSetores_RowEditing" OnSorting="gdvSetores_Sorting">
                                                        <Columns>
                                                            <asp:BoundField DataField="codSetor" HeaderText="codContato" SortExpression="codContato" Visible="False" />
                                                            <asp:BoundField DataField="codcliente" HeaderText="codcliente" SortExpression="codCliente" Visible="False" />
                                                            <asp:BoundField DataField="descricaoSetor" HeaderText="Nome" SortExpression="descricaoSetor" />
                                                            <asp:BoundField DataField="responsavelLegal" HeaderText="Resposnsável Legal" SortExpression="responsavelLegal" />
                                                            <asp:BoundField DataField="crmResponsavelLegal" HeaderText="CRM Responsável Legal" SortExpression="crmResponsavelLegal" />
                                                            <asp:BoundField DataField="responsavelTecnico" HeaderText="Responsável Técnico" SortExpression="responsavelTecnico" />
                                                            <asp:BoundField DataField="crmResponsavel" HeaderText="CRM Responsável Técnico" SortExpression="crmResponsavel" />
                                                            <asp:BoundField DataField="supervisorTecnico" HeaderText="Supervisor" SortExpression="supervisorTecnico" />
                                                            <asp:BoundField DataField="crvSupervisor" HeaderText="CPF Supervisor" SortExpression="crvSupervisor" />
                                                            <asp:CommandField ShowEditButton="True" ButtonType="Button" CancelText="Cancelar" DeleteText="Excluir" EditText="Modificar" NewText="Novo" ShowDeleteButton="True" UpdateText="Gravar">
                                                                <ControlStyle BackColor="Gray" ForeColor="White" Width="70px" />
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: center;">
                                                    <asp:Button ID="btnAddSt" runat="server" Text="Cadastrar novo Setor" BackColor="Gray" ForeColor="White" OnClick="btnAddSt_Click" ValidationGroup="cadastro" Enabled="False" TabIndex="15"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>

                                    <asp:View ID="View4" runat="server">
                                        <table class="tblConteudoTab">

                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvSalas" runat="server" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True" BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" Width="100%" OnPageIndexChanging="gdvSalas_PageIndexChanging" OnRowDeleting="gdvSalas_RowDeleting" OnRowEditing="gdvSalas_RowEditing" OnSorting="gdvSalas_Sorting">
                                                        <Columns>
                                                            <asp:BoundField DataField="CodSala" HeaderText="codContato" SortExpression="CodSala" Visible="False" />
                                                            <asp:BoundField DataField="codcliente" HeaderText="codcliente" SortExpression="codCliente" Visible="False" />
                                                            <asp:BoundField DataField="nomeSala" HeaderText="Nome" SortExpression="nomeSala" />
                                                            <asp:BoundField DataField="ResponsavelSala" HeaderText="Responsável" SortExpression="ResponsavelSala" />
                                                            <asp:BoundField DataField="descricaoSetor" HeaderText="Setor" SortExpression="descricaoSetor" />
                                                            <asp:CommandField ShowEditButton="True" ButtonType="Button" CancelText="Cancelar" DeleteText="Excluir" EditText="Modificar" NewText="Novo" ShowDeleteButton="True" UpdateText="Gravar">
                                                                <ControlStyle BackColor="Gray" ForeColor="White" Width="70px" />
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: center;">
                                                    <asp:Button ID="btnAddSl" runat="server" Text="Cadastrar Nova Sala" BackColor="Gray" ForeColor="White" OnClick="btnAddSl_Click" ValidationGroup="cadastro" Enabled="False" TabIndex="16"></asp:Button></td>
                                            </tr>
                                        </table>
                                    </asp:View>

                                    <asp:View ID="View5" runat="server">
                                        <table class="tblConteudoTab">
                                            <tr>
                                                <td>
                                                    <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvEquipamento" runat="server" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True" BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" Width="100%" OnPageIndexChanging="gdvEquipamento_PageIndexChanging" OnRowDeleting="gdvEquipamento_RowDeleting" OnRowEditing="gdvEquipamento_RowEditing" OnSorting="gdvEquipamento_Sorting">
                                                        <Columns>
                                                            <asp:BoundField DataField="codEquipamento" HeaderText="Código" SortExpression="codEquipamento" Visible="true" />
                                                            <asp:BoundField DataField="codCliente" HeaderText="codCliente" SortExpression="codCliente" Visible="False" />
                                                            <asp:BoundField DataField="CodSala" HeaderText="CodSala" SortExpression="CodSala" Visible="False" />
                                                            <asp:BoundField DataField="nomeSala" HeaderText="Sala" SortExpression="nomeSala" />
                                                            <asp:BoundField DataField="codTipoEquipamento" HeaderText="codTipoEquipamento" SortExpression="codTipoEquipamento" Visible="False" />
                                                            <asp:BoundField DataField="descricaoTipoEquipamento" HeaderText="Tipo" SortExpression="descricaoTipoEquipamento" />
                                                            <asp:BoundField DataField="marca" HeaderText="Marca" SortExpression="marca" />
                                                            <asp:BoundField DataField="modelo" HeaderText="Modelo" SortExpression="modelo" />
                                                            <asp:BoundField DataField="numeroSerie" HeaderText="Num. Serie" SortExpression="numeroSerie" />
                                                            <asp:BoundField DataField="numeroPatrimonio" HeaderText="Num. Patrimonio" SortExpression="numeroPatrimonio" />
                                                            <asp:BoundField DataField="registroAnvisa" HeaderText="Registro Anvisa" SortExpression="registroAnvisa" />
                                                            <asp:BoundField DataField="anoFabricacao" HeaderText="Ano Fabricação" SortExpression="anoFabricacao" />
                                                            <asp:BoundField DataField="statusEquip" HeaderText="Status" SortExpression="statusEquip" />
                                                            <asp:CommandField ShowEditButton="True" ButtonType="Button" CancelText="Cancelar" DeleteText="Excluir" EditText="Modificar" NewText="Novo" ShowDeleteButton="True" UpdateText="Gravar">
                                                                <ControlStyle BackColor="Gray" ForeColor="White" Width="70px" />
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: center;">
                                                    <asp:Button ID="btnAddEquip" runat="server" Text="Cadastrar Novo Equipamento" BackColor="Gray" ForeColor="White" OnClick="btnAddEquip_Click" ValidationGroup="cadastro" Enabled="False" TabIndex="17"></asp:Button></td>
                                            </tr>
                                        </table>
                                    </asp:View>

                                </asp:MultiView>
                                    
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

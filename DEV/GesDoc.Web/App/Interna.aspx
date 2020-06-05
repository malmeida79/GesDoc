<%@ Page Title="" Language="C#" MasterPageFile="~/App/Sistema.Master" AutoEventWireup="true" CodeBehind="Interna.aspx.cs" Inherits="GesDoc.Web.App.Interna" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="corpoPagina">
        <asp:HiddenField ID="hfAccordionIndex" runat="server" />
        <!-- foi necessario criar uma div para alinhamento interno -->
        <div class="mensagensEntrada">
            <div class="panel-group">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" href="#collapse1">Central de Informações</a>
                        </h4>
                    </div>
                    <div id="collapse1" class="panel-collapse">
                        <div class="panel-body">

                            <div class="panel-group" id="accordion">

                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#acc1">Mensagens
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="acc1" class="panel-collapse collapse in">
                                        <div class="panel-body">
                                            <asp:Label ID="lblComunicado" runat="server" Text="Comunicados Internos:"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblMsgsGeraisRad" runat="server" Style="width: 100%; height: 230px" BackColor="#6A92E4" BorderStyle="Solid" ForeColor="White"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <%if (!usuarioCliente)
                                    { %>
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#acc2">Assina Documento
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="acc2" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvDocumentoAssinar" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" Width="50%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gdvDocumentoAssinar_PageIndexChanging" AllowSorting="True" OnSorting="gdvDocumentoAssinar_Sorting" Caption="Listagem de Documentos a Assinar" ForeColor="Black" GridLines="Vertical" OnRowCommand="gdvDocumentoAssinar_RowCommand">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="nomeCliente" HeaderText="Cliente" SortExpression="NomeCliente" />
                                                    <asp:BoundField DataField="NomeDocumento" HeaderText="Documento" SortExpression="NomeDocumento" />
                                                    <asp:BoundField DataField="dataGeracao" HeaderText="Data Criação" SortExpression="DataGeracao" />
                                                    <asp:ButtonField ButtonType="Button" CommandName="irpara" Text="Ir para ..." />
                                                    <asp:BoundField DataField="codCliente" HeaderText="codCliente" SortExpression="codCliente" Visible="false" />
                                                    <asp:BoundField DataField="codEquipamento" HeaderText="codEquipamento" SortExpression="codEquipamento" Visible="false" />
                                                    <asp:BoundField DataField="codTipoServico" HeaderText="codTipoServico" SortExpression="codTipoServico" Visible="false" />
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
                                    </div>
                                </div>

                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#acc3">Libera Documentos
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="acc3" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            <asp:GridView CssClass="grids" HorizontalAlign="Center" ID="gdvDocumentoLiberar" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" Width="50%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gdvDocumentoLiberar_PageIndexChanging" AllowSorting="True" OnSorting="gdvDocumentoLiberar_Sorting" Caption="Listagem de Documentos a Liberar" ForeColor="Black" GridLines="Vertical" OnRowCommand="gdvDocumentoLiberar_RowCommand">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="NomeCliente" HeaderText="Cliente" SortExpression="NomeCliente" />
                                                    <asp:BoundField DataField="NomeDocumento" HeaderText="Documento" SortExpression="NomeDocumento" />
                                                    <asp:BoundField DataField="dataAssinatura" HeaderText="Data Criação" SortExpression="DataAssinatura" />
                                                    <asp:ButtonField ButtonType="Button" CommandName="irpara" Text="Ir para ..." />
                                                    <asp:BoundField DataField="codCliente" HeaderText="codCliente" SortExpression="codCliente" Visible="false" />
                                                    <asp:BoundField DataField="codEquipamento" HeaderText="codEquipamento" SortExpression="codEquipamento" Visible="false" />
                                                    <asp:BoundField DataField="codTipoServico" HeaderText="codTipoServico" SortExpression="codTipoServico" Visible="false" />
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
                                    </div>
                                </div>
                              
                                <%} %>
                            </div>

                        </div>
                    </div>
                </div>

            </div>
        </div>

        <script type="text/javascript">
            $(function () {
                var paneName = $("[id*=hfAccordionIndex]").val() != "" ? $("[id*=hfAccordionIndex]").val() : "collapseOne";

                //Remove the previous selected Pane.
                $("#accordion .in").removeClass("in");

                //Set the selected Pane.
                $("#" + paneName).collapse("show");

                //When Pane is clicked, save the ID to the Hidden Field.
                $(".panel-heading a").click(function () {
                    $("[id*=hfAccordionIndex]").val($(this).attr("href").replace("#", ""));
                });
            });
        </script>

    </div>
</asp:Content>

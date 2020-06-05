<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="erroAcesso.aspx.cs" Inherits="GesDoc.Web.App.erroAcesso" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>:: Application - Gestão de Clientes e Documentos ::</title>
    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>

<body class="bodyInterno">
    <form id="form1" runat="server">
        <header>
            <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <img src="/Imagens/logo.jpg" class="img-responsive img-fluid logo" />
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#colapseNavBar">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                    </div>
                    <div class="collapse navbar-collapse" id="colapseNavBar">
                    </div>
                </div>
            </nav>
        </header>
        <section>
            <div class="container containerInterno">
                <asp:Panel ID="pnlResultado" runat="server" CssClass="cosnsultadocumentos">

                    <table class="tblConteudoForms" style="margin-top:25%">
                        <tr>
                            <td class="cellCenter" colspan="2">
                                <asp:Label ID="lblPrincipal" runat="server" Text="Ocorreu erro de acesso ou a sessão expirou, por favor refaça login !" ForeColor="White"></asp:Label>
                                <br /><br />
                                <uc1:ButtonBar runat="server" ID="ButtonBar" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </section>
        <!-- Rodapé -->
        <footer class="navbar-default navbar-fixed-bottom rodapeInterno">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 text-left">
                        <img alt="Lion Systems Soluções" class="img-responsive minhaLogo" src="Imagens/logoLion.jpg" />
                    </div>
                </div>
            </div>
        </footer>
        <script type="text/javascript" src="/Scripts/geral_sitev0001.js"></script>
    </form>
</body>
</html>

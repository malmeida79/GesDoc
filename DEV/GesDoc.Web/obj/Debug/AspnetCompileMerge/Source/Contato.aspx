<%@ Page Title="Contato" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contato.aspx.cs" Inherits="GesDoc.Web.Contato" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        document.getElementById('fxTela').style.display = 'block';
        document.getElementById('fxTela').innerHTML = 'Entre em contato';
    </script>

    <div class="corpoPaginaContato">
        <div class="row">
            <div class="col-md-6">
                <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d14628.387680936838!2d-46.69122378177871!3d-23.564962328466365!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x94ce5798cede14ed%3A0xc40d8e1930eb9bf7!2sRad+Dimenstein+e+Associados!5e0!3m2!1spt-BR!2sbr!4v1497374383526" class="mapa"></iframe>
            </div>
            <div class="col-md-6">
                <div class="panel txtMapa">
                    <h3>Onde estamos?</h3>
                    <address>
                        Rua Cardeal Arcoverde, 1749 - cj.57B - Pinheiros<br />
                        São Paulo, SP - 05407-002<br />
                        <span class="glyphicon glyphicon-earphone"></span>(11)3885-6329/3884-7538<br />
                        <span class="glyphicon glyphicon-envelope"></span><a href="mailto:comercial@radimenstein.com.br">comercial@radimenstein.com.br</a>
                    </address>
                </div>

                <div class="contato">
                    <h3>SEJA UM CLIENTE</h3>
                    Vantagens de ser Cliente:<br />
                    - Acesso e atualização online de cadastro;<br />
                    - Acesso e download de documentos;<br />
                </div>

            </div>
        </div>
    </div>



</asp:Content>

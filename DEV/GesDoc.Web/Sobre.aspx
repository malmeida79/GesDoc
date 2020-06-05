<%@ Page Title="Sobre Nós" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sobre.aspx.cs" Inherits="GesDoc.Web.Sobre" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        document.getElementById('fxTela').style.display = 'block';
        document.getElementById('fxTela').innerHTML = 'Sobre';
    </script>
    <div class="corpoPaginaSobre">
        <div class="panel">
            <div class="row">
                <div class="col-md-6">
                    <img src="Imagens/historia.jpg" class="img-responsive imgPrcSobre" />
                </div>
                <div class="col-md-5 text-justify">
                    <h3>Nossa História</h3>
                    <p style="font-size: small;">
                        Fundada em 04 de julho de 1997, a empresa Rad Dimenstein e Associados tem forte atuação nas áreas de Controle de Qualidade e de Proteção 
                        Radiológica, as quais são desenvolvidas nas áreas de Radiodiagnóstico e Serviços de Medicina Nuclear. A empresa conta em sua equipe físicos 
                        com título de Supervisor de Proteção Radiológica emitidos pela CNEN e título de Especialista em Radiodiagóstico pela ABFM. Além disso, os 
                        profissionais participam de atualizações instrumentais para acompanhar as novas tecnologias na área de Imagens Diagnósticas. 
                        A empresa atende desde pequenas clínicas até grandes hospitais, como o Hospital Samaritano (SP), Hospital Albert Einstein, Grupo Fleury,
                        Hospital Santa Catarina, entre outros.
                    </p>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <div class="panel">
                    <h3>Conheça nossos profissionais</h3>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="thumbnail">
                    <img src="Imagens/renato.jpg" class="img-responsive imgSobre" />
                    <figcaption class="caption">
                        <h4>Renato Dimenstein</h4>
                        Bacharel em Física, Especialista em Radiodiagnóstico (ABMF RX-261/680), Supervisor de Radioproteção (CNEN FM-0004)
                    </figcaption>
                </div>
            </div>
            <div class="col-md-4">
                <div class="thumbnail">
                    <img src="Imagens/amelia.jpg" class="img-responsive imgSobre" />
                    <figcaption class="caption">
                        <h4>Dra. Amélia Amaral</h4>
                        Sou um subtítulo. Clique aqui para editar e contar aos seus clientes um pouco sobre você.
                    </figcaption>
                </div>
            </div>
            <div class="col-md-4">
                <div class="thumbnail">
                    <img src="Imagens/pedro.jpg" class="img-responsive imgSobre" />
                    <figcaption class="caption">
                        <h4>Dr. Pedro Nunes</h4>
                        Sou um subtítulo. Clique aqui para editar e contar aos seus clientes um pouco sobre você.
                    </figcaption>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
                <div class="thumbnail">
                    <img src="Imagens/Luiz.jpg" class="img-responsive imgSobre" />
                    <figcaption class="caption">
                        <h4>Dr. Luiz Fernando Schuh</h4>
                        Sou um subtítulo. Clique aqui para editar e contar aos seus clientes um pouco sobre você.
                    </figcaption>
                </div>
            </div>
            <div class="col-md-4">
                <div class="thumbnail">
                    <img src="Imagens/amelia.jpg" class="img-responsive imgSobre" />
                    <figcaption class="caption">
                        <h4>Dra. Amélia Amaral</h4>
                        Sou um subtítulo. Clique aqui para editar e contar aos seus clientes um pouco sobre você.
                    </figcaption>
                </div>
            </div>
            <div class="col-md-4">
                <div class="thumbnail">
                    <img src="Imagens/pedro.jpg" class="img-responsive imgSobre" />
                    <h4>Dr. Pedro Nunes</h4>
                    Sou um subtítulo. Clique aqui para editar e contar aos seus clientes um pouco sobre você.
                    </figcaption>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="thumbnail">
                    <img src="Imagens/Luiz.jpg" class="img-responsive imgSobre" />
                    <figcaption class="caption">
                        <h4>Dr. Luiz Fernando Schuh</h4>
                        Sou um subtítulo. Clique aqui para editar e contar aos seus clientes um pouco sobre você.
                    </figcaption>
                </div>
            </div>
            <div class="col-md-4">
                <div class="thumbnail">
                    <img src="Imagens/amelia.jpg" class="img-responsive imgSobre" />
                    <figcaption class="caption">
                        <h4>Dra. Amélia Amaral</h4>
                        Sou um subtítulo. Clique aqui para editar e contar aos seus clientes um pouco sobre você.
                    </figcaption>
                </div>
            </div>
            <div class="col-md-4">
                <div class="thumbnail">
                    <img src="Imagens/pedro.jpg" class="img-responsive imgSobre" />
                    <figcaption class="caption">
                        <h4>Dr. Pedro Nunes</h4>
                        Sou um subtítulo. Clique aqui para editar e contar aos seus clientes um pouco sobre você.
                    </figcaption>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>

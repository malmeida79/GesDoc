<%@ Page Title="Medicina Nuclear" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="medNuclear.aspx.cs" Inherits="GesDoc.Web.medNuclear" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        document.getElementById('fxTela').style.display = 'block';
        document.getElementById('fxTela').innerHTML = 'Serviços em Medicina Nuclear';
    </script>

    <div class="corpoPaginas">
        <div class="panel">
            <div class="row">
                <div class="col-md-6">
                    <img src="Imagens/nuclear1.jpg" class="img-responsive imgNuc" />
                </div>
                <div class="col-md-6 txtNuc">
                    <h4>Controle de Qualidade de Equipamentos</h4>
                    <i>December 31, 2019</i><br />
                    A Rad Dimenstein realiza os Controles de Qualidade dos equipamentos emissores de Raios-X conforme as orientações da AAPM e da Vigilância Sanitária, 
                    emitindo documentos técnicos que comprovam o desempenho de um equipamento ou recomendam a verificação por parte da engenharia clínica.
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <img src="Imagens/nuclear2.jpg" class="img-responsive imgNuc" />
                </div>
                <div class="col-md-6 txtNuc">
                    <h4>Proteção Radiológica</h4>
                    <i>January 02, 2023</i><br />
                    <br />
                    A Proteção Radiológica tem o propósito de garantir que os seus Princípios sejam cumpridos: Justificação da Prática, Otimização da Proteção Radiológica 
                    e Limitação de Doses Individuais. A dosimetria dos colaboradores é um dos indicadores analisados pelo acompanhamento da Proteção Radiológica feita pela Rad Dimenstein.<br />
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <img src="Imagens/nuclear3.jpg" class="img-responsive imgNuc" />
                </div>
                <div class="col-md-6 txtNuc">
                    <h4>Treinamentos</h4>
                    <i>February 22, 2023</i><br />
                    <br />
                    O conhecimento das bases físicas é fundamental para sua equipe entender as relações entre mAs e kV na realização de exames de imagens diagnósticas de modo
                     que elas apresentem o melhor contraste com o menor ruído, diminuindo repetições de exames e doses de radiação nos pacientes.
                <br />
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <img src="Imagens/nuclear4.jpg" class="img-responsive imgNuc" />
                </div>
                <div class="col-md-6 txtNuc">
                    <h4>Projetos de Blindagem</h4>
                    <i>May 27, 2023</i><br />
                    <br />
                    Para proteção dos Indivíduos Ocupacionalmente Expostos (IOE) às radiações, os equipamentos emissores de Raios-X necessitam de uma espaço físico onde suas
                     paredes devem ser construídas de tal forma a oferecer blindagem segura. A Rad Dimenstein realiza cálculos de blindagem para diversos equipamentos: Raios-X, 
                    Arco-cirúrgico, Tomografia Computadorizada, etc.<br />
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <img src="Imagens/nuclear5.jpg" class="img-responsive imgNuc" />
                </div>
                <div class="col-md-6 txtNuc">
                    <h4>Acessoria Técnica</h4>
                    <i>May 27, 2023</i><br />
                    <br />
                    As instalações de medicina nuclear necessitam de uma licença de autorização para comprar o material radioativo que será utilizado para realização dos exames 
                diagnósticos. A Rad Dimenstein realiza todo o contato com a CNEN (Comissão Nacional de Energia Nuclear) para conseguir esta autorização. Juntamente com o cálculo 
                de blindagem, elaboração e implementação do Plano de Radioproteção e proteção radiológica.
                </div>
            </div>
        </div>
    </div>


</asp:Content>

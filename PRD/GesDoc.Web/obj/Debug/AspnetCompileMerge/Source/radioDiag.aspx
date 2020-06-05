<%@ Page Title="Radiodiágnostico" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="radioDiag.aspx.cs" Inherits="GesDoc.Web.radioDiag" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        document.getElementById('fxTela').style.display = 'block';
        document.getElementById('fxTela').innerHTML = 'Serviços em Radiodiágnostico';
    </script>

    <div class="corpoPaginas">
        <div class="panel">
            <div class="row">
                <div class="col-md-6">
                    <img src="Imagens/radio1.jpg" class="img-responsive imgRDDiag" />
                </div>
                <div class="col-md-6 txtrdDiag">
                    <h4>Controle de Qualidade de Equipamentos</h4>
                    <i>December 31, 2019</i><br />
                    A Rad Dimenstein realiza os Controles de Qualidade dos equipamentos emissores de Raios-X conforme as orientações da AAPM e da Vigilância Sanitária, 
                    emitindo documentos técnicos que comprovam o desempenho de um equipamento ou recomendam a verificação por parte da engenharia clínica.
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <img src="Imagens/radio2.jpg" class="img-responsive imgRDDiag" />
                </div>
                <div class="col-md-6 txtrdDiag">
                    <h4>Proteção Radiológica</h4>
                    <i>January 02, 2023</i><br />
                    <br />
                    A Proteção Radiológica tem o propósito de garantir que os seus Princípios sejam cumpridos: Justificação da Prática, Otimização da Proteção Radiológica 
                    e Limitação de Doses Individuais. A dosimetria dos colaboradores é um dos indicadores analisados pelo acompanhamento da Proteção Radiológica feita pela Rad Dimenstein.<br />
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <img src="Imagens/radio3.jpg" class="img-responsive imgRDDiag" />
                </div>
                <div class="col-md-6 txtrdDiag">
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
                    <img src="Imagens/radio4.jpg" class="img-responsive imgRDDiag" />
                </div>
                <div class="col-md-6 txtrdDiag">
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
                    <img src="Imagens/radio5.jpg" class="img-responsive imgRDDiag" />
                </div>
                <div class="col-md-6 txtrdDiag">
                    <h4>Levantamento Radiométrico</h4>
                    <i>May 27, 2023</i><br />
                    <br />
                    A legislação brasileira exige que sejam realizados Levantamentos Radiométricos nas vizinhanças das salas onde se encontram equipamentos emissores de raios-x
                     para comprovação da eficiência da blindagem instalada.
                </div>
            </div>
        </div>
    </div>

</asp:Content>

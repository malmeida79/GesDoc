<%@ Page Title="Dúvidas Frequentes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="faq.aspx.cs" Inherits="GesDoc.Web.Faq" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        document.getElementById('fxTela').style.display = 'block';
        document.getElementById('fxTela').innerHTML = 'Dúvidas Frequentes';
    </script>

    <div class="corpoPaginaSobre">

        <div class="panel-group">

            <div class="panel panel-default">

                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#collapse1">Radiodiagnóstico</a>
                    </h4>
                </div>

                <div id="collapse1" class="panel-collapse collapse">
                    <div class="panel-body">

                        <div class="panel-group" id="accordion">

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc1">
                                            <b>1</b> - Qual a validade dos testes de controle de qualidade?
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc1" class="panel-collapse collapse in">
                                    <div class="panel-body">
                                        <b>R:</b> No caso dos equipamentos radiológicos os testes têm duração de 12 meses. 
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc2">
                                            <b>2</b> - Qual a periodicidade dos testes de controle de qualidade em mamografia?
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc2" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <b>R:</b> No caso dos mamógrafos são recomendados de acordo com a ANVISA testes anuais de gerador e tubo, testes semestrais do sistema automático e mensal de qualidade de imagem;
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc3">
                                            <b>3</b> - Qual a validade dos levantamentos radiométricos?
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc3" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <b>R:</b> No caso dos equipamentos radiológicos os testes têm duração de 4 anos. 
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc4">
                                            <b>4</b> - Em caso de troca de tubos de raios X é necessário a realização de novos testes, mesmo que esteja dentro da validade?
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc4" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <b>R:</b> Após a troca do tubo recomenda-se a realização de testes de controle de qualidade e testes de radiação de fuga da ampola de raios X; 
                                    </div>
                                </div>
                            </div>


                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc5">
                                            <b>5</b> - A espessura das blindagens da sala de raios X é sempre a mesma? 
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc5" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <b>R:</b> Para cada sala radiológica deve ser executado um memorial de cálculo com as espessuras das barreiras necessárias para a proteção radiológica. Isto deve levar em consideração a carga de trabalho de exames por semana, fator de ocupação e uso da sala. Com estes dados o físico elabora as espessuras das blindagens; 
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc6">
                                            <b>6</b> - Qual a validade dos planos de proteção radiológica?
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc6" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <b>R:</b> No caso dos serviços de radiologia o plano deve ser alterado a cada modificação da instalação e/ou equipamento; 
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc7">
                                            <b>7</b> - Qual a validade dos testes de integridade dos aventais?
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc7" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <b>R:</b> Os testes de epis devem ser realizados a cada 12 meses;
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc8">
                                            <b>8</b> - Qual a validade dos treinamentos em proteção radiológica?
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc8" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <b>R:</b> Os treinamentos dos colaboradores expostos a radiações ionizantes devem ser realizados a cada 12 meses;
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc9">
                                            <b>9</b> - Qual o procedimento a ser adotado em caso de doses superiores a 1, 0 mSv?
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc9" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <b>R:</b> Os colaboradores expostos a radiações ionizantes que ultrapassarem este limite devem ser submetidos a relatórios de investigação;
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>

            </div>

        </div>

        <div class="panel-group">

            <div class="panel panel-default">

                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#collapse2">Medicina Nuclear</a>
                    </h4>
                </div>

                <div id="collapse2" class="panel-collapse collapse">
                    <div class="panel-body">

                        <div class="panel-group" id="accordion2">

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc21">
                                            <b>1</b> - Qual a validade dos testes de controle de qualidade de Medicina Nuclear<br />
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc21" class="panel-collapse collapse in">
                                    <div class="panel-body">
                                        <b>R:</b> No caso dos equipamentos SPECT os testes de qualidade de imagens devem ser executados a cada seis meses. Os testes de uniformidade de campos diferencial e integral para baixa 
densidade de contagem, a centralização e verificação da largura da janela energética utilizados, a verificação da radiação de fundo e a inspeção visual da integridade física
do equipamento devem ser realizados diariamente, antes do início da rotina clínica. Mensalmente deverão ser realizados testes para análise da resolução e linearidade espacial, 
assim como o desvio do centro de rotação, a realização deverá ser mensal. 
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc31">
                                            <b>2</b> - Qual a periodicidade dos testes de controle de qualidade Calibradores de doses e Geiger (GM)?         
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc31" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <b>R:</b> De acordo com as normas da CNEN, no caso dos curiometros o teste é semestral quanto a precisão e exatidão. Anualmente deverão ser realizados testes de geometria e linearidade. Além 
disso, diariamente devem ser realizados os testes de tensão do equipamento, radiação de fundo, ajuste do zero e repetibilidade. 
No caso dos GM e sondas de contaminação e exposição os testes devem ser realizados mensalmente. A cada dois anos os Geigers devem ser enviados para calibração.  
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc41">
                                            <b>3</b> - Qual a validade dos levantamentos radiométricos?              
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc41" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <b>R:</b> No caso de medidas de contaminação são recomendadas aferições diárias. Para as medidas de taxas de exposição, a legislação exige um intervalo máximo de quinze dias. 
Em caso de troca de reparo de detector é necessário a realização de novos testes, mesmo que esteja dentro da validade? 
                                        <b>R:</b> Após a troca do cristal recomenda-se a realização de testes de controle de qualidade da gama câmara; 
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc51">
                                            <b>4</b> - A espessura das blindagens da sala de medicina nuclear é sempre a mesma?                 
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc51" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <b>R:</b> Para cada sala MN deve ser executado um memorial de cálculo com as espessuras das barreiras necessárias para a proteção radiológica. Isto deve levar em consideração a carga de
trabalho de exames por semana, fator de ocupação e uso da sala. Com estes dados o físico elabora as espessuras das blindagens; Para PET não se recomenda o uso de barita;
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc61">
                                            <b>5</b> - Qual a validade dos planos de proteção radiológica?                                                          
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc61" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <b>R:</b> No caso dos serviços de radioisótopos o plano deve ser alterado a cada modificação da instalação e/ou equipamento e ser feita uma alteração junto a CNEN; 
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc71">
                                            <b>6</b> - Qual a validade dos testes de integridade dos aventais?                                                      
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc71" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <b>R:</b> Os testes de epis devem ser realizados a cada 12 meses;
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc81">
                                            <b>7</b> - Qual a validade dos treinamentos em proteção radiológica?                                                     
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc81" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <b>R:</b> Os treinamentos dos colaboradores expostos a radiações ionizantes devem ser realizados a cada 12 meses;
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#acc91">
                                            <b>8</b> - Qual o procedimento a ser adotado em caso de doses superiores a 1, 0 mSv?            
                                        </a>
                                    </h4>
                                </div>
                                <div id="acc91" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <b>R:</b> Os colaboradores expostos a radiações ionizantes que ultrapassarem este limite devem ser submetidos a relatórios de investigação;  
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
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

    </div>

</asp:Content>

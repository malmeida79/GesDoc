using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Infraestructure;
using GesDoc.Web.Services;
using System;
using System.IO;
using System.Web.UI.WebControls;

namespace GesDoc.Web.App
{
    public partial class adicionaDocumento : System.Web.UI.Page
    {
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        ~adicionaDocumento()
        {
            UsuarioLogado = null;
            permissoes = null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Gestão de documentos ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            ButtonBar.AcaoClick += new EventHandler(btnAcao_Click);
            ButtonBar.CancelarClick += new EventHandler(btnCancelar_Click);

            DropTipoDocumento.SelectedIndexChanged += new EventHandler(cboTipoDocumento_SelectedIndexChanged);
            DropClientes.SelectedIndexChanged += new EventHandler(cboClientes_SelectedIndexChanged);
            DropEquipamentos.SelectedIndexChanged += new EventHandler(cboEquipamentos_SelectedIndexChanged);
            DropTipoServico.SelectedIndexChanged += new EventHandler(cboTipoServico_SelectedIndexChanged);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class=""glyphicon glyphicon-upload""></span> Upload Arquivo", habilitado: false);
                DropTipoDocumento.CarregaTipoDocumento();
                DropClientes.Visible = false;
                DropEquipamentos.Visible = false;
                DropTipoServico.Visible = false;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            FuncoesGerais.ExecutaJScript("$('.closebut').click();");
        }

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                String fileExtension = Path.GetExtension(FileUpload.FileName).ToLower();
                Log log = new Log();

                try
                {
                    TipoDocumento tpdoc = new TipoDocumento();
                    TipoDocumentoController ctrlTpDoc = new TipoDocumentoController();
                    string path = string.Empty;

                    tpdoc.CodTipoDocumento = DropTipoDocumento.GetSelectedValue();
                    tpdoc = ctrlTpDoc.GET(tpdoc);

                    DocumentosController ctrlDocs = new DocumentosController();
                    Arquivos flAdm = new Arquivos();

                    if (tpdoc.TipoCliente || tpdoc.ClassificadoTipoServico)
                    {
                        flAdm.CodCliente = DropClientes.GetSelectedValue();
                    }

                    if (tpdoc.ClassificadoTipoServico)
                    {
                        flAdm.CodEquipamento = DropEquipamentos.GetSelectedValue();
                        flAdm.CodTipoServico = DropTipoServico.GetSelectedValue();
                    }

                    // Criando entidade documento para armazenar os dados do processo.
                    Documentos documento = new Documentos();
                    documento.CodCliente = flAdm.CodCliente;

                    documento.CodEquipamento = flAdm.CodEquipamento;
                    documento.CodTipoServico = flAdm.CodTipoServico;

                    documento.CodTipoDocumento = tpdoc.CodTipoDocumento;
                    documento.NomeDocumento = Tratamentos.RemoveDiacritics(Tratamentos.RemoveSpecialCharacters(Path.GetFileNameWithoutExtension(FileUpload.FileName)));
                    documento.NomeDocumento = $"{documento.NomeDocumento}{fileExtension}";
                    documento.CodUsuarioGeracao = UsuarioLogado.codUsuario;

                    // verificando se é para enviar notificações e para quem
                    if (Ambiente.EnviaNotificacoes())
                    {
                        // notifica novo documento para assinar
                        Emails.EnviarEmail(Ambiente.EmailNotificacoes(), "sistema@radimenstein.com.br", "Novo documento", "Novo documento disponivel para assinatura e liberação.", "prog.marcos@gmail.com");
                    }

                    // Analisa tipo de documento para identificar forma de gravação
                    if (tpdoc.TipoGeral)
                    {
                        flAdm.SubPastaGeral = DropTipoDocumento.GetSelectedText();
                        path = $@"{flAdm.GETPasta(Ambiente.TipoPasta.Geral).ConverteVirtualParaFisico()}{documento.NomeDocumento}";
                    }
                    else if (tpdoc.TipoCliente && !tpdoc.ClassificadoTipoServico)
                    {
                        path = $"{flAdm.GETPasta(Ambiente.TipoPasta.DocsCliente).ConverteVirtualParaFisico()}{documento.NomeDocumento}";
                    }
                    else if (tpdoc.ClassificadoTipoServico)
                    {
                        path = $"{flAdm.GETPasta(Ambiente.TipoPasta.DocumentosServicoEquipamentoCliente).ConverteVirtualParaFisico()}{documento.NomeDocumento}";
                    }
                    else
                    {
                        throw new Exception("Tipo de arquivo não previsto !");
                    }

                    // verifica se já exite esse arquivo
                    if (File.Exists(path))
                    {
                        // apaga documento
                        File.Delete(path);

                        // pesquisa documento para excluir caso já cadastrado
                        Documentos docPesquisa = new Documentos();

                        if (tpdoc.TipoCliente || tpdoc.ClassificadoTipoServico)
                        {
                            docPesquisa.CodCliente = documento.CodCliente;
                        }

                        if (tpdoc.ClassificadoTipoServico)
                        {
                            docPesquisa.CodEquipamento = documento.CodEquipamento;
                            docPesquisa.CodTipoServico = documento.CodTipoServico;
                        }

                        if (tpdoc.TipoGeral)
                        {

                            TipoDocumento tpdocdel = new TipoDocumento();

                            tpdocdel.DescricaoTipoDocumento = DropTipoDocumento.GetSelectedText();
                            tpdocdel = ctrlTpDoc.GET(tpdocdel);

                            docPesquisa.CodTipoDocumento = tpdocdel.CodTipoDocumento;

                            tpdocdel = null;
                        }

                        docPesquisa.NomeDocumento = documento.NomeDocumento;

                        var ListaEncontrados = ctrlDocs.GET(docPesquisa);

                        if (ListaEncontrados.Count > 0)
                        {
                            foreach (var item in ListaEncontrados)
                            {
                                // exclui documento caso exista cadastrado no banco de dados.
                                ctrlDocs.Delete(item);
                            }
                        }

                        docPesquisa = null;
                    }

                    // cadastra documento
                    ctrlDocs.Put(documento);

                    // salva arquivo
                    FileUpload.SaveAs(path);

                    if (fileExtension == ".pdf")
                    {
                        // Apesar de o metodo insere rosto fazer a checagem de se deve ou nao inserir
                        // pagina de rosto, refazemos aqui também para podermos nem carregar o modulo 
                        // PDFS.
                        if (Ambiente.InserePaginaRosto())
                        {
                            PDFs docPdf = new PDFs();

                            // insere pagina de rosto
                            docPdf.InsereRosto(documento.NomeDocumento, documento.CodCliente, documento.CodEquipamento);

                            docPdf = null;
                        }
                    }

                    lblRetorno.Text = "Arquivo carregado com sucesso.";
                    Mensagens.Alerta(lblRetorno.Text);

                    // descarregando o upload
                    FileUpload.Dispose();

                    // limpando dados
                    tpdoc = null;
                    ctrlTpDoc = null;
                    documento = null;
                    flAdm = null;
                    ctrlDocs = null;
                }
                catch (Exception ex)
                {
                    lblRetorno.Text = $"Falha no upload do arquivo:{ex.Message.ToString()}";
                    Mensagens.Alerta(lblRetorno.Text);
                    log.RegistraLog(ex.Message.ToString(), "Upload de arquivos");
                }

                log = null;
            }
            else
            {
                lblRetorno.Text = "Selecione um arquivo.";
                Mensagens.Alerta(lblRetorno.Text);
            }
        }

        protected void cboTipoServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropTipoServico.GetSelectedIndex() > 0)
            {
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, habilitado: permissoes.Gravacao);
            }
            else
            {
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, habilitado: false);
            }
        }

        protected void cboTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropTipoDocumento.GetSelectedIndex() > 0)
            {
                TipoDocumento tpdoc = new TipoDocumento();
                TipoDocumentoController ctrlTpDoc = new TipoDocumentoController();

                DropClientes.Descarrega();
                DropEquipamentos.Descarrega();
                DropTipoServico.Descarrega();

                DropClientes.Visible = false;
                DropEquipamentos.Visible = false;
                DropTipoServico.Visible = false;

                tpdoc.CodTipoDocumento = DropTipoDocumento.GetSelectedValue();
                tpdoc = ctrlTpDoc.GET(tpdoc);

                if (tpdoc.TipoGeral)
                {
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, habilitado: permissoes.Gravacao);
                }

                if (tpdoc.TipoCliente || tpdoc.ClassificadoTipoServico)
                {
                    DropClientes.Visible = true;
                    DropClientes.CarregaClientes();
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, habilitado: permissoes.Gravacao);
                }

                if (tpdoc.ClassificadoTipoServico)
                {
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, habilitado: false);
                    DropEquipamentos.Visible = true;
                    DropTipoServico.Visible = true;
                }

                ctrlTpDoc = null;
                tpdoc = null;
            }
            else
            {
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, habilitado: false);

                DropClientes.Descarrega();
                DropEquipamentos.Descarrega();
                DropTipoServico.Descarrega();

                DropClientes.Visible = false;
                DropEquipamentos.Visible = false;
                DropTipoServico.Visible = false;
            }
        }

        protected void cboClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropClientes.GetSelectedValue() > 0)
            {
                DropEquipamentos.ClienteReferencia = DropClientes.GetSelectedValue();
                DropEquipamentos.CarregaEquipamentos();
                DropTipoServico.CarregaTipoServico();
            }
        }

        protected void cboEquipamentos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

// Chamada para windowmanager -----------------------------------------------------

function CallWm(urlAbrir, titleBar, comprimento, htmlContent) {

    var w = $().WM('open', urlAbrir).width(comprimento).scrollLeft('auto').resize();

    w.find('.windowcontent').html(htmlContent);

    if (titleBar !== null && titleBar !== "") {
        w.find('.titlebartext').text(titleBar);
    }

    if (htmlContent !== null && htmlContent !== "") {
        w.find('.windowcontent').html(htmlContent);
    }
}

// Método utilizado para Modais de alerta do site inteiro -------------------------

function AbreModal(frase) {
    $('#myModal .modal-body').text(frase);
    $('#myModal').modal('show');
}

function AbreLink(urlAtiva) {
    window.open('http://' + urlAtiva + '', '_blank');
}

function DonwloadLink(urlAtiva) {
    // with this solution, the browser handles the download link naturally (tested in chrome and firefox)
    $(document).ready(function () {
        $('#downloader').attr('src', 'downloader.aspx?caminho=' + urlAtiva);
        $('#downloader').load();
    });
}

// Controle da modal de duvidas do site inteiro ------------------------------------

function AbreModalDuvidas(frase) {

    var textoDuvida = $("input[id*='hfDuvidas']").val();

    if (textoDuvida === "" || textoDuvida === null || textoDuvida === undefined) {
        textoDuvida = "Essa página ainda não tem help configurado, por favor informe ao suporte a página para que possa ser criada.";
    }

    $('#myModal .modal-title').text(":: Dúvidas ::");
    $('#myModal .modal-body').text(textoDuvida);
    $('#myModal').modal('show');
}

function OcultaIconeAjuda() {
    $('.duvidasIco').css("display", "none");
}

function ExibeIconeAjuda() {
    $('.duvidasIco').addClass("duvidasIco");
}

// Método utilizado para Modais de confirmação do site inteiro
function AbreConfirmModal(frase) {
    $('#myConfirmModal .modal-body').text(frase);
    $('#myConfirmModal').modal('show');

    // Capturando click do botao e disprando botao ou evento da classe
    // css Cssclass=""
    $('#btnMethod').click(function (e) {
        $('.finalizarAcao').click();
    });
}

// Método utilizado para Modais de confirmação do site inteiro com
// opcional para camnpos que possuem mais de uma possivel acao na 
// tela (Como por exemplo a de cadastro de clientes):
//      btnAcaoJQueryEnd
//      btnAcaoJQueryCnt
//      btnAcaoJQuerySet
//      btnAcaoJQuerySla
//      btnAcaoJQueryEqp

function AbreConfirmModalCampo(frase, campo) {
    $('#myConfirmModal .modal-body').text(frase);
    $('#myConfirmModal').modal('show');

    // Capturando click do botao e disprando botao ou evento da classe
    // css Cssclass=""
    $('#btnMethod').click(function (e) {
        $('.finalizarAcao' + campo).click();
    });
}

// Script para girar as imagens
$('#myCarousel').carousel({
    pause: 'none'
});

// conjunto de metodos configures das tabelas de dados do sistema
$(document).ready(function () {

    // Start File tree ----------------------------------------------------------------

    $(document).ready(function () {
        if ($(".file-tree").length > 0) {
            $(".file-tree").filetree();
        }
    });

    // Chamada para modal de Duvidas ---------------------------------------------------

    $(".duvidasIco").click(function () {
        AbreModalDuvidas();
    });

    // inicio context menu -------------------------------------------------------------

    (function ($, window) {
        var menus = {};
        $.fn.contextMenu = function (settings) {
            var $menu = $(settings.menuSelector);
            $menu.data("menuSelector", settings.menuSelector);
            if ($menu.length === 0) return;

            menus[settings.menuSelector] = { $menu: $menu, settings: settings };

            //make sure menu closes on any click
            $(document).click(function (e) {
                hideAll();
            });
            $(document).on("contextmenu", function (e) {
                var $ul = $(e.target).closest("ul");
                if ($ul.length === 0 || !$ul.data("menuSelector")) {
                    hideAll();
                }
            });

            // On context menu 

            (function (element, menuSelector) {
                element.on("contextmenu", function (e) {
                    // return native menu if pressing control
                    if (e.ctrlKey) return;

                    hideAll();
                    var menu = getMenu(menuSelector);

                    //open menu
                    menu.$menu
                    .data("invokedOn", $(e.target))
                    .show()
                    .css({
                        position: "absolute",
                        left: getMenuPosition(e.clientX, 'width', 'scrollLeft'),
                        top: getMenuPosition(e.clientY, 'height', 'scrollTop') - 80
                    })
                    .off('click')
                    .on('click', 'a', function (e) {
                        menu.$menu.hide();

                        var $invokedOn = menu.$menu.data("invokedOn");
                        var $selectedMenu = $(e.target);

                        callOnMenuHide(menu);
                        menu.settings.menuSelected.call(this, $invokedOn, $selectedMenu);
                    });

                    callOnMenuShow(menu);
                    return false;
                });
            })($(this), settings.menuSelector);

            function getMenu(menuSelector) {
                var menu = null;
                $.each(menus, function (i_menuSelector, i_menu) {
                    if (i_menuSelector === menuSelector) {
                        menu = i_menu;
                        return false;
                    }
                });
                return menu;
            }

            function hideAll() {
                $.each(menus, function (menuSelector, menu) {
                    menu.$menu.hide();
                    callOnMenuHide(menu);
                });
            }

            function callOnMenuShow(menu) {
                var $invokedOn = menu.$menu.data("invokedOn");
                if ($invokedOn && menu.settings.onMenuShow) {
                    menu.settings.onMenuShow.call(this, $invokedOn);
                }
            }

            function callOnMenuHide(menu) {
                var $invokedOn = menu.$menu.data("invokedOn");
                menu.$menu.data("invokedOn", null);
                if ($invokedOn && menu.settings.onMenuHide) {
                    menu.settings.onMenuHide.call(this, $invokedOn);
                }
            }

            function getMenuPosition(mouse, direction, scrollDir) {
                var win = $(window)[direction](),
                    scroll = $(window)[scrollDir](),
                    menu = $(settings.menuSelector)[direction](),
                    position = mouse + scroll;

                // opening menu would pass the side of the page
                if (mouse + menu > win && menu < mouse) {
                    position -= menu;
                }

                return position;
            }
            return this;
        };
    })(jQuery, window);

    $(".file-tree li.menuItem").contextMenu({
        menuSelector: "#contextMenu",
        menuSelected: function ($invokedOn, $selectedMenu) {

            var urlService = "";

            var codigoBase = "";

            if ($invokedOn.attr('href').indexOf("#") > -1) {
                codigoBase = $invokedOn.attr('href').replace('#', '');
            }

            // chamados
            switch ($selectedMenu.text().trim()) {
                case "Abrir":
                    urlService = "/App/ListaArquivos.aspx/GetCaminhoAbrir";
                    break;
                case "Assinar":
                    urlService = "/App/ListaArquivos.aspx/Assinar";
                    break;
                case "Liberar":
                    urlService = "/App/ListaArquivos.aspx/Liberar";
                    break;
                case "Excluir":
                    $('#myConfirmModal .modal-body').text("Deseja realmente excluir esse documento?");
                    $('#myConfirmModal').modal('show');
                    $('#btnMethod').click(function (e) {
                        urlService = "/App/ListaArquivos.aspx/Excluir";
                        $('#myConfirmModal').modal('hide');
                        AcaoDocumento(urlService, codigoBase);
                    });
                    break;
                case "Download":
                    urlService = "/App/ListaArquivos.aspx/GetCaminhoDownload";
                    break;              
            }

            if (urlService === "") {
                return;
            }

            AcaoDocumento(urlService, codigoBase);

        },
        onMenuShow: function ($invokedOn) {
            var tr = $invokedOn.closest("a");
            $(tr).addClass("btn-warning");
        },
        onMenuHide: function ($invokedOn) {
            var tr = $invokedOn.closest("a");
            $(tr).removeClass("btn-warning");
        }
    });

    // Sem uso para pastas o menu
    //$(".file-tree li.menuPasta").contextMenu({
    //    menuSelector: "#contextMenuUsername",
    //    menuSelected: function ($invokedOn, $selectedMenu) {
    //        var urlService = "";

    //    },
    //    onMenuShow: function ($invokedOn) {
    //        $invokedOn.addClass("btn-success");
    //    },
    //    onMenuHide: function ($invokedOn) {
    //        $invokedOn.removeClass("btn-success");
    //    }
    //});

    // Inicio logon area --------------------------------------------------------------

    function AcaoDocumento(urlService, codigoBase) {
        $.ajax({
            type: "post",
            url: urlService,
            data: '{codigoBase:"' + codigoBase + '"}',
            contentType: "application/Json;charset=utf-8",
            dataType: "Json",
            success: function (response) {
                if (response.d.toLowerCase().indexOf("falha") > -1) {
                    AbreModal(response.d);
                }
                else if (response.d.toLowerCase().indexOf("abre:") > -1) {
                    AbreLink(response.d.replace('abre:', ''));
                }
                else if (response.d.toLowerCase().indexOf("baixa:") > -1) {
                    DonwloadLink(response.d.replace('baixa:', ''));
                } else {
                    AbreModal(response.d);
                }
            },
            error: function (req, status, error) {
                alert(status);
                alert(error);
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    }

    $("#efetuaLogin").click(function () {

        var logUser = $("#txtLogin").val();
        var senhaUser = $("#txtSenha").val();

        $.ajax({
            type: "post",
            url: "Acesso.aspx/GetAcesso",
            data: '{userLogin: "' + logUser + '",userSenha:"' + senhaUser + '" }',
            contentType: "application/Json;charset=utf-8",
            dataType: "Json",
            success: function (response) {
                if (response.d.toLowerCase().indexOf("falha") > -1) {
                    AbreModal(response.d);
                } else {
                    window.location = response.d;
                }
            },
            error: function (req, status, error) {
                alert(error);
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    });
});
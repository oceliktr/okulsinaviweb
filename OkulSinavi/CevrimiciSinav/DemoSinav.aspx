<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DemoSinav.aspx.cs" Inherits="Sinav_DemoSinav" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="tr">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="x-ua-compatible" content="ie=edge" />
    <title>Okul Sınavı</title>
    <link rel="stylesheet" href="/CevrimiciSinav/Content/fontawesome-free/css/all.min.css" />
    <link rel="stylesheet" href="/CevrimiciSinav/Content/css/adminlte.min.css" />
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet" />

    <link rel="stylesheet" href="Content/sweetalert2/sweetalert2.min.css" />
    <link rel="stylesheet" href="Content/ekko-lightbox/ekko-lightbox.css" />
    <link href="Content/css/Site.css" rel="stylesheet" />

    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-56545817-27"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-56545817-27');

    </script>
    <style>
        p {
            display: block;
            width: 100%;
        }
    </style>
    <script src="Content/js/snippet-javascript-console.min.js"></script>
</head>
<body class="hold-transition layout-top-nav">
    <form id="form1" runat="server">
        <div class="wrapper">

            <div class="content-wrapper">

                <div class="content">
                    <div class="row hiddens" id="ustBas">
                        <div class="col-12 mt-1 mb-1">
                            <div class="col-md-1 col-sm-3 col-xs-2 col-lg-1 float-left" style="max-width: 100px;">
                                <div class="w-100 badge badge-warning"><span id="sure">00 : 00 : 00</span></div>
                            </div>
                            <div class="col-md-1 col-sm-3 col-xs-2 col-lg-1 float-right" style="max-width: 150px;">
                                <a class="btn btn-info btn-sm float-right" data-tooltip="tooltip" title="Ekran bilgilendirme" data-toggle="lightbox" data-title="Ekran bilgilendirme" data-gallery="info" href="/upload/sinav-yardim-ekrani.jpg"><i class="fa fa-question"></i></a>

                                <a class="btn btn-danger btn-sm float-right mr-1" href="Sinavlar.aspx"><i class="fa fa-home mr-1"></i>Sınavlar</a>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-12" id="test">

                            <div class="card card-primary card-outline">
                                <div class="card-header">
                                    <h5 class="card-title m-0">
                                        <asp:Literal ID="ltrTestAdi" runat="server"></asp:Literal></h5>

                                </div>
                                <div class="card-body text-center">
                                    <p>
                                        Bu testte <strong>
                                            <asp:Literal ID="ltrSoruSayisi" runat="server"></asp:Literal></strong> soru bulunmaktadır. 
           Süreniz <strong>
               <asp:Literal ID="ltrSure" runat="server"></asp:Literal></strong> dakika
                                    </p>
                                    <p>Hazırsanız başlayalım.</p>

                                    <button type="button" class="btn btn-primary" onclick="Basla()"><i class="fa fa-check mr-4"></i>Başla</button>
                                    <a class="btn btn-danger" href="Sinavlar.aspx"><i class="fa fa-home mr-1"></i>Sınavlar</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="control-sidebar-bg"></div>
        </div>
        <div class="modal fade" id="video">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-body">
                        <video controls="controls" class="img-fluid">
                            <source src="/upload/testvideo.mp4" type="video/mp4" />
                            Tarayıcınız video oynatmayı desteklemiyor.
                        </video>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Kapat</button>
                    </div>
                </div>
            </div>
        </div>
        <script src="/CevrimiciSinav/Content/jquery/jquery.min.js"></script>
        <script src="/CevrimiciSinav/Content/bootstrap/js/bootstrap.bundle.min.js"></script>
        <script src="/CevrimiciSinav/Content/js/adminlte.min.js"></script>

        <script src="Content/sweetalert2/sweetalert2.min.js"></script>
        <script src="Content/ekko-lightbox/ekko-lightbox.min.js"></script>
        <script>

            //$(document).bind("contextmenu", function (e) { return false; });
            function DersDegis(e) {

                var AjaxJson = { OturumId: <%=Request.QueryString["t"]%>, BransId: e.value };
                $.ajax({
                    type: "POST",
                    url: "/CevrimiciSinav/DemoSinav.aspx/BransIlkSorusu",
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify(AjaxJson),
                    success: function (response) {
                        var data = $.parseJSON(response.d);
                        if (data.Sonuc === "ok") {
                            $("#test").load("/CevrimiciSinav/DemoSinavGetir.aspx?t=<%=Request.QueryString["t"]%>&s=" + data.Mesaj);
                        } else {
                            Swal.fire({
                                icon: 'warning',
                                title: 'Hata!',
                                text: data.Mesaj,
                                showConfirmButton: true,
                                confirmButtonText: "Tamam"
                            });
                        }
                    }
                });
            }
            
            $(function () {
                $(document).on('click',
                    '[data-toggle="lightbox"]',
                    function (event) {
                        event.preventDefault();
                        $(this).ekkoLightbox({
                            alwaysShowClose: true
                        });
                    });
                $('[data-tooltip="tooltip"]').tooltip();

            });

            //sayaç başlangıç
            var saniye = 0, dakika = 0, saat = 0;
            var sayac = null;
            function pad(d) {
                return (d < 10) ? '0' + d.toString() : d.toString();
            }

            function SureSay() {
                sayac = setInterval(() => {
                        if (saniye === 0 && dakika === 0 || (saat > 0 && dakika === 0 && saniye === 0)) {
                            saat--;
                            dakika = 59;
                            saniye = 60;
                            SureyiYenile();
                        }

                        if (saniye > 0) {
                            saniye--;
                            SureyiYenile();
                        } else if (saniye === 0) {
                            dakika--;
                            saniye = 59;
                            SureyiYenile();
                        }
                    },
                    1000);
            }

            function SureyiYenile() {

                if (saat < 0) {
                    $("#sure").html("00 : 00 : 00");
                } else {
                    $("#sure").html(pad(saat) + " : " + pad(dakika) + " : " + pad(saniye));
                }

                if (saat === 0 && dakika === 0 && saniye === 0) {
                    clearInterval(sayac);
                    DegerlendirAjax();
                }
            }
            //sayaç son

            function GeriDon(s) {
                $("#ustBas").removeClass("hiddens");
                $("#test").load("/CevrimiciSinav/DemoSinavGetir.aspx?t=<%=Request.QueryString["t"]%>&s=" + s);
            }
            function HizliBitir() {

                Swal.fire({
                    text: "Demo ekranda bu özellik kullanılmıyor",
                    icon: 'warning',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Tamam',
                });

            }

            function Basla() {
                var AjaxJson = { OturumId: <%=Request.QueryString["t"]%> };
                $.ajax({
                    type: "POST",
                    url: "/CevrimiciSinav/DemoSinav.aspx/SinavaBasla",
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify(AjaxJson),
                    success: function (response) {
                        var data = $.parseJSON(response.d);
                        if (data.Sonuc === "ok") {
                            saat = data.KalanSaat;
                            dakika = data.KalanDakika;
                            saniye = data.KalanSaniye;
                            sayac = SureSay();

                            $("#ustBas").removeClass("hiddens");

                            $("#test").load("/CevrimiciSinav/DemoSinavGetir.aspx?t=<%=Request.QueryString["t"]%>&s=1");
                        } else {
                            Swal.fire({
                                icon: 'warning',
                                title: 'Hata!',
                                text: data.Mesaj,
                                showConfirmButton: true,
                                confirmButtonText: "Tamam"
                            });
                        }
                    },
                    eror: function (data) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Hata oluştu! ' + data.Mesaj,
                            text: data.Mesaj,
                            showConfirmButton: true,
                            confirmButtonText: "Tamam"
                        });
                    }
                });

            }
            function SoruGetir(s) {
                $("#ustBas").removeClass("hiddens");

                $("#test").load("/CevrimiciSinav/DemoSinavGetir.aspx?t=<%=Request.QueryString["t"]%>&s=" + s);

            }
        </script>

    </form>
</body>
</html>


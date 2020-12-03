<%@ page language="C#" autoeventwireup="true" inherits="Sinav_Sinav, okulsinavi" enableEventValidation="false" %>

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

                                <button type="button" onclick="HizliBitir()" data-tooltip="tooltip" title="Sınavı Bitir" class="btn btn-danger btn-sm float-right mr-1"><i class="fa fa-power-off mr-1"></i>Sınavı Bitir</button>
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
           </p> Süreniz <h3>
               <asp:Literal ID="ltrSure" runat="server"></asp:Literal></h3> dakika
                                   
                                    <p>Soru çözümlerini yapabilmek için kalem, silgi ve kağıt hazırlamayı unutmayın.</p>
                                    <p>Başla butonuna tıkladığınızda süreniz başlayacaktır. Hazırsanız başlayalım.</p>

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
        

        <script src="/CevrimiciSinav/Content/jquery/jquery.min.js"></script>
        <script src="/CevrimiciSinav/Content/bootstrap/js/bootstrap.bundle.min.js"></script>
        <script src="/CevrimiciSinav/Content/js/adminlte.min.js"></script>

        <script src="Content/sweetalert2/sweetalert2.min.js"></script>
        <script src="Content/ekko-lightbox/ekko-lightbox.min.js"></script>
        <script>

            $(document).on("contextmenu", function (e) {e.preventDefault();});
            function DersDegis(e) {

                //ders değişirken cevabı kaydedelim.
                var buSoru = $("#hfSoruNo").val();
                var cevap = $('input[name=customRadio]:checked', '#cevap').val();

                if (cevap !== "") {
                    var AjaxJsonCevapla = { OturumId: <%=Request.QueryString["t"]%>, Cevap: cevap, SoruNo: buSoru };

                    $.ajax({
                        type: "POST",
                        url: "/CevrimiciSinav/Sinav.aspx/Cevap",
                        dataType: "json",
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(AjaxJsonCevapla),
                        success: function () {
                        }
                    });
                }
                var AjaxJson = { OturumId: <%=Request.QueryString["t"]%>, BransId: e.value };
                $.ajax({
                    type: "POST",
                    url: "/CevrimiciSinav/Sinav.aspx/BransIlkSorusu",
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify(AjaxJson),
                    success: function (response) {
                        var data = $.parseJSON(response.d);
                        if (data.Sonuc === "ok") {
                            $("#test").load("/CevrimiciSinav/SinavGetir.aspx?t=<%=Request.QueryString["t"]%>&s=" + data.Mesaj);
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
            function Isaretle(e) {
                var soruNo = $("#hfSoruNo").val();
                $("#lbl" + e).removeClass("disabled");
                $("#lbl" + e).removeClass("bg-gray");
                $("#lbl" + e).addClass("bg-indigo");

                $("#soru_" + soruNo).removeClass("bg-gray disabled");
                $("#soru_" + soruNo).addClass("badge-success");

                if (e !== 'A') {
                    $("#lblA").addClass("disabled");
                    $("#lblA").addClass("bg-gray");
                    $("#lblA").removeClass("bg-indigo");
                }
                if (e !== 'B') {
                    $("#lblB").addClass("disabled");
                    $("#lblB").addClass("bg-gray");
                    $("#lblB").removeClass("bg-indigo");
                }
                if (e !== 'C') {
                    $("#lblC").addClass("disabled");
                    $("#lblC").addClass("bg-gray");
                    $("#lblC").removeClass("bg-indigo");
                }
                if (e !== 'D') {
                    $("#lblD").addClass("disabled");
                    $("#lblD").addClass("bg-gray");
                    $("#lblD").removeClass("bg-indigo");
                }
            }

            function Temizle(e) {

                $("#soru_" + e).addClass("badge-info");
                $("#soru_" + e).removeClass("badge-success");

                $("#lblA").addClass("bg-gray disabled");
                $("#lblB").addClass("bg-gray disabled");
                $("#lblC").addClass("bg-gray disabled");
                $("#lblD").addClass("bg-gray disabled");
                $("#lblA").removeClass("bg-indigo");
                $("#lblB").removeClass("bg-indigo");
                $("#lblC").removeClass("bg-indigo");
                $("#lblD").removeClass("bg-indigo");
                $('input[name="customRadio"]').prop('checked', false);
                SoruGetir(e);

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
                    DegerlendirAjax("sayac");
                }
            }
            //sayaç son

            function GeriDon(s) {
                $("#ustBas").removeClass("hiddens");
                $("#test").load("/CevrimiciSinav/SinavGetir.aspx?t=<%=Request.QueryString["t"]%>&s=" + s);
            }
            function HizliBitir() {

                Swal.fire({
                    text: "Sınavı bitirmek istediğinizden emin misiniz?",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Evet',
                    cancelButtonText: 'Hayır'
                }).then((result) => {
                    if (result.value) {
                        //bitirirken son cevabı değerlendirelim. es uyarısına burada gerek yok
                        var cevap = $('input[name=customRadio]:checked', '#cevap').val();

                        if (cevap !== undefined) {
                            var buSoru = $("#hfSoruNo").val();

                            var AjaxJson = { OturumId: <%=Request.QueryString["t"]%>, Cevap: cevap, SoruNo: buSoru };

                            $.ajax({
                                type: "POST",
                                url: "/CevrimiciSinav/Sinav.aspx/Cevap",
                                dataType: "json",
                                contentType: "application/json;charset=utf-8",
                                data: JSON.stringify(AjaxJson),
                                success: function () {
                                    $("#test").load("/CevrimiciSinav/SinavBitir.aspx?t=<%=Request.QueryString["t"]%>");
                                }
                            });
                        } else {

                            $("#test").load("/CevrimiciSinav/SinavBitir.aspx?t=<%=Request.QueryString["t"]%>");
                        }

                        $("#ustBas").addClass("hiddens");
                    }
                });

            }

            function Basla() {
                var AjaxJson = { OturumId: <%=Request.QueryString["t"]%> };
                $.ajax({
                    type: "POST",
                    url: "/CevrimiciSinav/Sinav.aspx/SinavaBasla",
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

                            $("#test").load("/CevrimiciSinav/SinavGetir.aspx?t=<%=Request.QueryString["t"]%>&s=1");
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

            function BosSoruGetir(s) {

                $("#ustBas").removeClass("hiddens");
                $("#test").load("/CevrimiciSinav/SinavGetir.aspx?t=<%=Request.QueryString["t"]%>&s=" + s);
            }

            function SoruGetir(s) {
                $("#ustBas").removeClass("hiddens");
                var cevap = $('input[name=customRadio]:checked', '#cevap').val();

                if (cevap === undefined) {
                    cevap = " ";
                }
                var buSoru = $("#hfSoruNo").val();
                var AjaxJson = { OturumId: <%=Request.QueryString["t"]%>, Cevap: cevap, SoruNo: buSoru };
                $.ajax({
                    type: "POST",
                    url: "/CevrimiciSinav/Sinav.aspx/Cevap",
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify(AjaxJson),
                    success: function (response) {
                        var data = $.parseJSON(response.d);

                        if (data.Sonuc === "ok") {
                            saat = data.KalanSaat;
                            dakika = data.KalanDakika;
                            saniye = data.KalanSaniye;

                            $("#test").load("/CevrimiciSinav/SinavGetir.aspx?t=<%=Request.QueryString["t"]%>&s=" + s);
                        }
                        else if (data.Sonuc === "es") {
                            Swal.fire({
                                position: 'top-end',
                                icon: 'info',
                                title: 'Cevap değerlendirilmeyecektir.',
                                showConfirmButton: false,
                                timer: 1500
                            });
                            $("#test").load("/CevrimiciSinav/SinavGetir.aspx?t=<%=Request.QueryString["t"]%>&s=" + s);
                        }
                        else {
                            Swal.fire({
                                icon: 'error',
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
                            title: 'Hata oluştu! ' + data.d,
                            text: data.Mesaj,
                            showConfirmButton: true,
                            confirmButtonText: "Tamam"
                        });
                    }
                });

            }
            function Degerlendir() {
                Swal.fire({
                    title: 'Uyarı',
                    text: "Sınavı bitirmek istediğinizden emin misiniz?",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Evet',
                    cancelButtonText: 'Hayır'
                }).then((result) => {
                    if (result.value) {

                        DegerlendirAjax("kullanici");
                    }
                });
            }
            function DegerlendirAjax(txt) {
                var AjaxJson = { OturumId: <%=Request.QueryString["t"]%>, bitiren:txt };
                $.ajax({
                    type: "POST",
                    url: "/CevrimiciSinav/Sinav.aspx/Bitir",
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify(AjaxJson),
                    success: function (response) {
                        var data = $.parseJSON(response.d);

                        if (data.Sonuc === "ok") {
                            Swal.fire({
                                icon: 'info',
                                title: 'Dikkat!',
                                text: data.Mesaj,
                                showConfirmButton: true,
                                confirmButtonText: "Tamam"
                            }).then((result) => {
                                if (result.value) {
                                    $("#test").load("/CevrimiciSinav/OturumGetir.aspx?t=<%=Request.QueryString["t"]%>");
                                }
                            });
                        }
                        else if (data.Sonuc === "ok2") {
                            window.location.href = "/CevrimiciSinav/SinavSonuc.aspx?t=" + <%=Request.QueryString["t"]%>;
                        }
                        else {
                            Swal.fire({
                                icon: 'error',
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
                            title: 'Hata oluştu! ' + data.d,
                            text: data.Mesaj,
                            showConfirmButton: true,
                            confirmButtonText: "Tamam"
                        });
                    }
                });
            }


        </script>

    </form>
</body>
</html>


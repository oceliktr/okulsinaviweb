<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SinavRapor.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim_SinavRapor" %>

<%@ Register TagPrefix="uc1" TagName="UstMenu" Src="~/Yonetim/UstMenu.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-12">
                        <h1 class="m-0 text-dark">Çevrim İçi Sınav Modülü   <small class="float-right">Eğitim Öğretim Yılı: <%=TestSeciliDonem.SeciliDonem().Donem%></small></h1>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-lg-12">
                    <uc1:UstMenu runat="server" ID="UstMenu" />
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12 mb-2">
                                    <h3 class="card-title" id="sinavAdi">Sınav seçiniz</h3>
                                    <asp:DropDownList ID="ddlSinavlar" CssClass="form-control col-md-6 float-right" ValidationGroup="form" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-3 col-6">
                                                <div class="small-box bg-danger">
                                                    <div class="inner">
                                                        <h3>Sınav</h3>

                                                        <p>Sonuçları</p>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-chart-line"></i>
                                                    </div>
                                                    <a href="#" class="small-box-footer" onclick="SinavSonuclari()">Detaylar <i class="fas fa-arrow-circle-right"></i></a>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-6">
                                                <div class="small-box bg-fuchsia">
                                                    <div class="inner">
                                                        <h3>Öğrenci</h3>

                                                        <p>Sıralama</p>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-user-check"></i>
                                                    </div>
                                                    <a href="#" class="small-box-footer" onclick="OgrenciSiralama()">Detaylar <i class="fas fa-arrow-circle-right"></i></a>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-6">
                                                <div class="small-box bg-blue">
                                                    <div class="inner">
                                                        <h3>Puanı</h3>

                                                        <p>Hesaplanmayanlar</p>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-user-clock"></i>
                                                    </div>
                                                    <a href="#" class="small-box-footer" onclick="PuaniHesaplanmayanlar()">Detaylar <i class="fas fa-arrow-circle-right"></i></a>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-6">
                                                <div class="small-box bg-teal">
                                                    <div class="inner">
                                                        <h3>Öğrenci</h3>

                                                        <p>Arama</p>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-search"></i>
                                                    </div>
                                                    <a href="_Rapor/OgrenciArama.aspx" class="small-box-footer">Detaylar <i class="fas fa-arrow-circle-right"></i></a>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-6">
                                                <div class="small-box bg-cyan">
                                                    <div class="inner">
                                                        <h3>Sınav</h3>

                                                        <p>İstatistik (anlık)</p>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-chart-pie"></i>
                                                    </div>
                                                    <a href="#" class="small-box-footer" onclick="SinavIstatistik()">Detaylar <i class="fas fa-arrow-circle-right"></i></a>
                                                </div>
                                            </div>

                                            <div class="col-lg-3 col-6">
                                                <div class="small-box bg-gradient-dark">
                                                    <div class="inner">
                                                        <h3>Öğrenci</h3>

                                                        <p>Logları</p>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-history"></i>
                                                    </div>
                                                    <a href="TestLoglar.aspx" class="small-box-footer">Detaylar <i class="fas fa-arrow-circle-right"></i></a>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-6">
                                                <div class="small-box bg-gradient-blue">
                                                    <div class="inner">
                                                        <h3>Öğrenci ve Şube</h3>

                                                        <p>Sayıları</p>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-users"></i>
                                                    </div>
                                                    <a href="OgrenciveSubeSayilari.aspx" class="small-box-footer">Detaylar <i class="fas fa-arrow-circle-right"></i></a>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-6">
                                                <div class="small-box bg-info">
                                                    <div class="inner">
                                                        <h3>Kütük</h3>

                                                        <p>Girmeyen Okullar</p>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-users"></i>
                                                    </div>
                                                    <a href="#" class="small-box-footer" data-toggle="modal" data-target="#modal-kutuk">Detaylar <i class="fas fa-arrow-circle-right"></i></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-3 col-6">
                                                <div class="small-box bg-info">
                                                    <div class="inner">
                                                        <h3>Okul</h3>

                                                        <p>Katılım Oranları</p>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-users"></i>
                                                    </div>
                                                    <a href="#" class="small-box-footer" onclick="KatilimOranlari()">Detaylar <i class="fas fa-arrow-circle-right"></i></a>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-6">
                                                <div class="small-box bg-danger">
                                                    <div class="inner">
                                                        <h3>Sınava</h3>

                                                        <p>Katılmayan Kurumlar</p>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-ban"></i>
                                                    </div>
                                                    <a href="#" class="small-box-footer" data-toggle="modal" data-target="#modal-rapor" data-mod="katilmayanlar">Detaylar <i class="fas fa-arrow-circle-right"></i></a>
                                                </div>
                                            </div>

                                            <div class="col-lg-3 col-6">
                                                <div class="small-box bg-lime">
                                                    <div class="inner">
                                                        <h3>Okul</h3>

                                                        <p>Puan Ortalamaları</p>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-chart-line"></i>
                                                    </div>
                                                    <a href="#" class="small-box-footer" data-toggle="modal" data-target="#modal-rapor" data-mod="okul-puan-ortalamalari">Detaylar <i class="fas fa-arrow-circle-right"></i></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-3 col-6">
                                                <div class="small-box bg-success">
                                                    <div class="inner">
                                                        <h3>İlçe</h3>

                                                        <p>Katılım Oranları</p>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-chart-pie"></i>
                                                    </div>
                                                    <a href="#" class="small-box-footer" data-toggle="modal" data-target="#modal-rapor" data-mod="ilce-katilim-oran">Detaylar <i class="fas fa-arrow-circle-right"></i></a>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-6">
                                                <div class="small-box bg-info">
                                                    <div class="inner">
                                                        <h3>İlçe</h3>

                                                        <p>Ortalamaları</p>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-chart-line"></i>
                                                    </div>
                                                    <a href="#" class="small-box-footer" data-toggle="modal" data-target="#modal-rapor" data-mod="ilce-ortalamalari">Detaylar <i class="fas fa-arrow-circle-right"></i></a>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-6">
                                                <div class="small-box bg-lime">
                                                    <div class="inner">
                                                        <h3>İlçe</h3>

                                                        <p>Puan Ortalamaları</p>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-chart-line"></i>
                                                    </div>
                                                    <a href="#" class="small-box-footer" data-toggle="modal" data-target="#modal-rapor" data-mod="ilce-puan-ortalamalari">Detaylar <i class="fas fa-arrow-circle-right"></i></a>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-6">
                                                <div class="small-box bg-cyan">
                                                    <div class="inner">
                                                        <h3>İlçe</h3>

                                                        <p>Puan Ortalamaları (100ve üzeri)</p>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fas fa-chart-line"></i>
                                                    </div>
                                                    <a href="#" class="small-box-footer" data-toggle="modal" data-target="#modal-rapor" data-mod="ilce-puan-ortalamalari-100">Detaylar <i class="fas fa-arrow-circle-right"></i></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div class="modal fade" id="modal-rapor">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Kapat</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modal-kutuk">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Kütük Girmeyen Okullar</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="col-md-12 text-right">
                        <div class="row">
                            <div class="col-md-4">&nbsp; </div>
                            <label for="kurum-turu" class="col-md-2 col-form-label">Sınıf:</label>
                            <select class="col-md-2 form-control" id="sinif">
                                <option value="">Seçiniz</option>
                                <option value="5">5. Sınıf</option>
                                <option value="6">6. Sınıf</option>
                                <option value="7">7. Sınıf</option>
                                <option value="8">8. Sınıf</option>
                                <option value="9">9. Sınıf</option>
                                <option value="10">10. Sınıf</option>
                                <option value="11">11. Sınıf</option>
                                <option value="12">12. Sınıf</option>
                            </select>
                            <label for="kurum-turu" class="col-md-2 col-form-label">Okul Türü:</label>
                            <select class="col-md-2 form-control" id="kurum-turu">
                                <option></option>
                            </select>
                        </div>
                    </div>
                    <div id="kutuk-table" class="mt-2"></div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Kapat</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.js"></script>
    <script>

        function Sinav() {
            $("#sinavAdi").text($("#ContentPlaceHolder1_ddlSinavlar option:selected").html());
        }
        function KatilimOranlari() {
            var sinavId = $("#ContentPlaceHolder1_ddlSinavlar option:selected").val();
            if (sinavId === "") {
                Uyari("error", "", "Öncelikle Sınav seçiniz.");
            } else {
                window.location.href = "_Rapor/KatilimOranlari.aspx?SinavId=" + sinavId;
            }
        }
        function SinavIstatistik() {
            var sinavId = $("#ContentPlaceHolder1_ddlSinavlar option:selected").val();
            if (sinavId === "") {
                Uyari("error", "", "Öncelikle Sınav seçiniz.");
            } else {
                window.location.href = "SinavIstatistik.aspx?SinavId=" + sinavId;
            }
        }
        function OgrenciSiralama() {
            var sinavId = $("#ContentPlaceHolder1_ddlSinavlar option:selected").val();
            if (sinavId === "") {
                Uyari("error", "", "Öncelikle Sınav seçiniz.");
            } else {
                window.location.href = "_Rapor/OgrenciSiralama.aspx?SinavId=" + sinavId;
            }
        }
        function PuaniHesaplanmayanlar() {
            var sinavId = $("#ContentPlaceHolder1_ddlSinavlar option:selected").val();
            if (sinavId === "") {
                Uyari("error", "", "Öncelikle Sınav seçiniz.");
            } else {
                window.location.href = "_Rapor/PuanHesaplanmayanlar.aspx?SinavId=" + sinavId;
            }
        }
        function SinavSonuclari() {
            var sinavId = $("#ContentPlaceHolder1_ddlSinavlar option:selected").val();
            if (sinavId === "") {
                Uyari("error", "", "Öncelikle Sınav seçiniz.");
            } else {
                window.location.href = "/Yonetim/SinavDetay.aspx?SinavId=" + sinavId;
            }
        }

        $('#modal-kutuk').on('show.bs.modal',
            function (event) {

                $.ajax({
                    type: "POST",
                    url: "/Kurumlar.aspx/KurumTurleri",
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",
                    success: function (response) {
                        var data = $.parseJSON(response.d);

                        $("#kurum-turu").find("option").remove();
                        $("#kurum-turu").append("<option value=''>Kurum Türü Seçiniz</option>");

                        $.each(data, function (key, val) {
                            $("#kurum-turu").append("<option value=" + val.Tur + ">" + val.Tur + "</option>");
                        });
                    },
                    error: function (data) {
                        Uyari("error", "Hata oluştu!", data.Mesaj);
                    }

                });
            });

        $("#kurum-turu").on('change', function () {
            var tur = $("#kurum-turu").val();
            var sinif = $("#sinif").val();
            KutukGirmeyenKurumlar(tur, sinif);
        });
        $("#sinif").on('change', function () {
            var tur = $("#kurum-turu").val();
            var sinif = $("#sinif").val();
            KutukGirmeyenKurumlar(tur, sinif);
        });
        function KutukGirmeyenKurumlar(tur,sinif) {
            if (tur !== "" && sinif !== "") {
                $.ajax({
                    type: "POST",
                    url: "/Yonetim/SinavRapor.aspx/KutukGirmeyenler",
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ tur, sinif }),
                    success: function (response) {
                        var data = $.parseJSON(response.d);

                        var html = "<table class='table table-hover'><thead class='thead-dark'><tr><th></th><th>İlçe Adı</th><th>Kurum Adı</th></tr></thead><tbody>";
                        for (var i = 0; i < data.length; i++) {
                            html += "<tr><td>" + (i + 1) + "</td><td>" + data[i].IlceAdi + "</td><td>" + data[i].KurumAdi + "</td></tr>";
                        }
                        html += "</tbody></table>";

                        $("#kutuk-table").html(html);
                    },
                    error: function (data) {
                        Uyari("error", "Hata oluştu!", data.Mesaj);
                    }

                });
            }
        }
        $('#modal-rapor').on('show.bs.modal',
            function (event) {
                var mod = $(event.relatedTarget).data('mod');

                var sinavId = $("#ContentPlaceHolder1_ddlSinavlar option:selected").val();
                if (sinavId === "") {
                    Swal.fire({
                        icon: "error", //warning, error, success, info, and question.
                        text: "Öncelikle Sınav seçiniz.",
                        showConfirmButton: true,
                        confirmButtonText: "Tamam"
                    }).then(() => {
                        $("#modal-rapor").modal('hide');
                    });
                } else {
                    if (mod === "ilce-katilim-oran") {
                        $("#modal-rapor .modal-title").text($("#ContentPlaceHolder1_ddlSinavlar option:selected").html());
                        $("#modal-rapor .modal-body").load("_Rapor/IlceKatilimOranlari.aspx?SinavId=" + sinavId);
                    }
                    if (mod === "katilmayanlar") {
                        $("#modal-rapor .modal-title").text("Sınava Katılmayan Okullar");

                        $("#modal-rapor .modal-body").load("_Rapor/SinavaKatilmayanlar.aspx?SinavId=" + sinavId);
                    }
                    if (mod === "okul-puan-ortalamalari") {
                        $("#modal-rapor .modal-title").text($("#ContentPlaceHolder1_ddlSinavlar option:selected").html() + " Okul Ortalamaları");
                        $("#modal-rapor .modal-body").load("_Rapor/OkulOrtalamalari.aspx?SinavId=" + sinavId);

                    }
                    if (mod === "ilce-puan-ortalamalari") {
                        $("#modal-rapor .modal-title").text($("#ContentPlaceHolder1_ddlSinavlar option:selected").html() + " İlçe Ortalamaları");
                        $("#modal-rapor .modal-body").load("_Rapor/IlceOrtalamalari.aspx?SinavId=" + sinavId);
                    }
                    if (mod === "ilce-puan-ortalamalari-100") {
                        $("#modal-rapor .modal-title").text($("#ContentPlaceHolder1_ddlSinavlar option:selected").html() + " İlçe Ortalamaları (100+ üzeri)");
                        $("#modal-rapor .modal-body").load("_Rapor/IlceOrtalamalari100uzeri.aspx?SinavId=" + sinavId);
                    }
                }
            });


        function Uyari(ico, baslik, txt) {
            Swal.fire({
                icon: ico, //warning, error, success, info, and question.
                title: baslik,
                text: txt,
                showConfirmButton: true,
                confirmButtonText: "Tamam"
            });
        }
    </script>
</asp:Content>



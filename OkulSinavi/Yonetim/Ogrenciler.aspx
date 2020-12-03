<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Ogrenciler.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim_Ogrenciler" %>


<%@ Register TagPrefix="uc1" TagName="UstMenu" Src="~/Yonetim/UstMenu.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlIlce" CssClass="form-control" runat="server" onchange="okullariGetir()">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlKurum" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                <div class="col-md-1">
                                    <asp:DropDownList ID="ddlSinif" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="">Sınıf Seçiniz</asp:ListItem>
                                        <asp:ListItem Value="5">5. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="6">6. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="7">7. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="8">8. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="9">9. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="10">10. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="11">11. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="12">12. Sınıf</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 mb-2">
                                    <button type="button" class="btn btn-danger" onclick="OgrencileriGetir()"><i class="fa fa-list mr-2"></i>Listele</button>
                                
                                    <button type="button" data-toggle="modal" data-tooltip="tooltip" title="Tc Kimlik (OpaqId) numarası sorgula" class="btn btn-warning" data-target="#tckimlik-modal"><i class="fa fa-search"></i></button>
                                    <button type="button" data-tooltip="tooltip" title="JSON tipinde verileri almak için sınıf seçiniz." class="btn btn-info" onclick="JsonCek('<%=TestSeciliDonem.SeciliDonem().Id%>')"><i class="fa fa-database"></i></button>
                                </div>
                                <div class="col-md-4 text-right mb-2">
                                    <a href="OgrenciKayit.aspx" class="btn btn-primary"><i class="fa fa-plus-circle mr-2"></i>Yeni Öğrenci</a>
                                </div>
                            </div>
                               
                            <div class="col-lg-12" id="ogrenciler">
                                <div class="row">
                                    <div class="col-md-12 mt-4">
                                        <p class="text-center">İlçe ve okulseçip listele butonuna tıklayınız.</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div class="modal fade" id="tckimlik-modal" tabindex="-1" role="dialog" aria-labelledby="tckimlik-modalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Öğrenci Arama</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <small>En az bir alana aradığınız öğrenciye ait bilgileri giriniz. Tc Kimlik için bire bir eşleşme gerekmektedir</small>
                        <div class="form-group">
                            <label for="tcKimlik" class="col-form-label">Tc Kimlik:</label>
                            <input type="text" class="form-control" id="tcKimlik"/>
                        </div>
                    <div class="form-group">
                        <label for="ogrenci-adi" class="col-form-label">Adı:</label>
                        <input type="text" class="form-control" id="ogrenci-adi"/>
                    </div>
                    <div class="form-group">
                        <label for="ogrenci-soyadi" class="col-form-label">Soyadı:</label>
                        <input type="text" class="form-control" id="ogrenci-soyadi"/>
                    </div>
                </div>
                <div class="modal-footer justify-content-sm-between">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
                    <button type="button" class="btn btn-warning" id="btn-ogrenci-bul"><i class="fa fa-search mr-2"></i>Bul</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.js"></script>
    <script>
        function JsonCek(v) {
            var sinif = $("#ContentPlaceHolder1_ddlSinif").val();
            var donem = v;
            if (sinif === "") {
                Uyari("warning", "", "Sınıf seçiniz");
                return;
            }
            window.open("/Kutuphaneler/KutukList.ashx?donem="+donem+"&sinif="+sinif);

        }
        $(function () {
            $('[data-tooltip="tooltip"]').tooltip();
        });
        $("#btn-ogrenci-bul").unbind().on("click", function() {
            var tcKimlik = $("#tcKimlik").val();
            var ogrAdi = $("#ogrenci-adi").val();
            var ogrSoyadi = $("#ogrenci-soyadi").val();

            if (tcKimlik === "" && ogrAdi === "" && ogrSoyadi === "") {
                Uyari("error", "Dikat!", "En az bir alana aradığınız öğrenciye ait bilgileri giriniz.");
                return;
            }
            $("#btn-ogrenci-bul").html('<i class="fa fa-refresh fa-spinner fa-spin mr-2"></i>&nbsp;Aranıyor');
            $.ajax({
                type: "POST",
                url: "/Yonetim/Ogrenciler.aspx/OgrenciAra",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify({ tcKimlik, ogrAdi, ogrSoyadi }),
                success: function (response) {
                    var data = $.parseJSON(response.d);
                    console.log(data.kutukInfo);
                    if (data.Sonuc==="ok") {
                        var i = 0;
                        var html = "<table class='table table-hover table-striped table-responsive-sm'><thead><tr><th></th><th>İlçe</th><th>Okulu</th><th>Adı</th><th>Soyadı</th><th>Sınıfı</th><th>Şubesi</th><th>İşlem</th></tr></thead><tbody>";

                        data.kutukInfo.forEach((item) => {
                            i++;
                            html += "<tr><td>" + i + "</td><td>" + item.IlceAdi + "</td><td>" + item.KurumAdi + "</td><td>" + item.Adi + "</td><td>" + item.Soyadi + "</td><td>" + item.Sinifi + "</td><td>" + item.Sube + "</td>" +
                                "<td><a class='btn btn-default btn-sm' href='OgrenciKayit.aspx?Id=" + item.Id + "'><i class='fa fa-edit'></i></a><a class='btn btn-default btn-sm' href='#' onclick=Sil('"+item.OpaqId + "')><i class='fa fa-trash-alt'></i></a></td></tr>";
                        });
                        html += "</tbody></table>";
                        $("#ogrenciler").html(html);
                        $("#tckimlik-modal").modal('hide');
                        $("#btn-ogrenci-bul").html('<i class="fa fa-search mr-2"></i>&nbsp;Bul');
                    } else {
                        Uyari("warning", "Kayıt yok", data.Mesaj);
                        $("#btn-ogrenci-bul").html('<i class="fa fa-search mr-2"></i>&nbsp;Bul');
                    }
                },
                eror: function (data) {
                    Uyari("error", "Hata oluştu!", data.Mesaj);
                }
            });
        });
        function okullariGetir() {
            var ilce =$("#ContentPlaceHolder1_ddlIlce").val();

            $.ajax({
                type: "POST",
                url: "/Kurumlar.aspx/OkullariListele",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify({ilce}),
                success: function (response) {
                    var data = $.parseJSON(response.d);
                    $("#ContentPlaceHolder1_ddlKurum").find('option').remove();
                    $("#ContentPlaceHolder1_ddlKurum").append('<option value="">Kurum Seçiniz</option>');
                    $.each(data,
                        function(key, val) {
                            $("#ContentPlaceHolder1_ddlKurum").append('<option value=' + val.KurumKodu + '>' + val.KurumAdi + '</option>');
                        });
                   
                },
                eror: function (data) {
                    Uyari("error", "Hata oluştu!", data.Mesaj);
                }
            });
        }
        function OgrencileriGetir() {
            var kurumKodu = $("#ContentPlaceHolder1_ddlKurum").val();
            var sinif = $("#ContentPlaceHolder1_ddlSinif").val();
            if (kurumKodu==="") {
                Uyari("warning", "", "Okul seçiniz");
            } else if (sinif === "") {
                Uyari("warning", "", "Sınıf seçiniz");
            } else {
                $("#ogrenciler").load("/Yonetim/_Ogrenciler.aspx?KurumKodu=" + kurumKodu + "&Sinif=" + sinif);
            }
        }
        function Sil(id) {
            var kurumKodu = $("#ContentPlaceHolder1_ddlKurum").val();
            var sinif = $("#ContentPlaceHolder1_ddlSinif").val();
            Swal.fire({
                title: 'Silmek istiyor musunuz?',
                text: "Öğrenciyi silmek istediğinizden emin misiniz? İlişkili kayıtları silinecektir.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet Sil',
                cancelButtonText: 'Vazgeç'
            }).then((result) => {
                if (result.value) {
                    var AjaxJson = { OgrencId: id };
                    $.ajax({
                        type: "POST",
                        url: "/Yonetim/Ogrenciler.aspx/Sil",
                        dataType: "json",
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(AjaxJson),
                        success: function (response) {
                            var data = $.parseJSON(response.d);
                            if (data.Sonuc) {
                                Uyari("success", "Silindi!", data.Mesaj);
                                $("#ogrenciler").load("/Yonetim/_Ogrenciler.aspx?KurumKodu=" + kurumKodu + "&Sinif=" + sinif);
                            } else {

                                Uyari("warning", "Silinemedi!", data.Mesaj);
                            }
                        },
                        eror: function (data) {
                            Uyari("error", "Hata oluştu!", data.Mesaj);
                        }
                    });
                }
            });
        }
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


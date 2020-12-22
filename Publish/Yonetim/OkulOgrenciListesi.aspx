<%@ page title="" language="C#" masterpagefile="MasterPage.master" autoeventwireup="true" inherits="Okul_SinaviYonetim_OkulOgrenciListesi, okulsinavi" enableEventValidation="false" %>

<%@ Register Src="~/Yonetim/UstMenu.ascx" TagPrefix="uc1" TagName="UstMenu" %>


<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">

        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-12">
                        <h1 class="m-0 text-dark">Çevrim İçi Sınav Modülü</h1>
                    </div>
                </div>
            </div>
        </div>

        <div class="content">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="col-lg-12">
                                <div class="row" id="okulYoneticiBilgi" runat="server">
                                    <div class="col-md-12">
                                        <div class="callout callout-info">
                                            <h5>Sayın Okul Yöneticisi!</h5>
                                            <p>
                                                Öğrencilerinizin Çevrim İçi Sınav uygulamasına katılabilmeleri için sınıf listesine eklenmesi gerekmektedir. Öğrencilerinizi eklemek için <strong>Yeni Öğrenci</strong> butonuna tıklayınız. <br/>
                                                Öğrencileriniz Çevrim İçi Sınav uygulamasına sınav giriş bilgileri ile gireceklerdir.
                                            </p>
                                            <p>
                                                Öğrencinizin sınavlardaki tüm hareketlerini görmek için öğrenci ismine tıklayınız. Sınav sonuçları için <strong>Sınavlar</strong> menüsünü kullanınız.
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <label>Öğrenci Listesi İçin Sınıf Seçiniz</label>
                                        <asp:DropDownList ID="ddlSinif" CssClass="form-control" runat="server">
                                            <asp:ListItem>Sınıf Seçiniz</asp:ListItem>
                                            <asp:ListItem Value="1">1. Sınıf</asp:ListItem>
                                            <asp:ListItem Value="2">2. Sınıf</asp:ListItem>
                                            <asp:ListItem Value="3">3. Sınıf</asp:ListItem>
                                            <asp:ListItem Value="4">4. Sınıf</asp:ListItem>
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
                                    <div class="col-md-10 text-right mb-2" id="okulYoneticiKayit" runat="server">
                                        <a href="OgrenciKayit.aspx" class="btn btn-primary"><i class="fa fa-plus-circle mr-2"></i>Yeni Öğrenci</a>
                                    </div>
                                </div>
                                <div id="ogrenciler"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.js"></script>
    <script>
        $("#ContentPlaceHolder1_ddlSinif").on("change",
            function () {
                var sinif = $("#ContentPlaceHolder1_ddlSinif").val();
                if (sinif === "") {
                    Uyari("warning", "", "Sınıf seçiniz");
                } else {
                    $("#ogrenciler").load("/Yonetim/_OgrencilerOkul.aspx?Sinif=" +
                        sinif);
                }
            });
        function Sil(id) {
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
                        url: "/Yonetim/OkulOgrenciListesi.aspx/Sil",
                        dataType: "json",
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(AjaxJson),
                        success: function (response) {
                            var data = $.parseJSON(response.d);
                            if (data.Sonuc === "ok") {
                                Uyari("success", "Silindi!", data.Mesaj);
                                console.log(sinif);
                                $("#ogrenciler").load("/Yonetim/_OgrencilerOkul.aspx?Sinif=" +sinif);
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


<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="SinavYonetim.aspx.cs" Inherits="Okul_SinaviYonetim_SinavYonetim" %>


<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.css" />
    <style>
        input[type=checkbox], input[type=radio] {
            margin-right: 10px;
            margin-top: 10px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-12">
                        <h1 class="m-0 text-dark">Sınav Yönetim</h1>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="col-md-6">
                                <ol class="breadcrumb float-sm-left">
                                    <li class="breadcrumb-item"><a href="#">Sınav Yönetimi</a></li>
                                </ol>
                            </div>
                            <div class="text-right mb-2">
                                <a href="SinavKayit.aspx" class="btn btn-primary"><i class="fa fa-plus-circle mr-2"></i>Yeni Sınav</a>
                            </div>

                            <div class="col-lg-12" id="sinavlar">
                            </div>
                            
                            <div class="col-12">
                                <div class="callout callout-info help-sinav mt-2">
                                    <h4>Sınav Hazırlarken!</h4>
                                    <p>
                                        Sınav hazırlarken sağ üstte Yeni Sınav butonundan sınav ekleyiniz. Daha sonra sınava oturum ekleyiniz. Sınav sorularını bu oturumlara ekleyiniz.<br/>
                                        Her sınavın en az bir oturumu olmalıdır. Sınava oturum eklemek için <i class="fa fa-plus"></i> butonuna tıklayınız. Oturumları görmek ve yönetmek için sınav adına tıklayınız.
                                    </p>
                                    <h4>Sınav Durumu</h4>

                                    <p>
                                     Çevrimiçi sınav ekranında öğrencilere   Sınav durumu "Aktif" olanlar gösterilmektedir. Sınav bittikten sonra öğrencilerin sınava giriş ekranında görmemesini istiyorsanız "Kapalı" duruma getiriniz. <br/>
                                        Kapalı sınavlar öğrencilere gösterilmez. Ancak sınav girmiş ise Sınavlarım bağlantısından sınav sonucunu görebilir.
                                    </p>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="detay">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"></h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.js"></script>
    <script>
        $(function () {
            $('[data-tooltip="title"]').tooltip();
            $("#sinavlar").load("/Yonetim/_Sinavlar.aspx");
        });
        $('#detay').on('show.bs.modal',
            function (event) {
                var aId = $(event.relatedTarget).data('id');

                $("#detay .modal-body").load("/Yonetim/_SinavDetay.aspx?Id="+aId);

            });
        function Sil(id) {
            Swal.fire({
                title: 'Silmek istiyor musunuz?',
                text: "Seçili sınavı silmek istediğinizden emin misiniz? Tüm sınav ve ilişkili kayıtlar silinecektir.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet Sil',
                cancelButtonText: 'Vazgeç'
            }).then((result) => {
                if (result.value) {
                    var AjaxJson = { SinavId: id };
                    $.ajax({
                        type: "POST",
                        url: "/Yonetim/SinavYonetim.aspx/SinavSil",
                        dataType: "json",
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(AjaxJson),
                        success: function (response) {
                            var data = $.parseJSON(response.d);
                            if (data.Sonuc) {
                                Uyari("success", "Silindi!", data.Mesaj);
                                $("#sinavlar").load("/Yonetim/_Sinavlar.aspx");
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


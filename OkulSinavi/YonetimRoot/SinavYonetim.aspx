<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="SinavYonetim.aspx.cs" Inherits="Okul_SinaviYonetim_SinavYonetimRoot" %>


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


                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-6">
                                        <ol class="breadcrumb float-sm-left">
                                            <li class="breadcrumb-item"><a href="#">Sınav Yönetimi</a></li>
                                        </ol>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2 mb-4">

                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlIlceler" ValidationGroup="form2" AutoPostBack="True" OnSelectedIndexChanged="ddlIlceler_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3" id="okullarlist" runat="server">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlKurumlar" ValidationGroup="form2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
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
                                    <div class="col-md-5 pull-right">
                                        <a href="SinavKayit.aspx" class="btn btn-primary"><i class="fa fa-plus-circle mr-2"></i>Yeni Sınav</a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12" id="sinavlar">
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
        });
        $("#ContentPlaceHolder1_ddlSinif").on("change",
            function () {
                var kurumkodu = $("#ContentPlaceHolder1_ddlKurumlar").val();
                var sinif = $("#ContentPlaceHolder1_ddlSinif").val();
                
                if (sinif === "") {
                    Uyari("warning", "", "Sınıf seçiniz");
                } else {
                    $("#sinavlar").load("/YonetimRoot/_Sinavlar.aspx?Sinif="+sinif+"&kurumkodu="+kurumkodu);
                }
            });

        $('#detay').on('show.bs.modal',
            function (event) {
                var aId = $(event.relatedTarget).data('id');

                $("#detay .modal-body").load("/YonetimRoot/_SinavDetay.aspx?Id=" + aId);

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
                        url: "/YonetimRoot/SinavYonetim.aspx/SinavSil",
                        dataType: "json",
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(AjaxJson),
                        success: function (response) {
                            var data = $.parseJSON(response.d);
                            if (data.Sonuc) {
                                Uyari("success", "Silindi!", data.Mesaj);
                                $("#sinavlar").load("/YonetimRoot/_Sinavlar.aspx");
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


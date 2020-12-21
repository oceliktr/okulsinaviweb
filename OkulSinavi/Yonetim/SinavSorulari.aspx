<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="SinavSorulari.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim_SinavSorulari" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-12">
                        <h1 class="m-0 text-dark">'<asp:Literal ID="ltrOturumAdi" runat="server"></asp:Literal>' Oturum Soruları</h1>
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
                                    <li class="breadcrumb-item"><a href="SinavYonetim.aspx">Sınav Yönetimi</a></li>
                                    <li class="breadcrumb-item">
                                        <asp:HyperLink ID="hlOturum" runat="server">Oturumlar</asp:HyperLink></li>
                                    <li class="breadcrumb-item active">Sorular</li>
                                </ol>
                            </div>
                            <div class="text-right mb-2">
                                <a href="SoruKayit.aspx?OturumId=<%=Request.QueryString["OturumId"]%>" class="btn btn-primary"><i class="fa fa-plus-circle mr-2"></i>Yeni Soru</a>
                            </div>
                            <div class="col-lg-12" id="sorular">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <div class="modal fade" id="soruModal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Soru</h4>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.js"></script>
    <script>

        $(function () {
            $("#sorular").load("/Yonetim/_Sorular.aspx?OturumId=" +<%=Request.QueryString["OturumId"]%>);
        });

        $('#soruModal').on('show.bs.modal',
            function (event) {
                var soruId = $(event.relatedTarget).data('id');

                $("#soruModal .modal-body").load("/Yonetim/_SoruGetir.aspx?Id=" + soruId);

            });

        function Sil(id) {
            Swal.fire({
                title: 'Silmek istiyor musunuz?',
                text: "Soruyu silmek istediğinizden emin misiniz?",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet Sil',
                cancelButtonText: 'Vazgeç'
            }).then((result) => {
                if (result.value) {
                    var AjaxJson = { SoruId: id };
                    $.ajax({
                        type: "POST",
                        url: "/Yonetim/SinavSorulari.aspx/SoruSil",
                        dataType: "json",
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(AjaxJson),
                        success: function (response) {
                            var data = $.parseJSON(response.d);
                            if (data.Sonuc) {
                                Uyari("success", "Silindi!", data.Mesaj);
                                $("#sorular").load("/Yonetim/_Sorular.aspx?OturumId=" +<%=Request.QueryString["OturumId"]%>);
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


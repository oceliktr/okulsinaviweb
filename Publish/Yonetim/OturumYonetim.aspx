<%@ page title="" language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="Okul_SinaviYonetim_OturumYonetim, okulsinavi" enableEventValidation="false" %>

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
                            <div class="col-md-6">
                                <ol class="breadcrumb float-sm-left">
                                    <li class="breadcrumb-item"><a href="SinavYonetim.aspx"><%=TestSeciliDonem.SeciliDonem().Donem%> Sınav Yönetimi</a></li>
                                    <li class="breadcrumb-item active">Oturumlar</li>
                                </ol>
                            </div>
                            <div class="text-right mb-2">
                                <a href="OturumKayit.aspx?SinavId=<%=Request.QueryString["SinavId"]%>" class="btn btn-primary"><i class="fa fa-plus-circle mr-2"></i>Yeni Oturum</a>
                            </div>
                            <div class="col-lg-12" id="oturumlar">
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
        $(function () {
            $("#oturumlar").load("/Yonetim/_Oturumlar.aspx?SinavId=" +<%=Request.QueryString["SinavId"]%>);
        });

        function Sil(id) {
            Swal.fire({
                title: 'Silmek istiyor musunuz?',
                text: "Seçili oturumu silmek istediğinizden emin misiniz? Tüm ilişkili kayıtlar silinecektir.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet Sil',
                cancelButtonText: 'Vazgeç'
            }).then((result) => {
                if (result.value) {
                    var AjaxJson = { OturumId: id };
                    $.ajax({
                        type: "POST",
                        url: "/Yonetim/OturumYonetim.aspx/OturumSil",
                        dataType: "json",
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(AjaxJson),
                        success: function (response) {
                            var data = $.parseJSON(response.d);
                            if (data.Sonuc) {
                                Uyari("success", "Silindi!", data.Mesaj);
                                $("#oturumlar").load("/Yonetim/_Oturumlar.aspx?SinavId=" +<%=Request.QueryString["SinavId"]%>);
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


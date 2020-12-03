<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OgrenciveSubeSayilari.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim_OgrenciveSubeSayilari" %>


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
                                <div class="col-md-12">
                                    <ol class="breadcrumb float-sm-left">
                                        <li class="breadcrumb-item"><a href="SinavRapor.aspx">Raporlar</a></li>
                                        <li class="breadcrumb-item active">Öğrenci ve Şube Sayıları</li>
                                    </ol>
                                </div>
                            </div>

                            <div class="row">
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlIlce" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                <div class="col-md-2">
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
                                    <button type="button" id="listele" class="btn btn-danger" onclick="OgrencileriGetir()"><i class="fa fa-search mr-2"></i>Listele</button>
                                </div>
                            </div>
                            <div class="col-lg-12">
                               
                                   
                                    <div id="table">
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div class="modal fade" id="ogr-islem">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"></h4>
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
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
    <script src="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.js"></script>
   <script>
       function OgrencileriGetir() {
           var ilce = $("#ContentPlaceHolder1_ddlIlce").val();
           var sinif = $("#ContentPlaceHolder1_ddlSinif").val();
           if (ilce === "") {
               Uyari("warning", "", "İlçe seçiniz");
           } else if (sinif === "") {
               Uyari("warning", "", "Sınıf seçiniz");
           } else {
               $("#listele").html('<i class="fa fa-refresh fa-spinner fa-spin mr-2"></i>&nbsp;Hesaplanıyor');
               $("#listele").attr("disabled", true);
               $.ajax({
                   type: "POST",
                   url: "/Yonetim/OgrenciveSubeSayilari.aspx/Sonuc",
                   dataType: "json",
                   contentType: "application/json;charset=utf-8",
                   data: JSON.stringify({ ilce,sinif }),
                   success: function (response) {
                       var data = $.parseJSON(response.d).list;
                       var toplamSubeSayisi = 0;
                       var toplamOgrenciSayisi = 0;
                       var html = "<table class='table table-hover'><thead class='thead-dark'><tr><th></th><th>İlçe Adı</th><th>Kurum Adı</th><th>Şube Sayısı</th><th>Öğrenci Sayısı</th></tr></thead><tbody>";
                       for (var i = 0; i < data.length; i++) {
                           toplamSubeSayisi += data[i].SubeSayisi;
                           toplamOgrenciSayisi += data[i].OgrenciSayisi;
                           html += "<tr><td>" + (i + 1) + "</td><td>" + data[i].IlceAdi + "</td><td>" + data[i].KurumAdi + "</td><td>" + data[i].SubeSayisi + "</td><td>" + data[i].OgrenciSayisi + "</td></tr>";
                       }
                       html += "</tbody><thead class='thead-light'><tr><th></th><th>TOPLAM</th><th></th><th>" + toplamSubeSayisi + "</th><th>" + toplamOgrenciSayisi+"</th></tr></thead></table>";

                       $("#table").html(html);

                       $("#listele").html('<i class="fa fa-search mr-2"></i>Listele');
                       $("#listele").attr("disabled", false);
                   },
                   eror: function (data) {
                       Uyari("error", "Hata oluştu!", data.Mesaj);
                       $("#listele").attr("disabled", false);
                   }
               });
           }
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



<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OgrenciTopluKayit.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim_OgrenciTopluKayit" %>

<%@ Register TagPrefix="uc1" TagName="UstMenu" Src="~/Yonetim/UstMenu.ascx" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.css" />
    <style>
        .alert a {
            color: #000;
            text-decoration: underline;
        }
    </style>
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
                    <div class="card card-default">
                        <div class="row" style="margin-bottom: 20px;">
                            <div class="col-md-12">
                                <uc1:UstMenu runat="server" ID="UstMenu" />
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="col-lg-8 offset-md-2">
                                <asp:HiddenField ID="hfId" runat="server" Value="0" />
                                <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                                <div class="card card-warning" runat="server" id="kayitForm">
                                    <div class="card-body">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <asp:PlaceHolder ID="phOkulIlce" runat="server">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label>
                                                                    İlçe
                                                                    <asp:RequiredFieldValidator ControlToValidate="ddlIlce" ValidationGroup="form" ID="RequiredFieldValidator4" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                                <asp:DropDownList ID="ddlIlce" ValidationGroup="form" class="form-control" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlIlce_OnSelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>
                                                                    Okulu
                                                                    <asp:RequiredFieldValidator ControlToValidate="ddlKurum" ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                                <asp:DropDownList ID="ddlKurum" ValidationGroup="form" class="form-control" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </asp:PlaceHolder>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label>Sınıf<asp:RequiredFieldValidator ControlToValidate="ddlSinif" ValidationGroup="form" ID="RequiredFieldValidator2" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                            <asp:DropDownList ID="ddlSinif" ValidationGroup="form" class="form-control" runat="server">
                                                                <asp:ListItem Value="">Sınıf Seçiniz</asp:ListItem>
                                                                <asp:ListItem Value="5">5. Sınıf</asp:ListItem>
                                                                <asp:ListItem Value="6">6. Sınıf</asp:ListItem>
                                                                <asp:ListItem Value="7">7. Sınıf</asp:ListItem>
                                                                <asp:ListItem Value="8" Selected="True">8. Sınıf</asp:ListItem>
                                                                <asp:ListItem Value="9">9. Sınıf</asp:ListItem>
                                                                <asp:ListItem Value="10">10. Sınıf</asp:ListItem>
                                                                <asp:ListItem Value="11">11. Sınıf</asp:ListItem>
                                                                <asp:ListItem Value="12">12. Sınıf</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div id="verigirisi">
                                                    <label>
                                                        Excelden öğrenci listesini kopyalayıp aşağıdaki alana yapıştırınız. Örnek izlemek için <a href="#" data-toggle="modal" data-target="#video">tıklayınız.</a>
                                                    </label>
                                                    <textarea id="txtOgrenciler" class="form-control" rows="7"></textarea><br />
                                                    <button id="btnKontrol" type="button" class="btn btn-danger" onclick="kontrolEt()">Kontrol Et</button>
                                                </div>
                                                <div id="table">
                                                </div>
                                                <button id="btnVazgec" type="button" class="btn btn-danger d-none float-left" onclick="vazgec()"><i class="fas fa-sync mr-1"></i>Tekrar Dene</button>
                                                <button id="btnTopluKayit" type="button" class="btn btn-primary d-none float-right" onclick="kaydet()"><i class="fa fa-save mr-1"></i>Kaydet</button>
                                            </div>
                                            <div class="col-md-12 mt-2" id="uyari">
                                                <div class="alert alert-warning alert-dismissible fade show" role="alert">
                                                    <strong>Nasıl!</strong><br/> Excel dosyasında öğrenci listenizi sırasıyla T.C. Kimlik No, Adı, Soyadı ve Şube sütunları olacak şekilde oluşturunuz. <br/>
                                                    <em>Adı ve soyadı alanlarının ayrı olması gerektiğine dikkat ediniz.</em><br/>
                                                    Yalnızca öğrencilerin  T.C. Kimlik No, Adı, Soyadı ve Şube bilgilerini seçiniz. Kopyalayıp yukarıdaki alana yapıştırınız.<br/>
                                                    Listede başlık alanı varsa <span style="text-decoration: underline;">seçmeyiniz</span>.<br/><br/>
                                                    Örnek izlemek için  <a href="#" data-toggle="modal" data-target="#video">tıklayınız</a>
                                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                    </button>
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
    <!--Modal: Name-->
    <div class="modal fade" id="video" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
<div class="modal-content">
                <div class="modal-body mb-0 p-0">

                    <div class="embed-responsive embed-responsive-16by9 z-depth-1-half">
                        
                        <video controls="controls" class="embed-responsive-item" autoplay>
                            <source src="toplu_kayit.mp4" type="video/mp4" />
                            Tarayıcınız video etiketini desteklemiyor.
                        </video>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--Modal: Name-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.js"></script>
    <script>
        var veri = "";
        $('#video').on('show.bs.modal',
            function () {
                $("video")[0].play();
            });
        $('#video').on('hide.bs.modal',
            function () {
                $("video")[0].pause();
            });

        $(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
        function sinifDegis() {
            $("#table").html("");
            vazgec();
        }
        function kontrolEt() {

            var ilce = $("#ContentPlaceHolder1_ddlIlce").val();
            var okul = $("#ContentPlaceHolder1_ddlKurum").val();

            veri = $("#txtOgrenciler").val();
            var sinifi = $("#ContentPlaceHolder1_ddlSinif").val();
            if (ilce === "") {
                Uyari("warning", "İlçe seçmediniz");
                return;
            }
            if (okul === "") {
                Uyari("warning", "Okul seçmediniz");
                return;
            }
            if (sinifi === "") {
                Uyari("warning", "Sınıf seçmediniz");
                return;
            }
            if (veri === "") {
                Uyari("warning", "Excelden öğrenci listesini kopyalayınız");

                return;
            }
            $.ajax({
                type: "POST",
                url: "/Yonetim/OgrenciTopluKayit.aspx/KayitKontrol",
                dataType: "json",
                data: JSON.stringify({ veri: veri }),
                contentType: "application/json;charset=utf-8",
                success: function (response) {
                   
                    var data = $.parseJSON(response.d);
                    if (data.Sonuc === "no") {
                        Uyari("warning", data.Mesaj);
                        return;
                    }

                    $("#verigirisi").addClass("d-none");
                    $("#uyari").addClass("d-none");
                    $("#btnTopluKayit").removeClass("d-none");
                    $("#btnVazgec").removeClass("d-none");
                    $("#btnTopluKayit").attr("disabled", false);
                    $("#btnTopluKayit").html(sinifi+". sınıf Listesine Kaydet");
                    var html = "<table class='table table-hover'><thead><tr><th></th><th>TC Kimlik</th><th>Adı</th><th>Soyadı</th><th>Sınıfı</th><th>Şubesi</th><th>Sonuç</th></tr></thead><tbody>";
                    for (var i = 0; i < data.length; i++) {

                        if (data[i].Sonuc !== "") {
                            $("#btnTopluKayit").attr("disabled", true);
                            html += "<tr class='table-danger'>";
                        } else {
                            html += "<tr>";
                        }

                        html += "<td>" + (i + 1) + "</td><td>" + data[i].TcKimlik + "</td><td>" + data[i].Adi + "</td><td>" + data[i].Soyadi + "</td><td>" + sinifi + "</td><td>" + data[i].Sube.toUpperCase() + "</td><td>" + data[i].Sonuc + "</td></tr>";
                    }
                    html += "</tbody></table>";

                    $("#table").html(html);
                    
                }
            });

        }

        function vazgec() {
            $("#table").html("");
            $("#verigirisi").removeClass("d-none");
            $("#uyari").removeClass("d-none");
            $("#btnTopluKayit").addClass("d-none");
            $("#btnVazgec").addClass("d-none");
        }
        function kaydet() {

            var sinifi = $("#ContentPlaceHolder1_ddlSinif").val();

            var ilce = $("#ContentPlaceHolder1_ddlIlce").val();
            var kurumkodu = $("#ContentPlaceHolder1_ddlKurum").val();

            ilce = ilce === undefined ? "" : ilce;
            kurumkodu = kurumkodu === undefined ? "" : kurumkodu;

            $.ajax({
                type: "POST",
                url: "/Yonetim/OgrenciTopluKayit.aspx/TopluKayit",
                dataType: "json",
                data: JSON.stringify({ sinif: sinifi, ilce: ilce, kurumkodu: kurumkodu, veri: veri }),
                contentType: "application/json;charset=utf-8",
                success: function (response) {
                    $("#verigirisi").addClass("d-none");
                    $("#uyari").addClass("d-none");
                    $("#btnTopluKayit").removeClass("d-none");

                    var data = $.parseJSON(response.d);
                    
                    if (data.Sonuc === "ok") {
                        $("#btnTopluKayit").attr("disabled", true);
                        $("#btnVazgec").addClass("d-none");
                        Uyari("success", data.Mesaj);
                        $("#table").html("<div class='alert alert-warning alert-dismissible fade show' role='alert'>"+data.Mesaj+"<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div>");
                    } else {
                        Uyari("error", data.Mesaj);
                        vazgec();
                    }
                }
            });

        }
        function Uyari(ico, txt) {
            Swal.fire({
                icon: ico, //warning, error, success, info, and question.
                text: txt,
                showConfirmButton: true,
                confirmButtonText: "Tamam"
            });
        }
    </script>

</asp:Content>


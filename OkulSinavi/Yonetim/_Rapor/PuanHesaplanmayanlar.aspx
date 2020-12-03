<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PuanHesaplanmayanlar.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim__Rapor_PuanHesaplanmayanlar" %>

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
                                        <li class="breadcrumb-item"><a href="../SinavRapor.aspx">Raporlar</a></li>
                                        <li class="breadcrumb-item active">Öğrenci Sıralaması</li>
                                    </ol>
                                </div>
                            </div>

                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header">
                                        <h3 class="card-title">
                                            <asp:Literal ID="ltrSinavAdi" runat="server"></asp:Literal>
                                            Sınavı Öğrenci Puanı Hesaplanmayanlar</h3>
                                        <div class="float-right">
                                            <button type="button" class="btn btn-success btn-sm" id="hesapla" <%=(Session["PuaniHesaplanmayanlar"].ToInt32()==0?"disabled":"") %>><i class="fa fa-calculator mr-1"></i>Hesapla</button>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                       
                                        
                                        <div class="progress-group" style="<%=(Session["PuaniHesaplanmayanlar"].ToInt32()==0?"display:none":"") %>">
                                            Puanı hesaplanmayan öğrencilerin puanlarını hesaplamak için hesapla butonuna tıklayınız.
                                            <span class="float-right"><span id="kalan"><b><%=Session["PuaniHesaplanmayanlar"] %></b></span>/<%=Session["PuaniHesaplanmayanlar"] %></span>
                                            <div class="progress" style="height: 25px">
                                                <div id="progress" class="progress-bar progress-bar-striped progress-bar-animated" style="width: 100%"></div>
                                            </div>
                                        </div>
                                        <div style="<%=(Session["PuaniHesaplanmayanlar"].ToInt32()==0?"":"display:none") %>">
                                            Puanı hesaplanacak öğrenci bulunamadı. 
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.js"></script>

    <script>
        var sinavId = getUrlVars()["SinavId"];
        var kalanSayi = <%=Session["PuaniHesaplanmayanlar"].ToInt32()%>;
        var maxSay = <%=Session["PuaniHesaplanmayanlar"].ToInt32()%>;
       
        var sayac = null;

        $("#hesapla").click(function () {
            if (sinavId === undefined||sinavId==="") {
                Swal.fire({
                    icon: "error", //warning, error, success, info, and question.
                    text: "Öncelikle Sınav seçiniz.",
                    showConfirmButton: true,
                    confirmButtonText: "Tamam"
                }).then(() => {
                    window.location.href = "/Yonetim/SinavRapor.aspx";
                });
            } else {
                $("#hesapla").attr("disabled", true);
                sayac = setInterval(() => {
                    Slider();
                }, 2000);
            }

        });

        function Slider() {
           
            var AjaxJson = { SinavId: sinavId };

            $.ajax({
                type: "POST",
                url: "/Yonetim/_Rapor/PuanHesaplanmayanlar.aspx/Hesapla",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(AjaxJson),
                success: function (response) {
                    var data = $.parseJSON(response.d);
                    if (data.Sonuc === "ok") {
                        kalanSayi -= data.EksilenSayi;
                        
                        if (kalanSayi <= 0) {
                            clearInterval(sayac);

                            $("#kalan").text(0);
                            $("#hesapla").attr("disabled", false);
                        } else {
                            $("#kalan").text(kalanSayi);
                        }
                        var yuzde = 100 / maxSay * kalanSayi;
                        $("#progress").css("width", yuzde + "%");

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

        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

    </script>
</asp:Content>


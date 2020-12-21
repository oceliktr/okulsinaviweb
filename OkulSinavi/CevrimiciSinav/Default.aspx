<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Sinav_Default" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Okul Sınavı</title>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <link rel="stylesheet" href="/CevrimiciSinav/Content/fontawesome-free/css/all.min.css" />
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <link rel="stylesheet" href="/CevrimiciSinav/Content/icheck-bootstrap/icheck-bootstrap.min.css" />
    <link rel="stylesheet" href="/CevrimiciSinav/Content/css/adminlte.min.css" />
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet" />
    <link rel="stylesheet" href="Content/sweetalert2/sweetalert2.min.css" />
    <!-- Global site tag (gtag.js) - Google Analytics -->
<script async src="https://www.googletagmanager.com/gtag/js?id=G-9HV0023N3K"></script>
<script>
  window.dataLayer = window.dataLayer || [];
  function gtag(){dataLayer.push(arguments);}
  gtag('js', new Date());

  gtag('config', 'G-9HV0023N3K');
</script>
    <style>
        html,body{
            height: 100%;
        }
    </style>
</head>
<body class="hold-transition">
    <form runat="server" id="form1">
        
               <div class="container w-100 h-100 mt-5">
                    <div class="row justify-content-center align-self-center mt-5">
                        <div class="col-md-6 mt-5">
                            <div class="card card-warning mt-5">
                                <div class="card-header">
                                    <h3 class="card-title">Öğrenci Girişi</h3>
                                </div>

                                <div class="card-body login-card-body" id="ogrGiris">
                                    <div class="input-group mb-3">
                                                <asp:TextBox ID="txtOpaq" Text="" CssClass="form-control" runat="server" placeholder="Sınav giriş bilgisi"></asp:TextBox>
                                                <div class="input-group-append">
                                                    <div class="input-group-text">
                                                        <span class="fas fa-lock"></span>
                                                    </div>
                                                </div>
                                            </div>
                                    <div class="row">
                                        <div class="col-8">
                                        </div>
                                        <div class="col-4">
                                            <button type="button" id="giris" class="btn btn-primary btn-block">Giriş</button>
                                        </div>
                                    </div>


                                </div>
                            </div>


                            <div class="card card-info">
                                <div class="card-header">
                                    <h3 class="card-title">Bilgilendirme</h3>
                                </div>
                                <div class="card-body login-card-body">
                                    <p><strong>Sınav Yardım</strong> Ekran görüntüsü için <a href="#" data-toggle="modal" data-target="#video">tıklayınız</a></p>
                                    <p>Verimli bir sınav için geniş ekranlı tablet veya bilgisayar kullanmanızı öneririz.</p>
                                    <p>Okulunuzdan size verilen bilgiler ile sınava giriş yapınız.</p>
                                    
                                </div>

                            </div>
                        </div>
                    </div>
                   <div class='row float-right'>
                       <small>
                           <a style="color: #c1c1c1;" href="/Yonetim">yönetim</a>

                       </small>
                   </div>
                </div>
           

        <div class="modal fade" id="video">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-body">
                        <img src="/upload/sinav-yardim-ekrani.jpg" class="img-fluid"/>
                        <p><a href="/upload/sinav-yardim-ekrani.jpg" target="_blank">Yeni pencerede aç</a></p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Kapat</button>
                    </div>
                </div>
            </div>
        </div>
        
    </form>
    <script src="/CevrimiciSinav/Content/jquery/jquery.min.js"></script>
    <script src="/CevrimiciSinav/Content/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="/CevrimiciSinav/Content/js/adminlte.min.js"></script>
    <script src="Content/sweetalert2/sweetalert2.min.js"></script>
    <script>
        
    

    $("#giris").click(function () {
        var opaqId = $("#txtOpaq").val();

        if (opaqId === "") {
            Swal.fire({
                icon: 'warning',
                title: 'Dikkat!',
                text: "Sınav giriş bilginizi giriniz.",
                showConfirmButton: true,
                confirmButtonText: "Tamam"
            });
        } else {
            var GirisAjax = {  OpaqId: opaqId };
            $.ajax({
                type: "POST",
                url: "/CevrimiciSinav/Default.aspx/OgrenciGiris",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(GirisAjax),
                success: function (data) {
                    if (data.d === "ok") {
                        window.location.href = "/CevrimiciSinav/Sinavlar.aspx";
                    } else if (data.d === "no1") {
                        Swal.fire({
                            icon: 'error',
                            title: 'Hata!',
                            text: "Şifreniz rakam olmalıdır.",
                            showConfirmButton: true,
                            confirmButtonText: "Tamam"
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Hata!',
                            text:
                                "Giriş bilgileriniz geçersiz. Giriş bilgilerinizi kontrol ediniz.",
                            showConfirmButton: true,
                            confirmButtonText: "Tamam"
                        });
                    }
                },
                eror: function (data) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata oluştu! ' + data.d,
                        text: data.Mesaj,
                        showConfirmButton: true,
                        confirmButtonText: "Tamam"
                    });
                }
            });
        }
    });

    </script>
</body>
</html>

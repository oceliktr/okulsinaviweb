<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pilot.aspx.cs" Inherits="CevrimiciSinav_Pilot" %>

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
</head>
<body class="hold-transition login-page">
    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="login-box">
            <div class="login-logo">
                <img src="/CevrimiciSinav/Content/images/logo.png" alt="Okul Sınavı" />
            </div>
            <div class="card">

                <div class="card-body login-card-body" id="ogrGiris">
                    <div class="input-group mb-3">
                        <asp:TextBox ID="txtOpaq" Text="" CssClass="form-control" runat="server" placeholder="Şifreniz"></asp:TextBox>
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

            <div class="alert alert-info alert-dismissible login-box">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                <h4><i class="icon fa fa-info"></i>Bilgi!</h4>
                Sınav öncesi tanıtım videosunu izlemek için <a href="#" data-toggle="modal" data-target="#video">tıklayınız</a><br />
                <br />
                Pilot uygulama için telefon numaranızı başında 0 olmadan 5xxxxxxxxx formatında 10 hane olarak giriniz.
            </div>
        </div>

        <div class="modal fade" id="video">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-body">
                        <video controls="controls" class="img-fluid">
                            <source src="/meb_iys_dosyalar/2020_04/07162718_cevrimicisinav_kapakli.mp4" type="video/mp4" />
                            Tarayıcınız video oynatmayı desteklemiyor.
                        </video>
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
        $('#video').on('show.bs.modal',
            function () {
                $("video")[0].play();
            });
        $('#video').on('hide.bs.modal',
            function () {
                $("video")[0].pause();
            });
        $(function () {
            //   $("#video").modal("show");
        });
        $("#giris").click(function () {
            var opaqId = $("#txtOpaq").val();

            if (opaqId === "") {
                Swal.fire({
                    icon: 'warning',
                    title: 'Dikkat!',
                    text: "Telefon numaranızı başında 0 olmadan 5xxxxxxxxx formatında 10 hane olarak giriniz",
                    showConfirmButton: true,
                    confirmButtonText: "Tamam"
                });
            } else {
                var GirisAjax = { OpaqId: opaqId };
                $.ajax({
                    type: "POST",
                    url: "/CevrimiciSinav/Pilot.aspx/OgrenciGiris",
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
                        } else if (data.d === "no2") {
                            Swal.fire({
                                icon: 'error',
                                title: 'Hata!',
                                text: "Şifreniz en az 8 rakam olmalıdır.",
                                showConfirmButton: true,
                                confirmButtonText: "Tamam"
                            });
                        }else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Hata!',
                                text:
                                    "Giriş bilgileriniz geçersiz. Şifrenizi bilmiyorsanız okulunuzdan öğreniniz.",
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

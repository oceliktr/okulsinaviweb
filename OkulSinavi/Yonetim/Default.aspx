<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="OkulSinavi.SpVeriDefault" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Okul Sınavı</title>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <link rel="stylesheet" href="/Content/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <link rel="stylesheet" href="/Content/dist/css/AdminLTE.min.css" />
    <link rel="stylesheet" href="/Content/plugins/iCheck/square/blue.css" />

    <!--[if lt IE 9]>
  <script src="/Content/js/html5shiv.min.js"></script>
  <script src="/Content/js/respond.min.js"></script>
<![endif]-->
</head>
<body class="hold-transition login-page">
    <form id="form1" runat="server">
        <div class="wrapper">
            <p class="mt-5">
                <br/>
            </p>
            

            <div runat="server" id="divHata" visible="false" class="alert alert-danger alert-dismissible login-box">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                <h4><i class="icon fa fa-ban"></i>Uyarı!</h4>
                <asp:Literal ID="ltrHata" runat="server"></asp:Literal>
            </div>
            <div class="login-box" id="divGiris" runat="server">
            <div class="container">
                <div class="col-12">
                   
                    <div class="row"> 
                        <div class="offset-sm-2 offset-md-3 col-md-4">
                            <div runat="server" class="alert alert-danger alert-dismissible hidden">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                <h4><i class="icon fa fa-info"></i>Bilgi!</h4>
                                <a href="/CevrimiciSinav/">Çevrim İçi Sınava giriş için tıklayınız</a><br/>
                          
                            </div>
                            <div class="login-box-body">
                                <p class="login-box-msg">Kullanıcı Girişi</p>
                                <div class="form-group has-feedback">
                                    <asp:TextBox ID="txtKullaniciAdi" CssClass="form-control" runat="server" placeholder="Kullanıcı Adı/Kurum Kodu"></asp:TextBox>
                                    <span class="glyphicon glyphicon-user form-control-feedback"></span>
                                </div>
                                <div class="form-group has-feedback">
                                    <asp:TextBox ID="txtSifre" runat="server" TextMode="Password" CssClass="form-control" placeholder="Şifre"></asp:TextBox>
                                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                                </div>
                                <div class="row">
                                    <div class="col-xs-8">
                                        <div class="checkbox icheck">
                                            <label>
                                                <asp:CheckBox ID="cbBeniHatirla" Checked="true" runat="server" Text="Beni Hatırla" />
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-xs-4">
                                        <asp:Button ID="btnGiris" runat="server" Text="Giriş" CssClass="btn btn-primary btn-block btn-flat" OnClick="btnGiris_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-4"></div>
                                    <div class="col-xs-8 pull-right">
                                        <asp:LinkButton ID="lnkSifremiUnuttum" CssClass="pull-right" runat="server" OnClick="lnkSifremiUnuttum_OnClick">Şifremi Unuttum</asp:LinkButton>
                                    </div>
                                </div>
                                <br />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
                
            </div>
            <div class="login-box" id="divsifreUnuttum" runat="server" visible="False">

                <div class="login-box-body">
                    <p class="login-box-msg">Şifremi Unuttum</p>
                    <div class="form-group has-feedback">
                        <asp:DropDownList ID="ddlIlce" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIlce_OnSelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="form-group has-feedback">
                        <asp:DropDownList ID="ddlKurumAdi" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="form-group has-feedback">
                        <asp:TextBox ID="txtKurumKodu" CssClass="form-control" runat="server" placeholder="Okulunuz kurum kodunu giriniz"></asp:TextBox>
                    </div>
                    <div class="form-group has-feedback">
                        <asp:TextBox ID="txtKullaniciAdi2" CssClass="form-control" runat="server" placeholder="Kullanıcı adını/Kurum kodunu giriniz"></asp:TextBox>
                    </div>
                    <div class="row">
                        <div class="col-xs-4">
                        </div>
                        <div class="col-xs-8">
                            <asp:Button ID="btnSifreUnuttum" runat="server" Text="Giriş" CssClass="btn btn-primary btn-block btn-flat" OnClick="btnSifreUnuttum_OnClick" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="login-box" id="divSifreDegis" runat="server" visible="False">

                <div class="login-box-body">
                    <p class="login-box-msg">Şifre Değiştir</p>
                    <div class="form-group has-feedback">
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:TextBox ID="txtYeniSifre" CssClass="form-control" TextMode="Password" runat="server" placeholder="Yeni Şifre"></asp:TextBox>
                        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                    </div>
                    <div class="form-group has-feedback">
                        <asp:TextBox ID="txtYeniSifre2" runat="server" TextMode="Password" CssClass="form-control" placeholder="Yeni Şifre Tekrar"></asp:TextBox>
                        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                    </div>
                    <div class="row">
                        <div class="col-xs-8">
                        </div>
                        <div class="col-xs-4">
                            <asp:Button ID="btnSifreDegis" runat="server" Text="Oluştur" CssClass="btn btn-primary btn-block btn-flat" OnClick="btnSifreDegis_OnClick" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="blockquote-reverse" style="height: 150px;"></div>
            <footer>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-solid">
                            <div class="box-body">
                                <div class="pull-right hidden-xs">
                                    Her hakkı saklıdır.
                                </div>
                                <p class="text-green">
                                    <strong>Copyright © 2020 </strong>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </footer>
        </div>
        <script src="/Content/plugins/jQuery/jQuery-2.1.4.min.js"></script>
        <script src="/Content/bootstrap/js/bootstrap.min.js"></script>
        <script src="/Content/plugins/iCheck/icheck.min.js"></script>
        <script>
            $(function () {
                $('input').iCheck({
                    checkboxClass: 'icheckbox_square-blue',
                    radioClass: 'iradio_square-blue',
                    increaseArea: '20%' // optional
                });
            });
        </script>
    </form>
</body>
</html>

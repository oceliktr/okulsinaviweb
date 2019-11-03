<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnketAdmin.aspx.cs" Inherits="AnketAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Erzurum Ölçme Değerlendirme Merkezi</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />

    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Mukta:300,400,700" />
    <link rel="stylesheet" href="anket/fonts/icomoon/style.css" />
    <link rel="stylesheet" href="anket/css/bootstrap.min.css" />
    <link rel="stylesheet" href="anket/css/magnific-popup.css" />
    <link rel="stylesheet" href="anket/css/jquery-ui.css" />
    <link rel="stylesheet" href="anket/css/owl.carousel.min.css" />
    <link rel="stylesheet" href="anket/css/owl.theme.default.min.css" />

    <style type="text/css">
        .g {
            background-color: crimson;
        }

        .t {
            background-color: lightgreen;
        }
    </style>
    <link rel="stylesheet" href="anket/css/aos.css" />

    <link rel="stylesheet" href="anket/css/style.css" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="site-wrap">
            <div class="site-section">
                <div class="container">
                    <asp:PlaceHolder ID="phLogin" runat="server">
                        <div class="row">
                            <div class="col-md-6">
                                <asp:PlaceHolder ID="phGirisError" runat="server" Visible="False">
                                    <div class="col-md-12 text-center">
                                        <span class="icon-error_outline display-3 text-success"></span>
                                        <p class="lead mb-5">Hatalı giriş</p>
                                    </div>
                                </asp:PlaceHolder>
                                <div class="p-3 p-lg-5 border">
                                    <div class="form-group row">
                                        <div class="col-md-3">
                                            <label class="text-black">
                                                Tc Kimlik : <span class="text-danger">
                                                    <asp:RequiredFieldValidator ControlToValidate="txtKurumKodu" ID="RequiredFieldValidator8" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></span></label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtKurumKodu" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnGiris" CssClass="btn btn-primary btn-lg btn-block" runat="server" Text="GİRİŞ" OnClick="btnGiris_OnClick" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:PlaceHolder>
                    <div class="row" id="anketFormu" runat="server" visible="False">
                        <div class="col-md-6">
                            <asp:Button CssClass="btn btn-success btn-xs" ID="btnExceleAktar" runat="server" Text="Excele Aktar" OnClick="btnExceleAktar_OnClick" />
                            </div>
                            <div class="col-md-6 text-right"><a class="btn btn-danger btn-xs" href="AnketAdmin.aspx?cikis=ok">[Çıkış]</a></div>

                        <div class="col-md-6">
                            <h4>Eksik Okullar</h4>
                            <table class="table site-block-order-table mb-5">
                                <thead>
                                    <tr>
                                        <th>SEÇ</th>
                                        <th>İLÇE ADI</th>
                                        <th>OKUL ADI</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptOkullar" OnItemDataBound="rptOkullar_OnItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td><a href="AnketAdmin.aspx?Id=<%#Eval("YENIKURUMKODU") %>" class="btn btn-primary btn-xs">Seç</a></td>
                                                <td>
                                                    <%#Eval("YENIILCE") %>
                                                </td>
                                                <td>
                                                    <%#Eval("YENIOKUL") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        <div class="col-md-6 ml-auto">
                            <div class="site-blocks-table">
                                <h4>Anket durumu "Girilmedi" olan öğrenciler</h4>
                                <table class="table site-block-order-table mb-5">
                                    <thead>
                                        <tr>
                                            <th>TC KİMLİK</th>
                                            <th>ADI SOYADI</th>
                                            <th>DURUM</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater runat="server" ID="rptList">
                                            <ItemTemplate>
                                                <tr class="<%# (string)Eval("ANKETDURUMU") == "Girilmedi"? "g" : "t" %>">
                                                    <td>
                                                        <%#Eval("TCKN") %>
                                                    </td>
                                                    <td>
                                                        <%#Eval("AD_SOYAD") %>
                                                    </td>
                                                    <td>
                                                        <%#Eval("ANKETDURUMU") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

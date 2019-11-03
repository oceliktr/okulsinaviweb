<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Anket.aspx.cs" Inherits="Anket" %>

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
           background-color: lightcoral;
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
                                        <p class="lead mb-5">Bilgi için: 0554 115 88 18 (Osman ÇELİK)</p>
                                    </div>
                                </asp:PlaceHolder>
                                <div class="p-3 p-lg-5 border">
                                    <div class="form-group row">
                                        <div class="col-md-3">
                                            <label class="text-black">
                                                Kurum Kodu: <span class="text-danger">
                                                    <asp:RequiredFieldValidator ControlToValidate="txtKurumKodu" ID="RequiredFieldValidator8" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></span></label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtKurumKodu" CssClass="form-control" runat="server"></asp:TextBox>
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
                        <div class="col-md-12">
                            <div class="pull-right text-right"><a href="Anket.aspx?cikis=ok">Çıkış</a></div>
                            <h2 class="h3 mb-3 text-black">ABİDE-4 2018 Anketi Eksik Olan Öğrenciler</h2>
                            1 - Bu bilgiler, yaşlarının küçük olması nedeni ile öğrencilerden sağlıklı bir şekilde edinilemeyebilir bu nedenle bilgilerin velilerden alınması sağlanmalıdır.
                            <br />
                            2 - Mesleğe ilişkin bilgiler girilirken memur, özel sektör, işçi, esnaf, işletme sahibi gibi genel ifadelerin yanında mesleğe ilişkin net tanımlar istenmelidir;<br />
                            Öğretmen, doktor, hemşire, sağlık görevlisi, hasta bakıcı, teknisyen, tekniker, mühendis, işportacı, lokanta sahibi, mübaşir, temizlikçi, büro elemanı, emekli, ev hanımı, savcı, hakim, subay, astsubay, okul müdürü, banka müdürü, özel sektörde idareci, satış elemanı, bankacı, halı dokumacı, gümüşçü, makine operatörü, taksici, güvenlik görevlisi, işsiz, emekli çalışıyor, polis, memur, duvar ustası, araştırma görevlisi, profesör, ayakkabı tamircisi, kuaför vb. gibi.
                        </div>
                        <div class="col-md-6">
                            <div class="p-3 p-lg-5 border">
                                <asp:PlaceHolder ID="phOkey" runat="server" Visible="False">
                                    <div class="col-md-12 text-center">
                                        <span class="icon-check_circle display-3 text-success"></span>
                                        <p class="lead mb-5">Bilgiler Kaydedildi.</p>
                                        <p class="lead mb-5">Anket Durumu "Girilmedi" bir öğreni seçiniz</p>
                                    </div>
                                </asp:PlaceHolder>
                                <div class="form-group row">
                                    <div class="col-md-6">
                                        <label class="text-black">
                                            Adı Soyadı :
                                            <asp:Literal ID="ltrAdiSoyadi" runat="server"></asp:Literal></label>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-md-12">
                                        <label for="c_email" class="text-black">
                                            Baba Eğitim Düzeyi: <span class="text-danger">
                                                <asp:RequiredFieldValidator ControlToValidate="ddlBabaEgitim" ID="RequiredFieldValidator2" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></span></label>
                                        <asp:DropDownList CssClass="form-control" ID="ddlBabaEgitim" runat="server">
                                            <asp:ListItem Value="">Seçiniz</asp:ListItem>
                                            <asp:ListItem Value="Okula hiç gitmedi ya da İlkokul terk">Okula hiç gitmedi ya da İlkokul terk</asp:ListItem>
                                            <asp:ListItem Value="İlkokul mezunu">İlkokul mezunu</asp:ListItem>
                                            <asp:ListItem Value="Ortaokul mezunu">Ortaokul mezunu</asp:ListItem>
                                            <asp:ListItem Value="İlköğretim mezunu">İlköğretim mezunu</asp:ListItem>
                                            <asp:ListItem Value="Lise mezunu">Lise mezunu</asp:ListItem>
                                            <asp:ListItem Value="Ön lisans mezunu (Yüksekokul, 2 yıllık)">Ön lisans mezunu (Yüksekokul, 2 yıllık)</asp:ListItem>
                                            <asp:ListItem Value="Eğitim enstitüsü mezunu (3 yıllık)">Eğitim enstitüsü mezunu (3 yıllık)</asp:ListItem>
                                            <asp:ListItem Value="Lisans mezunu">Lisans mezunu</asp:ListItem>
                                            <asp:ListItem Value="Yüksek lisans mezunu">Yüksek lisans mezunu</asp:ListItem>
                                            <asp:ListItem Value="Doktora mezunu">Doktora mezunu</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-md-12">
                                        <label class="text-black">
                                            Baba Hayatta Mı: <span class="text-danger">
                                                <asp:RequiredFieldValidator ControlToValidate="ddlBabaHayatta" ID="RequiredFieldValidator1" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></span></label>
                                        <asp:DropDownList CssClass="form-control" ID="ddlBabaHayatta" runat="server">
                                            <asp:ListItem Value="">Seçiniz</asp:ListItem>
                                            <asp:ListItem Value="Evet">Evet Hayatta</asp:ListItem>
                                            <asp:ListItem Value="Hayir">Vefat Etmiş</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-md-12">
                                        <label class="text-black">
                                            Baba Meslek: <span class="text-danger">
                                                <asp:RequiredFieldValidator ControlToValidate="txtBabaMeslek" ID="RequiredFieldValidator3" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></span></label>
                                        <asp:TextBox ID="txtBabaMeslek" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-md-12">
                                        <label class="text-black">
                                            Anne Eğitim Düzeyi: <span class="text-danger">
                                                <asp:RequiredFieldValidator ControlToValidate="ddlAnneEgitimDuzeyi" ID="RequiredFieldValidator4" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></span></label>
                                        <asp:DropDownList CssClass="form-control" ID="ddlAnneEgitimDuzeyi" runat="server">
                                            <asp:ListItem Value="">Seçiniz</asp:ListItem>
                                            <asp:ListItem Value="Okula hiç gitmedi ya da İlkokul terk">Okula hiç gitmedi ya da İlkokul terk</asp:ListItem>
                                            <asp:ListItem Value="İlkokul mezunu">İlkokul mezunu</asp:ListItem>
                                            <asp:ListItem Value="Ortaokul mezunu">Ortaokul mezunu</asp:ListItem>
                                            <asp:ListItem Value="İlköğretim mezunu">İlköğretim mezunu</asp:ListItem>
                                            <asp:ListItem Value="Lise mezunu">Lise mezunu</asp:ListItem>
                                            <asp:ListItem Value="Ön lisans mezunu (Yüksekokul, 2 yıllık)">Ön lisans mezunu (Yüksekokul, 2 yıllık)</asp:ListItem>
                                            <asp:ListItem Value="Eğitim enstitüsü mezunu (3 yıllık)">Eğitim enstitüsü mezunu (3 yıllık)</asp:ListItem>
                                            <asp:ListItem Value="Lisans mezunu">Lisans mezunu</asp:ListItem>
                                            <asp:ListItem Value="Yüksek lisans mezunu">Yüksek lisans mezunu</asp:ListItem>
                                            <asp:ListItem Value="Doktora mezunu">Doktora mezunu</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-md-12">
                                        <label class="text-black">
                                            Anne Hayatta Mı: <span class="text-danger">
                                                <asp:RequiredFieldValidator ControlToValidate="ddlAnneHayatta" ID="RequiredFieldValidator5" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></span></label>
                                        <asp:DropDownList ID="ddlAnneHayatta" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="">Seçiniz</asp:ListItem>
                                            <asp:ListItem Value="Evet">Evet Hayatta</asp:ListItem>
                                            <asp:ListItem Value="Hayir">Vefat Etmiş</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-md-12">
                                        <label class="text-black">
                                            Anne Meslek: <span class="text-danger">
                                                <asp:RequiredFieldValidator ControlToValidate="txtAnneMeslek" ID="RequiredFieldValidator6" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></span></label>
                                        <asp:TextBox ID="txtAnneMeslek" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-md-12">
                                        <label class="text-black">
                                            Aile Gelir Durumu (Aylık): <span class="text-danger">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                    ControlToValidate="txtAileGelir"
                                                    ValidationExpression="\d+"
                                                    Display="Static"
                                                    EnableClientScript="true" Text="*"
                                                    ErrorMessage="*"
                                                    runat="server" /><asp:RequiredFieldValidator ControlToValidate="txtAileGelir" ID="RequiredFieldValidator7" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></span></label>
                                        <asp:TextBox ID="txtAileGelir" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-12">
                                        <asp:Button ID="btnKaydet" CssClass="btn btn-primary btn-lg btn-block" runat="server" Text="KAYDET" OnClick="btnKaydet_OnClick" />
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-md-6 ml-auto">

                            <div class="site-blocks-table">
                                <table class="table site-block-order-table mb-5">
                                    <thead>
                                        <tr>
                                            <th>SEÇ</th>
                                            <th>TC KİMLİK</th>
                                            <th>ADI SOYADI</th>
                                            <th>DURUM</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater runat="server" ID="rptList" OnItemDataBound="rptList_OnItemDataBound">
                                            <ItemTemplate>
                                                <tr class="<%# (string)Eval("ANKETDURUMU") == "Girilmedi"? "g" : "t" %>">
                                                    <td><a href="Anket.aspx?Id=<%#Eval("Id") %>" class="btn btn-primary btn-xs">Seç</a></td>
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

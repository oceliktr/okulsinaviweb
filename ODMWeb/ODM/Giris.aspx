<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Giris.aspx.cs" Inherits="ODM.AdminGiris" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Erzurum Ölçeme Değerlendirme Merkezi
        <small>
            <asp:Literal ID="ltrDonemAdi" runat="server"></asp:Literal></small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-lg-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="box box-solid box-default">
                        <div class="box-body">
                            <div class="col-lg-6">
                                <div class="box box-solid box-default">
                                    <div class="box-header with-border">
                                        <i class="fa fa-plus-square"></i>
                                        <h3 class="box-title">Erzurum Ölçeme Değerlendirme Merkezi</h3>
                                    </div>
                                    <div class="box-body">
                                        <p>
                                            <em><strong>
                                                <asp:Literal ID="ltrKullaniciAdi" runat="server"></asp:Literal>,</strong></em>
                                        </p>
                                        <p>Erzurum Ölçme Değerlendirme Merkezi aracılığıyla yapılan sınav işlemlerinin yürütüleceği modüle hoş geldiniz.</p>
                                        <p>Bu modül aracılığıyla aşağıdaki listelenen başlıklarda yetkiniz bulunmaktadır.</p>
                                        <p><strong>Yetkileriniz:</strong></p>

                                        <ul>
                                            <asp:Literal ID="ltrYetkiler" runat="server"></asp:Literal>
                                        </ul>
                                        <p><em>İyi çalışmalar dileriz...</em></p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="box box-solid box-default">
                                    <div class="box-header with-border">
                                        <i class="fa fa-bar-chart-o"></i>
                                        <h3 class="box-title">Rapor</h3>
                                    </div>
                                    <div class="box-body">

                                        <div class="col-xs-6 col-md-4 text-center">
                                            <asp:TextBox ID="txtAGrubu" CssClass="knob" Text="30" runat="server" data-width="90" data-height="90" data-fgColor="#3c8dbc" data-readonly="true"></asp:TextBox>
                                            <div class="knob-label">
                                                <asp:Literal ID="ltrAGrup" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                        <div class="col-xs-6 col-md-4 text-center">
                                            <asp:TextBox ID="txtBGrubu" CssClass="knob" Text="30" runat="server" data-width="90" data-height="90" data-fgColor="#3c8dbc" data-readonly="true"></asp:TextBox>
                                            <div class="knob-label">
                                                <asp:Literal ID="ltrBGrup" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                        <div class="col-xs-6 col-md-4 text-center">
                                            <asp:TextBox ID="txtUstP" CssClass="knob" Text="130" runat="server" data-width="90" data-height="90" data-fgColor="#3c8dbc" data-readonly="true"></asp:TextBox>
                                            <div class="knob-label">
                                                <asp:Literal ID="ltrUstP" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="box box-solid box-default">
                                    <div class="box-header with-border">
                                        <i class="fa fa-phone"></i>
                                        <h3 class="box-title">Ekip Üyeleri İletişim Bilgileri</h3>
                                    </div>
                                    <div class="box-body">
                                        <p><strong>ÖLÇME DEĞERLENDİRME MERKEZİ</strong></p>
                                        <p><strong>Adres : </strong> Murat Paşa Mahallesi Erzincan Kapı Caddesi No 73 Yakutiye ERZURUM (Yakutiye Rehberlik ve Araştırma Merkezi Binası)</p>
                                        <p><strong>Tlf :</strong></p>
                                        <p><strong>E-posta:</strong> odm25@meb.gov.tr</p>
                                        <p><strong>EKİP ÜYELERİ İLETİŞİM BİLGİLERİ</strong></p>
                                        <table id="example2" class="table table-bordered table-hover dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr role="row">
                                                    <th>İsim</th>
                                                    <th>E-Mail</th>
                                                    <th>Telefon</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr role="row" class="odd">
                                                    <td>Abdulkerim Adıgüzel</td>
                                                    <td>kerimadiguzel25@gmail.com</td>
                                                    <td>+90 506 587 16 81</td>
                                                </tr>
                                                <tr role="row" class="even">
                                                    <td>Abdusselam Koçak</td>
                                                    <td>aselam_25@hotmail.com</td>
                                                    <td>+90 535 888 67 90</td>
                                                </tr>
                                                <tr role="row" class="odd">
                                                    <td>M. Said Adıyaman</td>
                                                    <td>msaitadiyaman@gmail.com</td>
                                                    <td>+90 536 273 5187	</td>
                                                </tr>
                                                <tr role="row" class="even">
                                                    <td>Murat Ömer Yeşiloğlu</td>
                                                    <td>muratomeryesiloglu@hotmail.com</td>
                                                    <td>+90 543 268 8597</td>
                                                </tr>
                                                <tr role="row" class="odd">
                                                    <td>Mustafa Uğurlu	</td>
                                                    <td>m.ugurlu87@hotmail.com</td>
                                                    <td>+90 536 667 5602	</td>
                                                </tr>
                                                <tr role="row" class="even">
                                                    <td>Osman Çelik	</td>
                                                    <td>osmanceliktr@gmail.com</td>
                                                    <td>+90 554 115 8818	</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="footer" ID="con2">
    <script src="plugins/knob/jquery.knob.js"></script>
    <!-- Sparkline -->
    <script src="plugins/sparkline/jquery.sparkline.min.js"></script>
    <!-- page script -->
    <script>
        $(function () {
            /* jQueryKnob */

            $(".knob").knob({

                draw: function () {

                    // "tron" case
                    if (this.$.data('skin') == 'tron') {

                        var a = this.angle(this.cv)  // Angle
                            , sa = this.startAngle          // Previous start angle
                            , sat = this.startAngle         // Start angle
                            , ea                            // Previous end angle
                            , eat = sat + a                 // End angle
                            , r = true;

                        this.g.lineWidth = this.lineWidth;

                        this.o.cursor
                        && (sat = eat - 0.3)
                        && (eat = eat + 0.3);

                        if (this.o.displayPrevious) {
                            ea = this.startAngle + this.angle(this.value);
                            this.o.cursor
                            && (sa = ea - 0.3)
                            && (ea = ea + 0.3);
                            this.g.beginPath();
                            this.g.strokeStyle = this.previousColor;
                            this.g.arc(this.xy, this.xy, this.radius - this.lineWidth, sa, ea, false);
                            this.g.stroke();
                        }

                        this.g.beginPath();
                        this.g.strokeStyle = r ? this.o.fgColor : this.fgColor;
                        this.g.arc(this.xy, this.xy, this.radius - this.lineWidth, sat, eat, false);
                        this.g.stroke();

                        this.g.lineWidth = 2;
                        this.g.beginPath();
                        this.g.strokeStyle = this.o.fgColor;
                        this.g.arc(this.xy, this.xy, this.radius - this.lineWidth + 1 + this.lineWidth * 2 / 3, 0, 2 * Math.PI, false);
                        this.g.stroke();

                        return false;
                    }
                }
            });
            /* END JQUERY KNOB */

        });

    </script>
</asp:Content>

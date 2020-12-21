<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Giris.aspx.cs" Inherits="OkulSinavi.AdminGirisRoot" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">

        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Okul Sınavı</h1>
                    </div>
                </div>
            </div>
        </div>


        <div class="content">
            <div class="row">

                <div class="col-md-8">
                    <div class="card card-success card-outline">
                        <div class="card-header with-border">
                            <h3 class="card-title">Okul Sınavı</h3>
                        </div>
                        <div class="card-body">
                            <em><strong>
                                <asp:Literal ID="ltrKullaniciAdi" runat="server"></asp:Literal>,</strong></em>

                            <p>
                                Hoş geldiniz.<br />
                                Bu modül aracılığıyla aşağıdaki listelenen başlıklarda yetkiniz bulunmaktadır.
                            </p>
                            <p><strong>Yetkileriniz:</strong></p>
                            <ul>
                                <asp:Literal ID="ltrYetkiler" runat="server"></asp:Literal>
                            </ul>
                            <p><em>İyi çalışmalar dileriz...</em></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="footer" ID="con2">
    <script src="/Content/plugins/knob/jquery.knob.js"></script>
    <!-- Sparkline -->
    <script src="/Content/plugins/sparkline/jquery.sparkline.min.js"></script>
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

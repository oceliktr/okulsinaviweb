<%@ page title="" language="C#" masterpagefile="MasterPage.master" autoeventwireup="true" inherits="OkulSinavi_CevrimiciSinavYonetim_OturumKayit, okulsinavi" enableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/CevrimiciSinav/Content/bootstrap-datepicker/css/bootstrap-datepicker.min.css" />
    <link rel="stylesheet" href="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.css" />
</asp:Content>
<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-12">
                        <h1 class="m-0 text-dark">Oturum Kayıt</h1>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card card-default">
                        <div class="card-body">

                            <div class="row">
                                <div class="col-md-6">
                                    <ol class="breadcrumb float-sm-left">
                                        <li class="breadcrumb-item"><a href="SinavYonetim.aspx">Sınav Yönetimi</a></li>
                                        <li class="breadcrumb-item"><a href="OturumYonetim.aspx?SinavId=<%=Request.QueryString["SinavId"] %>">Oturum Yönetimi</a></li>
                                        <li class="breadcrumb-item active">Oturum Kayıt Formu</li>
                                    </ol>
                                </div>
                            </div>
                            <div class="col-md-12 col-lg-8">
                                <asp:HiddenField ID="hfSinavId" runat="server" Value="0" />
                                <asp:HiddenField ID="hfId" runat="server" Value="0" />
                                <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                                <div class="card card-warning">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Sınav Adı </label>
                                                    <asp:TextBox ID="txtSinavAdi" Enabled="False" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Oturum No <asp:RequiredFieldValidator ControlToValidate="txtSiraNo" ValidationGroup="form" ID="RequiredFieldValidator4" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtSiraNo" TextMode="Number" ValidationGroup="form" CssClass="form-control" runat="server" placeholder="Oturum numarasını giriniz"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label>Oturum Adı
                                                        <asp:RequiredFieldValidator ControlToValidate="txtOturmAdi" ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtOturmAdi" ValidationGroup="form" CssClass="form-control" runat="server" placeholder="Oturum adını giriniz"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                      
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Açıklama</label>
                                                    <asp:TextBox ID="txtAciklama" ValidationGroup="form" TextMode="MultiLine" runat="server" CssClass="form-control" placeholder="Sınavın içeriği hakkında bilgi giriniz..."></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Sınav Süresi (dakika)
                                                        <asp:RequiredFieldValidator ControlToValidate="txtSure" ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtSure" ValidationGroup="form" CssClass="form-control" runat="server" TextMode="Number" placeholder="Dakika olarak sınav süresini giriniz"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Sınav Tarihi
                                                        <asp:RequiredFieldValidator ControlToValidate="txtTarih" ValidationGroup="form" ID="RequiredFieldValidator2" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtTarih" ValidationGroup="form" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>İlk Giriş Saati</label>
                                                    <asp:TextBox ID="txtBaslangicSaati" ValidationGroup="form" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Son Giriş Saati</label>
                                                    <asp:TextBox ID="txtBitisSaati" ValidationGroup="form" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card-footer">
                                        <a href="OturumYonetim.aspx?SinavId=<%=Request.QueryString["SinavId"] %>" class="btn btn-secondary btn-flat"><i class="fas fa-chevron-circle-left mr-1"></i>Oturumlar</a>
                                        <asp:Button ID="btnKaydet" CssClass="btn btn-primary float-right d-none" runat="server" ValidationGroup="form" Text="Kaydet" OnClick="btnKaydet_OnClick" />
                                        <button id="btnsave" class="btn btn-primary float-right" onclick="oturumKayit();return false;"><i class="fa fa-save mr-1"></i>Kaydet</button>
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
    <script src="/CevrimiciSinav/Content/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script src="/CevrimiciSinav/Content/bootstrap-datepicker/locales/bootstrap-datepicker.tr.min.js"></script>
    <script src="/CevrimiciSinav/Content/input-mask/jquery.inputmask.bundle.js"></script>
    <script src="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.js"></script>
    <script>
        function oturumKayit() {
            var baslamaSaati = $("#<%=txtBaslangicSaati.ClientID %>").val().replace(":", "");
            var bitisSaati = $("#<%=txtBitisSaati.ClientID %>").val().replace(":", "");
            if (baslamaSaati===""||bitisSaati==="") {
                Uyari("warning", "Oturum başlangıç ve bitiş saatlerini giriniz");
            } else {
                var baslama = Math.floor(new Date(baslamaSaati).getTime() / 1000);
                var bitis = Math.floor(new Date(bitisSaati).getTime() / 1000);

                if (baslama > bitis)
                    Uyari("warning", "Başlama saati bitiş saatinden büyük olamaz.");
                else {
                    $("#<%=btnKaydet.ClientID %>").click();
                }
            }
        }
        $(function () {
            $('#<%=txtTarih.ClientID %>').datepicker({
                todayBtn: "linked",
                language: "tr",
                daysOfWeekHighlighted: "0,6",
                autoclose: true,
                todayHighlight: true,
                toggleActive: true
            });
            $('input[id$="ContentPlaceHolder1_txtBaslangicSaati"]').inputmask(
                "hh:mm", {
                    placeholder: "00:00",
                    insertMode: false,
                    showMaskOnHover: false
                }
            );
            $('input[id$="ContentPlaceHolder1_txtBitisSaati"]').inputmask(
                "hh:mm", {
                    placeholder: "00:00",
                    insertMode: false,
                    showMaskOnHover: false
                }
            );
            //$('#ContentPlaceHolder1_txtTarih').daterangepicker({
            //    timePicker: true,
            //    timePicker24Hour: true,
            //    showDropdowns: true,
            //    minYear: 2000,
            //    maxYear: 2050,
            //    "autoApply": true,
            //    "opens": "center",
            //    "drops": "up",
            //    locale: {
            //        format: 'DD.MM.YYYY H:mm',
            //        "timePicker24Hour": true,
            //        cancelLabel: 'Vazgeç',
            //        applyLabel: 'Tamam',
            //        "daysOfWeek": ["Pz", "Pt", "Sa", "Çr", "Pr", "Cu", "Ct"],
            //        "monthNames": ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"]
            //    }
            //});
            //$('#ContentPlaceHolder1_txtTarih').on('hide.daterangepicker', function (ev, picker) {
            //    console.log(ev);
            //    console.log("-");
            //    console.log(picker);
            //});
        });
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

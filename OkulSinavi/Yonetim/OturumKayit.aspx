<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OturumKayit.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim_OturumKayit" %>

<%@ Register TagPrefix="uc1" TagName="UstMenu" Src="~/Yonetim/UstMenu.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/Content/plugins/daterangepicker/daterangepicker.css" />
</asp:Content>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
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
                    <div class="card card-default">
                        <div class="row" style="margin-bottom: 20px;">
                            <div class="col-md-12">
                                <uc1:UstMenu runat="server" ID="UstMenu" />
                            </div>
                        </div>
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
                            <div class="col-lg-12">
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
                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    <label>Tarih
                                                        <asp:RequiredFieldValidator ControlToValidate="txtSure" ValidationGroup="form" ID="RequiredFieldValidator2" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtTarih" ValidationGroup="form" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card-footer">
                                        <a href="OturumYonetim.aspx?SinavId=<%=Request.QueryString["SinavId"] %>" class="btn btn-secondary btn-flat"><i class="fas fa-chevron-circle-left mr-1"></i>Oturumlar</a>
                                        <asp:Button ID="btnKaydet" CssClass="btn btn-primary float-right" runat="server" ValidationGroup="form" Text="Kaydet" OnClick="btnKaydet_OnClick" />
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
    <script src="/Content/plugins/moment/moment.min.js"></script>
    <script src="/Content/plugins/daterangepicker/daterangepicker.js"></script>
    <script>
        $(function () {

            $('#ContentPlaceHolder1_txtTarih').daterangepicker({
                timePicker: true,
                timePicker24Hour: true,
                showDropdowns: true,
                minYear: 2000,
                maxYear: 2050,
                "autoApply": true,
                "opens": "center",
                "drops": "up",
                locale: {
                    format: 'DD.MM.YYYY H:mm',
                    "timePicker24Hour": true,
                    cancelLabel: 'Vazgeç',
                    applyLabel: 'Tamam',
                    "daysOfWeek": ["Pz", "Pt", "Sa", "Çr", "Pr", "Cu", "Ct"],
                    "monthNames": ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"]
                }
            });
        });

    </script>

</asp:Content>

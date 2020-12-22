<%@ page title="" language="C#" masterpagefile="~/CevrimiciSinav/MasterPage.master" autoeventwireup="true" inherits="Sinav_SinavSonuc, okulsinavi" enableEventValidation="false" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="Content/ekko-lightbox/ekko-lightbox.css" />
    <link rel="stylesheet" href="Content/sweetalert2/sweetalert2.min.css" />
    <link href="Content/css/Site.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container">
                <div class="row mb-2">
                    <div class="col-sm-10">
                        <h4 class="m-0 text-dark">
                            <asp:Literal ID="ltrTestAdi" runat="server"></asp:Literal></h4>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <div class="card card-primary card-outline">
                            <div class="card-body text-center">
                                <div class="row">
                                        <div class="col-sm-6 col-md-3 float-left">
                                            <div class="info-box  mb-3 bg-warning">
                                                <span class="info-box-icon"><i class="fas fa-list"></i></span>
                                                <div class="info-box-content">
                                                    <span class="info-box-text">Soru Sayısı</span>
                                                    <span class="info-box-number">
                                                        <asp:Literal ID="ltrSoruSayisi" runat="server"></asp:Literal></span>
                                                </div>
                                            </div>
                                        </div>
                                    <div class="col-sm-6 col-md-3 float-left">
                                        <div class="info-box mb-3 bg-info">
                                            <span class="info-box-icon"><i class="fas fa-award"></i></span>

                                            <div class="info-box-content">
                                                <span class="info-box-text">Puanınız <span class="miniTxt" id="miniText" runat="server"></span></span>
                                                <span class="info-box-number">
                                                    <asp:Literal ID="ltrPuan" runat="server"></asp:Literal></span>
                                            </div>
                                        </div>
                                    </div>
                                        <div class="col-sm-6 col-md-3 float-left">
                                            <div class="info-box mb-3 bg-success">
                                                <span class="info-box-icon"><i class="far fa-check-circle"></i></span>

                                                <div class="info-box-content">
                                                    <span class="info-box-text">Doğru</span>
                                                    <span class="info-box-number">
                                                        <asp:Literal ID="ltrDogruSayisi" runat="server"></asp:Literal></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-md-3 float-left">
                                            <div class="info-box mb-3 bg-danger">
                                                <span class="info-box-icon"><i class="far fa-times-circle"></i></span>

                                                <div class="info-box-content">
                                                    <span class="info-box-text">Yanlış</span>
                                                    <span class="info-box-number">
                                                        <asp:Literal ID="ltrYanlis" runat="server"></asp:Literal></span>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                                        <asp:Repeater runat="server" ID="rptOturumlar" OnItemDataBound="rptOturumlar_OnItemDataBound">
                                            <ItemTemplate>
                                                     <div class="card">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title"><%#Eval("OturumAdi") %></h3>
                                            </div>
                                            <div class="card-body p-0">
                                                <div class="table-responsive">
                                                    <table class="table m-0">
                                                        <thead>
                                                        <tr>
                                                            <th class="w-50">Ders Adı</th>
                                                            <th>Soru No</th>
                                                            <th>Doğru Cevap</th>
                                                            <th>Cevabınız</th>
                                                            <th>Sonuç</th>
                                                        </tr>
                                                        </thead>
                                                        <tbody>
                                                        <asp:Repeater ID="rptSorular" runat="server" OnItemDataBound="rptSorular_OnItemDataBound">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <a data-toggle="lightbox" data-title="" data-gallery="gallery" href="<%#Eval("Soru") %>"><%#Eval("BransAdi") %></a>
                                                                    </td>
                                                                    <td><%#Eval("SoruNo") %></td>
                                                                    <td><%#Eval("Cevap") %></td>
                                                                    <td>
                                                                        <asp:Literal ID="ltrOgrenciCevap" runat="server"></asp:Literal></td>
                                                                    <td>
                                                                        <asp:Literal ID="ltrSonuc" runat="server"></asp:Literal></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                   
                                            </ItemTemplate>
                                        </asp:Repeater>
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
    <script src="Content/ekko-lightbox/ekko-lightbox.min.js"></script>
    <script src="Content/sweetalert2/sweetalert2.min.js"></script>
    <script>
       
        $(function () {
            
            $(document).on('click',
                '[data-toggle="lightbox"]',
                function (event) {
                    event.preventDefault();
                    $(this).ekkoLightbox({
                        alwaysShowClose: true
                    });
                });

        });
    </script>
</asp:Content>


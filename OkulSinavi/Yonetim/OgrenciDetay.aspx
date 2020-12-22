<%@ Page Title="" Language="C#" MasterPageFile="~/Yonetim/MasterPage.master" AutoEventWireup="true" CodeFile="OgrenciDetay.aspx.cs" Inherits="Yonetim_OgrenciDetay" %>

<%@ Register Src="~/Yonetim/UstMenu.ascx" TagPrefix="uc1" TagName="UstMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/CevrimiciSinav/Content/ekko-lightbox/ekko-lightbox.css" />
    <link rel="stylesheet" href="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">

        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-12">
                        <h1 class="m-0 text-dark">Çevrim İçi Sınav Modülü</h1>
                    </div>
                </div>
            </div>
        </div>

        <div class="content">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-10">
                                    <ol class="breadcrumb float-sm-left">
                                        <li class="breadcrumb-item"><a href="OkulOgrenciListesi.aspx">Öğrenciler</a></li>
                                        <li class="breadcrumb-item active">
                                            <asp:Literal ID="ltrOgrenciAdiSoyadi" runat="server"></asp:Literal>
                                            - Sınav Kartı</li>
                                    </ol>
                                </div>
                            </div>
                        </div>

                    </div>


                    <div class="row">
                        <div class="col-12">
                            <div class="card">
                                <div class="card-header d-flex p-0">
                                    <h3 class="card-title p-3">Sınav Kartı</h3>
                                    <ul class="nav nav-pills ml-auto p-2">
                                        <li class="nav-item"><a class="nav-link active" href="#tab_1" data-toggle="tab">Açıklamalar</a></li>
                                        <li class="nav-item"><a class="nav-link" href="#tab_sinav_hareket" data-toggle="tab">Sınav Hareketleri</a></li>
                                        <li class="nav-item"><a class="nav-link" href="#tab_puan_tablosu" data-toggle="tab" onclick="PuanTablosu()">Puan Tablosu</a></li>
                                        <li class="nav-item"><a class="nav-link" href="#tab_loglar" data-toggle="tab" onclick="LogGetir()">Loglar</a></li>
                                    </ul>
                                </div>
                                <!-- /.card-header -->
                                <div class="card-body">
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="tab_1">
                                            <ul>
                                                <li>Öğrencinin puanının hesaplanması için sınavdaki tüm oturumlara katılması gerekmektedir. 2 oturumlu bir sınavda öğrencinin tek oturum kaydını görüyorsanız diğer oturumlara katılmamış demektir.</li>
                                                <li><em>Öğrencinin Cevabı</em> sütunu öğrencinin verdiği cevapların sayısını gösterir.</li>

                                                <li><em>Öğrencinin Cevabı</em> sayısı 0'dan büyük ve <em>Doğru</em> ve <em>Yanlış</em> alanı 0 (sıfır) ise bakılır; 
                              <ul>
                                  <li>Sınav süresi bitmiş ve <em>Bitiş Tarihi</em> alanı boş ise öğrenci 'Sınavı Bitir' butonuna tıklamadan sınavdan çıkmıştır. Sistem sınav sonunda (tüm oturumları tamamlamış ise) öğrencinin puanını hesaplayacaktır.</li>
                                  <li>Sınav süresi bitmemiş ise öğrenciniz halen sınavda olabilir.</li>
                              </ul>
                                                </li>

                                                <li><em>Öğrencinin Cevabı</em>, <em>Doğru</em>, <em>Yanlış</em> alanları 0 ve <em>Bitiş Tarihi</em> de görülüyorsa öğrenci sınava girmiş fakat hiç bir cevap vermeden sınavı bitirmişir.</li>
                                                <li><em>Son İşlem</em> sütunu öğrencinin sistem üzerinde yaptığı en sonki işlemin saatini gösterir. Örneğin her soruya verdiği cevap bir işlemdir.</li>
                                            </ul>
                                        </div>
                                        <!-- /.tab-pane -->
                                        <div class="tab-pane" id="tab_sinav_hareket">
                                            <nav id="navbar-example2" class="navbar navbar-light">
                                                <label class="float-right" id="sinavAdi">Sınav seçiniz</label>
                                                <ul class="nav nav-pills">
                                                    <li class="nav-item">
                                                        <asp:DropDownList ID="ddlSinavlar" CssClass="form-control float-right" ValidationGroup="form" runat="server">
                                                        </asp:DropDownList>
                                                    </li>

                                                </ul>
                                            </nav>
                                            <div id="ogrSinavHareket" style="min-height: 100px">
                                                <div class="text-center">Bir sınav seçiniz.</div>
                                            </div>
                                            <div class="text-center"><small class="badge badge-warning">Öğrenci puanları için sınavlar menüsünü ziyaret ediniz.</small></div>

                                        </div>
                                        <!-- /.tab-pane -->
                                        <div class="tab-pane" id="tab_loglar"></div>
                                        <!-- /.tab-pane -->
                                        <div class="tab-pane" id="tab_puan_tablosu">
                                            <div id="puantablosu" class="mb-5">

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
            </div>

        </div>
    </div>
    <div class="modal fade" id="ogr-karne">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"></h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Kapat</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src="/CevrimiciSinav/Content/ekko-lightbox/ekko-lightbox.min.js"></script>
    <script src="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.js"></script>
    <script>
        function Sinav() {
            var sinavId = $("#ContentPlaceHolder1_ddlSinavlar option:selected").val();
            $("#ogrSinavHareket").load("/Yonetim/_OgrenciSinavDetay.aspx?OpaqId=<%=Request.QueryString["OpaqId"]%>&SinavId=" + sinavId);
        }

        function PuanTablosu() {
            $("#puantablosu").load("/Yonetim/_OgrenciPuanTablosu.aspx?OpaqId=<%=Request.QueryString["OpaqId"]%>");
        }
        function LogGetir() {
            $("#tab_loglar").load("/Yonetim/_Rapor/_OgrenciLogArama.aspx?aranan=<%=Request.QueryString["OpaqId"]%>");
        }
    </script>
</asp:Content>


<%@ page title="" language="C#" masterpagefile="MasterPage.master" autoeventwireup="true" inherits="OkulSinavi_CevrimiciSinavYonetim_OgrenciSinavDetayRoot, okulsinavi" enableEventValidation="false" %>

<%@ Register Src="~/Yonetim/UstMenu.ascx" TagPrefix="uc1" TagName="UstMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
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
                    <uc1:UstMenu runat="server" ID="UstMenu" />
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-10">
                                    <ol class="breadcrumb float-sm-left">
                                        <li class="breadcrumb-item"><a href="OkulOgrenciListesi.aspx">Öğrenciler</a></li>
                                        <li class="breadcrumb-item active">
                                            <asp:Literal ID="ltrOgrenciAdiSoyadi" runat="server"></asp:Literal> - Sınav Hareketleri</li>
                                    </ol>
                                </div>
                            </div>
                            <nav id="navbar-example2" class="navbar navbar-light bg-light">
                                <label class=" float-right" id="sinavAdi">Sınav seçiniz</label>
                                <ul class="nav nav-pills">
                                    <li class="nav-item">
                                        <asp:DropDownList ID="ddlSinavlar" CssClass="form-control float-right" ValidationGroup="form" runat="server">
                                        </asp:DropDownList>
                                    </li>
                                   
                                </ul>
                            </nav>
                            <div id="ogrSinavDurum" style="min-height:100px"><div class="text-center">Bir sınav seçiniz.</div></div>
                            <div class="text-center"><small class="badge badge-warning">Öğrenci puanları için sınavlar menüsünü ziyaret ediniz.</small></div>
                        </div>

                    </div>
                    
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">
                                <i class="fas fa-info-circle"></i>
                                Açıklamalar
                            </h3>
                        </div>
                        <!-- /.card-header -->
                        <div class="card-body">
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
                        <!-- /.card-body -->
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script>
        function Sinav() {
            var sinavId = $("#ContentPlaceHolder1_ddlSinavlar option:selected").val();
           if(sinavId!=="")
            $("#ogrSinavDurum").load("/Yonetim/_OgrenciSinavDetay.aspx?OpaqId=<%=Request.QueryString["OpaqId"]%>&SinavId="+sinavId);
        }

        function SinavTamamla() {
           
        }
    </script>
</asp:Content>


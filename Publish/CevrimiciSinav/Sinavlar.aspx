<%@ page title="" language="C#" masterpagefile="~/CevrimiciSinav/MasterPage.master" autoeventwireup="true" inherits="Sinav_Sinavlar, okulsinavi" enableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="Content/js/snippet-javascript-console.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="content-wrapper">
        <div class="content-header">
            <div class="container">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Sınavlar</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item active">
                                <asp:Literal ID="ltrAdiSoyadi" runat="server"></asp:Literal></li>
                         </ol>
                    </div>
                </div>
            </div>
        </div>
         
        <div class="content">
            <div class="container">
                <div class="alert alert-default-light alert-dismissible">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                    <h4><i class="icon fa fa-info"></i>Bilgi!</h4>
                    Sınav öncesi yanınıza soru çözümlerini yapabilmek için kalem, silgi ve kağıt hazırlamayı unutmayın..<br/>
                  Size kolaylık sağlaması için sınav yardım ekranını inceleyiniz.  <button class="btn btn-success btn-sm" type="button" data-toggle="modal" data-target="#video">tıklayınız</button>
                </div>
                <div class="row">
                    <asp:Repeater ID="rptSinavlar" runat="server">
                        <ItemTemplate>
                            <div class="col-md-6 col-sm-12 col-xl-4">
                                <div class="card card-primary card-outline">
                                     <div class="card-body">
                                        
                                         <div class="row">
                                             <div class="col-12">
                                                 <h3 class="card-title"><%#Eval("SinavAdi") %></h3>
                                             </div>
                                         </div>
                                         <div class="row">
                                            <div class="col-12">
                                                <span class="float-left">Sınıf: <%#Eval("Sinif") %></span>
                                            </div>
                                        </div>
                                        <p class="mt-2"><%#Eval("Aciklama") %></p>
                                        <button type="button" class="btn btn-primary"  data-toggle="modal" data-target="#modal-oturumlar" data-id="<%#Eval("Id") %>">Sınava Katıl</button>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Repeater ID="rptSinavlarDemo" runat="server">
                        <ItemTemplate>
                            <div class="col-md-6 col-sm-12 col-xl-4">
                                <div class="card card-primary card-outline">
                                    <div class="card-body">
                                        
                                        <div class="row">
                                            <div class="col-12">
                                                <h3 class="card-title"><%#Eval("SinavAdi") %></h3>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-12">
                                                <span class="float-left">Sınıf: <%#Eval("Sinif") %></span>
                                            </div>
                                        </div>
                                        <p class="mt-2"><%#Eval("Aciklama") %></p>
                                        <button type="button" class="btn btn-primary"  data-toggle="modal" data-target="#modal-oturumlar-demo" data-id="<%#Eval("Id") %>">Sınava Katıl</button>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-12 text-center">
                    <small>
                        <a href="#" data-toggle="modal" data-target="#video" class="ml-2"><i class="fa fa-image"></i> Sınav yardım ekranı</a></small> 
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="video">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-body">
                    <img src="/upload/sinav-yardim-ekrani.jpg" class="img-fluid"/>
                    <p><a href="/upload/sinav-yardim-ekrani.jpg" target="_blank">Yeni pencerede aç</a></p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Kapat</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modal-oturumlar">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Oturumlar</h4>
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
    <div class="modal fade" id="modal-oturumlar-demo">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Oturumlar</h4>
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
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
    
    <script>

        $(document).on("contextmenu", function (e) { e.preventDefault(); });
       

        var sinavId = 0;
        var sayac = null;
        $("#modal-oturumlar").on("show.bs.modal", function(event) {
            sinavId = $(event.relatedTarget).data('id');

            $("#modal-oturumlar .modal-body").load("/CevrimiciSinav/OturumGetirModal.aspx?SinavId=" + sinavId);

            sayac = setInterval(YenidenYukle, 10000);
        });
        $('#modal-oturumlar').on('hidden.bs.modal',
            function(e) {
                
                clearInterval(sayac);
            });
        function  YenidenYukle()
        {
            if (sinavId!==0) {
                $("#modal-oturumlar .modal-body").load("/CevrimiciSinav/OturumGetirModal.aspx?SinavId=" + sinavId);
            }
        };
        $("#modal-oturumlar-demo").on("show.bs.modal", function (event) {
            sinavId = $(event.relatedTarget).data('id');

            $("#modal-oturumlar-demo .modal-body").load("/CevrimiciSinav/DemoOturumGetirModal.aspx?SinavId=" + sinavId);

        });
    </script>
</asp:Content>


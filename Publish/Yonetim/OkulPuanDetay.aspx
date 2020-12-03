<%@ page title="" language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="OkulSinavi_CevrimiciSinavYonetim_OkulPuanDetay, okulsinavi" enableEventValidation="false" %>

<%@ Register TagPrefix="uc1" TagName="UstMenu" Src="~/Yonetim/UstMenu.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/CevrimiciSinav/Content/ekko-lightbox/ekko-lightbox.css" />
    <link rel="stylesheet" href="/CevrimiciSinav/Content/datatables-bs4/css/dataTables.bootstrap4.css" />
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
                    <div class="card card-default">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <ol class="breadcrumb float-sm-left">
                                        <li class="breadcrumb-item"><a href="Sinavlar.aspx"><%=TestSeciliDonem.SeciliDonem().Donem%> Sınavlar</a></li>
                                        <li class="breadcrumb-item"><a href="SinavDetay.aspx?SinavId=<%=Request.QueryString["SinavId"] %>">Okullar</a></li>
                                        <li class="breadcrumb-item active">Öğrenci Listesi</li>
                                    </ol>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <table id="table" class="table table-striped table-responsive-sm">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Adı Soyadı</th>
                                            <th>Sınıf - Şube</th>
                                            <th>Doğru</th>
                                            <th>Yanlış</th>
                                            <th>Boş</th>
                                            <th>Net</th>
                                            <th>Puan</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptKayitlar" runat="server" OnItemDataBound="rptKayitlar_OnItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <th scope="row">
                                                        <asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></th>
                                                    <td><a href="#" data-toggle="modal" data-target="#ogr-karne" data-adi="<%#Eval("Adi") %> <%#Eval("Soyadi") %>" data-sinavid="<%#Eval("SinavId") %>" data-opaqid="<%#Eval("OpaqId") %>"><%#Eval("Adi") %> <%#Eval("Soyadi") %></a></td>
                                                    <td><%#Eval("Sinifi") %> - <%#Eval("Sube") %></td>
                                                    <td><%#Eval("Dogru") %></td>
                                                    <td><%#Eval("Yanlis") %></td>
                                                    <td><%#Eval("Bos") %></td>
                                                    <td>
                                                        <asp:Literal ID="ltrNet" runat="server"></asp:Literal></td>
                                                    <td><%#Eval("Puan").ToDecimal()>100?Eval("Puan").ToDecimal().ToString("##.###"):"-" %></td>
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
    <script src="/CevrimiciSinav/Content/datatables/jquery.dataTables.js"></script>
    <script src="/CevrimiciSinav/Content/datatables-bs4/js/dataTables.bootstrap4.js"></script>
    <script>
        $(function () {
            $('#table').DataTable({
                "paging": true,
                "lengthChange": true,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": false,
                "lengthMenu": [[50, 75, 100, 200, -1], [50, 75, 100, 200, "Tümü"]]
            });
        });

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
        $("#ogr-karne").on("show.bs.modal", function (event) {
            var sinavId = $(event.relatedTarget).data("sinavid");
            var opaqid = $(event.relatedTarget).data("opaqid");
            var adi = $(event.relatedTarget).data("adi");
            $("#ogr-karne .modal-title").html(adi);
            $("#ogr-karne .modal-body").load("/Yonetim/OgrenciKarne.aspx?SinavId=" + sinavId + "&OpaqId=" + opaqid);

        });
    </script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OgrenciSiralama.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim__Rapor_OgrenciSiralama" %>

<%@ Register TagPrefix="uc1" TagName="UstMenu" Src="~/Yonetim/UstMenu.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <ol class="breadcrumb float-sm-left">
                                        <li class="breadcrumb-item"><a href="../SinavRapor.aspx">Raporlar</a></li>
                                        <li class="breadcrumb-item active">Öğrenci Sıralaması</li>
                                    </ol>
                                </div>
                            </div>

                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header">
                                        <h3 class="card-title">
                                            <asp:Literal ID="ltrSinavAdi" runat="server"></asp:Literal> Sınavı Öğrenci Sonuçları</h3>
                                    </div>
                                    <div class="card-body">
                                        <table id="table-oran" class="table table-bordered table-striped table-responsive-md">
                                            <thead>
                                                <tr>
                                                    <th style="width: 1%" class="text-center">#</th>
                                                    <th class="text-center">İlçe</th>
                                                    <th>Kurum Adı</th>
                                                    <th class="text-center">Opaq Id</th>
                                                    <th>Adı Soyadı</th>
                                                    <th>Sınıf - Şube</th>
                                                    <th style="width: 10%">Doğru</th>
                                                    <th style="width: 10%">Yanlış</th>
                                                    <th style="width: 10%">Net</th>
                                                    <th style="width: 10%">Puan</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptKayitlar" runat="server" OnItemDataBound="rptKayitlar_OnItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></td>
                                                            <td class="text-center"><%#Eval("IlceAdi") %></td>
                                                            <td><%#Eval("KurumAdi") %></td>
                                                            <td class="text-center"><%#Eval("OpaqId") %></td>
                                                            <td><a href="#" data-toggle="modal" data-target="#ogr-karne" data-adi="<%#Eval("Adi") %> <%#Eval("Soyadi") %>" data-sinavid="<%#Eval("SinavId") %>" data-opaqid="<%#Eval("OpaqId") %>"><%#Eval("Adi") %> <%#Eval("Soyadi") %></a></td>
                                                            <td><%#Eval("Sinifi") %> - <%#Eval("Sube") %></td>
                                                            
                                                            <td><%#Eval("Dogru") %></td>
                                                            <td><%#Eval("Yanlis") %></td>
                                                            <td>
                                                                <%#string.Format("{0:##.###}",Eval("Dogru").ToInt32()-(Eval("Yanlis").ToDecimal()/(Eval("Sinifi").ToInt32() > 9 ? 4 : 3 ))) %>
                                                            </td>
                                                            <td><%#Eval("Puan").ToDecimal().ToString("##.###") %></td>
                                                        </tr>

                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                            <tfoot>
                                            <tr>
                                                <th style="width: 1%" class="text-center">#</th>
                                                <th class="text-center">İlçe</th>
                                                <th class="text-center">Kurum Kodu</th>
                                                <th>Kurum Adı</th>
                                                <th>Adı Soyadı</th>
                                                <th>Sınıf - Şube</th>
                                                <th style="width: 10%">Doğru</th>
                                                <th style="width: 10%">Yanlış</th>
                                                <th style="width: 10%">Net</th>
                                                <th style="width: 10%">Puan</th>
                                            </tr>
                                            </tfoot>
                                        </table>
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
    <script src="/CevrimiciSinav/Content/datatables/jquery.dataTables.js"></script>
    <script src="/CevrimiciSinav/Content/datatables-bs4/js/dataTables.bootstrap4.js"></script>
    <script>
        $(function () {
            $('#table-oran').DataTable({
                "paging": true,
                "lengthChange": true,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": false,
                "lengthMenu": [[10, 25, 50,100, -1], [10, 25, 50,100, "Tümü"]]
            });
        });
        $("#ogr-karne").on("show.bs.modal", function (event) {
            var sinavId = $(event.relatedTarget).data("sinavid");
            var opaqid = $(event.relatedTarget).data("opaqid");
            var adi = $(event.relatedTarget).data("kurumkodu");
            $("#ogr-karne .modal-title").html(adi);
            $("#ogr-karne .modal-body").load("/Yonetim/OgrenciKarne.aspx?SinavId=" + sinavId + "&OpaqId=" + opaqid);

        });
    </script>
</asp:Content>


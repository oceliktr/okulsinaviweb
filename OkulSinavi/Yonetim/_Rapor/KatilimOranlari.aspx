<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="KatilimOranlari.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim__Rapor_KatilimOranlari" %>

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
                                        <li class="breadcrumb-item active">Katılım Oranları</li>
                                    </ol>
                                </div>
                            </div>

                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header">
                                        <h3 class="card-title">
                                            <asp:Literal ID="ltrSinavAdi" runat="server"></asp:Literal> Katılım Oranları</h3>
                                    </div>
                                    <div class="card-body">
                                        <table id="table-oran" class="table table-bordered table-striped table-responsive-md">
                                            <thead>
                                                <tr>
                                                    <th style="width: 1%" class="text-center">#</th>
                                                    <th style="width: 10%" class="text-center">İlçe</th>
                                                    <th style="width: 10%" class="text-center">Kurum Kodu</th>
                                                    <th>Kurum Adı</th>
                                                    <th style="width: 20%" class="text-center">Öğrenci Sayısı</th>
                                                    <th style="width: 20%" class="text-center">Katılım</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptKayitlar" runat="server" OnItemDataBound="rptKayitlar_OnItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></td>
                                                            <td class="text-center"><%#Eval("IlceAdi") %></td>
                                                            <td class="text-center"><%#Eval("KurumKodu") %></td>
                                                            <td><%#Eval("KurumAdi") %></td>
                                                            <td class="text-center"><%#Eval("OgrenciSayisi") %></td>
                                                            <td class="text-center">

                                                                <div class="progress progress-sm">
                                                                    <div class="progress-bar bg-green" role="progressbar" aria-volumenow="<%#string.Format("{0:##}",Eval("OgrenciSayisi").ToInt32()>0?(100/Eval("OgrenciSayisi").ToDecimal())*Eval("KatilanOgrSayisi").ToInt32():0) %>" aria-volumemin="0" aria-volumemax="100" style='width: <%#string.Format("{0:##}",Eval("OgrenciSayisi").ToInt32()>0?(100/Eval("OgrenciSayisi").ToDecimal())*Eval("KatilanOgrSayisi").ToInt32():0) %>%'>
                                                                    </div>
                                                                </div>
                                                                <small>% <%#string.Format("{0:##}",Eval("OgrenciSayisi").ToInt32()>0?(100/Eval("OgrenciSayisi").ToDecimal())*Eval("KatilanOgrSayisi").ToInt32():0) %> katılım (<%#Eval("KatilanOgrSayisi").ToInt32() %> öğrenci)
                                                                </small>
                                                            </td>
                                                        </tr>

                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                            <tfoot>
                                                <tr>
                                                    <th style="width: 1%" class="text-center">#</th>
                                                    <th style="width: 10%" class="text-center">İlçe</th>
                                                    <th style="width: 10%" class="text-center">Kurum Kodu</th>
                                                    <th>Kurum Adı</th>
                                                    <th style="width: 20%" class="text-center">Öğrenci Sayısı</th>
                                                    <th style="width: 20%" class="text-center">Katılım</th>
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
    </script>
</asp:Content>


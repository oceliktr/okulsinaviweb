<%@ page title="" language="C#" masterpagefile="MasterPage.master" autoeventwireup="true" inherits="Okul_SinaviYonetim_SinavDetay, okulsinavi" enableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/CevrimiciSinav/Content/datatables-bs4/css/dataTables.bootstrap4.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-12">
                        <h1 class="m-0 text-dark">Çevrim İçi Sınav Modülü </h1>
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
                                <div class="col-md-10">
                                    <ol class="breadcrumb float-sm-left">
                                        <li class="breadcrumb-item"><a href="Sinavlar.aspx"> Sınavlar</a></li>
                                        <li class="breadcrumb-item active">Okullar</li>
                                    </ol>
                                </div>
                                <div class="col-md-2 text-right mb-2">

                                    <asp:DropDownList ID="ddlIlce" Visible="False" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlIlce_OnSelectedIndexChanged" ValidationGroup="form" runat="server">
                                        <asp:ListItem Value="">İlçe Seçiniz</asp:ListItem>
                                        <asp:ListItem Value="AŞKALE">AŞKALE</asp:ListItem>
                                        <asp:ListItem Value="AZİZİYE">AZİZİYE</asp:ListItem>
                                        <asp:ListItem Value="ÇAT">ÇAT</asp:ListItem>
                                        <asp:ListItem Value="HINIS">HINIS</asp:ListItem>
                                        <asp:ListItem Value="HORASAN">HORASAN</asp:ListItem>
                                        <asp:ListItem Value="İSPİR">İSPİR</asp:ListItem>
                                        <asp:ListItem Value="KARAÇOBAN">KARAÇOBAN</asp:ListItem>
                                        <asp:ListItem Value="KARAYAZI">KARAYAZI</asp:ListItem>
                                        <asp:ListItem Value="KÖPRÜKÖY">KÖPRÜKÖY</asp:ListItem>
                                        <asp:ListItem Value="NARMAN">NARMAN</asp:ListItem>
                                        <asp:ListItem Value="OLTU">OLTU</asp:ListItem>
                                        <asp:ListItem Value="OLUR">OLUR</asp:ListItem>
                                        <asp:ListItem Value="PALANDÖKEN">PALANDÖKEN</asp:ListItem>
                                        <asp:ListItem Value="PASİNLER">PASİNLER</asp:ListItem>
                                        <asp:ListItem Value="PAZARYOLU">PAZARYOLU</asp:ListItem>
                                        <asp:ListItem Value="ŞENKAYA">ŞENKAYA</asp:ListItem>
                                        <asp:ListItem Value="TEKMAN">TEKMAN</asp:ListItem>
                                        <asp:ListItem Value="TORTUM">TORTUM</asp:ListItem>
                                        <asp:ListItem Value="UZUNDERE">UZUNDERE</asp:ListItem>
                                        <asp:ListItem Value="YAKUTİYE">YAKUTİYE</asp:ListItem>
                                        <asp:ListItem Value="BÜYÜKŞEHİR">BÜYÜKŞEHİR</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-lg-12">
                                <table id="table" class="table table-striped table-responsive-sm">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Okul Adı</th>
                                            <th>Katılan Öğrenci Sayısı</th>
                                            <th>Okul Puan Ort.</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptKayitlar" runat="server" OnItemDataBound="rptKayitlar_OnItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <th scope="row">
                                                        <asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></th>
                                                    <td><a href="OkulPuanDetay.aspx?SinavId=<%#Eval("SinavId") %>&KurumKodu=<%#Eval("KurumKodu") %>&ilce=<%#Eval("IlceAdi") %>"><%#Eval("KurumAdi") %></a></td>
                                                    <td><%#Eval("OgrSayisi") %></td>
                                                    <td><%#Eval("Puan").ToDecimal().ToString("##.###") %></td>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
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
    </script>
</asp:Content>


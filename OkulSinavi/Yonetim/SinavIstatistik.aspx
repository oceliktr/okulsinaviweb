<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SinavIstatistik.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim_SinavIstatistik" %>

<%@ Register TagPrefix="uc1" TagName="UstMenu" Src="~/Yonetim/UstMenu.ascx" %>

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
                                <div class="col-md-12 mb-2">
                                    <h3 class="card-title">
                                        <asp:Literal ID="ltrSinavAdi" runat="server"></asp:Literal>
                                        Anlık İstatistikleri
                                    </h3>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12 col-sm-6 col-md-6">
                                    <div class="info-box mb-3">
                                        <span class="info-box-icon bg-danger elevation-1"><i class="fas fa-users"></i></span>
                                        <div class="info-box-content">
                                            <div class="progress-group">
                                                Öğrenci Katılım Oranı (% <%=Session["OgrenciOran"].ToString() %>)
                                                <span class="float-right"><b>
                                                    <asp:Literal ID="ltrKatilanOgrenciSayisi" runat="server"></asp:Literal></b>/<asp:Literal ID="ltrOgrenciSayisi" runat="server"></asp:Literal></span>
                                                <div class="progress progress-sm" style="height: 15px">
                                                    <div class="progress-bar bg-primary" style='width: <%=Session["OgrenciOran"].ToString().Replace(",",".") %>%'></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-6 col-md-6">
                                    <div class="info-box mb-3">
                                        <span class="info-box-icon bg-danger elevation-1"><i class="fas fa-school"></i></span>
                                        <div class="info-box-content">
                                            <div class="progress-group">
                                                Okul Katılım Oranı (% <%=Session["OkulOran"].ToString() %>)
                                                <span class="float-right"><b>
                                                    <asp:Literal ID="ltrKatilanOkulSayisi" runat="server"></asp:Literal></b>/<asp:Literal ID="ltrOkulSayisi" runat="server"></asp:Literal></span>
                                                <div class="progress progress-sm" style="height: 15px">
                                                    <div class="progress-bar bg-primary" style='width: <%=Session["OkulOran"].ToString().Replace(",",".") %>%'></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="callout callout-danger">
                        <h5>Önemli Bilgi!</h5>

                        <p>Bu ekrandaki veriler anlık sınava girişleri göstermektedir. Diğer ekranlardaki katılım oranları sınavı tamamlayanları kapsadığından bu ekrandaki değerlerle tutarsızlık gösterebilir.</p>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">İlçe Katılım Oranları</h3>
                        </div>
                        <!-- /.card-header -->
                        <div class="card-body p-0">
                            <table class="table table-sm">
                                <thead>
                                    <tr>
                                        <th style="width: 10px">#</th>
                                        <th>İlçe Adı</th>
                                        <th>Kurum Katılım</th>
                                        <th>Öğrenci Katılım</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptIlceler" OnItemDataBound="rptIlceler_OnItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td><asp:Label runat="server" Text="" ID="lblSira"></asp:Label></td>
                                                <td class="w-25"><%#Eval("IlceAdi") %></td>
                                                <td>
                                                    <span class="badge <%#((100/Eval("KurumSayisi").ToDecimal())*Eval("KatilanKurumSayisi").ToInt32())<=90?"bg-danger":"bg-success" %>"><%#((100/Eval("KurumSayisi").ToDecimal())*Eval("KatilanKurumSayisi").ToInt32()).ToString("##.##") %> %</span>
                                                    <span class="float-right"><small><b><%#Eval("KatilanKurumSayisi") %></b>/<%#Eval("KurumSayisi") %></small></span>
                                                    <div class="progress progress-xs mb-2" style="height: 2px">
                                                        <div class="progress-bar progress-bar-danger" style='width: <%#((100/Eval("KurumSayisi").ToDecimal())*Eval("KatilanKurumSayisi").ToInt32()).ToString("##.##").Replace(",",".") %>%'></div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <span class="badge <%#((100/Eval("OgrenciSayisi").ToDecimal())*Eval("KatilanOgrenciSayisi").ToInt32())<=70?"bg-danger":"bg-success" %>"><%#((100/Eval("OgrenciSayisi").ToDecimal())*Eval("KatilanOgrenciSayisi").ToInt32()).ToString("##.##") %> %</span>
                                                    <span class="float-right"><small><b><%#Eval("KatilanOgrenciSayisi") %></b>/<%#Eval("OgrenciSayisi") %></small></span>
                                                    <div class="progress progress-xs mb-2" style="height: 2px">
                                                        <div class="progress-bar progress-bar-success" style='width: <%#((100/Eval("OgrenciSayisi").ToDecimal())*Eval("KatilanOgrenciSayisi").ToInt32()).ToString("##.##").Replace(",",".") %>%'></div>
                                                    </div>
                                                </td>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
</asp:Content>


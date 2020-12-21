<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OgrenciKarne.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim_OgrenciKarne" %>

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
                <span class="info-box-text">Puan <span class="miniTxt" id="miniText" runat="server"></span></span>
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
         <asp:Repeater runat="server" ID="rptOturumlar" OnItemDataBound="rptOturumlar_OnItemDataBound">
            <itemtemplate>
        <div class="card">
            <div class="card-header border-transparent">
                <h3 class="card-title"><%#Eval("OturumAdi") %></h3>
            </div>
            <div class="card-body p-0">
                <div class="table-responsive">
                    <table class="table m-0">
                        <thead>
                            <tr>
                                <th>Ders Adı</th>
                                <th>Soru No</th>
                                <th>Doğru Cevap</th>
                                <th>Öğr. Cevabı</th>
                                <th>Sonuç</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptSorular" runat="server" OnItemDataBound="rptSorular_OnItemDataBound">
                                <itemtemplate>
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
                             </itemtemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

         </itemtemplate>
            <FooterTemplate>
                <asp:PlaceHolder ID="phEmpty" runat="server" Visible="false">
                <div class="card-header border-transparent">
                    <h3 class="card-title">Veri Yok</h3>
                </div>
                </asp:PlaceHolder>
            </footertemplate>
        </asp:Repeater>
    </div>
</div>

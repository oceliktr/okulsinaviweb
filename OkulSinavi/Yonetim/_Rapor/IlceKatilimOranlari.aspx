<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="IlceKatilimOranlari.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim__Rapor_IlceKatilimOranlari" %>

<table class="table table-striped">
    <thead>
        <tr>
            <th style="width: 10px"></th>
            <th></th>
            <th colspan="2" class="text-center">Kurum</th>
            <th colspan="2" class="text-center">Öğrenci</th>
            <th></th>
        </tr>
    <tr>
        <th style="width: 10px">#</th>
        <th>İlçe Adı</th>
        <th>Toplam</th>
        <th>Katılım</th>
        <th>Toplam</th>
        <th>Katılım</th>
        <th>Puan Ortalaması</th>
    </tr>
    </thead>
    <tbody>
        <asp:Repeater runat="server" ID="rptKayitlar" OnItemDataBound="rptKayitlar_OnItemDataBound">
            <ItemTemplate>
                <tr>
                    <td><asp:Label runat="server" Text="" ID="lblSira"></asp:Label></td>
                    <td><%#Eval("IlceAdi") %></td>
                    <td><%#Eval("ToplamKurumSayisi") %></td>
                    <td>                        <div class="progress progress-sm">
                            <div class="progress-bar bg-green" role="progressbar" aria-volumenow="<%#string.Format("{0:##}",Eval("ToplamKurumSayisi").ToInt32()>0?(100/Eval("ToplamKurumSayisi").ToDecimal())*Eval("KatilanKurumSayisi").ToInt32():0) %>" aria-volumemin="0" aria-volumemax="100" style='width: <%#string.Format("{0:##}",Eval("ToplamKurumSayisi").ToInt32()>0?(100/Eval("ToplamKurumSayisi").ToDecimal())*Eval("KatilanKurumSayisi").ToInt32():0) %>%'>
                            </div>
                        </div>
                        <small>% <%#string.Format("{0:##}",Eval("ToplamKurumSayisi").ToInt32()>0?(100/Eval("ToplamKurumSayisi").ToDecimal())*Eval("KatilanKurumSayisi").ToInt32():0) %> katılım (<%#Eval("KatilanKurumSayisi").ToInt32() %> kurum)
                        </small></td>
                    <td><%#Eval("ToplamOgrenciSayisi") %></td>
                    <td>
                        <div class="progress progress-sm">
                            <div class="progress-bar bg-green" role="progressbar" aria-volumenow="<%#string.Format("{0:##}",Eval("ToplamOgrenciSayisi").ToInt32()>0?(100/Eval("ToplamOgrenciSayisi").ToDecimal())*Eval("KatilanOgrenciSayisi").ToInt32():0) %>" aria-volumemin="0" aria-volumemax="100" style='width: <%#string.Format("{0:##}",Eval("ToplamOgrenciSayisi").ToInt32()>0?(100/Eval("ToplamOgrenciSayisi").ToDecimal())*Eval("KatilanOgrenciSayisi").ToInt32():0) %>%'>
                            </div>
                        </div>
                        <small>% <%#string.Format("{0:##}",Eval("ToplamOgrenciSayisi").ToInt32()>0?(100/Eval("ToplamOgrenciSayisi").ToDecimal())*Eval("KatilanOgrenciSayisi").ToInt32():0) %> katılım (<%#Eval("KatilanOgrenciSayisi").ToInt32() %> öğrenci)
                        </small>
                    </td>
                    <td><%#Eval("PuanOrtalama").ToDecimal().ToString("##.###") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>

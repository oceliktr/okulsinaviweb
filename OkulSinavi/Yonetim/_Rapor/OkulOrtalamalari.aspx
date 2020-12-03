<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OkulOrtalamalari.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim__Rapor_OkulOrtalamalari" %>

<table class="table table-striped">
    <thead>
    <tr>
        <th style="width: 10px">#</th>
        <th>İlçe Adı</th>
        <th>Kurum Adı</th>
        <th>Öğrenci Sayısı</th>
        <th>Doğru Ort.</th>
        <th>Yanlış Ort.</th>
        <th>Puan Ort.</th>
    </tr>
    </thead>
    <tbody>
    <asp:Repeater runat="server" ID="rptKayitlar" OnItemDataBound="rptKayitlar_OnItemDataBound">
        <ItemTemplate>
            <tr>
                <td><asp:Label runat="server" Text="" ID="lblSira"></asp:Label></td>
                <td><%#Eval("IlceAdi") %></td>
                <td><%#Eval("KurumAdi") %></td>
                <td><%#Eval("OgrSayisi") %></td>
                <td><%#(Eval("Dogru").ToInt32()/Eval("OgrSayisi").ToDecimal()).ToString("##.##") %></td>
                <td><%#(Eval("Yanlis").ToInt32()/Eval("OgrSayisi").ToDecimal()).ToString("##.##") %></td>
                <td><%#Eval("Puan").ToDecimal().ToString("##.###") %></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    </tbody>
</table>
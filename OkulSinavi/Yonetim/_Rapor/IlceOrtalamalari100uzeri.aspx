<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IlceOrtalamalari100uzeri.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim__Rapor_IlceOrtalamalari100uzeri" %>


<table class="table table-striped">
    <thead>
    <tr>
        <th style="width: 10px">#</th>
        <th>İlçe Adı</th>
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
                <td><%#Eval("OgrenciSayisi") %></td>
                <td><%#Eval("DogruOrtalamasi").ToDouble().ToString("##.##") %></td>
                <td><%#Eval("YanlisOrtalamasi").ToDouble().ToString("##.##") %></td>
                <td><%#Eval("PuanOrtalamasi").ToDecimal().ToString("##.###") %></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    </tbody>
</table>
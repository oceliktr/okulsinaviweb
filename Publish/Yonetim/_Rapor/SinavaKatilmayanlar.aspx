<%@ page language="C#" autoeventwireup="true" inherits="OkulSinavi_CevrimiciSinavYonetim__Rapor_SinavaKatilmayanlar, okulsinavi" enableEventValidation="false" %>

<table class="table table-striped">
    <thead>
    <tr>
        <th style="width: 10px">#</th>
        <th>İlçe Adı</th>
        <th>Kurum Kodu</th>
        <th>Kurum Adı</th>
    </tr>
    </thead>
    <tbody>
    <asp:Repeater runat="server" ID="rptKayitlar" OnItemDataBound="rptKayitlar_OnItemDataBound">
        <ItemTemplate>
            <tr>
                <td><asp:Label runat="server" Text="" ID="lblSira"></asp:Label></td>
                <td><%#Eval("IlceAdi") %></td>
                <td><%#Eval("KurumKodu") %></td>
                <td><%#Eval("KurumAdi") %></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    </tbody>
</table>
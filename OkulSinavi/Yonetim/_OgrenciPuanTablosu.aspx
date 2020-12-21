<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_OgrenciPuanTablosu.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim__OgrenciPuanTablosu_" %>

<table class="table table-striped table-responsive-sm">
    <thead>
        <tr>
            <th>Sınav Adı</th>
            <th>Doğru</th>
            <th>Yanlış</th>
            <th>Boş</th>
            <th>Puan </th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="rptPuanTablosu" runat="server" OnItemDataBound="rptPuanTablosu_OnItemDataBound">
            <itemtemplate>
            <tr>
                <td><a href="#" data-toggle="modal" data-target="#ogr-karne" data-sinavid="<%#Eval("SinavId") %>" data-opaqid="<%#Eval("OpaqId") %>"><%#Eval("SinavAdi") %></a></td>
                <td><%#Eval("Dogru") %></td>
                <td><%#Eval("Yanlis") %></td>
                <td><%#Eval("Bos") %></td>
                <td><%#Eval("Puan").ToDecimal().ToString("##.###")%></td>
             </tr>
        </itemtemplate>
            <footertemplate>
                    <asp:PlaceHolder ID="phEmpty" runat="server" Visible="false">
            <tr>
                <td colspan="8" class="text-center">Herhangi bir bilgi bulunamadı.</td>
            </tr>
                    </asp:PlaceHolder>
</table>
</FooterTemplate>
    </asp:Repeater>
    </tbody>
</table>

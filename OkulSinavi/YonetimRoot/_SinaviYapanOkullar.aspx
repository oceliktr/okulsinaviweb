<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_SinaviYapanOkullar.aspx.cs" Inherits="YonetimRoot_SinaviYapanOkullar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table class="table table-bordered table-hover dataTable" role="grid">
            <thead>
                <tr role="row">
                    <th>İlçe Adı</th>
                    <th>Kurum Adı</th>
                    <th>Ögr Sayısı</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptKurumlar" runat="server">
                    <ItemTemplate>
                        <tr role="row" class="odd">
                            <td><%#Eval("IlceAdi") %></td>
                            <td><%#Eval("KurumAdi") %></td>
                            <td><%#Eval("OgrenciSayisi") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            <tr role="row" class="odd">
                <th class="table-secondary" colspan="2">TOPLAM</th>
                <td class="table-secondary">
                    <asp:Literal ID="ltrToplamOgrenciSayisi" runat="server"></asp:Literal></td>
            </tr>
            </tbody>
        </table>
        <p><small>Öğrenci sayısı bu sınava katılabilecek öğrenci sayısını gösterir.</small></p>
    </form>
</body>
</html>

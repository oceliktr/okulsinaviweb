<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_OgrenciAra.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim__Rapor_OgrenciAra" %>
<form runat="server" ID="from1">
    <div class="float-right">
       <small> <asp:Literal ID="ltrKayit" runat="server"></asp:Literal></small>
    </div>
    <table class="table table-striped table-responsive-sm">
        <thead><tr>
            <th>#</th>
            <th>İlçe Adı</th>
            <th>Kurum Kodu</th>
            <th>Adı</th>
            <th>Soyadı</th>
            <th>Sınıfı</th>
            <th>Şubesi</th>
            <th>Son Giriş</th>
            <th></th>
        </tr></thead><tbody>
        <asp:Repeater ID="rptOgrenciler" runat="server" OnItemDataBound="rptOgrenciler_OnItemDataBound">
            <ItemTemplate>
                <tr>
                    <th scope="row"><asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></th>
                     <td><%#Eval("IlceAdi") %></td>
                    <td><%#Eval("KurumKodu") %></td>
                    <td><a href="#" data-toggle="modal" data-target="#ogr-islem" data-adi="<%#Eval("Adi") %> <%#Eval("Soyadi") %>" data-opaqid="<%#Eval("OpaqId") %>"><%#Eval("Adi") %></a></td>
                    <td><a href="#" data-toggle="modal" data-target="#ogr-islem" data-adi="<%#Eval("Adi") %> <%#Eval("Soyadi") %>" data-opaqid="<%#Eval("OpaqId") %>"><%#Eval("Soyadi") %></a></td>
                    <td><%#Eval("Sinifi") %></td>
                    <td><%#Eval("Sube") %></td>
                    <td><%#Eval("SonGiris").ToString()==""?"<small class='badge badge-danger'>Hiç giriş yapmadı</small>":Eval("SonGiris").ToDateTime().TarihYaz() %></td>
                    <td><button type="button" data-opaqid="<%#Eval("OpaqId") %>" data-adi="<%#Eval("Adi") %> <%#Eval("Soyadi") %>" class="btn btn-default btn-sm text-sm mr-1" data-toggle="modal" title="Loglar" data-target="#logModal"><i class="fa fa-history"></i></button></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater></tbody>
    </table>
</form>
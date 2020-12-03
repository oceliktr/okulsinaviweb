<%@ page language="C#" autoeventwireup="true" inherits="OkulSinavi_CevrimiciSinavYonetim__OgrencilerOkul, okulsinavi" enableEventValidation="false" %>
<form runat="server" ID="from1">
    <table class="table table-striped table-responsive-sm">
        <thead><tr>
            <th>#</th>
             <th>Adı Soyadı</th>
            <th>Sınıfı</th>
            <th>Şubesi</th>
            <th>Son Giriş</th>
            <th>İşlem</th>
        </tr></thead><tbody>
        <asp:Repeater ID="rptOgrenciler" runat="server" OnItemDataBound="rptOgrenciler_OnItemDataBound">
            <ItemTemplate>
                <tr>
                    <th scope="row"><asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></th>
                    <td><a href="OgrenciSinavDetay.aspx?OpaqId=<%#Eval("OpaqId") %>"><%#Eval("Adi") %> <%#Eval("Soyadi") %></a></td>
                    <td><%#Eval("Sinifi") %></td>
                    <td><%#Eval("Sube") %></td>
                    <td><%#Eval("SonGiris").ToString()==""?"<small class='badge badge-danger'>Hiç giriş yapmadı</small>":Eval("SonGiris").ToDateTime().TarihYaz() %></td>
                    <td>
                        <a class="btn btn-default btn-sm" href="OgrenciKayit.aspx?Id=<%#Eval("Id") %>"><i class="fa fa-edit"></i></a>
                        <a class="btn btn-default btn-sm" href="#" onclick="Sil('<%#Eval("OpaqId") %>')"><i class="fa fa-trash-alt"></i></a>
                    </td>
                </tr>
            </ItemTemplate>
            <footertemplate>
                <asp:PlaceHolder ID="phEmpty" runat="server" Visible="false">
                    <tr>
                        <td colspan="6" class="text-center"><%=TestSeciliDonem.SeciliDonem().Donem%> dönemine ait kayıtlı öğrenci bulunamadı.
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
            </FooterTemplate>
        </asp:Repeater></tbody>
    </table>
</form>
<%@ page language="C#" autoeventwireup="true" inherits="OkulSinavi_CevrimiciSinavYonetim_OgrenciSinavDetay, okulsinavi" enableEventValidation="false" %>

<form runat="server" id="frm1">
    <table class="table table-striped table-responsive-sm">
        <thead>
        <tr>
            <th>Oturum Adı</th>
            <th>Oturum Süresi</th>
            <th>Öğrencinin Cevabı</th>
            <th>Doğru</th>
            <th>Yanlış</th>
            <th>Baslangiç Tarihi</th>
            <th>Bitiş Tarihi </th>
            <th>Son İşlem </th>
        </tr>
        </thead>
        <tbody>
        <asp:Repeater ID="rptOturumlar" runat="server" OnItemDataBound="rptOturumlar_OnItemDataBound">
            <itemtemplate>
                <tr>
                    <td><%#Eval("OturumAdi") %></td>
                    <td><%#Eval("Sure") %> dakika</td>
                    <td><%#Eval("Cevap").ToString() %></td>
                    <td><%#Eval("Dogru") %></td>
                    <td><%#Eval("Yanlis") %></td>
                    <td><%#Eval("Baslangic").ToString()==""?"":Eval("Baslangic").ToDateTime().TarihYaz() %></td>
                    <td><%#Eval("Bitis").ToString()==""?"":Eval("Baslangic").ToDateTime().TarihYaz() %>
                        <asp:Button ID="btnBitir" runat="server" CssClass="btn btn-sm btn-danger" Text="Bitir"></asp:Button>
                    </td>
                    <td><%#Eval("SonIslem").ToString()==""?"":Eval("SonIslem").ToDateTime().TarihYaz() %></td>
                </tr>
            </itemtemplate>
            <footertemplate>
                <asp:PlaceHolder ID="phEmpty" runat="server" Visible="false">
                    <tr>
                        <td colspan="8" class="text-center">Öğrenci bu sınava girmedi.
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </footertemplate>

        </asp:Repeater>
        </tbody>
    </table>

</form>
<%@ page language="C#" autoeventwireup="true" inherits="OkulSinavi_CevrimiciSinavYonetim__Rapor_OgrenciIslem, okulsinavi" enableEventValidation="false" %>

<form runat="server" id="from1">
    <div class="float-right">
        <small>
            <asp:Literal ID="ltrKayit" runat="server"></asp:Literal></small>
    </div>
    <table class="table table-striped table-responsive">
        <thead>
            <tr>
                <th>#</th>
                <th>Sınav - Oturum Adı</th>
                <th>Cevapları</th>
                <th>Tarihi</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptOgrenciler" runat="server" OnItemDataBound="rptOgrenciler_OnItemDataBound">
                <itemtemplate>
                <tr>
                    <th scope="row"><asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></th>
                    <td><a href="#" data-toggle="modal" data-target="#ogr-karne" data-sinavid="<%#Eval("SinavId") %>" data-opaqid="<%#Eval("OpaqId") %>"><%#Eval("SinavAdi") %></a> - <%#Eval("OturumAdi") %></td>
                    <td><%#Eval("Cevap") %>   <br/>
                      <strong>Doğru Sayısı:</strong>  <%#Eval("Dogru") %> - 
                      <strong>Yanlış Sayısı:</strong>  <%#Eval("Yanlis") %></td>
                    <td>
                        <strong>Başlama Tarihi:</strong> <%#Eval("Baslangic").ToDateTime().TarihYaz() %><br/>
                        <strong>Bitiş Tarihi:</strong> <%#Eval("Bitis").ToString()==""?"-":Eval("Bitis").ToDateTime().TarihYaz() %> <%#Eval("Bitti").ToInt32()==0?"":"<small class='badge badge-success'>Değerlendirme Bitti</small>" %><br/>
                    <strong>Son Cevap Tarihi:</strong> <%#Eval("SonIslem").ToString()==""?"":Eval("SonIslem").ToDateTime().TarihYaz() %></td>
                </tr>
            </itemtemplate>
            </asp:Repeater>
        </tbody>
    </table>
</form>

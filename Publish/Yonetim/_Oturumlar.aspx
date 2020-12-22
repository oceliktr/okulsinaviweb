<%@ page language="C#" autoeventwireup="true" inherits="Okul_SinaviYonetim_Oturumlar, okulsinavi" enableEventValidation="false" %>

<table class="table table-striped table-responsive-sm">
    <thead>
        <tr>
            <th>#</th>
            <th>Oturum Adı</th>
            <th>Açıklama</th>
            <th>Süre</th>
            <th>Tarih</th>
            <th>İşlem</th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="rptOturumlar" runat="server">
            <itemtemplate>
            <tr>
                <th scope="row"><%#Eval("SiraNo") %></th>
                <td><a href="SinavSorulari.aspx?OturumId=<%#Eval("Id") %>"><%#Eval("OturumAdi") %></a></td>
                <td class="w-25"><%#Eval("Aciklama") %></td>
                <td class="align-content-center"><%#Eval("Sure") %></td>
                <td>
                    <div class="row"><label class="col-5">Başlama Tarihi</label> <%#Eval("BaslamaTarihi").ToDateTime().TarihYaz() %></div> 
                    <div class="row"><label class="col-5">Son Giriş</label> <%#Eval("BitisTarihi").ToDateTime().TarihYaz() %></div>
                    <div class="row"><label class="col-5"> Oturum Bitiş</label> <%#Eval("BitisTarihi").ToDateTime().AddMinutes(Eval("Sure").ToInt32()).TarihYaz() %></div>
                </td>
                <td>
                    <a class="btn btn-default btn-sm" href="OturumKayit.aspx?SinavId=<%#Eval("SinavId") %>&id=<%#Eval("Id") %>"><i class="fa fa-edit"></i></a>
                    <a class="btn btn-default btn-sm" href="#" onclick="Sil(<%#Eval("Id") %>)"><i class="fa fa-trash-alt"></i></a>
                </td>
            </tr>
        </itemtemplate>
        </asp:Repeater>
    </tbody>
</table>
<div class="col-12">
    <div class="callout callout-info help-sinav mt-2">
        <h4>Sınav Sonuçlarının Açıklanması ve Cevap Anahtarının Gösterimi</h4>

        <p>
            Öğrenciler yukarıdaki oturumları tamamlayınca doğru yanlış sayılarını ve sınav puanlarını görebileceklerdir.<br />
            Cevap anahtarlarını, cevaplarını ve soruları ancak oturumlar bittikten sonra görebileceklerdir. Oturumların bitiş saati <strong>
                <asp:Literal ID="ltrCevapAnahtariBilgi" runat="server"></asp:Literal></strong>
        </p>
    </div>
</div>

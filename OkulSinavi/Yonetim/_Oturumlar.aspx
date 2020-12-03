<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_Oturumlar.aspx.cs" Inherits="Okul_SinaviYonetim_Oturumlar" %>

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

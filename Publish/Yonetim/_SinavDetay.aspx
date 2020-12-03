<%@ page language="C#" autoeventwireup="true" inherits="OkulSinavi_CevrimiciSinavYonetim_SinavDetay, okulsinavi" enableEventValidation="false" %>


<table class="table table-striped table-responsive-sm">
    <tbody>
        <tr>
            <td class="w-25"><strong>Sınav adı: </strong></td>
            <td>
                <asp:Literal ID="ltrSinavAdi" runat="server"></asp:Literal></td>
        </tr>
    <tr>
        <td><strong>Sınıf Seviyesi: </strong></td>
        <td>
            <asp:Literal ID="ltrSinif" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td><strong>Puanlama: </strong></td>
        <td>
            <asp:Literal ID="ltrPuanlama" runat="server"></asp:Literal> üzerinden</td>
    </tr>
    <tr>
        <td><strong>Açıklama: </strong></td>
        <td>
            <asp:Literal ID="ltrAciklama" runat="server"></asp:Literal></td>
    </tr>
    
    <tr>
        <td><strong>Sınav Durumu: </strong></td>
        <td>
            <asp:Literal ID="ltrAktif" runat="server"></asp:Literal></td>
    </tr>
    
    <tr>
        <td><strong>Oturum Tercihi: </strong></td>
        <td>
            <asp:Literal ID="ltrOturumTarcihi" runat="server"></asp:Literal></td>
    </tr>
    </tbody>
</table>

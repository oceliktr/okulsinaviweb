<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UstMenu.ascx.cs" Inherits="Okul_SinaviYonetim_UstMenu" %>
<%
     string sayfaAdi = new System.IO.FileInfo(HttpContext.Current.Request.Url.AbsolutePath).Name;

     if (sayfaAdi == "OgrenciSifreleri.aspx")
         liOkulOgrListesi.Attributes.Add("class", "nav-item active");
     else if (sayfaAdi == "Sinavlar.aspx"||sayfaAdi=="SinavDetay.aspx"||sayfaAdi=="OkulPuanDetay.aspx")
         liSinavlar.Attributes.Add("class", "nav-item active");
     else if (sayfaAdi == "SinavYonetim.aspx"||sayfaAdi=="SinavKayit.aspx")
         liSinavYonetim.Attributes.Add("class", "nav-item active");
    else if (sayfaAdi == "Ogrenciler.aspx")
         liOgrenciler.Attributes.Add("class", "nav-item active");
     else if (sayfaAdi == "SinavRapor.aspx")
         liSinavRapor.Attributes.Add("class", "nav-item active");

%>

<nav class="navbar navbar-expand-lg navbar-dark bg-aqua">
    <a class="navbar-brand" href="#"></a>
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav">
            <li class="nav-item" id="liOgrenciler" runat="server" Visible="False"><a class="nav-link" href="/Yonetim/Ogrenciler.aspx">Öğrenciler</a></li>
            <li class="nav-item" id="liOkulOgrListesi" runat="server" visible="False"><a class="nav-link" href="/Yonetim/OkulOgrenciListesi.aspx">Öğrenci Listesi</a></li>
            <li class="nav-item" id="liSinavlar" runat="server" Visible="False"><a class="nav-link" href="/Yonetim/Sinavlar.aspx">Sınavlar</a></li>
            <li class="nav-item" id="liSinavYonetim" runat="server" Visible="False"><a class="nav-link" href="/Yonetim/SinavYonetim.aspx">Sınav Yönetim</a></li>
            <li class="nav-item" id="liSinavRapor" runat="server" Visible="False"><a class="nav-link" href="/Yonetim/SinavRapor.aspx">Raporlar</a></li>
             <li class="nav-item" id="liDemo" runat="server" Visible="False"><a class="nav-link" href="/Yonetim/DemoGiris.aspx">Demo Sınav</a></li>
        </ul>
    </div>
</nav>

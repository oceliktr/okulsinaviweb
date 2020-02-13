<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminMenu.ascx.cs" Inherits="ODM.AdminAdminMenu" %>

<ul class="sidebar-menu">
    <li class="header">MENU</li>
    <li><a href="Bilgilerim.aspx"><i class="fa fa-file-image-o"></i><span>Bilgilerim</span></a></li>
    <li id="liBaranslar" runat="server" visible="False"><a href="Branslar.aspx"><i class="fa fa-file-image-o"></i><span>Branşlar</span></a></li>
    <li id="liKurumlar" runat="server" visible="False"><a href="Kurumlar.aspx"><i class="fa fa-file-image-o"></i><span>Kurumlar</span></a></li>
    <li id="liSinavEvraklari" runat="server" visible="False"><a href="SinavEvrak.aspx"><i class="fa fa-file-image-o"></i><span>Sınav Evrakları</span></a></li>
    <li id="liKullanicilar" runat="server" visible="False"><a href="Kullanicilar.aspx"><i class="fa fa-file-image-o"></i><span>Kullanıcılar</span></a></li>
    <li id="liSinavlar" runat="server" visible="False"><a href="Sinavlar.aspx"><i class="fa fa-file-image-o"></i><span>Sınavlar</span></a></li>
    <li id="liKazanimKarne" runat="server" visible="False"><a href="KazanimKarne.aspx"><i class="fa fa-file-image-o"></i><span>Kazanım Karneleri</span></a></li>
    <li id="liLgsKazanimKarne" runat="server" visible="False"><a href="LGSKazanimKarne.aspx"><i class="fa fa-file-image-o"></i><span>Kazanım Karneleri</span></a></li>
    <li id="liCkDataYukle" runat="server" visible="False"><a href="CKDataYukle.aspx"><i class="fa fa-file-image-o"></i><span>CK Yazılım Veri Yükle</span></a></li>
    <li id="liLgsRapor" runat="server" visible="False"><a href="LgsRapor.aspx"><i class="fa fa-file-image-o"></i><span>Sınav Rapor</span></a></li>
    <li id="liKazanimKarneAdmin" runat="server" visible="False"><a href="KazanimKarneAdmin.aspx"><i class="fa fa-file-image-o"></i><span>Kazanım Karneleri</span></a></li>
    <li id="liPuanlayciIslemleri" runat="server" visible="False"><a href="PuanlayiciIslemleri.aspx"><i class="fa fa-file-image-o"></i><span>Puanlayıcı İşlemleri</span></a></li>
    <li id="liKazanim" runat="server" visible="False"><a href="Kazanimlar.aspx"><i class="fa fa-file-image-o"></i><span>Kazanımlar</span></a></li>
    <li id="liRubrik" runat="server" visible="False"><a href="Rubrik.aspx"><i class="fa fa-file-image-o"></i><span>Rubrik</span></a></li>
    <li id="liDegerlendirme" runat="server" visible="False"><a href="Degerlendirme.aspx"><i class="fa fa-file-image-o"></i><span>Değerlendirme Ekranı</span></a></li>
    <li id="liDegerlendirmeUst" runat="server" visible="False"><a href="DegerlendirmeUst.aspx"><i class="fa fa-file-image-o"></i><span>Üst Değerlendirici Ekranı</span></a></li>
    <li id="liLgsSoruBankasi" runat="server" visible="False"><a href="/LGSSoruBank/"><i class="fa fa-file-image-o"></i><span>LGS Soru Bankası</span></a></li>
    <li id="liSoruBankasi" runat="server" visible="False"><a href="/SoruBank/"><i class="fa fa-file-image-o"></i><span>Soru Bankası</span></a></li>
</ul>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminMenu.ascx.cs" Inherits="OkulSinavi.AdminAdminMenu" %>

<ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
    <li class="nav-item"><a class="nav-link" href="/Yonetim/Bilgilerim.aspx"><i class="nav-icon fa fa-user"></i><p>Bilgilerim</p></a></li>
    <li class="nav-item" id="liKullanicilar" runat="server" visible="False"><a class="nav-link" href="/Yonetim/Kullanicilar.aspx"><i class="nav-icon fa fa-users"></i><p>Kullanıcılar</p></a></li>
     <li class="nav-item" id="liOkulOgrListesi" runat="server" visible="False"><a class="nav-link" href="/Yonetim/OkulOgrenciListesi.aspx"><i class="nav-icon fa fa-users"></i>Öğrenci Listesi</a></li>
    <li class="nav-item" id="liSinavlar" runat="server" Visible="False"><a class="nav-link" href="/Yonetim/Sinavlar.aspx"><i class="nav-icon fa fa-edit"></i>Sınavlar</a></li>
    <li class="nav-item" id="liSinavYonetim" runat="server" Visible="False"><a class="nav-link" href="/Yonetim/SinavYonetim.aspx"><i class="nav-icon fa fa-user-cog"></i>Sınav Yönetim</a></li>
     <li class="nav-item" id="liDemo" runat="server" Visible="False"><a class="nav-link" href="/Yonetim/DemoGiris.aspx"><i class="nav-icon fa fa-file"></i>Demo Sınav</a></li>
</ul>

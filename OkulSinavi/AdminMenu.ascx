<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminMenu.ascx.cs" Inherits="OkulSinavi.AdminAdminMenu" %>

<ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
    <li class="nav-item"><a class="nav-link" href="/Bilgilerim.aspx"><i class="nav-icon fa fa-user"></i><p>Bilgilerim</p></a></li>
    <li class="nav-item" id="liBaranslar" runat="server" visible="False"><a class="nav-link" href="/Branslar.aspx"><i class="nav-icon fa fa-code-branch"></i><p>Branşlar</p></a></li>
    <li class="nav-item" id="liKurumlar" runat="server" visible="False"><a class="nav-link" href="/Kurumlar.aspx"><i class="nav-icon fa fa-school"></i><p>Kurumlar</p></a></li>
    <li class="nav-item" id="liKullanicilar" runat="server" visible="False"><a class="nav-link" href="/Kullanicilar.aspx"><i class="nav-icon fa fa-users"></i><p>Kullanıcılar</p></a></li>
    <li class="nav-item" id="liSinavYonetim" runat="server" visible="False"><a class="nav-link" href="/Yonetim/Default2.aspx"><i class="nav-icon fa fa-edit"></i><p>Çevrimiçi Sınav</p></a></li>
</ul>

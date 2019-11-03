<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminMenu.ascx.cs" Inherits="SoruBank.AdminAdminMenu" %>

<ul class="sidebar-menu">
    <li class="header">MENU</li>
   <li id="liSorular" runat="server" Visible="True"><a href="Sorular.aspx"><i class="fa fa-file-image-o"></i><span>Sorular</span></a></li>
   <li><a href="Bilgilerim.aspx"><i class="fa fa-file-image-o"></i><span>Bilgilerim</span></a></li>
   <li><a href="Sorularim.aspx"><i class="fa fa-file-image-o"></i><span>Sorularım</span></a></li>
   <li><a href="SoruEkle.aspx"><i class="fa fa-file-image-o"></i><span>Soru Ekle</span></a></li>
   <li id="liSinavModulu" runat="server" Visible="False"><a href="/ODM/"><i class="fa fa-file-image-o"></i><span>Sınav Modülü</span></a></li>
   </ul>

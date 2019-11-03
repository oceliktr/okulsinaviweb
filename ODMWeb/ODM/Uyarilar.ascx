<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Uyarilar.ascx.cs" Inherits="ODM.AdminUyarilar"  %>



    <asp:Panel ID="pnlUyariKirmizi" Visible="false" runat="server" CssClass="alert alert-danger alert-dismissible">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
        <h4><i class="icon fa fa-ban"></i>Uyarı!</h4>
        <asp:Literal ID="ltrUyariKirmizi" runat="server"></asp:Literal>
 </asp:Panel>
   <asp:Panel ID="pnlBilgilendirme" Visible="false" runat="server" CssClass="alert alert-info alert-dismissible">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
        <h4><i class="icon fa fa-info"></i>Bilgi!</h4>
       <asp:Literal ID="ltrBilgilendirme" runat="server"></asp:Literal>
   </asp:Panel>
    <asp:Panel ID="pnlUyariTuruncu" Visible="false" runat="server" CssClass="alert alert-warning alert-dismissible">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
        <h4><i class="icon fa fa-warning"></i>Uyarı!</h4>
        <asp:Literal ID="ltrUyariTuruncu" runat="server"></asp:Literal>
 </asp:Panel>
    <asp:Panel ID="pnlOnay" Visible="false" runat="server" CssClass="alert alert-success alert-dismissible">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
        <h4><i class="icon fa fa-check"></i>Bilgi!</h4>
        <asp:Literal ID="ltrOnay" runat="server"></asp:Literal>
    </asp:Panel>


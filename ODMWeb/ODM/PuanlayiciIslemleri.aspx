<%@ Page Title="" Language="C#" MasterPageFile="~/ODM/MasterPage.master" AutoEventWireup="true" CodeFile="PuanlayiciIslemleri.aspx.cs" Inherits="ODM.OdmPuanlayiciIslemleri" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Puanlayıcı İşlemleri</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
                <li class="active">Puanlayıcı İşlemleri</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="box">
                        <div class="box-header">
                            <div class="col-md-2">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSinavlar" ValidationGroup="form2"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBranslar" ValidationGroup="form2"></asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btnListele" CssClass="btn btn-primary" runat="server" ValidationGroup="form2" Text="Listele" OnClick="btnListele_OnClick" />
                            </div>
                             <div class="col-md-1">
                                <asp:Button ID="btnGrupla" CssClass="btn btn-primary" runat="server" ValidationGroup="form3" Text="Cevapları Öğretmenlere Dağıt" OnClick="btnGrupla_OnClick" />
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box box-warning">
                                        <table class="table table-bordered table-hover dataTable" role="grid">
                                            <thead>
                                                <tr role="row">
                                                    <th>#</th>
                                                    <th>İlçe</th>
                                                    <th>Kurumu</th>
                                                    <th>Adı Soyadı</th>
                                                    <th>Branş</th>
                                                    <th>Grup</th>
                                                    <th>Okunacak Cevap Sayısı</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptKullanicilar" runat="server" OnItemDataBound="rptKullanicilar_OnItemDataBound">
                                                    <ItemTemplate>
                                                        <tr role="row" class="odd">
                                                            <td>
                                                                <asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></td>
                                                            <td>
                                                                <asp:Literal ID="ltrIlce" runat="server"></asp:Literal></td>
                                                            <td>
                                                                <asp:Literal ID="ltrKurumAdi" runat="server"></asp:Literal></td>
                                                            <td><%#Eval("AdiSoyadi") %></td>
                                                            <td>
                                                                <asp:Literal ID="ltrBrans" runat="server"></asp:Literal></td>
                                                            <td><%#Eval("Grup") %></td>
                                                            <td>
                                                                <asp:Literal ID="ltrOkunacakCevapSayisi" runat="server"></asp:Literal></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                              
                            </div>
                        </div>
                    </div>
                </div>
                </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
</asp:Content>


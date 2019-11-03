<%@ Page Title="" Language="C#" MasterPageFile="~/ODM/MasterPage.master" AutoEventWireup="true" CodeFile="Ogrenciler.aspx.cs" Inherits="ODM_Ogrenciler" %>


<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Sınav Sonuçları</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
                <li class="active">Sınav Sonuçları</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="box">
                        <div class="box-header">
                            <label class="col-sm-1 control-label">Aktif Sınav:</label>
                            <div class="col-md-2">
                                <asp:Literal ID="ltrAktifSinav" runat="server"></asp:Literal>
                            </div>
                            <label class="col-sm-1 control-label">Dosya Seç:</label>
                             <div class="col-md-2">
                                <asp:FileUpload ID="fuOgrenci" runat="server" />
                            </div>

                            <div class="col-md-1">
                                <asp:Button ID="btnListele" CssClass="btn btn-primary" runat="server" Text="Listele" OnClick="btnListele_OnClick" />
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
                                                    <th>Kurum Kodu</th>
                                                    <th>Okulu</th>
                                                    <th>Tc Kimlik</th>
                                                    <th>Adı</th>
                                                    <th>Soyadı</th>
                                                    <th>Okul No </th>
                                                    <th>Sınıf </th>
                                                    <th>Şube</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptSonuclar" runat="server">
                                                    <ItemTemplate>
                                                        <tr role="row" class="odd">
                                                            <td>
                                                                <asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></td>
                                                            <td><%#Eval("Adi") %> </td>
                                                            <td><%#Eval("SoyAdi") %></td>
                                                            <td><%#Eval("Sinifi") %> </td>
                                                            <td> <%#Eval("Sube") %></td>
                                                            <td><%#Eval("BransAdi") %></td>
                                                            <td><%#Eval("AuNotu") %></td>
                                                            <td><%#Eval("OptikNotu") %></td>
                                                            <td><%#Eval("Toplam") %></td>
                                                           
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
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>


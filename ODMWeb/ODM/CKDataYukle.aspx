<%@ Page Title="" Language="C#" MasterPageFile="~/ODM/MasterPage.master" AutoEventWireup="true" CodeFile="CKDataYukle.aspx.cs" Inherits="ODM.CKDataYukle" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>CK Yazýlým Veri Yükle</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriþ</a></li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-lg-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="box box-solid box-default">

                        <div class="box-body">


                            <div class="col-lg-12 form-horizontal">

                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Sýnav :  </label>
                                    <div class="col-sm-3">  <asp:DropDownList runat="server" CssClass="form-control" ValidationGroup="form" ID="ddlSinavlar"></asp:DropDownList>
                                    </div>
                                    
                                    <div class="col-sm-5">
                                       Yüklenen dosyada sýnav adý bilgisi ( {SinavAdi} taðý ) varsa sýnav seçmeyiniz.<br/>
                                        <asp:LinkButton ID="lnkSil" runat="server" OnClick="lnkSil_OnClick" OnClientClick="return confirm('Seçili sýnava ait tüm verileri silmek istediðinizden emin misiniz?');">Sýnavý sil</asp:LinkButton>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-4 control-label">CK Oluþturma Yazýlýmýndan Alýnan Veri Dosyasý</label>
                                    <div class="col-sm-3">
                                        <asp:FileUpload ID="fuFile" CssClass="form-control" runat="server" placeholder="Fotoðraf seçiniz"></asp:FileUpload>
                                    </div>

                                    <div class="col-sm-1">
                                        <asp:Button ID="btnDosyaEkle" ValidationGroup="form" CssClass="btn btn-primary pull-left" runat="server" Text="Yükle" OnClick="btnDosyaEkle_OnClick" />
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


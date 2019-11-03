<%@ Page Title="" Language="C#" MasterPageFile="~/ODM/MasterPage.master" AutoEventWireup="true" CodeFile="Degerlendirme.aspx.cs" Inherits="ODM.OdmDegerlendirme" %>


<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Değerlendirme Ekranı</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
                <li class="active">Değerlendirme Ekranı</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="box">
                        <div class="box-body" id="divDegerlendirmeEkrani" runat="server">
                            <div class="row">
                                <div class="col-md-7">
                                    <div class="box box-solid box-danger">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Öğrenci Cevabı</h3>
                                        </div>
                                        <div class="box-body">
                                            <div class="progress-group">
                                                <span class="progress-text">Kalan Cevap Kağıdı</span>
                                                <span class="progress-number"><b>
                                                    <asp:Literal ID="ltrKalanCKSayisi" runat="server"></asp:Literal></b>/<asp:Literal ID="ltrAtananCKSayisi" runat="server"></asp:Literal></span>

                                                <div class="progress sm">
                                                    <div class="progress-bar progress-bar-aqua" id="divProgres" runat="server"></div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <asp:Button ID="btnKaydet" CssClass="btn btn-primary pull-right" runat="server" ValidationGroup="form" Text="Kaydet" OnClick="btnKaydet_OnClick" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Image ID="imgDosya" CssClass="img-responsive pad" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="box-footer">

                                            <div class="row">
                                                <div class="col-md-2">
                                                    <asp:RadioButton GroupName="x" ID="rbBos" runat="server" Text=" Boş" />
                                                </div>
                                                <div class="col-md-1"></div>
                                                <div class="col-md-2">
                                                    <asp:RadioButton GroupName="x" ID="rbYanlis" runat="server" Text=" Yanlış" />
                                                </div>
                                                <div class="col-md-1"></div>
                                                <div class="col-md-2">
                                                    <asp:RadioButton GroupName="x" ID="rbKismi" runat="server" Text=" Kısmi Puan" />
                                                </div>
                                                <div class="col-md-1"></div>
                                                <div class="col-md-2">
                                                    <asp:RadioButton CssClass="" GroupName="x" ID="rbTampuan" runat="server" Text=" Tam Puan" />
                                                </div>
                                            </div>
                                                    <asp:PlaceHolder ID="phUyariYeniSoru" runat="server"></asp:PlaceHolder>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="box box-solid box-warning">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Rubrik</h3>
                                        </div>
                                        <div class="box-body">
                                            <div class="row" id="divKismiPuan" runat="server">
                                                <div class="col-md-3"><b>Kısmi Puan :</b></div>
                                                <div class="col-md-9">
                                                    <asp:Literal ID="ltrKismiPuan" runat="server"></asp:Literal>
                                                    puan
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-3"><b>Tam Puan :</b></div>
                                                <div class="col-lg-9">
                                                    <asp:Literal ID="ltrTamPuan" runat="server"></asp:Literal>
                                                    puan
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3"><b>Ders Adı :</b></div>
                                                <div class="col-sm-9">
                                                    <asp:Literal ID="ltrBransAdi" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3"><b>Soru No :</b></div>
                                                <div class="col-md-9">
                                                    <asp:HiddenField ID="hfSoruNo" runat="server" />
                                                    <asp:Literal ID="ltrSoruNo" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <br />

                                                <div class="nav-tabs-custom">
                                                    <ul class="nav nav-tabs">
                                                        <li class="active"><a href="#tab_1" data-toggle="tab" aria-expanded="false">Doğru Cevap</a></li>
                                                        <li class="" id="divKismiCevap" runat="server"><a href="#ContentPlaceHolder1_tab_2" data-toggle="tab" aria-expanded="false">Kısmi Cevap</a></li>
                                                        <li class=""><a href="#tab_3" data-toggle="tab" aria-expanded="true">Yanlış Cevap</a></li>
                                                        <li class=""><a href="#tab_4" data-toggle="tab" aria-expanded="true">Soru</a></li>
                                                    </ul>
                                                    <div class="tab-content box-solid box-warning">
                                                        <div class="tab-pane active" id="tab_1">
                                                            <asp:Literal ID="ltrDogruCevap" runat="server"></asp:Literal>
                                                        </div>
                                                        <div class="tab-pane" id="tab_2" runat="server">
                                                            <asp:Literal ID="ltrKismiCevap" runat="server"></asp:Literal>
                                                        </div>
                                                        <div class="tab-pane" id="tab_3">
                                                            <asp:Literal ID="ltrYanlisCevap" runat="server"></asp:Literal>
                                                        </div>
                                                        <div class="tab-pane" id="tab_4">
                                                            <asp:Literal ID="ltrSoru" runat="server"></asp:Literal>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

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

<%@ Page Title="" Language="C#" MasterPageFile="~/ODM/MasterPage.master" AutoEventWireup="true" CodeFile="CKDataYukle.aspx.cs" Inherits="ODM.CKDataYukle" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>CK Yaz�l�m Veri Y�kle</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giri�</a></li>
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
                                    <label class="col-sm-4 control-label">S�nav :  </label>
                                    <div class="col-sm-3">  <asp:DropDownList runat="server" CssClass="form-control" ValidationGroup="form" ID="ddlSinavlar"></asp:DropDownList>
                                    </div>
                                    
                                    <div class="col-sm-5">
                                       Y�klenen dosyada s�nav ad� bilgisi ( {SinavAdi} ta�� ) varsa s�nav se�meyiniz.<br/>
                                        <asp:LinkButton ID="lnkSil" runat="server" OnClick="lnkSil_OnClick" OnClientClick="return confirm('Se�ili s�nava ait t�m verileri silmek istedi�inizden emin misiniz?');">S�nav� sil</asp:LinkButton>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-4 control-label">CK Olu�turma Yaz�l�m�ndan Al�nan Veri Dosyas�</label>
                                    <div class="col-sm-3">
                                        <asp:FileUpload ID="fuFile" CssClass="form-control" runat="server" placeholder="Foto�raf se�iniz"></asp:FileUpload>
                                    </div>

                                    <div class="col-sm-1">
                                        <asp:Button ID="btnDosyaEkle" ValidationGroup="form" CssClass="btn btn-primary pull-left" runat="server" Text="Y�kle" OnClick="btnDosyaEkle_OnClick" />
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


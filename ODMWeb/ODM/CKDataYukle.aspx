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
                                    <label class="col-sm-4 control-label">CK Olu�turma Yaz�l�m�ndan Al�nan Veri Dosyas�</label>
                                    <div class="col-sm-3">
                                        <asp:FileUpload ID="fuFile" CssClass="form-control" runat="server" placeholder="Foto�raf se�iniz"></asp:FileUpload>
                                    </div>

                                     <div class="col-sm-1">
                                        <asp:Button ID="btnDosyaEkle" ValidationGroup="form" CssClass="btn btn-primary pull-left" runat="server" Text="Y�kle" OnClick="btnDosyaEkle_OnClick" />
                                    </div>
                                </div>
                                
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">K�t�k Dosyas�</label>
                                    <div class="col-sm-3">
                                        <asp:FileUpload ID="fuFileKutuk" CssClass="form-control" runat="server" placeholder="Foto�raf se�iniz"></asp:FileUpload>
                                    </div>

                                    <div class="col-sm-1">
                                        <asp:Button ID="btnKutuk" ValidationGroup="form" CssClass="btn btn-primary pull-left" runat="server" Text="Y�kle" OnClick="btnKutuk_OnClick" />
                                    </div>
                                </div>
                                
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">��renci Cevaplar� Dosyas�</label>
                                    <div class="col-sm-3">
                                        <asp:FileUpload ID="fuFileOc" CssClass="form-control" runat="server" placeholder="Foto�raf se�iniz"></asp:FileUpload>
                                    </div>

                                    <div class="col-sm-1">
                                        <asp:Button ID="btnOgrCevap" ValidationGroup="form" CssClass="btn btn-primary pull-left" runat="server" Text="Y�kle" OnClick="btnOgrCevap_OnClick" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Karne Sonu�lar� Dosyas�</label>
                                    <div class="col-sm-3">
                                        <asp:FileUpload ID="fuFileKarneSonuc" CssClass="form-control" runat="server" placeholder="Foto�raf se�iniz"></asp:FileUpload>
                                    </div>

                                    <div class="col-sm-1">
                                        <asp:Button ID="btnKarneSonuc" ValidationGroup="form" CssClass="btn btn-primary pull-left" runat="server" Text="Y�kle" OnClick="btnKarneSonuc_OnClick" />
                                    </div>
                                </div>
                                <asp:Label runat="server" ID="lblBilgi" Text="Label"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </div>
</asp:Content>


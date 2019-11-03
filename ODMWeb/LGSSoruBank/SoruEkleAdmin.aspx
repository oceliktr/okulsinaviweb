<%@ Page Title="" Language="C#" MasterPageFile="~/LGSSoruBank/MasterPage.master" AutoEventWireup="true" CodeFile="SoruEkleAdmin.aspx.cs" Inherits="LGSSoruBank_SoruEkleAdmin" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scriptManager1" runat="server"></asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hfSoruId" Value="0"/>
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Erzurum Ölçme Değerlendirme Merkezi
                <small>
                    <asp:Literal ID="ltrDonemAdiBreadCrumb" runat="server"></asp:Literal></small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
                <li class="active">Soru Ekleme Formu</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>

                    <div class="col-md-push-3 col-md-6" id="divAsama1" runat="server">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Soru Ekleme Formu</h3>
                            </div>
                            <div class="box-body">
                                <div class="form-group">
                                    <label> Sınav Adı </label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSinavlar"></asp:DropDownList>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                                    <div class="form-group">
                                        <label>
                                            Branşı
                                        </label>
                                        <asp:DropDownList CssClass="form-control" ID="ddlBrans" ValidationGroup="form" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrans_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Soru Yazarı
                                        </label>
                                        <asp:DropDownList CssClass="form-control" ID="ddlSoruYazarlari" ValidationGroup="form" runat="server"></asp:DropDownList>
                                    </div>
                                </ContentTemplate></asp:UpdatePanel>
                                <div class="form-group">
                                    <label>
                                        Sınıf Düzeyi
                                        <asp:RequiredFieldValidator ControlToValidate="ddlSinif" ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                    <asp:DropDownList ID="ddlSinif" CssClass="form-control" ValidationGroup="form" runat="server">
                                        <asp:ListItem Value="">--- Seçiniz ---</asp:ListItem>
                                        <asp:ListItem Value="7">7. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="8">8. Sınıf</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label> Kazanım <asp:RequiredFieldValidator ControlToValidate="txtKazanim" ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                    <asp:TextBox ID="txtKazanim" MaxLength="250" CssClass="form-control" ValidationGroup="form" runat="server" placeholder="Kazanım numarası / Kazanım adı"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label> Dosya Yükle </label>
                                    <asp:FileUpload ID="fuDosya" CssClass="form-control" runat="server" />
                                </div>
                                <div class="form-group">
                                    <label> Onay </label>
                                    <asp:CheckBox ID="cbOnay" runat="server" CssClass="form-control" Text="İncelendi"/>
                                </div>
                            </div>

                            <div class="box-footer">
                                <asp:Button ID="btnYukle" CssClass="btn btn-primary pull-right" ValidationGroup="form" runat="server" Text="Yükle" OnClick="btnYukle_OnClick" />
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


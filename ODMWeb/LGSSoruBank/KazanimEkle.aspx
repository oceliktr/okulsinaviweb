<%@ Page Title="" Language="C#" MasterPageFile="~/LGSSoruBank/MasterPage.master" AutoEventWireup="true" CodeFile="KazanimEkle.aspx.cs" Inherits="LGSSoruBank_KazanimEkle" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Erzurum Ölçme Değerlendirme Merkezi</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
                <li class="active">Kazanım Kayıt Formu</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <asp:HiddenField ID="hfId" runat="server" Value="0"/>
                    <div class="col-md-push-3 col-md-6" id="divAsama1" runat="server">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Kazanım Kayıt Formu</h3>
                            </div>
                            <div class="box-body">
                                <div class="form-group">
                                    <label>
                                        Branş
                                        <asp:RequiredFieldValidator ControlToValidate="ddlBrans" ValidationGroup="form" ID="RequiredFieldValidator2" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                    <asp:DropDownList CssClass="form-control" ID="ddlBrans" ValidationGroup="form" runat="server"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Sınıf Düzeyi
                                        <asp:RequiredFieldValidator ControlToValidate="ddlSinif" ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                    <asp:DropDownList ID="ddlSinif" CssClass="form-control" ValidationGroup="form" runat="server">
                                        <asp:ListItem Value="">--- Seçiniz ---</asp:ListItem>
                                        <asp:ListItem Value="5">5. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="6">6. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="7">7. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="8">8. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="9">9. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="10">10. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="11">11. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="12">12. Sınıf</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Kazanım
                                        <asp:RequiredFieldValidator ControlToValidate="txtKazanimNo" ValidationGroup="form" ID="RequiredFieldValidator4" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                    <asp:TextBox ID="txtKazanimNo" MaxLength="15" CssClass="form-control" ValidationGroup="form" runat="server" placeholder="Kazanım numarası"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Kazanım
                                        <asp:RequiredFieldValidator ControlToValidate="txtKazanim" ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                    <asp:TextBox ID="txtKazanim" MaxLength="250" CssClass="form-control" ValidationGroup="form" runat="server" placeholder="Kazanım adı"></asp:TextBox>
                                </div>
                            </div>

                            <div class="box-footer">
                                <asp:Button ID="btnKaydet" CssClass="btn btn-primary pull-right" ValidationGroup="form" runat="server" Text="Kaydet" OnClick="btnKaydet_OnClick" />
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


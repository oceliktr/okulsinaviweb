<%@ Page Title="" Language="C#" MasterPageFile="~/ODM/MasterPage.master" AutoEventWireup="true" CodeFile="Kullanicilar.aspx.cs" Inherits="ODM.OdmKullanicilar" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Kullanıcılar</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
                <li class="active">Kullanıcılar</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="box">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs">
                                <li id="tabliSayfalar" runat="server" class="active"><a href="#ContentPlaceHolder1_Sayfalar" data-toggle="tab" aria-expanded="true"><i class="fa fa-fw fa-list-alt"></i>Kullanıcılar</a></li>
                                <li id="tabliKayit" runat="server"><a href="#ContentPlaceHolder1_Kayit" data-toggle="tab" aria-expanded="false"><i class="fa fa-fw fa-edit"></i>Kayıt Formu</a></li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane active" id="Sayfalar" runat="server">
                                    <div class="box-header">
                                        <div class="col-md-2">

                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlIlceler" ValidationGroup="form2" AutoPostBack="True" OnSelectedIndexChanged="ddlIlceler_OnSelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlKurumlar" ValidationGroup="form2"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBranslar" ValidationGroup="form2"></asp:DropDownList>
                                        </div>
                                        
                                        <div class="col-md-2">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlYetki" ValidationGroup="form2">
                                                <asp:ListItem Value="" Text="Yetki Seçiniz"></asp:ListItem>
                                                <asp:ListItem Value="Root" Text="Administrotor"></asp:ListItem>
                                                <asp:ListItem Value="Admin" Text="ÖDM Ekibi"></asp:ListItem>
                                                <asp:ListItem Value="IlceMEMYetkilisi" Text="İlçe MEM"></asp:ListItem>
                                                <asp:ListItem Value="OkulYetkilisi" Text="Okul - Kurum"></asp:ListItem>
                                                <asp:ListItem Value="Ogretmen" Text="Öğretmen"></asp:ListItem>
                                                <asp:ListItem Value="UstDegerlendirici" Text="Üst Değerlendirici"></asp:ListItem>
                                                <asp:ListItem Value="LgsYazari" Text="Soru Yazarı (LGS)"></asp:ListItem>
                                                <asp:ListItem Value="LgsIlKomisyonu" Text="LGS İl Komisyon Üyesi"></asp:ListItem>
                                                <asp:ListItem Value="SoruYazari" Text="Soru Yazarı"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnListele" CssClass="btn btn-primary" runat="server" ValidationGroup="form2" Text="Listele" OnClick="btnListele_OnClick" />
                                        </div>
                                    </div>
                                    <div class="box-body">
                                        <div class="box box-warning">
                                            <table class="table table-bordered table-hover dataTable" role="grid">
                                                <thead>
                                                    <tr role="row">
                                                        <th>#</th>
                                                        <th>Adı Soyadı</th>
                                                        <th>Kurumu</th>
                                                        <th>Branşı</th>
                                                        <th>Önceki Giris</th>
                                                        <th>Son Giris</th>
                                                        <th>İşlem</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptKullanicilar" runat="server" OnItemCommand="rptKullanicilar_ItemCommand" OnItemDataBound="rptKullanicilar_OnItemDataBound">
                                                        <ItemTemplate>
                                                            <tr role="row" class="odd">
                                                                <td><%#Eval("Id") %></td>
                                                                <td><%#Eval("AdiSoyadi") %></td>
                                                                <td>
                                                                    <asp:Literal ID="ltrKurumAdi" runat="server"></asp:Literal></td>
                                                                <td>
                                                                    <asp:Literal ID="ltrBransi" runat="server"></asp:Literal></td>
                                                                <td><%#Eval("OncekiGiris").ToDateTime().TarihYaz() %></td>
                                                                <td><%#Eval("SonGiris").ToDateTime().TarihYaz() %></td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkDuzenle" runat="server" CommandName="Duzenle" CommandArgument='<%#Eval("Id") %>'><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkSil" runat="server" CommandName="Sil" OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');" CommandArgument='<%#Eval("Id") %>'><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>

                                </div>
                                <div class="tab-pane" id="Kayit" runat="server">
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="box box-comments">
                                                    <div class="form-horizontal">
                                                        <div class="box-body">

                                                            <div class="box-header">
                                                                <h3 class="box-title">
                                                                    <asp:Literal ID="ltrKayitBilgi" Text="Yeni Kullanıcı Kayıt Formu" runat="server"></asp:Literal></h3>
                                                            </div>
                                                            <asp:HiddenField ID="hfId" Value="0" runat="server" />
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">Tc Kimlik</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtTcKimlik" AutoCompleteType="Enabled" CssClass="form-control" runat="server" placeholder="Tc Kimlik No" ValidationGroup="form"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">Şifre</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtSifre" AutoCompleteType="Enabled" CssClass="form-control" TextMode="Password" runat="server" placeholder="Şifre"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">
                                                                    Adı Soyadı
                                                                    <asp:RequiredFieldValidator ControlToValidate="txtAdiSoyadi" ValidationGroup="form" ID="RequiredFieldValidator2" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtAdiSoyadi" CssClass="form-control" runat="server" placeholder="Adı Soyadı" ValidationGroup="form"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">
                                                                    Branşı
                                                                    <asp:RequiredFieldValidator ControlToValidate="ddlBrans" ValidationGroup="form" ID="RequiredFieldValidator5" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBrans" ValidationGroup="form"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">
                                                                    E-mail Adresi
                                                                </label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtEpostaAdresi" CssClass="form-control" runat="server" placeholder="E-posta Adresi" ValidationGroup="form"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">Cep Tlf</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtCepTlf" data-inputmask='"mask": "(999) 999-99-99"' data-mask="" ValidationGroup="form" placeholder="Cep Telefonu"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">
                                                                    İlçe
                                                                    <asp:RequiredFieldValidator ControlToValidate="ddlIlce" ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlIlce" ValidationGroup="form" AutoPostBack="True" OnSelectedIndexChanged="ddlIlce_OnSelectedIndexChanged"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">
                                                                    Kurum
                                                                    <asp:RequiredFieldValidator ControlToValidate="ddlKurum" ValidationGroup="form" ID="RequiredFieldValidator4" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlKurum" ValidationGroup="form"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">
                                                                    Yetki</label>
                                                                <div class="col-sm-8">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:CheckBox ID="cbRoot" runat="server" Text="Administrotor" /><br />
                                                                            <asp:CheckBox ID="cbAdmin" runat="server" Text="ÖDM Ekibi" /><br />
                                                                            <asp:CheckBox ID="cbIlceMEMYetkilisi" runat="server" Text="İlçe MEM" /><br />
                                                                            <asp:CheckBox ID="cbOkulYetkilisi" runat="server" Text="Okul - Kurum" /><br />
                                                                            <asp:CheckBox ID="cbOgretmen" runat="server" Text="Öğretmen" AutoPostBack="True" OnCheckedChanged="cbOgretmen_OnCheckedChanged" /><br />
                                                                            <asp:CheckBox ID="cbUstDegerlendirici" runat="server" Text="Üst Değerlendirici" AutoPostBack="True" OnCheckedChanged="cbUstDegerlendirici_OnCheckedChanged" /><br />
                                                                            <asp:CheckBox ID="cbLgsYazari" runat="server" Text="Soru Yazarı (LGS)" /><br />
                                                                            <asp:CheckBox ID="cbLgsIlKomisyonu" runat="server" Text="LGS İl Komisyon Üyesi" /><br />
                                                                            <asp:CheckBox ID="cbSoruYazari" runat="server" Text="Soru Yazarı" /><br />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                            <div class="box-footer">
                                                                <asp:Button ID="btnVazgec" CssClass="btn btn-default" runat="server" Text="Vazgeç" OnClick="btnVazgec_Click" />
                                                                <asp:Button ID="btnKaydet" CssClass="btn btn-primary pull-right" runat="server" ValidationGroup="form" Text="Kaydet" OnClick="btnKaydet_Click" />
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
            </div>
        </section>
    </div>
</asp:Content>


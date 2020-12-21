<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Kullanicilar.aspx.cs" Inherits="OkulSinavi.OkulSinaviKullanicilarRoot" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        input[type='checkbox'] {
            margin-right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">

        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Kullanıcılar</h1>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="card">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-pills ml-auto p-2">
                                <li class="nav-item"><a id="tabliSayfalar" runat="server" class="nav-link active" href="#ContentPlaceHolder1_Sayfalar" data-toggle="tab" aria-expanded="true"><i class="fa fa-fw fa-list-alt"></i>Kullanıcılar</a></li>
                                <li class="nav-item"><a id="tabliKayit" runat="server" class="nav-link" href="#ContentPlaceHolder1_Kayit" data-toggle="tab" aria-expanded="false"><i class="fa fa-fw fa-edit"></i>Kayıt Formu</a></li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane active" id="Sayfalar" runat="server">
                                    <div class="card-header">
                                        <div class="row">
                                            <div class="col-md-2" id="ilcelerlist" runat="server">

                                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlIlceler" ValidationGroup="form2" AutoPostBack="True" OnSelectedIndexChanged="ddlIlceler_OnSelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-3" id="okullarlist" runat="server">
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlKurumlar" ValidationGroup="form2"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBranslar" ValidationGroup="form2"></asp:DropDownList>
                                            </div>

                                            <div class="col-md-2">
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlYetki" ValidationGroup="form2">
                                                  </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="btnListele" CssClass="btn btn-primary" runat="server" ValidationGroup="form2" Text="Listele" OnClick="btnListele_OnClick" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="card card-warning">
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
                                                                    <asp:LinkButton ID="lnkDuzenle" runat="server" CommandName="Duzenle" CommandArgument='<%#Eval("Id") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkSil" runat="server" CommandName="Sil" OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');" CommandArgument='<%#Eval("Id") %>'><i class="fa fa-trash"></i></asp:LinkButton>
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
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="card card-comments">
                                                    <div class="form-horizontal">
                                                        <div class="card-body">

                                                            <div class="card-header">
                                                                <h3 class="card-title">
                                                                    <asp:Literal ID="ltrKayitBilgi" Text="Yeni Kullanıcı Kayıt Formu" runat="server"></asp:Literal></h3>
                                                            </div>
                                                            <asp:HiddenField ID="hfId" Value="0" runat="server" />
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">Giriş Bilgisi</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtTcKimlik" AutoCompleteType="Enabled" CssClass="form-control" runat="server" placeholder="Giriş bilgisi" ValidationGroup="form"></asp:TextBox>
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
                                                                <label class="col-sm-4 control-label">Branşı</label>
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

                                                            <asp:UpdatePanel runat="server" ID="up2">
                                                                <ContentTemplate>
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
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">
                                                                    Yetki</label>
                                                                <div class="col-sm-8">
                                                                    <asp:CheckBox ID="cbRoot" runat="server" Text=" Root" /><br />
                                                                    <asp:CheckBox ID="cbAdmin" runat="server" Text=" Okul Yetkilisi - Admin" /><br />
                                                                    <asp:CheckBox ID="cbOgretmen" runat="server" Text=" Öğretmen" />
                                                                </div>
                                                            </div>
                                                            <div class="card-footer">
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
        </div>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Ayarlar.aspx.cs" Inherits="ODM.admin_Ayarlar" %>

<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="plugins/select2/select2.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Site Ayarları
        <small>Bu alanda sitenizin özel ve genel ayarlarını yapılandırabilirsiniz.</small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
                <li class="active">Ayarlar</li>
            </ol>
        </section>
        <section class="content">

            <div class="row">
                <div class="col-md-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="box">
                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs">
                                    <li class="active" id="tabli1" runat="server"><a href="#ContentPlaceHolder1_tab_1" data-toggle="tab" aria-expanded="true"><i class="fa fa-file"></i> Site Bilgileri</a></li>
                                    <li><a href="#tab_2" data-toggle="tab" aria-expanded="false"><i class="fa fa-phone"></i> İletişim Bilgileri</a></li>
                                    <li><a href="#tab_3" data-toggle="tab" aria-expanded="false"><i class="fa fa-envelope"></i> E-Posta Ayarları</a></li>
                                </ul>

                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab_1" runat="server">
                                        <section class="content">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="box box-info">
                                                        <div class="box-header with-border">
                                                            <h3 class="box-title">Site Bilgileri</h3>
                                                        </div>
                                                        <div class="box-body form-horizontal">
                                                            <div class="form-group">
                                                                <label for="ContentPlaceHolder1_txtSiteAdi" class="col-sm-2 control-label">Site Adı</label>
                                                                <div class="col-sm-10">
                                                                    <asp:TextBox ID="txtSiteAdi" CssClass="form-control" runat="server" ValidationGroup="ayarlar" MaxLength="50" placeholder="Sitenizin veya firmanızın adı (Max:50 karakter)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="ContentPlaceHolder1_txtDescription" class="col-sm-2 control-label">Site Açıklama (description)</label>
                                                                <div class="col-sm-10">
                                                                    <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" ValidationGroup="ayarlar" MaxLength="150" placeholder="Arama sonuçlarında gösterilecek site içeriğiyle ilgili veya firmanızla ilgili tanıtım (Max:150 karakter)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="ContentPlaceHolder1_txtKeywords" class="col-sm-2 control-label">Meta Taglar (keywords)</label>
                                                                <div class="col-sm-10">
                                                                    <asp:TextBox ID="txtKeywords" runat="server" CssClass="form-control" ValidationGroup="ayarlar" MaxLength="250" placeholder="Arama sonuçlarında gösterilecek site içeriğiyle ilgili veya firmanızla ilgili tanıtım (Max:250 karakter)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </section>
                                    </div>
                                    <div class="tab-pane" id="tab_2">
                                        <section class="content">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="box box-info">
                                                        <div class="box-header with-border">
                                                            <h3 class="box-title">İletişim Bilgileri</h3>
                                                        </div>
                                                        <div class="box-body form-horizontal">
                                                            <div class="form-group">
                                                                <label for="ContentPlaceHolder1_txtAdres" class="col-sm-2 control-label">
                                                                    Firma Adresi<asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="ayarlar" ControlToValidate="txtAdres" ValidationExpression="\d+" Display="Static" EnableClientScript="true" ErrorMessage="*"
                                                                        runat="server">*</asp:RegularExpressionValidator></label>
                                                                <div class="col-sm-10">
                                                                    <asp:TextBox ID="txtAdres" CssClass="form-control" runat="server" ValidationGroup="ayarlar" placeholder="Firmanızın adresi"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="ContentPlaceHolder1_txtTelefon" class="col-sm-2 control-label">Firma Telefon</label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox ID="txtTelefon" CssClass="form-control" runat="server" ValidationGroup="ayarlar" data-inputmask='"mask": "(999) 999-99-99"' data-mask=""></asp:TextBox>
                                                                </div>
                                                                 <label for="ContentPlaceHolder1_txtTelefon2" class="col-sm-2 control-label">Firma Telefon 2</label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox ID="txtTelefon2" CssClass="form-control" runat="server" ValidationGroup="ayarlar" data-inputmask='"mask": "(999) 999-99-99"' data-mask=""></asp:TextBox>
                                                                </div>

                                                                <label for="ContentPlaceHolder1_txtFax" class="col-sm-2 control-label">Firma Fax</label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox ID="txtFax" runat="server" CssClass="form-control" ValidationGroup="ayarlar" data-inputmask='"mask": "(999) 999-99-99"' data-mask=""></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </section>
                                    </div>
                                    <div class="tab-pane" id="tab_3">
                                        <section class="content">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="box box-info">
                                                        <div class="box-header with-border">
                                                            <div class="pull-right">
                                                                <asp:Button ID="btnMailTest" CssClass="btn btn-info" runat="server" Text="Mail Test Et" OnClick="btnMailTest_Click" />
                                                            </div>
                                                            <h3 class="box-title">E-Posta Ayarları</h3>
                                                        </div>
                                                        <div class="box-body form-horizontal">
                                                            <div class="form-group">
                                                                <label for="ContentPlaceHolder1_txtEpostaGonderenIsmi" class="col-sm-2 control-label">e-Posta İsim</label>
                                                                <div class="col-sm-10">
                                                                    <asp:TextBox ID="txtEpostaGonderenIsmi" CssClass="form-control" runat="server" ValidationGroup="ayarlar" placeholder="Site üzerinden gönderilen e-postalarda görünecek isim. Genellikle site adı yazılır."></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="ContentPlaceHolder1_txtEpostaServer" class="col-sm-2 control-label">e-Posta Server Adresi</label>


                                                                <div class="col-sm-10">
                                                                    <asp:TextBox ID="txtEpostaServer" CssClass="form-control" runat="server" ValidationGroup="ayarlar" placeholder="Kullandıınız e-posta server adresi. Genellikle mail.siteniz.com şeklindedir."></asp:TextBox>

                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="ContentPlaceHolder1_txtEpostaGonderenAdres" class="col-sm-2 control-label">
                                                                    Gönderen (Site) Adresi
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEpostaGonderenAdres" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                                                ForeColor="Red" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="ayarlar">*</asp:RegularExpressionValidator>
                                                                </label>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="txtEpostaGonderenAdres" CssClass="form-control" runat="server" ValidationGroup="ayarlar" placeholder="Site üzerinden gönderi yapan e-posta adresi."></asp:TextBox>
                                                                </div>
                                                                <label for="ContentPlaceHolder1_txtEpostaPass" class="col-sm-2 control-label">Site E-posta Şifresi</label>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="txtEpostaPass" runat="server" CssClass="form-control" ValidationGroup="ayarlar" placeholder="Gönderen (Site) Adresi şifresi"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="ContentPlaceHolder1_txtEpostaSSL" class="col-sm-2 control-label">SSL</label>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="txtEpostaSSL" runat="server" CssClass="form-control" ValidationGroup="ayarlar" placeholder="SSL"></asp:TextBox>
                                                                </div>
                                                                <label for="ContentPlaceHolder1_txtEpostaPort" class="col-sm-2 control-label">Port</label>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="txtEpostaPort" runat="server" CssClass="form-control" ValidationGroup="ayarlar" placeholder="Port"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="ContentPlaceHolder1_txtEpostaSiteAdres" class="col-sm-2 control-label">
                                                                    Site E-posta
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEpostaSiteAdres" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                                            ForeColor="Red" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="ayarlar">*</asp:RegularExpressionValidator></label>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="txtEpostaSiteAdres" runat="server" CssClass="form-control" ValidationGroup="ayarlar" placeholder="Sitenin çeşitli bölümlerinde gösterilen e-posta adresi"></asp:TextBox>
                                                                </div>
                                                                <label for="ContentPlaceHolder1_txtEpostaAliciAdres" class="col-sm-2 control-label">
                                                                    Alıcı E-posta Adresi 
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEpostaAliciAdres" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                                            ForeColor="Red" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="ayarlar">*</asp:RegularExpressionValidator></label>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="txtEpostaAliciAdres" runat="server" CssClass="form-control" ValidationGroup="ayarlar" placeholder="Bilgilendirme yapılacak e-posta adresi"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </section>
                                    </div>
                                   
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnKaydet" CssClass="btn btn-info pull-right" runat="server" Text=" Kaydet" OnClick="btnKaydet_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

</asp:Content>

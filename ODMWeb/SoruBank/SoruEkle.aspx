<%@ Page Title="" Language="C#" MasterPageFile="~/SoruBank/MasterPage.master" AutoEventWireup="true" CodeFile="SoruEkle.aspx.cs" Inherits="SoruBank_SoruEkle" %>

<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField runat="server" ID="hfSoruId" Value="0"/>
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Soru Ekle</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
                <li class="active">Soru Ekle</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>

                    <div class="col-md-6" id="divAsama1" runat="server">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Seçiniz</h3>
                            </div>
                            <div class="box-body">
                                <div class="progress">
                                    <div class="progress-bar progress-bar-green" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100" style="width: 25%">
                                        <span class="sr-only">1/4</span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Madde Türü
                                        <asp:RequiredFieldValidator ControlToValidate="ddlMaddeTuru" ValidationGroup="form" ID="RequiredFieldValidator2" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                    <asp:DropDownList CssClass="form-control" ID="ddlMaddeTuru" ValidationGroup="form" runat="server"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Branşınız
                                        <asp:RequiredFieldValidator ControlToValidate="ddlBrans" ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                    <asp:DropDownList CssClass="form-control" ID="ddlBrans" ValidationGroup="form" runat="server"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Sınıf Düzeyi
                                        <asp:RequiredFieldValidator ControlToValidate="ddlSinif" ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                    <asp:DropDownList ID="ddlSinif" CssClass="form-control" ValidationGroup="form" runat="server">
                                        <asp:ListItem Value="">--- Seçiniz ---</asp:ListItem>
                                        <asp:ListItem Value="1">1. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="2">2. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="3">3. Sınıf</asp:ListItem>
                                        <asp:ListItem Value="4">4. Sınıf</asp:ListItem>
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
                            </div>

                            <div class="box-footer">
                                <asp:Button ID="btnAsama1" CssClass="btn btn-primary pull-right" ValidationGroup="form" runat="server" Text="Devam" OnClick="btnAsama1_OnClick" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12" id="divAsama2" runat="server" visible="False">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Seçiniz</h3>
                            </div>
                            <div class="box-body">
                                <div class="progress">
                                    <div class="progress-bar progress-bar-green" role="progressbar" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100" style="width: 50%">
                                        <span class="sr-only">2/4</span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Öğrenme Alanı
                                        <asp:RequiredFieldValidator ControlToValidate="ddlOgrenmeAlani" ValidationGroup="form" ID="RequiredFieldValidator4" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                    <asp:DropDownList CssClass="form-control" ID="ddlOgrenmeAlani" runat="server" ValidationGroup="form" AutoPostBack="True" OnSelectedIndexChanged="ddlOgrenmeAlani_OnSelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Alt Öğrenme Alanı</label>
                                    <asp:DropDownList CssClass="form-control" ID="ddlAltOgrenmeAlani" ValidationGroup="form" AutoPostBack="True" OnSelectedIndexChanged="ddlAltOgrenmeAlani_OnSelectedIndexChanged" runat="server"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Kazanımlar
                                        <asp:RequiredFieldValidator ControlToValidate="lbKazanimlar" ValidationGroup="form" ID="RequiredFieldValidator6" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                    <asp:ListBox ID="lbKazanimlar" Height="150" SelectionMode="Multiple" ValidationGroup="form" CssClass="form-control" runat="server"></asp:ListBox>
                                    <label>Ctrl+Mouse ile birden fazla kazanım seçimi yapabilirsiniz.</label>
                                </div>

                            </div>

                            <div class="box-footer">
                                <asp:Button ID="btnAsama1eDon" CssClass="btn btn-default pull-left" runat="server" Text="Geri" OnClick="btnAsama1eDon_OnClick" />
                                <asp:Button ID="btnKazanimlar" CssClass="btn btn-primary pull-right" ValidationGroup="form" runat="server" Text="Devam" OnClick="btnKazanimlar_OnClick" />

                            </div>
                        </div>
                    </div>
                    <div class="col-md-12" id="divAsama3MaddeEkle" runat="server" visible="False">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Madde Ekle</h3>
                            </div>
                            <div class="box-body">

                                <div class="progress">
                                    <div class="progress-bar progress-bar-green" role="progressbar" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100" style="width: 75%">
                                        <span class="sr-only">3/4</span>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label>
                                            Madde Kökü</label>
                                        <asp:TextBox CssClass="form-control" TextMode="MultiLine" ID="txtMaddeKoku" ValidationGroup="formx" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-lg-6">A</label>
                                        <div class="col-lg-6">
                                            <asp:RadioButton ID="rbSecenekA" Checked="True" GroupName="X" CssClass="pull-right" runat="server" Text="Doğru Cevap" />
                                        </div>
                                        <asp:TextBox TextMode="MultiLine" Height="100" ValidationGroup="formx" ID="txtSecenekA" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-lg-6">B</label>
                                        <div class="col-lg-6">
                                            <asp:RadioButton ID="rbSecenekB" GroupName="X" CssClass="pull-right" runat="server" Text="Doğru Cevap" />
                                        </div>
                                        <asp:TextBox TextMode="MultiLine" ID="txtSecenekB" ValidationGroup="formx" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-lg-6">C</label>
                                        <div class="col-lg-6">
                                            <asp:RadioButton ID="rbSecenekC" GroupName="X" CssClass="pull-right" runat="server" Text="Doğru Cevap" />
                                        </div>
                                        <asp:TextBox TextMode="MultiLine" ValidationGroup="formx" ID="txtSecenekC" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6" id="divSecenekD" runat="server">
                                    <div class="form-group">
                                        <label class="col-lg-6">D</label>
                                        <div class="col-lg-6">
                                            <asp:RadioButton ID="rbSecenekD" GroupName="X" CssClass="pull-right" runat="server" Text="Doğru Cevap" />
                                        </div>
                                        <asp:TextBox CssClass="form-control" TextMode="MultiLine" ValidationGroup="formx" ID="txtSecenekD" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6" id="divSecenekE" runat="server">
                                    <div class="form-group">
                                        <label class="col-lg-6">E</label>
                                        <div class="col-lg-6">
                                            <asp:RadioButton ID="rbSecenekE" GroupName="X" CssClass="pull-right" runat="server" Text="Doğru Cevap" />
                                        </div>
                                        <asp:TextBox TextMode="MultiLine" ValidationGroup="formx" ID="txtSecenekE" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="box-footer">
                                <asp:Button ID="btnBasaDon" CssClass="btn btn-default pull-left" runat="server" Text="Geri" OnClick="btnAsama1eDon_OnClick" />
                                <asp:Button ID="btnAsama3Devam" CssClass="btn btn-primary pull-right" ValidationGroup="formx" runat="server" Text="Devam" OnClick="btnAsama3Devam_Click" />

                            </div>
                        </div>
                    </div>
                    <div class="col-md-12" id="divAsama4BilgiEkle" runat="server" visible="False">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Madde Hakkında Bilgi</h3>
                            </div>
                            <div class="box-body">

                                <div class="progress">
                                    <div class="progress-bar progress-bar-green" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                                        <span class="sr-only">4/4</span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Maddenin Zorluk Derecesi
                                        <asp:RequiredFieldValidator ControlToValidate="ddlZorlukDerecesi" ValidationGroup="form" ID="RequiredFieldValidator13" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                    <asp:DropDownList CssClass="form-control" ID="ddlZorlukDerecesi" runat="server" ValidationGroup="form">
                                        <asp:ListItem Value="">--- Seçiniz ---</asp:ListItem>
                                        <asp:ListItem Value="Kolay">Kolay</asp:ListItem>
                                        <asp:ListItem Value="Zor">Zor</asp:ListItem>
                                        <asp:ListItem Value="Orta">Orta</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>Soru Hakkında </label>
                                    <asp:TextBox CssClass="form-control" TextMode="MultiLine" ValidationGroup="form" ID="txtMaddeBilgi" runat="server"></asp:TextBox>
                                    <label>Soru hakkında bize iletmek istediğiniz açıklamalarınızı yazınız. (Daha önce bir yerde yayınlanıp yayınlanmadığını, telif hakkı olup olmadığını yazabilirsiniz)</label>
                                </div>
                                <div class="form-group" id="divDurum" runat="server">
                                    <label>
                                        Durumu</label>
                                    <asp:DropDownList CssClass="form-control" ID="ddlDurum" runat="server" ValidationGroup="form">
                                        <asp:ListItem Value="0">İnceleniyor</asp:ListItem>
                                        <asp:ListItem Value="1">Redakte Ediliyor</asp:ListItem>
                                        <asp:ListItem Value="2">RedakteEdildi</asp:ListItem>
                                        <asp:ListItem Value="3">Geri Gönderildi</asp:ListItem>
                                        <asp:ListItem Value="4">Kullanıldı</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="box-footer">
                                <asp:Button ID="Button1" CssClass="btn btn-default pull-left" runat="server" Text="Geri" OnClick="btnAsama1eDon_OnClick" />
                                <asp:Button ID="btnTamamla" CssClass="btn btn-primary pull-right" ValidationGroup="form" runat="server" Text="Tamamla" OnClick="btnTamamla_OnClick" />

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src="/ckeditor/ckeditor.js"></script>
    <asp:PlaceHolder ID="phMeddeveCevaplar" runat="server" Visible="False">
        <script>
            $(function () {
                CKEDITOR.replace('ContentPlaceHolder1_txtMaddeKoku', { toolbar: [{ name: 'basicstyles', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo', '-', 'Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', 'NumberedList', 'BulletedList', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'Image', 'Table', '-', 'TextColor', 'BGColor'] }] });
                CKEDITOR.replace('ContentPlaceHolder1_txtSecenekA', { toolbar: [{ name: 'basicstyles', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo', '-', 'Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', 'Image', 'Table'] }] });
                CKEDITOR.replace('ContentPlaceHolder1_txtSecenekB', { toolbar: [{ name: 'basicstyles', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo', '-', 'Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', 'Image', 'Table'] }] });
                CKEDITOR.replace('ContentPlaceHolder1_txtSecenekC', { toolbar: [{ name: 'basicstyles', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo', '-', 'Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', 'Image', 'Table'] }] });
                CKEDITOR.replace('ContentPlaceHolder1_txtSecenekD', { toolbar: [{ name: 'basicstyles', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo', '-', 'Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', 'Image', 'Table'] }] });
                CKEDITOR.replace('ContentPlaceHolder1_txtSecenekE', { toolbar: [{ name: 'basicstyles', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo', '-', 'Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', 'Image', 'Table'] }] });
                $(".textarea").wysihtml5();
            });  </script>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phMaddeBilgi" runat="server" Visible="False">
        <script>
            $(function () {
                CKEDITOR.replace('ContentPlaceHolder1_txtMaddeBilgi', { toolbar: [{ name: 'basicstyles', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo', '-', 'Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', 'Image', 'Table'] }] });
                $(".textarea").wysihtml5();
            });  </script>
    </asp:PlaceHolder>
</asp:Content>


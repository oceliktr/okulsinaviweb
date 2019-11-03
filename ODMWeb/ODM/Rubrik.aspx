<%@ Page Title="" Language="C#" MasterPageFile="~/ODM/MasterPage.master" AutoEventWireup="true" CodeFile="Rubrik.aspx.cs" Inherits="ODM.OdmRubrik" %>


<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
    <div class="content-wrapper">
        <asp:Repeater runat="server" ID="rptList">
            <ItemTemplate><%#Eval("Kelime") %>, </ItemTemplate>
        </asp:Repeater>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <section class="content-header">
            <h1>Rubrik</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
                <li class="active">Rubrik</li>
            </ol>
        </section>
        <asp:HiddenField ID="hfId" Value="0" runat="server" />
        <section class="content">
            <div class="row">
                <div class="col-lg-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>

                    <div class="box box-solid box-default">
                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs">
                                    <li id="tabliSayfalar" runat="server" class="active"><a href="#ContentPlaceHolder1_Sayfalar" data-toggle="tab" aria-expanded="true"><i class="fa fa-list-alt"></i>Kayıtlar</a></li>
                                    <li id="tabliKayit" runat="server"><a href="#ContentPlaceHolder1_Kayit" data-toggle="tab" aria-expanded="false"><i class="fa fa-edit"></i>Kayıt Formu</a></li>
                                    <li id="tabliKazanim" runat="server" visible="False"><a href="#ContentPlaceHolder1_Kazanim" data-toggle="tab" aria-expanded="false"><i class="fa fa-edit"></i>Kazanımlar</a></li>
                                </ul>

                                <div class="tab-content">
                                    <div class="tab-pane active" id="Sayfalar" runat="server">
                                        <div class="box box-solid box-default">
                                            <div class="box-body">
                                                <div class="form-group">
                                                    <label class="col-sm-1 control-label">Sınav</label>
                                                    <div class="col-sm-2">
                                                        <asp:DropDownList ID="ddlSinavId" CssClass="form-control" runat="server" ValidationGroup="form" AutoPostBack="True" OnSelectedIndexChanged="ddlSinavId_OnSelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:DropDownList ID="ddlKitapcikTuru" CssClass="form-control" runat="server" ValidationGroup="form" AutoPostBack="True" OnSelectedIndexChanged="ddlKitapcikTuru_OnSelectedIndexChanged">
                                                            <asp:ListItem Value="">Kitapçık Türünü Seçiniz</asp:ListItem>
                                                            <asp:ListItem Value="A">A Kitapçığı</asp:ListItem>
                                                            <asp:ListItem Value="B">B Kitapçığı</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="box-body">
                                                <table class="table table-bordered table-hover dataTable" role="grid">
                                                    <thead>
                                                        <tr role="row">
                                                            <th>Sınıf</th>
                                                            <th>Ders Adı</th>
                                                            <th>Kazanım</th>
                                                            <th>Kitapçık</th>
                                                            <th>Soru No</th>
                                                            <th>Doğru Cevap</th>
                                                            <th>Kısmi Puan</th>
                                                            <th>Tam Puan</th>
                                                            <th>İşlem</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rptKayitlar" runat="server" OnItemCommand="rptKayitlar_OnItemCommand" OnItemDataBound="rptKayitlar_OnItemDataBound">
                                                            <ItemTemplate>
                                                                <tr role="row" class="odd">
                                                                    <td><%#Eval("Id")%></td>
                                                                    <td>
                                                                        <asp:Literal ID="ltrBrans" runat="server"></asp:Literal></td>
                                                                    <td>
                                                                        <asp:Literal ID="ltrKazanim" runat="server"></asp:Literal></td>
                                                                    <td><%#Eval("KitapcikTuru")%></td>
                                                                    <td><%#Eval("SoruNo")%></td>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkKazanim" CommandName="Kazanim" CommandArgument='<%#Eval("Id") %>' ToolTip="Kazanım Ekle" runat="server"><span class="glyphicon glyphicon-transfer"></span></asp:LinkButton>
                                                                        <%#Eval("DogruCevap").ToString().HtmlTemizle().SoldanMetinAl(50)%>...
                                                                    </td>
                                                                    <td><%#Eval("KismiPuan")%></td>
                                                                    <td><%#Eval("Tampuan")%></td>
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
                                        <div class="box box-solid box-default form-horizontal">
                                            <div class="box-body">
                                                <div class="form-group">
                                                    <label class="col-sm-1 control-label">
                                                        Sınav
                                                        <asp:RequiredFieldValidator ValidationGroup="form" ID="RequiredFieldValidator2" ForeColor="Red" ControlToValidate="ddlSinavId2" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlSinavId2" CssClass="form-control" runat="server" ValidationGroup="form"></asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-1 control-label">Sınıf</label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlSinif" CssClass="form-control" runat="server">
                                                            <asp:ListItem Value="">Sınıf Seçiniz</asp:ListItem>
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
                                                    <label class="col-sm-1 control-label">Ders Adı</label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlBrans" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-1 control-label">Soru Tipi <asp:RequiredFieldValidator ValidationGroup="form" ID="RequiredFieldValidator5" ForeColor="Red" ControlToValidate="ddlSoruTipi" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <div class="col-sm-2">
                                                        <asp:DropDownList ID="ddlSoruTipi" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSoruTipi_OnSelectedIndexChanged" ValidationGroup="form">
                                                            <asp:ListItem Value="">Seçiniz</asp:ListItem>
                                                            <asp:ListItem Value="au">Açık Uclu</asp:ListItem>
                                                            <asp:ListItem Value="of">Optik Form</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-1 control-label">
                                                        Soru No 
                                                        <asp:RequiredFieldValidator ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="ddlSoruNo" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <div class="col-sm-2">
                                                        <asp:DropDownList ID="ddlSoruNo" CssClass="form-control" runat="server" ValidationGroup="form"></asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-1 control-label">
                                                        Kısmi Puan 
                                                        <asp:RequiredFieldValidator ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" ControlToValidate="txtKismiPuan" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtKismiPuan" CssClass="form-control" runat="server" placeholder="Kısmi puan yoksa 0 yazınız" ValidationGroup="form" />
                                                    </div>
                                                    <label class="col-sm-1 control-label">
                                                        Tam Puan 
                                                        <asp:RequiredFieldValidator ValidationGroup="form" ID="RequiredFieldValidator4" ForeColor="Red" ControlToValidate="txtTamPuan" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtTamPuan" CssClass="form-control" runat="server" ValidationGroup="form" />
                                                    </div>
                                                </div>
                                             <asp:PlaceHolder runat="server" ID="phAcikUclu" Visible="False">
                                                    <div class="form-group">
                                                    <label class="col-sm-1 control-label">Soru </label>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="txtSoru" ValidationGroup="form" CssClass="form-control" runat="server" placeholder="Doğru cevabı giriniz" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-1 control-label">Doğru Cevap </label>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="txtDogruCevap" ValidationGroup="form" CssClass="form-control" runat="server" placeholder="Doğru cevabı giriniz" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-1 control-label">Kısmi Cevap</label>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="txtKismiCevap" ValidationGroup="form" CssClass="form-control" runat="server" placeholder="Yanlış cevabı giriniz" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-1 control-label">Yanlış Cevap </label>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="txtYanlisCevap" ValidationGroup="form" CssClass="form-control" runat="server" placeholder="Yanlış cevabı giriniz" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                             </asp:PlaceHolder>
                                                <div class="box-footer">
                                                    <asp:Button ID="btnVazgec" ValidationGroup="vazgec" CssClass="btn btn-default" runat="server" Text="Vazgeç" OnClick="btnVazgec_OnClick" />
                                                    <asp:Button ID="btnKaydet" ValidationGroup="form" CssClass="btn btn-primary pull-right" runat="server" Text="Kaydet" OnClick="btnKaydet_OnClick" />
                                                </div>
                                            </div>

                                        </div>
                                        <asp:PlaceHolder ID="phDosyaYukle" runat="server">
                                            <div class="box box-solid box-default form-horizontal">
                                                <div class="box-body">
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Dosya Yükle</label>
                                                        <div class="col-sm-5">
                                                            <asp:FileUpload ID="fuDosya" CssClass="form-control" runat="server"></asp:FileUpload>
                                                        </div>
                                                        <div class="col-sm-5">
                                                            <asp:Button ID="btnDosyaYukle" runat="server" Text="Yükle" CssClass="btn btn-primary pull-left" OnClick="btnDosyaYukle_OnClick"></asp:Button>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Dosya Url</label>
                                                        <div class="col-sm-10">
                                                            <asp:Image ID="imgDosyaResim" Visible="False" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:PlaceHolder>
                                    </div>
                                    <div class="tab-pane" id="Kazanim" runat="server" visible="False">
                                        <asp:HiddenField ID="hfSoruId" runat="server" />
                                        <div class="box box-solid box-default form-horizontal">
                                            <div class="box-body">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <div class="form-group">
                                                            <label class="col-sm-2 control-label">Öğrenme Alanı</label>
                                                            <div class="col-sm-4">
                                                                <asp:DropDownList ID="ddlOgrenmeAlani" CssClass="form-control" runat="server" ValidationGroup="formk" AutoPostBack="True" OnSelectedIndexChanged="ddlOgrenmeAlani_OnSelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <label class="col-sm-2 control-label">Alt Öğrenme Alanı</label>
                                                            <div class="col-sm-4">
                                                                <asp:DropDownList ID="ddlAltOgrenmeAlani" CssClass="form-control" runat="server" ValidationGroup="formk" AutoPostBack="True" OnSelectedIndexChanged="ddlAltOgrenmeAlani_OnSelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-sm-1 control-label">Kategoriler</label>
                                                            <div class="col-sm-5">
                                                                <asp:ListBox ID="lbKategoriler" Rows="10" CssClass="form-control" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:Button ID="blnSecileniEkle" runat="server" Text=">>" OnClick="blnSecileniEkle_OnClick" /><br />
                                                                <br />
                                                                <asp:Button ID="blnSecileniCikar" runat="server" Text="<<" OnClick="blnSecileniCikar_OnClick" />
                                                            </div>
                                                            <div class="col-sm-5">
                                                                <asp:ListBox ID="lbSecilenler" Rows="10" CssClass="form-control" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="box-footer">
                                                    <asp:Button ID="btnKazanimVazgec" ValidationGroup="vazgec" CssClass="btn btn-default" runat="server" Text="Vazgeç" OnClick="btnKazanimVazgec_OnClick" />
                                                    <asp:Button ID="btnKazanimKaydet" ValidationGroup="formk" CssClass="btn btn-primary pull-right" runat="server" Text="Kaydet" OnClick="btnKazanimKaydet_OnClick" />
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
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="/ckeditor/ckeditor.js"></script>
    <script>
        $(function () {
            // Replace the <textarea id="editor1"> with a CKEditor
            // instance, using default configuration.
            CKEDITOR.replace('ContentPlaceHolder1_txtDogruCevap');
            CKEDITOR.replace('ContentPlaceHolder1_txtYanlisCevap');
            CKEDITOR.replace('ContentPlaceHolder1_txtKismiCevap');
            CKEDITOR.replace('ContentPlaceHolder1_txtSoru');
            //bootstrap WYSIHTML5 - text editor
            $(".textarea").wysihtml5();
        });
    </script>
</asp:Content>

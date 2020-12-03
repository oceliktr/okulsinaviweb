<%@ page title="" language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="OkulSinavi_CevrimiciSinavYonetim_SoruKayit, okulsinavi" enableEventValidation="false" %>


<%@ Register TagPrefix="uc1" TagName="UstMenu" Src="~/Yonetim/UstMenu.ascx" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-12">
                        <h1 class="m-0 text-dark">Çevrim İçi Sınav Modülü   <small class="float-right">Eğitim Öğretim Yılı: <%=TestSeciliDonem.SeciliDonem().Donem%></small></h1>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card card-default">
                        <div class="card-body">
                            <div class="row" style="margin-bottom: 20px;">
                                <div class="col-md-12">
                                    <uc1:UstMenu runat="server" ID="UstMenu" />
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <asp:HiddenField ID="hfOturum" runat="server" Value="0" />
                                <asp:HiddenField ID="hfId" runat="server" Value="0" />
                                <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                                <div class="card card-warning">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <ol class="breadcrumb float-sm-left">
                                                    <li class="breadcrumb-item"><a href="SinavYonetim.aspx">Sınav Yönetimi</a></li>
                                                    <li class="breadcrumb-item">
                                                        <asp:HyperLink ID="hlOturum" runat="server">Oturumlar</asp:HyperLink></li>
                                                    <li class="breadcrumb-item"><a href="SinavSorulari.aspx?OturumId=<%=Request.QueryString["OturumId"]%>">Sorular</a></li>
                                                    <li class="breadcrumb-item active">Soru Kayıt</li>
                                                </ol>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Oturum Adı </label>
                                                    <asp:TextBox ID="txtOturmAdi" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div id="accordion">
                                                    <div class="card card-primary">
                                                        <div class="card-header">
                                                            <h4 class="card-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Soru Yaz</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapseOne" class="panel-collapse collapse in">
                                                            <div class="card-body">
                                                               
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtSoru" ValidationGroup="form" TextMode="MultiLine" runat="server" CssClass="form-control" placeholder="Sınavın içeriği hakkında bilgi giriniz..."></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label>Veya Soruyu Fotoğraf Olarak Yükle </label>
                                                    <asp:FileUpload ID="fuResim" CssClass="form-control" ValidationGroup="upload" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Url Adresi </label>
                                                    <asp:TextBox ID="txtUrl" ValidationGroup="upload" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <br />
                                                <asp:Button ID="btnYukle" CssClass="btn btn-success pull-right mt-1" runat="server" ValidationGroup="upload" Text="Yükle" OnClick="btnYukle_OnClick" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>
                                                        Branş
                                                        <asp:RequiredFieldValidator ControlToValidate="ddlBrans" ValidationGroup="form" ID="RequiredFieldValidator2" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <asp:DropDownList ID="ddlBrans" ValidationGroup="form" class="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>
                                                        Soru No
                                                        <asp:RequiredFieldValidator ControlToValidate="txtSoruNo" ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtSoruNo" ValidationGroup="form" TextMode="Number" runat="server" CssClass="form-control" Text="1" placeholder="Soru No"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>
                                                        Cevap
                                                        <asp:RequiredFieldValidator ControlToValidate="ddlCevap" ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <asp:DropDownList ID="ddlCevap" ValidationGroup="form" class="form-control" runat="server">
                                                        <asp:ListItem Value="">Doğru Cevabı Seçiniz</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="B">B</asp:ListItem>
                                                        <asp:ListItem Value="C">C</asp:ListItem>
                                                        <asp:ListItem Value="D">D</asp:ListItem>
                                                        <asp:ListItem Value="E">E</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                      
                                       
                                    </div>

                                    <div class="card-footer">
                                        <a href="SinavSorulari.aspx?OturumId=<%=Request.QueryString["OturumId"] %>" class="btn btn-secondary btn-flat"><i class="fas fa-chevron-circle-left mr-1"></i>Sorular</a>
                                        <asp:Button ID="btnKaydet" CssClass="btn btn-primary float-right" runat="server" ValidationGroup="form" Text="Kaydet" OnClick="btnKaydet_OnClick" />
                                    </div>
                                </div>
                                <div class="alert alert-light" role="alert">
                                    <h5>Fotoğraf Yükleme Hakkında</h5>

                                    <p>Fotoğraf yükleme işleminde 'Yükle' butonuna yalnızca 'Soru Yaz' alanındaki editöre dosya yükleneceğinde kullanınız.</p>
                                    <p>Soru bir fotoğraf olacaksa dosya yükleme işlemi kayıt işleminde yüklenecektir.(kaydet butonuna tıklayınca) </p>
                                    <p>Eğer dosya seçili veya 'Url Adresi' dolu ise 'Soru Yaz' metin alanı <strong>kaydedilmez.</strong></p>
                                    <p>Dosya seçili ve 'Url Adresi' dolu ise öncelik seçili dosyaya aittir.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">

    <script src="/CevrimiciSinav/Content/ckeditor/ckeditor.js"></script>
    <script>
        CKEDITOR.replace('ContentPlaceHolder1_txtSoru');
    </script>
</asp:Content>

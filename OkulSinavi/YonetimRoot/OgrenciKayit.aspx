<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="OgrenciKayit.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim_OgrenciKayitRoot" %>

<%@ Register TagPrefix="uc1" TagName="UstMenu" Src="~/Yonetim/UstMenu.ascx" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                        <div class="row" style="margin-bottom: 20px;">
                            <div class="col-md-12">
                                <uc1:UstMenu runat="server" ID="UstMenu" />
                            </div>
                        </div>
                        <div class="card-body">

                            <div class="col-lg-8 offset-md-2">
                                <asp:HiddenField ID="hfId" runat="server" Value="0" />
                                <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                                <div class="card card-warning" runat="server" id="kayitForm">
                                    <div class="card-body">

                                        <div class="row">
                                            <div class="col-md-12">
                                                <a href="OgrenciTopluKayit.aspx" class="btn btn-warning btn-flat float-right"><i class="fas fa-table mr-1"></i>Toplu Kayıt Yap</a>
                                            </div>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>
                                                                    İlçe
                                                                    <asp:RequiredFieldValidator ControlToValidate="ddlIlce" ValidationGroup="form" ID="RequiredFieldValidator4" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                                <asp:DropDownList ID="ddlIlce" ValidationGroup="form" class="form-control" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlIlce_OnSelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>
                                                                    Okulu
                                                                    <asp:RequiredFieldValidator ControlToValidate="ddlKurum" ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                                <asp:DropDownList ID="ddlKurum" ValidationGroup="form" class="form-control" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                    </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    Sınav Giriş Bilgisi <i data-toggle="tooltip" title="TC Kimlik yazabilirsiniz. TC Kimlik bilgisi sistemimizde tutulmamaktadır. Yalnızca benzersiz kayıt oluşturmak için kullanılmaktadır." class="fa fa-question-circle"></i>
                                                    </label>
                                                <asp:TextBox ID="txtTcKimlik" MaxLength="11" ValidationGroup="form" CssClass="form-control" runat="server" placeholder="Giriş bilgisini giriniz"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Sınıf<asp:RequiredFieldValidator ControlToValidate="ddlSinif" ValidationGroup="form" ID="RequiredFieldValidator2" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                <asp:DropDownList ID="ddlSinif" ValidationGroup="form" class="form-control" runat="server">
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
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    Şube
                                                        <asp:RequiredFieldValidator ControlToValidate="txtSube" ValidationGroup="form" ID="RequiredFieldValidator6" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                <asp:TextBox ID="txtSube"  ValidationGroup="form" MaxLength="2" CssClass="form-control" runat="server" placeholder="Şubesini giriniz"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    Öğrencinin Adı
                                                        <asp:RequiredFieldValidator ControlToValidate="txtAdi" ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                <asp:TextBox ID="txtAdi" ValidationGroup="form" CssClass="form-control" runat="server" placeholder="Öğrencinin adını giriniz"></asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    Öğrencinin Soyadı
                                                        <asp:RequiredFieldValidator ControlToValidate="txtSoyadi" ValidationGroup="form" ID="RequiredFieldValidator5" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                <asp:TextBox ID="txtSoyadi" ValidationGroup="form" CssClass="form-control" runat="server" placeholder="Öğrencinin soyadını giriniz"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    </div>

                                    <div class="card-footer">
                                        <asp:Button ID="btnKaydet" CssClass="btn btn-primary float-right" runat="server" ValidationGroup="form" Text="Kaydet" OnClick="btnKaydet_OnClick" />
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
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script>
        $(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
</asp:Content>


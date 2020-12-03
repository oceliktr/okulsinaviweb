<%@ page title="" language="C#" masterpagefile="MasterPage.master" autoeventwireup="true" inherits="OkulSinavi.AdminKurumlar, okulsinavi" enableEventValidation="false" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Kurumlar</h1>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-md-12">
                    
                    <div class="card">
                        <div class="card-header">
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlIlceler" AutoPostBack="True" OnSelectedIndexChanged="ddlIlceler_OnSelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="card card-warning">
                                        <table class="table table-bordered table-hover dataTable" role="grid">
                                            <thead>
                                                <tr role="row">
                                                    <th>#</th>
                                                    <th>Kurum Adı</th>
                                                    <th>İşlem</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptKurumlar" runat="server" OnItemCommand="rptKurumlar_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr role="row" class="odd">
                                                            <td><%#Eval("KurumKodu") %></td>
                                                            <td><%#Eval("KurumAdi") %> <%#Eval("Kapali").ToInt32()==1?"<span class='badge badge-danger'>Kapalı</span>":"" %></td>
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
                                <div class="col-md-6">
                                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                                    <div class="card card-comments">
                                        <div class="form-horizontal">
                                            <div class="card-body">
                                                <div class="nav-tabs-custom">

                                                    <div class="tab-content">
                                                        <div class="tab-pane active" id="tab_1">
                                                            <div class="card-header">
                                                                <h3 class="card-title">
                                                                    <asp:Literal ID="ltrKayitBilgi" Text="Yeni Kurum Kayıt Formu" runat="server"></asp:Literal></h3>
                                                            </div>
                                                            <asp:HiddenField ID="hfId" Value="0" runat="server" />
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    Kurum Kodu
                                                                    <asp:RequiredFieldValidator ControlToValidate="txtKurumKodu" ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                                <div class="col-sm-9">
                                                                    <asp:TextBox ID="txtKurumKodu" CssClass="form-control" runat="server" placeholder="Kurum Kodu" ValidationGroup="form"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    Kurum Adı
                                                                    <asp:RequiredFieldValidator ControlToValidate="txtKurumAdi" ValidationGroup="form" ID="RequiredFieldValidator2" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                                <div class="col-sm-9">
                                                                    <asp:TextBox ID="txtKurumAdi" CssClass="form-control" runat="server" placeholder="Kurum Adı" ValidationGroup="form"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    İlçe
                                                                    <asp:RequiredFieldValidator ControlToValidate="ddlIlce" ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlIlce" ValidationGroup="form"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    Kurum Türü
                                                                    <asp:RequiredFieldValidator ControlToValidate="ddlKurumTuru" ValidationGroup="form" ID="RequiredFieldValidator4" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlKurumTuru" ValidationGroup="form">
                                                                        <asp:ListItem Value="">Kurum türünü seçiniz</asp:ListItem>
                                                                        <asp:ListItem Value="Lise">Anadolu İmam Hatip Lisesi</asp:ListItem>
                                                                        <asp:ListItem Value="Lise">Anadolu Lisesi</asp:ListItem>
                                                                        <asp:ListItem Value="MeslekiLise">Anadolu Otelcilik ve Turizm Mes. Lisesi</asp:ListItem>
                                                                        <asp:ListItem Value="MeslekiLise">Anadolu Sağlık Meslek Lisesi</asp:ListItem>
                                                                        <asp:ListItem Value="MeslekiLise">Anadolu Ticaret Meslek Lisesi</asp:ListItem>
                                                                        <asp:ListItem Value="Anaokulu">Anaokulu</asp:ListItem>
                                                                        <asp:ListItem Value="MeslekiLise">Çok Programlı Anadolu Lisesi</asp:ListItem>
                                                                        <asp:ListItem Value="MeslekiLise">Endüstri Meslek Lisesi</asp:ListItem>
                                                                        <asp:ListItem Value="Lise">Fen Lisesi</asp:ListItem>
                                                                        <asp:ListItem Value="Lise">Güzel Sanatlar Lisesi</asp:ListItem>
                                                                        <asp:ListItem Value="HEM">Halk Eğitim Merkezi</asp:ListItem>
                                                                        <asp:ListItem Value="MEM">İl - İlçe Milli Eğitim Müdürlüğü</asp:ListItem>
                                                                        <asp:ListItem Value="İlkokul">İlkokul</asp:ListItem>
                                                                        <asp:ListItem Value="İlkokul">İlkokul (Görme - İşitme Engelliler)</asp:ListItem>
                                                                        <asp:ListItem Value="Lise">İmam Hatip Lisesi</asp:ListItem>
                                                                        <asp:ListItem Value="Ortaokul">İmam Hatip Ortaokulu</asp:ListItem>
                                                                        <asp:ListItem Value="MeslekiLise">Kız Meslek Lisesi</asp:ListItem>
                                                                        <asp:ListItem Value="Lise">Meslek Lisesi(İşitme Engelliler)</asp:ListItem>
                                                                        <asp:ListItem Value="HEM">Mesleki Eğitim Merkezi</asp:ListItem>
                                                                        <asp:ListItem Value="Kurum">Mesleki ve Teknik Eğitim Merkezi (ETÖGM)</asp:ListItem>
                                                                        <asp:ListItem Value="Ortaokul">Ortaokul</asp:ListItem>
                                                                        <asp:ListItem Value="Ortaokul">Ortaokul (Görme - İşitme Engelliler)</asp:ListItem>
                                                                        <asp:ListItem Value="Kurum">Özel Eğitim Uygulama Merkezi (I. II. III. Kademe)</asp:ListItem>
                                                                        <asp:ListItem Value="Kurum">Rehberlik ve Araştırma Merkezi</asp:ListItem>
                                                                        <asp:ListItem Value="MeslekiLise">Sağlık Meslek Lisesi</asp:ListItem>
                                                                        <asp:ListItem Value="Lise">Sosyal Bilimler Lisesi</asp:ListItem>
                                                                        <asp:ListItem Value="Lise">Spor Lisesi</asp:ListItem>
                                                                        <asp:ListItem Value="MeslekiLise">Ticaret Meslek Lisesi</asp:ListItem>
                                                                        <asp:ListItem Value="Kurum">Üstün veya Özel Yetenekliler</asp:ListItem>
                                                                        <asp:ListItem Value="Ortaokul">Yatılı Bölge Ortaokulu</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    Kapalı
                                                                </label>
                                                                <div class="col-sm-9">
                                                                    <asp:CheckBox ID="cbKapali" runat="server" />
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


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SinavKayit.aspx.cs" Inherits="Okul_SinaviYonetim_SinavKayit" %>

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
                        <div class="row" style="margin-bottom: 20px;">
                            <div class="col-md-12">
                                <uc1:UstMenu runat="server" ID="UstMenu" />
                            </div>
                        </div>
                        <div class="card-body">

                            <div class="row">
             
                                <div class="col-md-6">
                                    <ol class="breadcrumb float-sm-left">
                                        <li class="breadcrumb-item"><a href="SinavYonetim.aspx">Sınav Yönetimi</a></li>
                                        <li class="breadcrumb-item active">Sınav Kayıt Formu</li>
                                    </ol>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <asp:HiddenField ID="hfId" runat="server" Value="0" />
                                <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                                <div class="card card-warning">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                
                                                <div class="alert alert-info" role="alert">
                                                    <asp:Literal ID="ltrDonem" runat="server"></asp:Literal><br/>
                                                    Yalnızca aktif döneme kayıt yapılır. 
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>
                                                        Aktif Dönem: </label>
                                                    <asp:TextBox ID="txtDonem" ValidationGroup="form" ReadOnly="True" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    <label>
                                                        Sınav Adı
                                                        <asp:RequiredFieldValidator ControlToValidate="txtSinavAdi" ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtSinavAdi" ValidationGroup="form" CssClass="form-control" runat="server" placeholder="Sınavın adını giriniz"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Açıklama</label>
                                                    <asp:TextBox ID="txtAciklama" ValidationGroup="form" TextMode="MultiLine" runat="server" CssClass="form-control" placeholder="Sınavın içeriği hakkında bilgi giriniz..."></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Sınıf<asp:RequiredFieldValidator ControlToValidate="ddlSinif" ValidationGroup="form" ID="RequiredFieldValidator2" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <asp:DropDownList ID="ddlSinif" ValidationGroup="form" class="form-control" runat="server">
                                                        <asp:ListItem Value="">Seçiniz</asp:ListItem>
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
                                                        Puanlama Türü
                                                        <asp:RequiredFieldValidator ControlToValidate="ddlPuanlama" ValidationGroup="form" ID="RequiredFieldValidator4" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <asp:DropDownList ID="ddlPuanlama" ValidationGroup="form" class="form-control" runat="server">
                                                        <asp:ListItem Value="">Seçiniz</asp:ListItem>
                                                        <asp:ListItem Value="100">100 puan üzerinden</asp:ListItem>
                                                        <asp:ListItem Value="500">500 puan üzerinden</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>
                                                        Oturum Giriş Tercihi
                                                        <asp:RequiredFieldValidator ControlToValidate="ddlOturumTercihi" ValidationGroup="form" ID="RequiredFieldValidator5" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <asp:DropDownList ID="ddlOturumTercihi" ValidationGroup="form" class="form-control" runat="server">
                                                        <asp:ListItem Value="">Seçiniz</asp:ListItem>
                                                        <asp:ListItem Value="0">Oturum saatlerine göre</asp:ListItem>
                                                        <asp:ListItem Value="1">Oturum sırasına göre</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group d-none" id="beklemesuresi">
                                                    <label>Bekleme Süresi</label>
                                                    <asp:TextBox ID="txtBeklemeSuresi" ValidationGroup="form" TextMode="Number" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="alert alert-info d-none note-fontsize-10" id="uyari" role="alert">
                                                    
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label> Durum</label> <asp:CheckBox ID="cbDurum" Checked="True" CssClass="form-control" runat="server" Text=" Aktif - Öğrencilere göster/gizle" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card-footer">
                                        <a href="SinavYonetim.aspx" class="btn btn-secondary btn-flat"><i class="fas fa-chevron-circle-left mr-1"></i>Sınavlar</a>
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
            //başlangıçta
            var secim = $("#ContentPlaceHolder1_ddlOturumTercihi").children("option:selected").val();
            SonucRes(secim);

            //select işleminde
            $("#ContentPlaceHolder1_ddlOturumTercihi").on('change',function() {
                SonucRes(this.value);

            });

            function SonucRes(e) {
                if (e === "1") {
                    $("#beklemesuresi").removeClass("d-none");
                    $("#uyari").removeClass("d-none");
                    $("#uyari").html("<small>Oturumlar; oturum sırasına göre belirtilen saatlerde açılır. Oturum saatlerinin çakışması durumunda bu seçeneği seçiniz. İki oturum arasındaki bekleme süresini dakika cinsinden giriniz.</small>");
                } else if (e === "0") {
                    $("#ContentPlaceHolder1_txtBeklemeSuresi").val("0");
                    $("#uyari").removeClass("d-none");
                    $("#uyari").html("<small>Oturumlar; oturumlar sırası dikkate alınmadan belirtilen saatlerde açılır.</small>");
                } else {
                    $("#beklemesuresi").addClass("d-none");
                }
            }
        });
    </script>
</asp:Content>


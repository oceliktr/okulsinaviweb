<%@ Page Title="" Language="C#" MasterPageFile="~/ODM/MasterPage.master" AutoEventWireup="true" CodeFile="SinavEvrak.aspx.cs" Inherits="ODM.OdmSinavEvrak" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Sýnav Evraklarý</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Yönetim Giriþ</a></li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-lg-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="box box-solid box-default">
                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs">
                                    <li id="tabliSayfalar" runat="server" class="active"><a href="#ContentPlaceHolder1_Sayfalar" data-toggle="tab" aria-expanded="true"><i class="fa fa-fw fa-list-alt"></i>Evrak Ýndir</a></li>
                                    <li id="tabliKayit" runat="server"><a href="#ContentPlaceHolder1_Kayit" data-toggle="tab" aria-expanded="false"><i class="fa fa-fw fa-edit"></i>Kayýt Formu</a></li>
                                    <li id="tabliEvrakGonder" runat="server"><a href="#ContentPlaceHolder1_EvrakGonder" data-toggle="tab" aria-expanded="false"><i class="fa fa-fw fa-edit"></i>Sýnav Evraký Gönder</a></li>
                                    <li id="tabliEvrakGelenler" runat="server"><a href="#ContentPlaceHolder1_EvrakGelenler" data-toggle="tab" aria-expanded="false"><i class="fa fa-fw fa-edit"></i>Gelen Evraklar</a></li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="Sayfalar" runat="server">

                                        <table class="table table-bordered table-hover dataTable" role="grid">
                                            <thead>
                                                <tr role="row">
                                                    <th>Sýra</th>
                                                    <th>Sýnav Evraðý</th>
                                                    <th>Sýnav Adý</th>
                                                    <th>Ýndirme Zamaný</th>
                                                    <th>Ýþlem</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptSinavEvraklari" runat="server" OnItemDataBound="rptSinavEvraklari_OnItemDataBound" OnItemCommand="rptSinavEvraklari_OnItemCommand">
                                                    <ItemTemplate>
                                                        <tr role="row" class="odd">
                                                            <td>
                                                                <asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkUrl" runat="server" CommandName="Indir" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton></td>
                                                            <td><%#Eval("SinavAdi")%> <i class="label label-primary pull-right">#<%#Eval("Id") %></i></td>
                                                            <td><%#Eval("BaslangicTarihi").ToDateTime().TarihYaz() %> - <%#Eval("BitisTarihi").ToDateTime().TarihYaz() %></td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkDuzenle" runat="server" CommandName="Duzenle" CommandArgument='<%#Eval("Id") %>'><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkSil" runat="server" CommandName="Sil" OnClientClick="return confirm('Silmek istediðinizden emin misiniz?');" CommandArgument='<%#Eval("Id") %>'><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>

                                    </div>
                                    <div class="tab-pane" id="Kayit" runat="server">
                                        <div class="box box-solid box-default">
                                            <div class="box-body">
                                                <div class="col-lg-12 form-horizontal">

                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">
                                                            Sýnav
                                                            <asp:RequiredFieldValidator ControlToValidate="ddlSinavId" ValidationGroup="form" ID="RequiredFieldValidator5" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-2">
                                                            <asp:DropDownList ID="ddlSinavId" CssClass="form-control" runat="server" ValidationGroup="form"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Dosya</label>
                                                        <div class="col-sm-4">
                                                            <asp:FileUpload ID="fuFoto" CssClass="form-control" runat="server" placeholder="Fotoðraf seçiniz"></asp:FileUpload>
                                                        </div>

                                                        <label class="col-sm-2 control-label">
                                                            Veya Url Adresi</label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtUrl" CssClass="form-control" MaxLength="100" ValidationGroup="form" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">
                                                            Eriþim Baþl. Tarihi
                                                            <asp:RequiredFieldValidator ControlToValidate="txtBaslangicTarihi" ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtBaslangicTarihi" CssClass="form-control" ValidationGroup="form" runat="server" placeholder="11.11.2017 gibi"></asp:TextBox>
                                                        </div>

                                                        <label class="col-sm-2 control-label">
                                                            Eriþim Bitiþ Tarihi
                                                            <asp:RequiredFieldValidator ControlToValidate="txtBitisTarihi" ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtBitisTarihi" CssClass="form-control" ValidationGroup="form" runat="server" placeholder="11.11.2017 gibi"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <div class="form-group">
                                                                <label class="col-sm-1 control-label">Okullar</label>
                                                                <div class="col-sm-5">
                                                                    <asp:ListBox ID="lbKurumlar" Rows="15" CssClass="form-control" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <asp:Button ID="blnSecileniEkle" runat="server" Text=">>" OnClick="blnSecileniEkle_OnClick" /><br />
                                                                    <br />
                                                                    <asp:Button ID="blnSecileniCikar" runat="server" Text="<<" OnClick="blnSecileniCikar_OnClick" />
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <asp:ListBox ID="lbSecilenler" Rows="15" CssClass="form-control" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="form-group">
                                                        <label class="col-sm-10 control-label">
                                                            <asp:Literal ID="Literal1" runat="server"></asp:Literal></label>
                                                        <div class="col-sm-2">
                                                            <asp:HiddenField ID="hfId" runat="server" Value="0" />
                                                            <asp:Button ID="btnVazgec" ValidationGroup="vazgec" CssClass="btn btn-default" runat="server" Text="Vazgeç" OnClick="btnVazgec_OnClick" />
                                                            <asp:Button ID="btnDosyaEkle" ValidationGroup="form" CssClass="btn btn-primary pull-right" runat="server" Text="Ekle" OnClick="btnDosyaEkle_OnClick" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="EvrakGonder" runat="server">
                                        <div class="box box-solid box-default">
                                            <div class="box-body">
                                                <div class="col-lg-5 form-horizontal">
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Dosya</label>
                                                        <div class="col-sm-10">
                                                            <asp:FileUpload ID="fuEvrakGnder" CssClass="form-control" runat="server" placeholder="Fotoðraf seçiniz"></asp:FileUpload>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-10 control-label">
                                                            <asp:Literal ID="Literal2" runat="server"></asp:Literal></label>
                                                        <div class="col-sm-2">
                                                            <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                                            <asp:Button ID="btnEvrakGonder" ValidationGroup="eg" CssClass="btn btn-primary pull-right" runat="server" Text="Yükle" OnClick="btnEvrakGonder_OnClick" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="EvrakGelenler" runat="server">
                                                                                    <div class="box-body">
                                                <div class="form-group">
                                                    <label class="col-sm-1 control-label">Sýnav</label>
                                                    <div class="col-sm-2">
                                                        <asp:DropDownList ID="ddlSinavId2" CssClass="form-control" runat="server" ValidationGroup="form" AutoPostBack="True" OnSelectedIndexChanged="ddlSinavId2_OnSelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        <table class="table table-bordered table-hover dataTable" role="grid">
                                            <thead>
                                                <tr role="row">
                                                    <th>Sýra</th>
                                                    <th>KurumAdý</th>
                                                    <th>Dosya</th>
                                                    <th>Ýþlem</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptGelenEvraklar" runat="server" OnItemDataBound="rptGelenEvraklar_OnItemDataBound" OnItemCommand="rptGelenEvraklar_OnItemCommand">
                                                    <ItemTemplate>
                                                        <tr role="row" class="odd">
                                                            <td>
                                                                <asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></td>
                                                            <td><%#Eval("IlceAdi")%> - <%#Eval("KurumAdi")%> </td>
                                                            <td><a href="<%#Eval("Dosya")%>"><%#Eval("Dosya")%></a> </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkSil" runat="server" CommandName="Sil" OnClientClick="return confirm('Silmek istediðinizden emin misiniz?');" CommandArgument='<%#Eval("Id") %>'><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>

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


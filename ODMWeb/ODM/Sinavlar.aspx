<%@ Page Title="" Language="C#" MasterPageFile="~/ODM/MasterPage.master" AutoEventWireup="true" CodeFile="Sinavlar.aspx.cs" Inherits="ODM.OdmSinavlar" %>


<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Sınavlar</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
                <li class="active">Sınavlar</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Sınavlar</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="box box-warning">
                                        <table class="table table-bordered table-hover dataTable" id="sinavlar" role="grid">
                                            <thead>
                                                <tr role="row">
                                                    <th>Id</th>
                                                    <th>Sınavlar</th>
                                                    <th>Dönem</th>
                                                    <th>İşlem</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptKayitlar" runat="server" OnItemCommand="rptKayitlar_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr role="row" class="odd">
                                                            <td><%#Eval("Id") %></td>
                                                            <td><%#Eval("SinavAdi") %></td>
                                                            <td><%#Eval("DonemAdi") %></td>
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
                                <div class="col-md-6">
                                    <div class="box box-comments">
                                        <div class="form-horizontal">
                                            <div class="box-body">
                                                <div class="nav-tabs-custom">
                                                    <div class="box-header">
                                                        <h3 class="box-title">
                                                            <asp:Literal ID="ltrKayitBilgi" Text="Yeni sınav kayıt formu" runat="server"></asp:Literal></h3>
                                                    </div>
                                                    <asp:HiddenField ID="hfId" Value="0" runat="server" />
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">
                                                            Sınav Adı
                                                                    <asp:RequiredFieldValidator ControlToValidate="txtSinav" ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtSinav" CssClass="form-control" runat="server" placeholder="Sınav adını giriniz" ValidationGroup="form"></asp:TextBox>
                                                        </div><label class="col-sm-2 control-label">
                                                            Dönem Adı
                                                            <asp:RequiredFieldValidator ControlToValidate="txtDonem" ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtDonem" CssClass="form-control" runat="server" placeholder="Dönem adını giriniz" ValidationGroup="form"></asp:TextBox>
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
                                    <div class="box box-comments">
                                        <div class="form-horizontal">
                                            <div class="box-body">
                                                <div class="nav-tabs-custom">
                                                    <div class="box-header">
                                                        <h3 class="box-title">Aktif Sınav</h3>
                                                    </div>
                                                    <asp:HiddenField ID="HiddenField1" Value="0" runat="server" />
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label">Aktif Sınav</label>
                                                        <div class="col-sm-8">
                                                            <asp:HiddenField ID="hfAktifSinav" runat="server" />
                                                            <asp:DropDownList runat="server" ID="ddlAktifSinav" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label">Veri Girişi</label>
                                                        <div class="col-sm-8">
                                                            <asp:CheckBox runat="server" ID="cbVeriGirisi" Text=" Aktif" />
                                                        </div>
                                                    </div>
                                                    <div class="box-footer">
                                                        <asp:Button ID="btnAktifSinav" CssClass="btn btn-primary pull-right" runat="server" Text="Kaydet" OnClick="btnAktifSinav_OnClick" />
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


<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Branslar.aspx.cs" Inherits="OkulSinavi_BranslarRoot" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Branşlar</h1>
                    </div>
                </div>
            </div>
        </div>
        <section class="content">
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
                                   <div class="box-body">
                                        <div class="box box-warning">
                                            <table class="table table-bordered table-hover dataTable" role="grid">
                                                <thead>
                                                    <tr role="row">
                                                        <th>#</th>
                                                        <th>Branş Adı</th>
                                                        <th>İşlem</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptBranslar" runat="server" OnItemCommand="rptBranslar_OnItemCommand">
                                                        <ItemTemplate>
                                                            <tr role="row" class="odd">
                                                                <td><%#Eval("Id") %></td>
                                                                <td><%#Eval("BransAdi") %></td>
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
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="box box-comments">
                                                    <div class="form-horizontal">
                                                        <div class="box-body">
                                                            <div class="box-header">
                                                                <h3 class="box-title">
                                                                    <asp:Literal ID="ltrKayitBilgi" Text="Yeni Branş Kayıt Formu" runat="server"></asp:Literal></h3>
                                                            </div>
                                                            <asp:HiddenField ID="hfId" Value="0" runat="server" />
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">Branş Adı</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtBransAdi" CssClass="form-control" runat="server" placeholder="Branş Adı" ValidationGroup="form"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">Katsayı</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtKatsayi" TextMode="Number" CssClass="form-control" runat="server" placeholder="Branş Adı" ValidationGroup="form"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="box-footer">
                                                                <asp:Button ID="btnVazgec" CssClass="btn btn-default" runat="server" Text="Vazgeç" OnClick="btnVazgec_OnClick" />
                                                                <asp:Button ID="btnKaydet" CssClass="btn btn-primary pull-right" runat="server" ValidationGroup="form" Text="Kaydet" OnClick="btnKaydet_OnClick" />
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
        </section>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
</asp:Content>


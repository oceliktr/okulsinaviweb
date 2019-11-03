<%@ Page Title="" Language="C#" MasterPageFile="~/ODM/MasterPage.master" AutoEventWireup="true" CodeFile="Kazanimlar.aspx.cs" Inherits="ODM_Kazanimlar" %>


<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Kazanımlar</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Yönetim Giriş</a></li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-lg-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="box box-solid box-default">
                        <div class="box-body">
                            <div class="col-lg-12 form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-1 control-label">Sınıf</label>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="ddlSinif" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlSinif_OnSelectedIndexChanged" AutoPostBack="True">
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
                                    <label class="col-sm-1 control-label">Branş</label>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="ddlBrans" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlBrans_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box box-solid box-default">
                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs">
                                    <li id="tabliKazanimlar" runat="server" class="active"><a href="#ContentPlaceHolder1_Kazanimlar" data-toggle="tab" aria-expanded="true"><i class="fa fa-fw fa-list-alt"></i>Kazanımlar</a></li>
                                    <li id="tabliOgrenmeAlanlari" runat="server"><a href="#ContentPlaceHolder1_OgrenmeAlanlari" data-toggle="tab" aria-expanded="false"><i class="fa fa-fw fa-edit"></i>Öğrenme Alanlar</a></li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="Kazanimlar" runat="server">
                                        <div class="box box-solid box-default">
                                            <div class="box-body">
                                                <div class="col-lg-12 form-horizontal">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <div class="form-group">
                                                                <label class="col-sm-2 control-label">
                                                                    Öğrenme Alanı
                                                            <asp:RequiredFieldValidator ControlToValidate="ddlKazanimOgrenmeAlani" ValidationGroup="form" ID="RequiredFieldValidator6" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                                <div class="col-sm-3">
                                                                    <asp:DropDownList ID="ddlKazanimOgrenmeAlani" CssClass="form-control" runat="server" ValidationGroup="form" AutoPostBack="True" OnSelectedIndexChanged="ddlKazanimOgrenmeAlani_OnSelectedIndexChanged"></asp:DropDownList>
                                                                </div>
                                                                <label class="col-sm-2 control-label">
                                                                    Alt Öğrenme Alanı
                                                            <asp:RequiredFieldValidator ControlToValidate="ddlKazanimAltOgrenmeAlani" ValidationGroup="form" ID="RequiredFieldValidator7" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                                <div class="col-sm-3">
                                                                    <asp:DropDownList ID="ddlKazanimAltOgrenmeAlani" CssClass="form-control" runat="server" ValidationGroup="form"></asp:DropDownList>
                                                                </div>

                                                                <label class="col-sm-1 control-label">
                                                                    Kazanım No
                                                            <asp:RequiredFieldValidator ControlToValidate="txtKazanimNo" ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                                <div class="col-sm-1">
                                                                    <asp:TextBox ID="txtKazanimNo" CssClass="form-control" MaxLength="3" ValidationGroup="form" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">
                                                            Kazanım
                                                            <asp:RequiredFieldValidator ControlToValidate="txtKazanim" ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-10">
                                                            <asp:TextBox ID="txtKazanim" CssClass="form-control" ValidationGroup="form" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-9">
                                                            <asp:Button ID="btnKazanimVazgec" ValidationGroup="vazgec2" CssClass="btn btn-default" runat="server" Text="Vazgeç" OnClick="btnKazanimVazgec_OnClick" />
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:HiddenField ID="hfKazanimId" runat="server" Value="0" />

                                                            <asp:Button ID="btnKazanimKaydet" ValidationGroup="form" CssClass="btn btn-primary pull-right" runat="server" Text="Kaydet" OnClick="btnKazanimKaydet_OnClick" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <table class="table table-bordered table-hover dataTable" role="grid">
                                                    <thead>
                                                        <tr role="row">
                                                            <th>Sıra</th>
                                                            <th>Kazanım No</th>
                                                            <th>Kazanım</th>
                                                            <th>İşlem</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rptKazanimlar" runat="server" OnItemDataBound="rptKazanimlar_OnItemDataBound" OnItemCommand="rptKazanimlar_OnItemCommand">
                                                            <ItemTemplate>
                                                                <tr role="row" class="odd">
                                                                    <td>
                                                                        <asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Literal ID="ltrKazanimNo" runat="server" Text=""></asp:Literal>
                                                                    </td>
                                                                    <td><%#Eval("Kazanim") %></td>
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
                                    <div class="tab-pane" id="OgrenmeAlanlari" runat="server">
                                        <div class="box box-solid box-default">
                                            <div class="box-body">
                                                <div class="col-lg-8 form-horizontal">
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">
                                                            Öğrenme Alanı
                                                            <asp:RequiredFieldValidator ControlToValidate="txtOgrenmeAlani" ValidationGroup="form2" ID="RequiredFieldValidator2" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtOgrenmeAlani" CssClass="form-control" MaxLength="150" ValidationGroup="form2" runat="server"></asp:TextBox>
                                                        </div>
                                                        <label class="col-sm-2 control-label">
                                                            Alan No
                                                            <asp:RequiredFieldValidator ControlToValidate="txtOgrenmeAlaniNo" ValidationGroup="form2" ID="RequiredFieldValidator4" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="txtOgrenmeAlaniNo" CssClass="form-control" MaxLength="3" ValidationGroup="form2" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">
                                                            Üst Öğrenme Alanı
                                                            <asp:RequiredFieldValidator ControlToValidate="ddlAnaKat" ValidationGroup="form2" ID="RequiredFieldValidator5" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-10">
                                                            <asp:DropDownList ID="ddlAnaKat" CssClass="form-control" runat="server" ValidationGroup="form2"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-8">
                                                            <asp:Button ID="btnOgrenmeAlaniVazgec" ValidationGroup="vazgec" CssClass="btn btn-default" runat="server" Text="Vazgeç" OnClick="btnOgrenmeAlaniVazgec_OnClick" />
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:HiddenField ID="hfOgrenmeAlaniId" runat="server" Value="0" />
                                                            <asp:Button ID="btnOgrenmeAlaniKaydet" ValidationGroup="form2" CssClass="btn btn-primary pull-right" runat="server" Text="Kaydet" OnClick="btnOgrenmeAlaniKaydet_OnClick" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 form-horizontal callout callout-info">
                                                    <h4>Bilgi!</h4>
                                                    <p>Ekleyeceğiniz öğrenme alanı alt öğrenme alanı değil ise "Üst Öğrenme Alanı" başlığında "Üst Öğrenme Alanı" seçeneğini seçiniz.</p>
                                                </div>

                                            </div>
                                            <table class="table table-bordered table-hover dataTable" role="grid">
                                                <thead>
                                                    <tr role="row">
                                                        <th>Sıra</th>
                                                        <th>Kazanım No</th>
                                                        <th>Kazanım</th>
                                                        <th>İşlem</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptUstOgrenmeAlanlari" runat="server" OnItemDataBound="rptUstOgrenmeAlanlari_OnItemDataBound" OnItemCommand="rptUstOgrenmeAlanlari_OnItemCommand">
                                                        <ItemTemplate>
                                                            <tr role="row" class="odd">
                                                                <td>
                                                                    <asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></td>
                                                                <td>
                                                                    <asp:Literal ID="ltrKazanimNo" runat="server" Text=""></asp:Literal></td>
                                                                <td>
                                                                    <asp:Literal ID="ltrOgrenmeAlani" runat="server"></asp:Literal></td>
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
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>

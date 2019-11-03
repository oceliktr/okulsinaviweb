<%@ Page Title="" Language="C#" MasterPageFile="~/SoruBank/MasterPage.master" AutoEventWireup="true" CodeFile="Sorular.aspx.cs" Inherits="SoruBank_Sorular" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Soru Bankası</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
                <li class="active">Soru Bankası</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="box">
                        <div class="box-header">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-2">
                                            <label>Madde Türü</label>
                                            <asp:DropDownList CssClass="form-control" ID="ddlMaddeTuru" ValidationGroup="form" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-2">
                                            <label>Branş</label>
                                            <asp:DropDownList CssClass="form-control" ID="ddlBrans" ValidationGroup="form" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrans_OnSelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-2">
                                            <label>Sınıf Düzeyi</label>
                                            <asp:DropDownList ID="ddlSinif" CssClass="form-control" ValidationGroup="form" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSinif_OnSelectedIndexChanged">
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
                                        <div class="col-lg-2">
                                            <label>&nbsp; </label>
                                            <asp:Button runat="server" CssClass="form-control btn btn-success" ID="btnListele" Text="Listele" OnClick="btnListele_OnClick" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-2">
                                            <label>Öğrenme Alanı</label>
                                            <asp:DropDownList CssClass="form-control" ID="ddlOgrenmeAlani" runat="server" ValidationGroup="form" AutoPostBack="True" OnSelectedIndexChanged="ddlOgrenmeAlani_OnSelectedIndexChangedlectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-2">
                                            <label>Alt Öğrenme Alanı</label>
                                            <asp:DropDownList CssClass="form-control" ID="ddlAltOgrenmeAlani" ValidationGroup="form" AutoPostBack="True" OnSelectedIndexChanged="ddlAltOgrenmeAlani_OnSelectedIndexChanged" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-8">
                                            <label>Kazanımlar</label>
                                            <asp:ListBox ID="lbKazanimlar" Height="50" SelectionMode="Multiple" ValidationGroup="form" CssClass="form-control" on runat="server"></asp:ListBox>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="box-body">

                            <div class="box box-warning">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <table class="table table-bordered table-hover dataTable" id="sinavlar" role="grid">
                                            <thead>
                                            <tr role="row">
                                                <th>Sınıf</th>
                                                <th>Soru Kökü</th>
                                                <th>İşlem</th>
                                            </tr>
                                            </thead>
                                            <tbody>
                                            <asp:Repeater ID="rptKayitlar" runat="server" OnItemCommand="rptKayitlar_OnItemCommand" OnItemDataBound="rptKayitlar_OnItemDataBound">
                                                <ItemTemplate>
                                                    <tr role="row" class="odd">
                                                        <td><%#Eval("Sinif") %></td>
                                                        <td><%#Eval("SoruKoku").ToString().HtmlTemizle() %>  <%#Eval("Durum").ToInt32().DurumBul() %></td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkDuzenle" runat="server" PostBackUrl='<%#string.Format("SoruEkle.aspx?Id={0}",Eval("Id")) %>'><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkSil" runat="server" CommandName="Sil" OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');" CommandArgument='<%#Eval("Id") %>'><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            </tbody>
                                        </table>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnListele" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
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


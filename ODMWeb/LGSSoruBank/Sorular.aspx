<%@ Page Title="" Language="C#" MasterPageFile="~/LGSSoruBank/MasterPage.master" AutoEventWireup="true" CodeFile="Sorular.aspx.cs" Inherits="LGSSoruBank_Sorular" %>


<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>    <div class="content-wrapper">
        <section class="content-header">
            <h1>Gönderilen Sorular</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
                <li class="active">Gönderilen Sorular</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:placeholder id="phUyari" runat="server"></asp:placeholder>
                    <div class="box">
                        <div class="box-header">
                            <div class="col-md-2">
                                <b> Sınav :</b>  <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSinavlar"></asp:DropDownList>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                                
                                <div class="col-md-2">
                                    <b>Branş :</b>    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBrans" AutoPostBack="True" OnSelectedIndexChanged="ddlBrans_OnSelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <b>  Soru Yazarı:</b>  <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSoruYazarlari"></asp:DropDownList>
                                </div>
                            </ContentTemplate></asp:UpdatePanel>
                            <div class="col-md-2">
                                <b>Sınıf :</b>     <asp:DropDownList ID="ddlSinif" CssClass="form-control" ValidationGroup="form" runat="server">
                                <asp:ListItem Value="0">Seçiniz</asp:ListItem>
                                <asp:ListItem Value="7">7. Sınıf</asp:ListItem>
                                <asp:ListItem Value="8">8. Sınıf</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <b>Durum :</b>     <asp:DropDownList ID="ddlDurum" CssClass="form-control" ValidationGroup="form" runat="server">
                                    <asp:ListItem Value="0">Yeni</asp:ListItem>
                                    <asp:ListItem Value="1">İncelendi</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"><b> &nbsp;</b>
                                <asp:Button ID="btnListele" CssClass="form-control btn btn-primary" runat="server" ValidationGroup="form2" Text="Listele" OnClick="btnListele_OnClick" />
                            </div>
                            <div class="col-md-1 pull-right"><b> &nbsp;</b>
                            <a href="SoruEkleAdmin.aspx" class="form-control btn bg-purple"><i class="fa fa-fw fa-plus"></i> Yeni</a>
                        </div>
                        </div>
                        <div class="box-body">
                            
                                <div class="box box-warning">
                                    <table class="table table-bordered table-hover dataTable" id="sinavlar" role="grid">
                                        <thead>
                                            <tr role="row">
                                                <th></th>
                                                <th>Soru Yazarı</th>
                                                <th>Branş</th>
                                                <th>Sınıf</th>
                                                <th>Soru</th>
                                                <th>Soru No</th>
                                                <th>Kazanım</th>
                                                <th>Tarih</th>
                                                <th>Durum</th>
                                                <th>İşlem</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:repeater id="rptKayitlar" runat="server" onitemcommand="rptKayitlar_OnItemCommand" OnItemDataBound="rptKayitlar_OnItemDataBound">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfId" Value='<%#Eval("Id") %>' runat="server" />
                                                        <tr role="row" class="odd">
                                                            <td><asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></td>
                                                            <td><%#Eval("AdiSoyadi") %></td>
                                                            <td><%#Eval("BransAdi") %></td>
                                                            <td><%#Eval("Sinif") %></td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkDownload" CommandName="Download" CommandArgument='<%#Eval("Id") %>' runat="server"><i class="fa fa-file-word-o"></i></asp:LinkButton></td>
                                                            <td><%#Eval("Id") %> </td>
                                                            <td><%#Eval("Kazanim") %> </td>
                                                            <td><%#Eval("Tarih").ToDateTime().TarihYaz() %></td>
                                                            <td>
                                                                <asp:Literal ID="ltrOnay"  runat="server" Text='<%#Eval("Onay").ToInt32().LgsDurumBul() %>'></asp:Literal>
                                                                <asp:CheckBox ID="cbOnay" runat="server" AutoPostBack="True" ClientIDMode="AutoID" OnCheckedChanged="cbOnay_OnCheckedChanged" />
                                                                
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkDuzenle" runat="server" PostBackUrl='<%#string.Format("SoruEkleAdmin.aspx?Id={0}",Eval("Id")) %>'><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkSil" runat="server" CommandName="Sil" OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');" CommandArgument='<%#Eval("Id") %>'><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:repeater>
                                        </tbody>
                                    </table>
                                    <div class="row">
                                        <div class="col-sm-12">
                                                <ul class="pagination pull-right">
                                                    <asp:Repeater ID="rptSayfalar" runat="server" OnItemCommand="rptSayfalar_ItemCommand">
                                                        <ItemTemplate>
                                                            <li class="paginate_button">
                                                                <asp:LinkButton ID="btnSayfa" CommandName="Sayfa" CommandArgument="<%#Container.DataItem %>" runat="server"><%# Container.DataItem %></asp:LinkButton>
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            <asp:DropDownList ID="ddlEkSayfalar" Visible="false" runat="server" Width="60" AutoPostBack="True" OnSelectedIndexChanged="ddlEkSayfalar_SelectedIndexChanged">
                                            </asp:DropDownList>
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
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>


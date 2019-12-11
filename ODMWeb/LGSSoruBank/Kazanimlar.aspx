<%@ Page Title="" Language="C#" MasterPageFile="~/LGSSoruBank/MasterPage.master" AutoEventWireup="true" CodeFile="Kazanimlar.aspx.cs" Inherits="LGSSoruBank_Kazanimlar" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Kazanımlar</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
                <li class="active">Kazanımlar</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="box">
                        <div class="box-header">

                            <div class="col-md-2">
                                <label>
                                    Branş :
                                        <asp:RequiredFieldValidator ControlToValidate="ddlBrans" ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBrans"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>
                                    Sınıf Düzeyi:
                                    <asp:RequiredFieldValidator ControlToValidate="ddlSinif" ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                <asp:DropDownList ID="ddlSinif" CssClass="form-control" ValidationGroup="form" runat="server">
                                    <asp:ListItem Value="">--- Seçiniz ---</asp:ListItem>
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
                            <div class="col-md-1">
                                <b>&nbsp;</b>
                                <asp:Button ID="btnListele" CssClass="form-control btn btn-primary" runat="server" ValidationGroup="form" Text="Listele" OnClick="btnListele_OnClick" />
                            </div>
                            <div class="col-md-1 pull-right">
                                <b>&nbsp;</b>
                                <a href="KazanimEkle.aspx" class="form-control btn btn-danger"><i class="fa fa-fw fa-plus"></i>Yeni</a>
                            </div>
                        </div>
                        <div class="box-body">

                            <div class="box box-warning">
                                <table class="table table-bordered table-hover dataTable" id="sinavlar" role="grid">
                                    <thead>
                                        <tr role="row">
                                            <th></th>
                                            <th>Branş</th>
                                            <th>Sınıf</th>
                                            <th>Kazanım No</th>
                                            <th>Kazanım</th>
                                            <th>İşlem</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptKayitlar" runat="server" OnItemCommand="rptKayitlar_OnItemCommand" OnItemDataBound="rptKayitlar_OnItemDataBound">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfId" Value='<%#Eval("Id") %>' runat="server" />
                                                <tr role="row" class="odd">
                                                    <td>
                                                        <asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></td>
                                                    <td><%#Eval("BransAdi") %></td>
                                                    <td><%#Eval("Sinif") %></td>
                                                    <td><%#Eval("KazanimNo") %> </td>
                                                    <td><%#Eval("Kazanim") %> </td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkDuzenle" runat="server" PostBackUrl='<%#string.Format("KazanimEkle.aspx?Id={0}",Eval("Id")) %>'><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
</asp:Content>


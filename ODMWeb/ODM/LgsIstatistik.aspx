<%@ Page Title="" Language="C#" MasterPageFile="~/ODM/MasterPage.master" AutoEventWireup="true" CodeFile="LgsIstatistik.aspx.cs" Inherits="ODM_LgsIstatistik" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Yoklama İstatistikler </h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-lg-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="box box-default">
                        <div class="box-header">
                             <div class="col-md-3">
                                <b>Sınav :  <asp:RequiredFieldValidator ControlToValidate="ddlSinavlar" ValidationGroup="form" ID="RequiredFieldValidator2" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></b>
                                <asp:DropDownList runat="server" CssClass="form-control" ValidationGroup="form" ID="ddlSinavlar"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <b>Sınıf :  <asp:RequiredFieldValidator ControlToValidate="ddlSinif" ValidationGroup="form" ID="RequiredFieldValidator4" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></b>
                                <asp:DropDownList ID="ddlSinif" CssClass="form-control" ValidationGroup="form" runat="server">
                                    <asp:ListItem Value="">Seçiniz</asp:ListItem>
                                    <asp:ListItem Value="7">7. Sınıf</asp:ListItem>
                                    <asp:ListItem Value="8">8. Sınıf</asp:ListItem>
                                    <asp:ListItem Value="9">9. Sınıf</asp:ListItem>
                                    <asp:ListItem Value="10">10. Sınıf</asp:ListItem>
                                    <asp:ListItem Value="11">11. Sınıf</asp:ListItem>
                                    <asp:ListItem Value="12">12. Sınıf</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                           
                            <div class="col-md-2">
                                <b>&nbsp;</b>
                                <asp:Button ID="btnSinavaGirenGirmeyenler" CssClass="form-control btn btn-success" runat="server" ValidationGroup="form2" Text="Giren Girmeyen Sayıları" OnClick="btnSinavaGirenGirmeyenler_OnClick" />
                            </div>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlGirenGirmeyenler" Visible="False" runat="server">
                                
                                <table class="table table-bordered table-hover dataTable" role="grid">
                                <thead>
                                <tr role="row">
                                    <th>No</th>
                                    <th>İlçesi</th>
                                    <th>Kurum Adı</th>
                                    <th>Sınıf</th>
                                    <th>Öğrenci Sayısı</th>
                                    <th>Sınava Girmeyen Sayısı</th>
                                    <th>Katılım Oranı</th>
                                </tr>
                                </thead>
                                <tbody>
                                <asp:Repeater ID="rptGirenGirmeyenSayisi" runat="server" OnItemDataBound="rptGirenGirmeyenSayisi_OnItemDataBound">
                                    <ItemTemplate>
                                        <tr role="row" class="odd" id="satir" runat="server">
                                            <td>
                                                <asp:Label ID="lblSira" runat="server" Text=""></asp:Label></td>
                                            <td><%#Eval("IlceAdi") %></td>
                                            <td><%#Eval("KurumAdi") %> </td>
                                            <td><%#Eval("Sinifi") %> </td>
                                            <td><%#Eval("OgrenciSayisi") %> </td>
                                            <td>
                                                <asp:Literal ID="ltrGirmeyenSayisi" runat="server"></asp:Literal> </td>
                                            <td>
                                                <asp:Literal ID="ltrKatilimOrani" runat="server"></asp:Literal> </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <tr><td colspan="4" class="text-center">
                                            <asp:Literal ID="ltrBilgi" runat="server"></asp:Literal>
                                        </td></tr>
                                    </FooterTemplate>
                                </asp:Repeater>
                                </tbody>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
</asp:Content>

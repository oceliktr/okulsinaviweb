<%@ Page Title="" Language="C#" MasterPageFile="~/ODM/MasterPage.master" AutoEventWireup="true" CodeFile="LGSKazanimKarne.aspx.cs" Inherits="ODM_LGSKazanimKarne" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Kazanım Karneleri
                <small>
                    <asp:Literal ID="ltrDonemAdi" runat="server"></asp:Literal></small></h1>
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
                            <div class="col-md-2">
                                <b>Sınav :</b>
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSinavlar"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <b>İlçe :</b>
                                <asp:DropDownList ID="ddlIlce" CssClass="form-control" ValidationGroup="form" runat="server">
                                    <asp:ListItem Value="">Seçiniz</asp:ListItem>
                                    <asp:ListItem Value="AŞKALE">AŞKALE</asp:ListItem>
                                    <asp:ListItem Value="AZİZİYE">AZİZİYE</asp:ListItem>
                                    <asp:ListItem Value="ÇAT">ÇAT</asp:ListItem>
                                    <asp:ListItem Value="HINIS">HINIS</asp:ListItem>
                                    <asp:ListItem Value="HORASAN">HORASAN</asp:ListItem>
                                    <asp:ListItem Value="İSPİR">İSPİR</asp:ListItem>
                                    <asp:ListItem Value="KARAÇOBAN">KARAÇOBAN</asp:ListItem>
                                    <asp:ListItem Value="KARAYAZI">KARAYAZI</asp:ListItem>
                                    <asp:ListItem Value="KÖPRÜKÖY">KÖPRÜKÖY</asp:ListItem>
                                    <asp:ListItem Value="NARMAN">NARMAN</asp:ListItem>
                                    <asp:ListItem Value="OLTU">OLTU</asp:ListItem>
                                    <asp:ListItem Value="OLUR">OLUR</asp:ListItem>
                                    <asp:ListItem Value="PALANDÖKEN">PALANDÖKEN</asp:ListItem>
                                    <asp:ListItem Value="PASİNLER">PASİNLER</asp:ListItem>
                                    <asp:ListItem Value="PAZARYOLU">PAZARYOLU</asp:ListItem>
                                    <asp:ListItem Value="ŞENKAYA">ŞENKAYA</asp:ListItem>
                                    <asp:ListItem Value="TEKMAN">TEKMAN</asp:ListItem>
                                    <asp:ListItem Value="TORTUM">TORTUM</asp:ListItem>
                                    <asp:ListItem Value="UZUNDERE">UZUNDERE</asp:ListItem>
                                    <asp:ListItem Value="YAKUTİYE">YAKUTİYE</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            
                            <div class="col-md-1">
                                <b>&nbsp;</b>
                                <asp:Button ID="btnListele" CssClass="form-control btn btn-primary" runat="server" ValidationGroup="form2" Text="Listele" OnClick="btnListele_OnClick" />
                            </div>
                        </div>
                        <div class="box-body">
                            <table class="table table-bordered table-hover dataTable" role="grid">
                                <thead>
                                <tr role="row">
                                    <th>Kurum Kodu</th>
                                    <th>Kurum Adı</th>
                                    <th>Karne</th>
                                </tr>
                                </thead>
                                <tbody>
                                <tr role="row" class="odd" id="ilceTr" runat="server">
                                    <td></td>
                                    <td>
                                        <asp:Literal ID="ltrIlceAdi" runat="server"></asp:Literal> İLÇE KARNESİ</td>
                                    <td>
                                        <asp:LinkButton ToolTip="İlçe Karnesini İndir" ID="lnkIlceKarnesi" OnClick="lnkIlceKarnesi_OnClick" runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>

                                    </td>
                                </tr>
                                <asp:Repeater ID="rptKurumlar" runat="server" OnItemCommand="rptKurumlar_OnItemCommand">
                                    <ItemTemplate>
                                        <tr role="row" class="odd">
                                            <td><%#Eval("KurumKodu") %></td>
                                            <td><%#Eval("KurumAdi") %> Karnesi</td>
                                            <td>
                                                <asp:LinkButton ID="lnkOkulKarnesi" ToolTip="Okul Karnesini İndir" runat="server" CommandName="Karne" CommandArgument='<%#string.Format("{0},{1},{2},{3}",Eval("SinavId"),Eval("KurumKodu"),Eval("IlceAdi"),Eval("KurumAdi") ) %>'><i class="fa fa-file-pdf-o"></i></asp:LinkButton>

                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </tbody>
                            </table>
                            
                        </div>
                           
                    </div>
                  
                </div>
                <div class="col-md-7"><div class="box box-danger">
                    <div class="box-body">
                        <ul class="list-unstyled">
                            <li>Sınav ve ilçe seçimi yaparak listele butonuna tıklayınız. Sınava giren okullar listelenecektir.</li>
                            <li>İlçe alanı okul ve kurumlar için sınırlandırılmıştır.</li>
                            <li>İlçe ve okul karnelerini bilgisayarınıza indirmek için <i class="fa fa-file-pdf-o"></i> butonuna tıklayınız.</li>
                            <li><b>İlçe Karnesi :</b> Branş ve sınıf düzeyinde ilçe karnesini içermektedir. Sayfa sayısı, sınava katılan branş ve sınıf sayısına göre değişmektedir.</li>
                            <li><b>Okul Karnesi :</b> Branş ve sınıf ve şube düzeyinde sınıf, şube ve öğrenci karnelerini içermektedir. Sayfa sayısı, sınava katılan branş, sınıf, şube ve öğrenci sayısına göre değişmektedir.</li>
                        </ul>
                    </div>
                </div></div>
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
</asp:Content>


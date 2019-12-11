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
                            <div class="col-md-4">
                                <b>Sınav :  <asp:RequiredFieldValidator ControlToValidate="ddlSinavlar" ValidationGroup="form" ID="RequiredFieldValidator2" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></b>
                                <asp:DropDownList runat="server" CssClass="form-control" ValidationGroup="form" ID="ddlSinavlar"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <b>İlçe :  <asp:RequiredFieldValidator ControlToValidate="ddlIlce" ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></b>
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
                            <div class="col-md-1">
                                <b>&nbsp;</b>
                                <asp:Button ID="btnListele" CssClass="form-control btn btn-primary" runat="server" ValidationGroup="form" Text="Listele" OnClick="btnListele_OnClick" />
                            </div>
                            <div class="col-md-2">
                                <b>&nbsp;</b>
                                <asp:Button ID="btnIlKarnesi" CssClass="form-control btn btn-success" runat="server" ValidationGroup="form2" Text="İl Karnesi" OnClick="btnIlKarnesi_OnClick" />
                            </div>
                        </div>
                        <div class="box-body">
                            <table class="table table-bordered table-hover dataTable" role="grid">
                                <thead>
                                <tr role="row">
                                    <th>Kurum Kodu</th>
                                    <th>Kurum Adı</th>
                                    <th colspan="2">Karne</th>
                                </tr>
                                </thead>
                                <tbody>
                                <tr role="row" class="odd" id="ilceTr" runat="server">
                                    <td></td>
                                    <td>
                                        <asp:Literal ID="ltrIlceAdi" runat="server"></asp:Literal> İLÇE KARNESİ </td>
                                    <td>
                                        <asp:LinkButton ToolTip="İlçe Karnesini İndir" ID="lnkIlceKarnesi" OnClick="lnkIlceKarnesi_OnClick" runat="server"><i class="fa fa-file-pdf-o"></i> İlçe Karnesi</asp:LinkButton> 

                                    </td>
                                </tr>
                                <asp:Repeater ID="rptKurumlar" runat="server" OnItemCommand="rptKurumlar_OnItemCommand" OnItemDataBound="rptKurumlar_OnItemDataBound">
                                    <ItemTemplate>
                                        <tr role="row" class="odd">
                                            <td><%#Eval("KurumKodu") %></td>
                                            <td><%#Eval("KurumAdi") %> </td>
                                            <td>
                                               <asp:LinkButton ID="lnkOkulKarnesi" ToolTip="Okul Karnesini İndir" runat="server" CommandName="okul" CommandArgument='<%#string.Format("{0},{1},{2},{3}",Eval("SinavId"),Eval("KurumKodu"),Eval("IlceAdi"),Eval("KurumAdi") ) %>'><i class="fa fa-file-pdf-o"></i> Okul Karnesi</asp:LinkButton> 
                                            </td>
                                            <td>  <asp:LinkButton ID="lnkOgrenci" ToolTip="Öğrenci Karnesini İndir" runat="server" CommandName="ogrenci" CommandArgument='<%#string.Format("{0},{1},{2},{3}",Eval("SinavId"),Eval("KurumKodu"),Eval("IlceAdi"),Eval("KurumAdi") ) %>'><i class="fa fa-file-pdf-o"></i> Öğrenci Karnesi</asp:LinkButton> 
                                            </td>
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
                            
                        </div>
                           
                    </div>
                  
                </div>
                <div class="col-md-7"><div class="box box-danger">
                    <div class="box-body">
                        <ul class="list-unstyled">
                            <li>Sınav, ilçe, sınıf ve ders seçimi yaparak listele butonuna tıklayınız. Sınava giren okullar listelenecektir.</li>
                            <li>İlçe alanı okul ve kurumlar için sınırlandırılmıştır.</li>
                            <li>İlçe ve okul karnelerini bilgisayarınıza indirmek için <i class="fa fa-file-pdf-o"></i> butonuna tıklayınız.</li>
                            <li><b>İlçe Karnesi :</b>Branş ve sınıf düzeyinde ilçe karnesini içermektedir. Sayfa sayısı, sınava katılan branş ve sınıf sayısına göre değişmektedir.</li>
                            <li><b>Okul Karnesi :</b>Seçilen branş ve sınıf ve şube düzeyinde sınıf, şube ve öğrenci karnelerini içermektedir. Sayfa sayısı, sınava katılan branş, sınıf, şube ve öğrenci sayısına göre değişmektedir.</li>
                        </ul>
                    </div>
                </div></div>
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    
</asp:Content>


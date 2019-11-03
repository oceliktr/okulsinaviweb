<%@ Page Title="" Language="C#" MasterPageFile="~/ODM/MasterPage.master" AutoEventWireup="true" CodeFile="KazanimKarneAdmin.aspx.cs" Inherits="ODM_KazanimKarneAdmin" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @media print {
            body * {
                visibility: hidden;
            }

            #ContentPlaceHolder1_yazdir * {
                visibility: visible;
            }

            #ContentPlaceHolder1_yazdir {
                position: absolute;
                top: 40px;
                left: 30px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Erzurum Ölçme Değerlendirme Merkezi
        <small>
            <asp:Literal ID="ltrDonemAdi" runat="server"></asp:Literal></small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-lg-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="box box-solid box-default">
                        <div class="box-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="col-md-2">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBrans"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ValidationGroup="form" ID="RequiredFieldValidator4" ForeColor="Red" ControlToValidate="ddlBrans" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlIlce" AutoPostBack="True" OnSelectedIndexChanged="ddlIlce_OnSelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ValidationGroup="form" ID="RequiredFieldValidator5" ForeColor="Red" ControlToValidate="ddlIlce" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOkul" AutoPostBack="True" OnSelectedIndexChanged="ddlOkul_OnSelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="ddlOkul" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSube"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" ControlToValidate="ddlSube" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-md-1">
                                <asp:Button ID="btnListele" CssClass="btn btn-primary" ValidationGroup="form" runat="server" Text="Listele" OnClick="btnListele_OnClick" />
                            </div>
                            <div class="col-md-1">
                                <button type="button" onclick="window.print();" class="btn btn-default"><i class="fa fa-print"></i>Yazdır</button>
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btnRapor" CssClass="btn btn-danger" ValidationGroup="formR" runat="server" Text="Rapor" OnClick="btnRapor_OnClick" />
                                <br/><a href="KazanimKarne.aspx">İl Raporu</a>
                            </div>
                        </div>
                    </div>
                    <asp:Panel id="yazdir" runat="server" visible="False">
                       
                        <div class="box box-solid box-default">
                            <div class="box-body">

                                <div style="width: 100%">
                                    <div style="font-size: 16px; text-align: center; font-weight: bold;">
                                        ERZURUM İL MİLLİ EĞİTİM MÜDÜRLÜĞÜ<br />
                                        Ölçme Değerlendirme Merkezi<br />
                                        <asp:Literal ID="ltrBaslik" runat="server"></asp:Literal>
                                        <asp:Literal ID="ltrSinif" runat="server"></asp:Literal>
                                    </div>
                                    <div style="font-size: 14px; text-align: center; font-weight: bold;">
                                        <strong>
                                            <asp:Literal ID="ltrIlceOkulSube" runat="server"></asp:Literal></strong>
                                    </div>
                                </div>
                                <hr />
                                <asp:PlaceHolder ID="phIlRapor" Visible="False" runat="server">
                                    <table class="table table-bordered table-striped dataTable" style="width: 100%">
                                        <tr>
                                            <th>&nbsp;</th>
                                            <th>&nbsp;</th>
                                            <th>&nbsp;</th>
                                            <th colspan="2" style="text-align: center;">Edinilme Oranı %</th>
                                        </tr>
                                        <tr>
                                            <th>No</th>
                                            <th style="width: 50px;">Kznm No</th>
                                            <th>Kazanım</th>
                                            <th style="width: 50px">Türkiye</th>
                                            <th style="width: 50px">İl</th>
                                        </tr>
                                        <asp:Repeater ID="rptKazanimKarneIl" runat="server" OnItemDataBound="rptKazanimKarneIl_OnItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <asp:Label ID="lblSira" runat="server"></asp:Label></td>
                                                    <td>
                                                        <%#Eval("KazanimNo") %></td>
                                                    <td><%#Eval("Kazanim") %></td>
                                                    <td style="text-align: center;">
                                                        <asp:Literal ID="ltrTrBasariOrani" runat="server"></asp:Literal></td>
                                                    <td style="text-align: center;">
                                                        <asp:Literal ID="ltrIlBasariOrani" runat="server"></asp:Literal></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <th></th>
                                            <th style="width: 50px;"> </th>
                                            <th></th>
                                            <th style="width: 50px">
                                                <asp:Literal ID="ltrTrOrtalama" runat="server"></asp:Literal></th>
                                            <th style="width: 50px">
                                                <asp:Literal ID="ltrIlOrtalama" runat="server"></asp:Literal></th>
                                        </tr>
                                    </table>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="phIlceRapor" Visible="False" runat="server">
                                    <table class="table table-bordered table-striped dataTable" style="width: 100%">
                                        <tr>
                                            <th>&nbsp;</th>
                                            <th>&nbsp;</th>
                                            <th>&nbsp;</th>
                                            <th colspan="3" style="text-align: center;">Edinilme Oranı %</th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <th>No</th>
                                            <th style="width: 50px;">Kznm No</th>
                                            <th>Kazanım</th>
                                            <th style="width: 50px">Türkiye</th>
                                            <th style="width: 50px">İl</th>
                                            <th style="width: 50px">İlçe</th>
                                            <th style="width: 170px"></th>
                                        </tr>
                                        <asp:Repeater ID="rptKazanimKarneIlce" runat="server" OnItemDataBound="rptKazanimKarneIlce_OnItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <asp:Label ID="lblSira" runat="server"></asp:Label></td>
                                                    <td>
                                                        <%#Eval("KazanimNo") %></td>
                                                    <td><%#Eval("Kazanim") %></td>
                                                    <td style="text-align: center;">
                                                        <asp:Literal ID="ltrTrBasariOrani" runat="server"></asp:Literal></td>
                                                    <td style="text-align: center;">
                                                        <asp:Literal ID="ltrIlBasariOrani" runat="server"></asp:Literal></td>
                                                    <td style="text-align: center;">
                                                        <asp:Literal ID="ltrBasariOrani" runat="server"></asp:Literal></td>
                                                    <td>
                                                        <asp:Literal ID="ltrSonuc" runat="server"></asp:Literal></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="phOkulRapor" Visible="False" runat="server">
                                    <table class="table table-bordered table-striped dataTable" style="width: 100%">
                                        <tr>
                                            <th>&nbsp;</th>
                                            <th>&nbsp;</th>
                                            <th>&nbsp;</th>
                                            <th colspan="4" style="text-align: center;">Edinilme Oranı %</th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <th>No</th>
                                            <th style="width: 50px;">Kznm No</th>
                                            <th>Kazanım</th>
                                            <th style="width: 50px">Türkiye</th>
                                            <th style="width: 50px">İl</th>
                                            <th style="width: 50px">İlçe</th>
                                            <th style="width: 50px">Okul</th>
                                            <th style="width: 170px"></th>
                                        </tr>
                                        <asp:Repeater ID="rptKazanimKarneOkul" runat="server" OnItemDataBound="rptKazanimKarneOkul_OnItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <asp:Label ID="lblSira" runat="server"></asp:Label></td>
                                                    <td>
                                                        <%#Eval("KazanimNo") %></td>
                                                    <td><%#Eval("Kazanim") %></td>
                                                    <td style="text-align: center;">
                                                        <asp:Literal ID="ltrTrBasariOrani" runat="server"></asp:Literal></td>
                                                    <td style="text-align: center;">
                                                        <asp:Literal ID="ltrIlBasariOrani" runat="server"></asp:Literal></td>
                                                    <td style="text-align: center;">
                                                        <asp:Literal ID="ltrIlceBasariOrani" runat="server"></asp:Literal></td>
                                                    <td style="text-align: center;">
                                                        <asp:Literal ID="ltrOkulBasariOrani" runat="server"></asp:Literal></td>
                                                    <td>
                                                        <asp:Literal ID="ltrSonuc" runat="server"></asp:Literal></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="phSubeRapor" runat="server">
                                    <table class="table table-bordered table-striped dataTable" style="width: 100%">
                                        <tr>
                                            <th>&nbsp;</th>
                                            <th>&nbsp;</th>
                                            <th>&nbsp;</th>
                                            <th colspan="5" style="text-align: center;">Edinilme Oranı %</th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <th>No</th>
                                            <th style="width: 50px;">Kznm No</th>
                                            <th>Kazanım</th>
                                            <th style="width: 50px">Türkiye</th>
                                            <th style="width: 50px">İl</th>
                                            <th style="width: 50px">İlçe</th>
                                            <th style="width: 50px">Okul</th>
                                            <th style="width: 50px">Şube</th>
                                            <th style="width: 170px"></th>
                                        </tr>
                                        <asp:Repeater ID="rptKazanimKarneSube" runat="server" OnItemDataBound="rptKazanimKarneSube_OnItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <asp:Label ID="lblSira" runat="server"></asp:Label></td>
                                                    <td>
                                                        <%#Eval("KazanimNo") %></td>
                                                    <td><%#Eval("Kazanim") %></td>
                                                    <td style="text-align: center;">
                                                        <asp:Literal ID="ltrTrBasariOrani" runat="server"></asp:Literal></td>
                                                    <td style="text-align: center;">
                                                        <asp:Literal ID="ltrIlBasariOrani" runat="server"></asp:Literal></td>
                                                    <td style="text-align: center;">
                                                        <asp:Literal ID="ltrIlceBasariOrani" runat="server"></asp:Literal></td>
                                                    <td style="text-align: center;">
                                                        <asp:Literal ID="ltrOkulBasariOrani" runat="server"></asp:Literal></td>
                                                    <td style="text-align: center;">
                                                        <asp:Literal ID="ltrSubeBasariOrani" runat="server"></asp:Literal></td>
                                                    <td>
                                                        <asp:Literal ID="ltrSonuc" runat="server"></asp:Literal></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>

                                </asp:PlaceHolder>
                                <br />
                                <asp:Literal ID="ltrRapor" runat="server"></asp:Literal>
                                <br />
                                <br />
                                <br />
                                <center><i>Erzurum Ölçme ve Değerlendirme Merkezi - Adres: Yukarı Mumcu Cad. Atatürk Evi Sok. No:1 Kat:5-6 Yakutiye/Erzurum<br/> Web: erzurumodm.meb.gov.tr&nbsp;E-Mail: odm25@meb.gov.tr</i></center>

                            </div>
                        </div>
                    </asp:Panel>

                    <asp:PlaceHolder runat="server" ID="phRaporAlmayanlar" Visible="False">
                        <div class="box box-solid box-default">
                            <div class="box-body">
                                <div class="col-sm-12">

                                    <table class="table table-bordered table-hover" role="grid" aria-describedby="example2_info">
                                        <thead>
                                            <tr role="row">
                                                <th>xNO</th>
                                                <th>İLÇE</th>
                                                <th>KURUM ADI</th>
                                                <th>DERS ADI</th>
                                                <th>SINIF</th>
                                                <th>ŞUBE</th>
                                                <th>KARNE TÜRÜ</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Literal ID="ltrRaporAlamayanlar" runat="server"></asp:Literal>
                                          </tbody>
                                    </table>
                                </div>
                               
                            </div>
                        </div>
                    </asp:PlaceHolder>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
</asp:Content>


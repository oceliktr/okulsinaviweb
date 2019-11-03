<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KazanimKarne.aspx.cs" Inherits="ODM_KazanimKarne" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kazanım Karneleri</title>

    <style type="text/css">
        @import url('https://fonts.googleapis.com/css?family=Roboto');

        body {
            font-family: 'Roboto', sans-serif;
            font-size: 11px;
        }


        .col-md-2 {
            width: 186px;
            float: left;
        }

        .table {
            border-collapse: collapse;
            background-color: #515459;
            border: 1px solid black;
            margin-top: 10px;
        }
        .bilgi {
            font-size: 16px;
        }
            .table td {
                border: 1px solid black;
                background-color: #fff;
                color: black;
                border: 1px solid black;
                vertical-align: center;
                font-size: 12px;
                padding: 2px;
            }

            .table th {
                background-color: #515459;
                color: #fff;
                text-align: center;
            }

        @media print {
            body * {
                visibility: hidden;
            }

            #yazdir * {
                visibility: visible;
            }

            #yazdir {
                position: absolute;
                top: 40px;
                left: 30px;
            }
        }

        .break {
            page-break-after: always;
        }
        a {
            text-decoration: none;
            vertical-align: top;
            font-size: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
       <div class="bilgi">Sayın, <asp:Literal ID="ltrOkulIlceAdi" runat="server"></asp:Literal> yetkilisi;<br/><br/> Aşağıdaki alandan ders adını ve şubeyi seçip listeleyiniz. Okul ortalamasını almak için şube seçmeyiniz.<br/>Not: Tek şubesi olan okullar için şube karnesi düzenlenmemektedir.<br/><br/>
       </div>
        <div class="col-md-2">
        <b>  Ders Adı:</b>  <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBrans"></asp:DropDownList>
            <asp:RequiredFieldValidator ValidationGroup="form" ID="RequiredFieldValidator4" ForeColor="Red" ControlToValidate="ddlBrans" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-2">
        <b>Sınıf/Şube :</b>    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSube"></asp:DropDownList>
            <asp:RequiredFieldValidator ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" ControlToValidate="ddlSube" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnListele" CssClass="btn btn-primary" ValidationGroup="form" runat="server" Text="Listele" OnClick="btnListele_OnClick" />
        </div>
        <a href="KazanimKarne.aspx">Yenile</a>
        <asp:PlaceHolder ID="phRapor" Visible="False" runat="server"><a href="#" onclick="window.print();"><img src="images/print.png" alt="Yazdır" style=" cursor: pointer;" /> Rapor Yazdır</a></asp:PlaceHolder>
        <p>
            <br />
        </p>
        <div id="yazdir" runat="server" visible="False">
            <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
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
            <center><i>Erzurum Ölçme ve Değerlendirme Merkezi - Adres: Yukarı Mumcu Cad. Atatürk Evi Sok. No:1 Kat:4-5 Yakutiye/Erzurum<br/> Web: erzurumodm.meb.gov.tr&nbsp;E-Mail: odm25@meb.gov.tr</i></center>

        </div>
    </form>
</body>
</html>

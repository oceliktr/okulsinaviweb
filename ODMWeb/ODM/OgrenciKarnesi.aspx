<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OgrenciKarnesi.aspx.cs" Inherits="ODM_OgrenciKarnesi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .table {
            border-collapse: collapse;
            background-color: #515459;
            border: 1px solid black;
        }

            .table td {
                border: 1px solid black;
                background-color: #fff;
                color: black;
                border: 1px solid black;
                vertical-align: center;
                font-size: 10px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="box">
                        <div class="box-header">
                            <div class="col-md-2">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSinavId" AutoPostBack="True" OnSelectedIndexChanged="ddlSinavId_OnSelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator ValidationGroup="form" ID="RequiredFieldValidator2" ForeColor="Red" ControlToValidate="ddlSinavId" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOkul" AutoPostBack="True" OnSelectedIndexChanged="ddlOkul_OnSelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="ddlOkul" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSube"></asp:DropDownList>
                                <asp:RequiredFieldValidator ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" ControlToValidate="ddlSube" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBrans"></asp:DropDownList>
                                <asp:RequiredFieldValidator ValidationGroup="form" ID="RequiredFieldValidator4" ForeColor="Red" ControlToValidate="ddlBrans" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-1">
                                <asp:Button ID="btnListele" CssClass="btn btn-primary" runat="server" Text="Listele" OnClick="btnListele_OnClick" />
                            </div>

                            <div class="col-md-1">
                                <a href="javascript:window.print()">Yazdır</a>
                                <asp:Button ID="btnPdfOlustur" CssClass="btn btn-primary" runat="server" Text="Pdf Oluştur" OnClick="btnPdfOlustur_OnClick" />
                                <br />
                                <asp:TextBox ID="Literal1" runat="server" Width="400"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="box-body" id="yazdir"><p class="break"></p>
                        <asp:Repeater ID="rptOgrenciler" runat="server" OnItemDataBound="rptOgrenciler_OnItemDataBound">
                            <ItemTemplate>
                               <div style="width: 100%">
                                    <div style="font-size: 16px; text-align: center; font-weight: bold;">
                                        <asp:Literal ID="ltrBaslik" runat="server"></asp:Literal>
                                    </div>
                                    <div style="text-align: center"><strong>ÖĞRENCİ KARNESİ</strong></div>
                                </div>
                                <hr /> <asp:HiddenField ID="hfOgrenciId" runat="server" Value='<%#Eval("OgrenciId") %>' />
                                <table class="table" style="width: 500px">
                                    <tr>
                                        <th colspan="2">ÖĞRENCİ BİLGİLERİ</th>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px">ADI SOYADI</td>
                                        <td><%#Eval("Adi") %> <%#Eval("Soyadi") %></td>
                                    </tr>
                                    <tr>
                                        <td>SINIFI / OKUL NO</td>
                                        <td><%#Eval("Sinifi") %> - <%#Eval("Sube") %> / <%#Eval("OgrOkulNo") %></td>
                                    </tr>
                                    <tr>
                                        <td>İLÇESİ - OKULU</td>
                                        <td>
                                            <asp:Literal ID="ltrIlcesi" runat="server"></asp:Literal>
                                            - 
                                                                <asp:Literal ID="ltrOkulu" runat="server"></asp:Literal></td>
                                    </tr>
                                </table>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 30%">
                                            <table class="table" style="width: 100%">
                                                <tr>
                                                    <th colspan="5">Uygulama Sonucu</th>

                                                </tr>
                                                <tr>
                                                    <th><strong>Soru Sayısı</strong></th>
                                                    <th><strong>D</strong></th>
                                                    <th><strong>Y</strong></th>
                                                    <th><strong>B</strong></th>
                                                    <th><strong>KT</strong></th>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center">
                                                        <asp:Literal ID="ltrSoyuSayisi" runat="server"></asp:Literal></td>
                                                    <td style="text-align: center">
                                                        <asp:Literal ID="ltrDogruSoruSayisi" runat="server"></asp:Literal></td>
                                                    <td style="text-align: center">
                                                        <asp:Literal ID="ltrYanlisSoruSayisi" runat="server"></asp:Literal></td>
                                                    <td style="text-align: center">
                                                        <asp:Literal ID="ltrBosSoruSayisi" runat="server"></asp:Literal></td>
                                                    <td style="text-align: center">
                                                        <asp:Literal ID="ltrKitapcikTuru" runat="server"></asp:Literal></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 70%; padding: 8px">
                                            <table class="table" style="width: 100%">
                                                <tr>
                                                    <th>&nbsp;</th>
                                                    <asp:Repeater runat="server" ID="rptSoruNo">
                                                        <ItemTemplate>
                                                            <th style="width: 10px; text-align: center"><%#Eval("SoruNo") %></th>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tr>
                                                <tr>
                                                    <th style="width: 100px; text-align: center">Cevap Anahtarı</th>
                                                    <asp:Repeater runat="server" ID="rptSecenekA" Visible="False">
                                                        <ItemTemplate>
                                                            <td style="width: 10px; text-align: center"><%#Eval("KitapcikA") %></td>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:Repeater runat="server" ID="rptSecenekB" Visible="False">
                                                        <ItemTemplate>
                                                            <td style="width: 10px; text-align: center"><%#Eval("KitapcikB") %></td>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tr>
                                                <tr>
                                                    <th style="width: 100px; text-align: center">Öğrenci Cevabı</th>
                                                    <asp:Repeater runat="server" ID="rptCevaplar">
                                                        <ItemTemplate>
                                                            <td style="width: 10px; text-align: center"><%#Eval("Secenek") %></td>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tr>
                                            </table>

                                        </td>
                                    </tr>
                                </table>

                                <table class="table" style="width: 100%">
                                    <tr>
                                        <th><strong>S. No</strong></th>
                                        <th><strong>Sonuç</strong></th>
                                        <th><strong>Kazanım</strong></th>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptKazanim" OnItemDataBound="rptKazanim_OnItemDataBound">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfOgrIdValue" runat="server" Value='<%#DataBinder.Eval(((RepeaterItem)Container.Parent.Parent).DataItem,"OgrenciId") %>' />
                                            <tr>
                                                <td style="text-align: center"><%#Eval("SoruNo") %></td>
                                                <td style="text-align: center">
                                                    <asp:Literal ID="ltrDY" runat="server"></asp:Literal></td>
                                                <td>
                                                    <asp:Literal ID="ltrKazanimKarne" runat="server"></asp:Literal></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>

                                <table class="table" style="width: 100%">
                                    <tr>
                                        <th colspan="3">ÖĞRETMEN GÖRÜŞÜ</th>
                                    </tr>
                                    <tr>
                                        <th>Olumlu Yönler</th>
                                        <th>Geliştirilmesi Gereken Yönler</th>
                                        <th>Yapması Gerekenler</th>
                                    </tr>
                                    <tr>
                                        <td style="height: 180px">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="right">

                                            <p>Ders Öğretmeni (Adı Soyadı İmza) : ..........................................</p>
                                            <p>&nbsp;</p>
                                        </td>
                                    </tr>
                                </table>
                                <p class="break"></p>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>
                </div>
            </div>
        </section>

    </form>
</body>
</html>

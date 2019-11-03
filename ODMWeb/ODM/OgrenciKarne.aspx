<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OgrenciKarne.aspx.cs" Inherits="ODM_OgrenciKarne" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

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
        <table border="1">
            <tr>
                <td rowspan="2">Id</td>
                <td rowspan="2">İLÇE</td>
                <td rowspan="2">KURUM KODU</td>
                <td rowspan="2">KURUM ADI</td>
                <td rowspan="2">ÖĞRENCİ TC</td>
                <td rowspan="2">UYRUĞU</td>
                <td colspan="4">TÜRKÇE</td>
                <td colspan="4">MATEMATİK</td>
                <td colspan="4">FEN BİLİMLERİ</td>
            </tr>
            <tr>
                <td>KİTAPÇIK </td>
                <td>DOĞRU</td>
                <td>YANLIŞ</td>
                <td>BOŞ</td>
                <td>KİTAPÇIK </td>
                <td>DOĞRU</td>
                <td>YANLIŞ</td>
                <td>BOŞ</td>
                <td>KİTAPÇIK </td>
                <td>DOĞRU</td>
                <td>YANLIŞ</td>
                <td>BOŞ</td>
            </tr>
            <asp:Repeater runat="server" ID="rptOgrenciler" OnItemDataBound="rptOgrenciler_OnItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("OgrenciId") %></td>
                        <td>
                            <asp:Literal ID="ltrIlceAdi" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltrKurumKodu" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltrKurumAdi" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltrGeciciTc" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltrUyrugu" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltrTKitapciktur" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltrTDogru" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltrTYanlis" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltrTBos" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltrMKitapciktur" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltrMDogru" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltrMYanlis" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltrMBos" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltrFKitapciktur" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltrFDogru" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltrFYanlis" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltrFBos" runat="server"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

        </table>
    </form>
</body>
</html>

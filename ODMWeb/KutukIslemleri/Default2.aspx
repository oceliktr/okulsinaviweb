<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="KutukIslemleriDefault2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        table, th, td {
            border-collapse: collapse;
            padding: 1px;
            border: 1px solid black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table border="1">
               <tr>
                    <td><b></b></td>
                    <td><b>Ilce</b></td>
                    <td><b>KurumKodu</b></td>
                    <td><b>KurumAdi</b></td>
                    <td><b>Sube Sayısı</b></td>
                    <td><b>Öğrenci Sayısı</b></td>
                </tr>
            <asp:Repeater ID="rptOkullar" runat="server" OnItemDataBound="rptOkullar_OnItemDataBound">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></td>
                    <td><%#Eval("Ilce") %></td>
                    <td><%#Eval("KurumKodu") %></td>
                    <td><%#Eval("KurumAdi") %></td>
                    <td>
                        <asp:Literal ID="ltrToplamSubeSayisi" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="ltrToplamOgrenciSayisi" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
       </table>
    </div>
    </form>
</body>
</html>

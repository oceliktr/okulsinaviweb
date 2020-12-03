<%@ page language="C#" autoeventwireup="true" inherits="OkulSinavi_CevrimiciSinavYonetim_Sorular, okulsinavi" enableEventValidation="false" %>

<table class="table table-striped table-responsive-sm">
    <thead>
        <tr>
            <th>#</th>
            <th>Branş Adı</th>
            <th>Soru</th>
            <th>Soru No</th>
            <th>Cevap</th>
            <th>İşlem</th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="rptSorular" runat="server" OnItemDataBound="rptSorular_OnItemDataBound">
            <itemtemplate>
            <tr>
                <th scope="row"><asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></th>
                <td><%#Eval("BransAdi") %></td>
                <td><button type="button" data-toggle="modal" data-target="#soruModal" data-id="<%#Eval("Id") %>" class="btn btn-sm btn-default"><i class="fa fa-copy"></i></button></td>
                <td><%#Eval("SoruNo") %></td>
                <td><%#Eval("Cevap") %></td>
                <td>
                    <a class="btn btn-default btn-sm" href="SoruKayit.aspx?OturumId=<%#Eval("OturumId") %>&id=<%#Eval("Id") %>"><i class="fa fa-edit"></i></a>
                    <a href="#" class="btn btn-default btn-sm" onclick="Sil(<%#Eval("Id") %>)"><i class="far fa-trash-alt"></i></a>
                </td>
            </tr>
        </itemtemplate>
        </asp:Repeater>
    </tbody>
</table>

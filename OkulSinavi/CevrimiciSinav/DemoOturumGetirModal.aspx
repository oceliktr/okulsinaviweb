<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DemoOturumGetirModal.aspx.cs" Inherits="CevrimiciSinav_DemoOturumGetirModal" %>


<table id="oturumTable" class="table table-bordered table-responsive-sm">
    <thead>
        <tr>
            <th>Oturum Adı</th>
            <th>Süre</th>
            <th>İlk Giriş</th>
            <th>Son Giriş</th>
            <th>İşlem</th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="rptOturumlar" runat="server">
            <itemtemplate>
                <tr>
                    <td><%#Eval("OturumAdi") %></td>
                    <td><%#Eval("Sure") %> dakika</td>
                    <td><%#Eval("BaslamaTarihi").ToDateTime().TarihYaz() %></td>
                    <td><%#Eval("BitisTarihi").ToDateTime().TarihYaz() %></td>
                    <td>
<asp:HyperLink ID="ltrBasla" NavigateUrl='<%#String.Format("DemoSinav.aspx?t={0}",Eval("Id")) %>' runat="server" CssClass="btn btn-success btn-sm"><i class='fas fa-chevron-circle-right mr-1'></i> Test Et</asp:HyperLink>
                    </td>
                </tr>
                <tr><td colspan="5"><small><%#Eval("Aciklama") %></small><hr/></td></tr>
        </itemtemplate>
        </asp:Repeater>
    </tbody>
</table>

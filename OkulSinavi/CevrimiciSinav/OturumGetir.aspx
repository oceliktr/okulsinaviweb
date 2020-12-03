<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OturumGetir.aspx.cs" Inherits="CevrimiciSinav_OturumGetir" %>

<div class="col-12">
    <div class="card">
        <div class="card-body">
            <div class="row">
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
                        <asp:Repeater ID="rptOturumlar" runat="server" OnItemDataBound="rptOturumlar_OnItemDataBound">
                            <itemtemplate>
                <tr>
                    <td><%#Eval("OturumAdi") %></td>
                    <td><%#Eval("Sure") %> dakika</td>
                    <td><asp:Literal ID="ltrBaslangicTarihi" runat="server"></asp:Literal></td>
                    <td><%#Eval("BitisTarihi").ToDateTime().TarihYaz() %></td>
                    <td>
<asp:HyperLink ID="ltrBasla" NavigateUrl="#" runat="server" CssClass="btn btn-success btn-sm"></asp:HyperLink>
                    </td>
                </tr>
                <tr><td colspan="5"><small><%#Eval("Aciklama") %></small><hr/></td></tr>
        </itemtemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <div class="row">
                  <div class="col-3 rounded mx-auto d-block">
                    <a class="btn btn-success btn-block" href="Sinavlar.aspx"><i class="fa fa-home mr-1"></i>Sınavlar</a>
                </div> 
            </div>
        </div>
    </div>
</div>

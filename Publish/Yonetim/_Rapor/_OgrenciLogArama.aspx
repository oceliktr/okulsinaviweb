<%@ page language="C#" autoeventwireup="true" inherits="OkulSinavi_CevrimiciSinavYonetim__Rapor_OgrenciLogArama, okulsinavi" enableEventValidation="false" %>

    <form id="form1" runat="server">
        <ul class="products-list product-list-in-card pl-2 pr-2">
            <asp:Repeater ID="rptLog" runat="server">
                <ItemTemplate>
                    <li class="item">
                        <div class="product-info" style="margin-left: 0px;">
                            <span class="product-title">
                                İşlem: <%#Eval("Grup") %>
                                <span class="badge badge-warning float-right"><%#Eval("Tarih").ToDateTime().TarihYaz() %></span>
                            </span>
                            <span class="product-description">
                               Rapor: <%#Eval("Rapor") %>
                            </span>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </form>

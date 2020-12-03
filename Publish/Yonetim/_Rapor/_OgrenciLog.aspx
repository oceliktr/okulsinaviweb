<%@ page language="C#" autoeventwireup="true" inherits="OkulSinavi_CevrimiciSinavYonetim__Rapor_OgrenciLog, okulsinavi" enableEventValidation="false" %>

<form runat="server" id="from1">

    <ul class="products-list product-list-in-card pl-2 pr-2">
        <asp:Repeater ID="rptLog" runat="server">
            <itemtemplate>
        <li class="item">
                      <div class="product-info" style="margin-left: 0px;">
                      <span class="product-title"><%#Eval("Grup") %>
                        <span class="badge badge-warning float-right"><%#Eval("Tarih").ToDateTime().TarihYaz() %></span></span>
                      <span class="product-description">
                          <%#Eval("Rapor") %>
                      </span>
                    </div>
                  </li>
        </itemtemplate>
        </asp:Repeater>
    </ul>
</form>

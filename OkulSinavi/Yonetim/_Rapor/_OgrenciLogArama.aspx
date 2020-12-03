<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_OgrenciLogArama.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim__Rapor_OgrenciLogArama" %>

    <form id="form1" runat="server">
        <ul class="products-list product-list-in-card pl-2 pr-2">
            <asp:Repeater ID="rptLog" runat="server">
                <itemtemplate>
                    <li class="item">
                        <div class="product-info" style="margin-left: 0px;">
                            <span class="product-title">
                                <button type="button" class="btn btn-default btn-sm text-sm mr-1"  data-toggle="modal" data-target="#ogr-islem" data-adi="<%#Eval("Adi") %> <%#Eval("Soyadi") %>" data-opaqid="<%#Eval("OpaqId") %>"><i class="fa fa-user"></i></button>
                                <a href="<%#Eval("OpaqId") %>"><%#Eval("OpaqId") %> - <%#Eval("Adi") %> <%#Eval("Soyadi") %></a> <br/> <%#Eval("Grup") %>
                                <span class="badge badge-warning float-right"><%#Eval("Tarih").ToDateTime().TarihYaz() %></span>
                            </span>
                            <span class="product-description">
                                <%#Eval("Rapor") %>
                            </span>
                        </div>
                    </li>
                </itemtemplate>
            </asp:Repeater>
        </ul>
    </form>

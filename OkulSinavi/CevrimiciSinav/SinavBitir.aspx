<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SinavBitir.aspx.cs" Inherits="Sinav_SinavBitir" %>
<div class="col-12">
    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-12 center-block">
                    <img src="/CevrimiciSinav/Content/images/uyari.png" alt="Tamam" class="rounded mx-auto d-block" />

                </div>
            </div>
            <p class="mt-2 login-box-msg">
                <asp:Literal ID="ltrMesaj" runat="server"></asp:Literal>
            </p> 
            <p class="mt-2 login-box-msg">
                <asp:PlaceHolder ID="phBosSorular" runat="server" Visible="False">
                    <asp:Repeater ID="rptBosSorular" runat="server">
                        <itemtemplate>
                            <a href="#" onclick="BosSoruGetir(<%#Eval("SoruNoValue") %>)" class="badge badge-info mr-1 mb-1"><%#Eval("SoruNoText") %></a>
                        </itemtemplate>
                    </asp:Repeater>
                </asp:PlaceHolder>
            </p>
            <p class="mt-2 login-box-msg">
                Dilerseniz geri dönerek süreniz tamamlanana kadar cevaplarınızı kontrol edebilirsiniz. 
            </p>
            <div class="row">
                <div class="col-3 rounded mx-auto d-block">
                    <button type="button" id="geridon" onclick="GeriDon(<%=Session["SoruSayisi"].ToInt32()%>)" class="btn btn-warning btn-block"><i class="fa fa-arrow-left mr-1"></i>Geri Dön</button>
                </div> 
                <div class="col-3 rounded mx-auto d-block">
                    <button type="button" id="bitir" onclick="Degerlendir()" class="btn btn-danger btn-block"><i class="fa fa-power-off mr-1"></i>Sınavı Bitir</button>
                </div> 
                <div class="col-3 rounded mx-auto d-block">
                    <a class="btn btn-success btn-block" href="Sinavlar.aspx"><i class="fa fa-home mr-1"></i>Sınavlar</a>
                </div> 
            </div>

        </div>
    </div>
</div>
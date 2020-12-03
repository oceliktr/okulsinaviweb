<%@ Page Title="" Language="C#" MasterPageFile="~/CevrimiciSinav/MasterPage.master" AutoEventWireup="true" CodeFile="Sinavlarim.aspx.cs" Inherits="Sinav_Sinavlarim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="content-wrapper">
        <div class="content-header">
            <div class="container">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h4 class="m-0 text-dark">Katıldığım Sınavlar</h4>
                    </div>
                </div>
            </div>
        </div>

        <div class="content">
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <div class="card card-primary card-outline">
                            <div class="card-header">
                                <h5 class="card-title m-0">Katıldığım Sınavlar</h5>
                            </div>
                            <div class="card-body text-center">
                              
                                <div class="row">
                                    <div class="col-12">
                                        <div class="card">
                                            <div class="card-body p-0">
                                                <div class="table-responsive">
                                                    <table class="table m-0 table-striped table-valign-middle">
                                                        <thead>
                                                        <tr>
                                                            <th>Dönem</th>
                                                            <th>Sınav Adı</th>
                                                            <th>Doğru Cevap Sayısı</th>
                                                            <th>Yanlış Cevap Sayısı</th>
                                                            <th>Puanınız</th>
                                                        </tr>
                                                        </thead>
                                                        <tbody>
                                                        <asp:Repeater ID="rptSinavlar" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%#Eval("Donem") %></td>
                                                                    <td><a href="SinavSonuc.aspx?SinavId=<%#Eval("SinavId") %>"><%#Eval("SinavAdi") %></a></td>
                                                                    <td><%#Eval("Dogru") %></td>
                                                                    <td><%#Eval("Yanlis") %></td>
                                                                    <td><%#Eval("Puan").ToDecimal()>100?Eval("Puan").ToDecimal().ToString("##.###"):"-" %></td>
                                                                     </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <small>100 puan ve altı için hesaplama yapılmamaktadır.</small>
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>


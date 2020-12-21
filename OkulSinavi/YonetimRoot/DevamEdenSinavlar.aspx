<%@ Page Title="" Language="C#" MasterPageFile="~/YonetimRoot/MasterPage.master" AutoEventWireup="true" CodeFile="DevamEdenSinavlar.aspx.cs" Inherits="YonetimRoot_DevamEdenSinavlar" %>


<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Devam Eden Sınavlar</h1>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-md-12">
                    
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card card-warning">
                                        <table class="table table-bordered table-hover dataTable" role="grid">
                                            <thead>
                                                <tr role="row">
                                                    <th></th>
                                                    <th>Sınav Adı</th>
                                                    <th>Oturum Adı</th>
                                                    <th>Sınıf</th>
                                                    <th>Puanlama</th>
                                                    <th>Oturum Tercihi</th>
                                                    <th>Sınav Tarihi</th>
                                                    <th>Son Giriş</th>
                                                    <th>Süre</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptDevamEdenSinavlar" runat="server">
                                                    <ItemTemplate>
                                                        <tr role="row" class="odd">
                                                            <td><button type="button" data-toggle="modal" data-id="<%#Eval("Id") %>" data-target="#okullar" class="btn btn-xs btn-danger"><i class="fa fa-home"></i></button> </td>
                                                            <td><%#Eval("Id") %>  - <%#Eval("SinavAdi") %></td>
                                                            <td><%#Eval("OturumId") %> - <%#Eval("OturumAdi") %></td>
                                                            <td><%#Eval("Sinif") %></td>
                                                            <td><%#Eval("Puanlama") %></td>
                                                            <td><%#Eval("OturumTercihi").ToString()=="0"?"Oturum saatlerine göre":"Oturum sırasına göre" %></td>
                                                            <td><%#Eval("BaslamaTarihi") %></td>
                                                            <td><%#Eval("BitisTarihi").ToDateTime().SaatYaz() %></td>
                                                             <td><%#Eval("Sure") %></td>
                                                            </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="modal" tabindex="-1" role="dialog" id="okullar">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Sınava Katılan Kurumlar</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <p>Modal body text goes here.</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script>
        $('#okullar').on('shown.bs.modal', function (e) {
            var sinavId = $(e.relatedTarget).data("id");

            $(this).find('.modal-body').load("/YonetimRoot/_SinaviYapanOkullar.aspx?SinavId="+sinavId);
        })
    </script>
</asp:Content>


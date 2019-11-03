<%@ Page Title="" Language="C#" MasterPageFile="~/LGSSoruBank/MasterPage.master" AutoEventWireup="true" CodeFile="Sorularim.aspx.cs" Inherits="LGSSoruBank_Sorularim" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="content-wrapper">
        <section class="content-header">
            <h1>Sorularım</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
                <li class="active">Sorularım</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:placeholder id="phUyari" runat="server"></asp:placeholder>
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Sorularım</h3>
                            <a href="SoruEkle.aspx" class="btn bg-purple pull-right"><i class="fa fa-fw fa-plus"></i> Yeni Soru</a>
                        </div>
                        <div class="box-body">
                            
                                <div class="box box-warning">
                                    <table class="table table-bordered table-hover dataTable" id="sinavlar" role="grid">
                                        <thead>
                                            <tr role="row">
                                                <th>Sınıf</th>
                                                <th>Soru</th>
                                                <th>Kazanım</th>
                                                <th>Tarih</th>
                                                <th>Durum</th>
                                                <th>İşlem</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:repeater id="rptKayitlar" runat="server" onitemcommand="rptKayitlar_OnItemCommand" OnItemDataBound="rptKayitlar_OnItemDataBound">
                                                    <ItemTemplate>
                                                        <tr role="row" class="odd">
                                                            <td><%#Eval("Sinif") %></td>
                                                            <td><a title="İndirmek için tıklayınız" href="<%#Eval("SoruUrl") %>"><i class="fa fa-file-word-o"></i></a></td>
                                                            <td><%#Eval("Kazanim") %> </td>
                                                            <td><%#Eval("Tarih").ToDateTime().TarihYaz() %></td>
                                                            <td><%#Eval("Onay").ToInt32().LgsDurumBul() %></td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkDuzenle" runat="server" PostBackUrl='<%#string.Format("SoruEkle.aspx?Id={0}",Eval("Id")) %>'><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkSil" runat="server" CommandName="Sil" OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');" CommandArgument='<%#Eval("Id") %>'><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:repeater>
                                        </tbody>
                                    </table>
                                </div>
                           
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>


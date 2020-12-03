<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Sinavlar.aspx.cs" Inherits="Okul_SinaviYonetim_Sinavlar" %>

<%@ Register TagPrefix="uc1" TagName="UstMenu" Src="~/Yonetim/UstMenu.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-12">
                        <h1 class="m-0 text-dark">Çevrim İçi Sınav Modülü   <small class="float-right">Eğitim Öğretim Yılı: <%=TestSeciliDonem.SeciliDonem().Donem%></small></h1>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-lg-12">
                    <uc1:UstMenu runat="server" ID="UstMenu" />
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <ol class="breadcrumb float-sm-left">
                                        <li class="breadcrumb-item active"> <%=TestSeciliDonem.SeciliDonem().Donem%> Sınavlar</li>
                                    </ol>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <table class="table table-striped table-responsive-sm">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Sınav Adı</th>
                                            <th>Sınıf</th>
                                            <th>Puanlama</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptTestler" runat="server" OnItemDataBound="rptTestler_OnItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <th scope="row">
                                                        <asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></th>
                                                    <td><a href="SinavDetay.aspx?SinavId=<%#Eval("Id") %>"><%#Eval("SinavAdi") %></a></td>
                                                    <td><%#Eval("Sinif") %>. Sınıf</td>
                                                    <td><%#Eval("Puanlama") %> üzerinden</td>
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


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
</asp:Content>


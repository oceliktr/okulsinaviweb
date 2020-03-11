<%@ Page Title="" Language="C#" MasterPageFile="~/ODM/MasterPage.master" AutoEventWireup="true" CodeFile="SinavEvrak.aspx.cs" Inherits="ODM.OdmSinavEvrak" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <section class="content-header">
            <h1>S�nav Evraklar�</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giri�</a></li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-lg-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                    <div class="box box-solid box-default">
                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs">
                                    <li id="tabliSayfalar" runat="server" class="active"><a href="#ContentPlaceHolder1_Sayfalar" data-toggle="tab" aria-expanded="true"><i class="fa fa-fw fa-list-alt"></i>Evrak �ndir</a></li>
                                    <li id="tabliKayit" runat="server"><a href="#ContentPlaceHolder1_Kayit" data-toggle="tab" aria-expanded="false"><i class="fa fa-fw fa-edit"></i>Kay�t Formu</a></li>
                                 </ul>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="Sayfalar" runat="server">

                                        <table class="table table-bordered table-hover dataTable" role="grid">
                                            <thead>
                                                <tr role="row">
                                                    <th>S�ra</th>
                                                    <th>S�nav Evra��</th>
                                                    <th>A��klama</th>
                                                    <th>�ndirme Zaman�</th>
                                                    <th>��lem</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptSinavEvraklari" runat="server" OnItemDataBound="rptSinavEvraklari_OnItemDataBound" OnItemCommand="rptSinavEvraklari_OnItemCommand">
                                                    <ItemTemplate>
                                                        <tr role="row" class="odd">
                                                            <td>
                                                                <asp:Label ID="lblSira" runat="server" Text="Label"></asp:Label></td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkUrl" runat="server" CommandName="Indir" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton></td>
                                                            <td><%#Eval("Aciklama")%> <i class="label label-primary pull-right">#<%#Eval("Id") %></i></td>
                                                            <td><%#Eval("BaslangicTarihi").ToDateTime().TarihYaz() %> - <%#Eval("BitisTarihi").ToDateTime().TarihYaz() %></td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkDuzenle" runat="server" CommandName="Duzenle" CommandArgument='<%#Eval("Id") %>'><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkSil" runat="server" CommandName="Sil" OnClientClick="return confirm('Silmek istedi�inizden emin misiniz?');" CommandArgument='<%#Eval("Id") %>'><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>

                                    </div>
                                    <div class="tab-pane" id="Kayit" runat="server">
                                        <div class="box box-solid box-default">
                                            <div class="box-body">
                                                <div class="col-lg-12 form-horizontal">

                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">
                                                            A��klama
                                                            <asp:RequiredFieldValidator ControlToValidate="txtAciklama" ValidationGroup="form" ID="RequiredFieldValidator5" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtAciklama" MaxLength="50" CssClass="form-control" runat="server" ValidationGroup="form"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Dosya</label>
                                                        <div class="col-sm-4">
                                                            <asp:FileUpload ID="fuFoto" CssClass="form-control" runat="server" placeholder="Foto�raf se�iniz"></asp:FileUpload>
                                                        </div>

                                                        <label class="col-sm-2 control-label">
                                                            Veya Url Adresi</label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtUrl" CssClass="form-control" MaxLength="100" ValidationGroup="form" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">
                                                            Eri�im Ba�l. Tarihi
                                                            <asp:RequiredFieldValidator ControlToValidate="txtBaslangicTarihi" ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtBaslangicTarihi" CssClass="form-control" ValidationGroup="form" runat="server" placeholder="11.11.2017 gibi"></asp:TextBox>
                                                        </div>

                                                        <label class="col-sm-2 control-label">
                                                            Eri�im Biti� Tarihi
                                                            <asp:RequiredFieldValidator ControlToValidate="txtBitisTarihi" ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtBitisTarihi" CssClass="form-control" ValidationGroup="form" runat="server" placeholder="11.11.2017 gibi"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <div class="form-group">
                                                            <label class="col-sm-1 control-label">Okul T�r�</label>
                                                            <div class="col-sm-4">
                                                                <asp:DropDownList ID="ddlOkulTuru" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlOkulTuru_OnSelectedIndexChanged" runat="server"></asp:DropDownList>
                                                            </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <label class="col-sm-1 control-label">Okullar</label>
                                                                <div class="col-sm-5">
                                                                    <asp:ListBox ID="lbKurumlar" Rows="15" CssClass="form-control" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <asp:Button ID="blnSecileniEkle" runat="server" Text=">>" OnClick="blnSecileniEkle_OnClick" /><br />
                                                                    <br />
                                                                    <asp:Button ID="blnSecileniCikar" runat="server" Text="<<" OnClick="blnSecileniCikar_OnClick" />
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <asp:ListBox ID="lbSecilenler" Rows="15" CssClass="form-control" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="form-group">
                                                        <label class="col-sm-10 control-label">
                                                            <asp:Literal ID="Literal1" runat="server"></asp:Literal></label>
                                                        <div class="col-sm-2">
                                                            <asp:HiddenField ID="hfId" runat="server" Value="0" />
                                                            <asp:Button ID="btnVazgec" ValidationGroup="vazgec" CssClass="btn btn-default" runat="server" Text="Vazge�" OnClick="btnVazgec_OnClick" />
                                                            <asp:Button ID="btnDosyaEkle" ValidationGroup="form" CssClass="btn btn-primary pull-right" runat="server" Text="Ekle" OnClick="btnDosyaEkle_OnClick" />
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
                <div class="col-md-7"><div class="box box-danger">
                    <div class="box-body">
                        <ul class="list-unstyled">
                            <li>
                               Bu ekranda sadece okulunuza �zel g�nderilen s�nav evraklar� listelenir. Gelmesi gereken evra�� listede g�remiyorsan�z l�tfen bize <a href="Giris.aspx">buradaki</a> numaralardan ula��n�z.
                            </li>
                            <li>Dosyaya eri�im zaman� gelinceye kadar indirme butonu kapal� kalacakt�r. S�nav evra��n� indirebilmek i�in indirme zaman�n� beklemeniz gerekmektedir.</li>
                            <li></li>
                            </ul>
                    </div>
                </div></div>
            </div>
        </section>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SinavYonetim.aspx.cs" Inherits="Okul_SinaviYonetim_SinavYonetim" %>

<%@ Register TagPrefix="uc1" TagName="UstMenu" Src="~/Yonetim/UstMenu.ascx" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.css" />
    <style>
        input[type=checkbox], input[type=radio] {
            margin-right: 10px;
            margin-top: 10px;
        }
    </style>
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
                            <div class="col-md-6">
                                <ol class="breadcrumb float-sm-left">
                                    <li class="breadcrumb-item"><a href="#"><%=TestSeciliDonem.SeciliDonem().Donem%> Sınav Yönetimi</a></li>
                                </ol>
                            </div>
                            <div class="text-right mb-2">
                                <a href="SinavKayit.aspx" class="btn btn-primary"><i class="fa fa-plus-circle mr-2"></i>Yeni Sınav</a>
                            </div>

                            <div class="col-lg-12" id="sinavlar">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            
            
            <div class="row">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Dönemler</h3>
                        </div>
                        <div class="card-body">
                            <table class="table table-bordered table-hover dataTable" role="grid">
                                <thead>
                                    <tr role="row">
                                        <th>Dönemler</th>
                                        <th>Veri Girişi</th>
                                        <th>İşlem</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptKayitlar" runat="server" OnItemCommand="rptKayitlar_ItemCommand">
                                        <ItemTemplate>
                                            <tr role="row" class="odd">
                                                <td><%#Eval("Aktif").ToString()=="1"?"<i class='far fa-check-square'></i>":"<i class='far fa-square'></i>" %> <%#Eval("Donem") %></td>
                                                <td><%#Eval("VeriGirisi").ToString()=="1"?"<i class='far fa-check-square'></i>":"<i class='far fa-square'></i>" %></td>
                                                <td>
                                                    <asp:LinkButton ID="lnkDuzenle" runat="server" CommandName="Duzenle" CommandArgument='<%#Eval("Id") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkSil" runat="server" CommandName="Sil" OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');" CommandArgument='<%#Eval("Id") %>'><i class="fa fa-trash"></i></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>

                        </div>
                    </div>
                </div>
                <div class="col-md-6">

                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>


                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">
                                <asp:Literal ID="ltrKayitBilgi" Text="Yeni dönem kayıt formu" runat="server"></asp:Literal></h3>
                        </div>
                        <div class="card-body">
                            <asp:HiddenField ID="hfId" Value="0" runat="server" />
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Dönem
                                                                    <asp:RequiredFieldValidator ControlToValidate="txtDonem" ValidationGroup="form" ID="RequiredFieldValidator1" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtDonem" CssClass="form-control" runat="server" placeholder="Dönem tanımını giriniz" ValidationGroup="form"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <asp:Button ID="btnVazgec" CssClass="btn btn-default" runat="server" Text="Vazgeç" OnClick="btnVazgec_Click" />
                            <asp:Button ID="btnKaydet" CssClass="btn btn-primary pull-right" runat="server" ValidationGroup="form" Text="Kaydet" OnClick="btnKaydet_Click" />
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Aktif Dönem</h3>
                        </div>
                        <div class="card-body">
                            <div class="form-horizontal">
                                <asp:HiddenField ID="HiddenField1" Value="0" runat="server" />
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Aktif dönem</label>
                                    <div class="col-sm-8">
                                        <asp:HiddenField ID="hfAktifdonem" runat="server" />
                                        <asp:DropDownList runat="server" ID="ddlAktifDonem" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <label class="col-sm-4 control-label">
                                        <asp:CheckBox ID="cbVeriGirisi" runat="server" Text=" Okullara veri girişi açık" /> </label>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <asp:Button ID="btnAktifDonem" CssClass="btn btn-primary pull-right" runat="server" Text="Kaydet" OnClick="btnAktifDonem_OnClick" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="detay">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"></h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                   
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Kapat</button>
                 </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.js"></script>
    <script>
        $(function () {
            $("#sinavlar").load("/Yonetim/_Sinavlar.aspx");
        });
        $('#detay').on('show.bs.modal',
            function (event) {
                var aId = $(event.relatedTarget).data('id');

                $("#detay .modal-body").load("/Yonetim/_SinavDetay.aspx?Id="+aId);

            });
        function Sil(id) {
            Swal.fire({
                title: 'Silmek istiyor musunuz?',
                text: "Seçili sınavı silmek istediğinizden emin misiniz? Tüm sınav ve ilişkili kayıtlar silinecektir.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet Sil',
                cancelButtonText: 'Vazgeç'
            }).then((result) => {
                if (result.value) {
                    var AjaxJson = { SinavId: id };
                    $.ajax({
                        type: "POST",
                        url: "/Yonetim/SinavYonetim.aspx/SinavSil",
                        dataType: "json",
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(AjaxJson),
                        success: function (response) {
                            var data = $.parseJSON(response.d);
                            if (data.Sonuc) {
                                Uyari("success", "Silindi!", data.Mesaj);
                                $("#sinavlar").load("/Yonetim/_Sinavlar.aspx");
                            } else {

                                Uyari("warning", "Silinemedi!", data.Mesaj);
                            }
                        },
                        eror: function (data) {
                            Uyari("error", "Hata oluştu!", data.Mesaj);
                        }
                    });
                }
            });
        }
        function Uyari(ico, baslik, txt) {
            Swal.fire({
                icon: ico, //warning, error, success, info, and question.
                title: baslik,
                text: txt,
                showConfirmButton: true,
                confirmButtonText: "Tamam"
            });
        }
    </script>
</asp:Content>


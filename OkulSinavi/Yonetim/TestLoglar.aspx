﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TestLoglar.aspx.cs" Inherits="OkulSinavi_CevrimiciSinavYonetim_TestLoglar" %>

<%@ Register TagPrefix="uc1" TagName="UstMenu" Src="~/Yonetim/UstMenu.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.css" />
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
                                        <li class="breadcrumb-item"><a href="SinavRapor.aspx">Raporlar</a></li>
                                        <li class="breadcrumb-item active">Öğrenci Log</li>
                                    </ol>
                                </div>
                            </div>

                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header">
                                        <h3 class="card-title">Öğrenci Arama</h3>
                                        <div class="float-right col-md-3">
                                            <div class="row float-right">
                                                <div class="form-group">
                                                    <input type="text" id="ara" name="ara" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body" id="ogrenciler">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div class="modal fade" id="ogr-islem">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"></h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
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
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
    <script src="/CevrimiciSinav/Content/sweetalert2/sweetalert2.min.js"></script>

    <script>
        $("#ara").keyup(function () {
            var araStr = $("#ara").val();
            if (araStr.length >= 4) {
                $("#ogrenciler").load("/Yonetim/_Rapor/_OgrenciLogArama.aspx?aranan=" + araStr);
            } else {
                $("#ogrenciler").html("<center><small>En az dört karakter giriniz.</small></center>");
            }
        });
        $("#ogr-islem").on("show.bs.modal", function (event) {
            var opaqid = $(event.relatedTarget).data("opaqid");
            var adi = $(event.relatedTarget).data("adi");
            $("#ogr-islem .modal-title").html(adi);
            $("#ogr-islem .modal-body").load("/Yonetim/_Rapor/_OgrenciIslem.aspx?OpaqId=" + opaqid);
        });
    </script>
</asp:Content>

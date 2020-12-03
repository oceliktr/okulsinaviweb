<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Okul_SinaviYonetim_Default2" %>


<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Src="~/Yonetim/UstMenu.ascx" TagPrefix="uc1" TagName="UstMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-10">
                        <h1 class="m-0 text-dark">Çevrim İçi Sınav Modülü</h1>
                    </div>

                </div>
            </div>
        </div>
        <div class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12">
                        <uc1:UstMenu runat="server" ID="UstMenu" />
                        <div class="card card-default">

                            <div class="box-body">
                                <div class="col-lg-12">
                                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>

                                    <div class="callout callout-info mt-2">
                                        <h2 class="text-center"><span class="badge badge-warning">
                                            <label>Aktif Dönem:</label>
                                            <asp:Literal ID="ltrAktifDonem" runat="server"></asp:Literal>

                                        </span></h2>

                                        <p class="row">
                                            <label class="col-md-1">Dönem seçiniz: </label>
                                            <asp:DropDownList ID="ddlDonemler" CssClass="form-control col-md-2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDonemler_OnSelectedIndexChanged"></asp:DropDownList></p>
                                        <p class="d-flex p-2">Geçmiş yıllara ait verileri görmek için dönem seçiniz. Seçtiğiniz döneme ait verileri göreceksiniz.</p>
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


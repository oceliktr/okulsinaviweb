<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="SinavAktar.aspx.cs" Inherits="Yonetim_SinavAktarRoot" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<%@ Register Src="~/Yonetim/UstMenu.ascx" TagPrefix="uc1" TagName="UstMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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

                                       <h2><span>
                                            <label>Sınav:</label>
                                            <asp:Literal ID="ltrSinav" runat="server"></asp:Literal>
                                        </span></h2>

                                        <asp:UpdatePanel runat="server" ID="up2">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label">
                                                        İlçe
                                                                    <asp:RequiredFieldValidator ControlToValidate="ddlIlce" ValidationGroup="form" ID="RequiredFieldValidator3" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlIlce" ValidationGroup="form" AutoPostBack="True" OnSelectedIndexChanged="ddlIlce_OnSelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label">
                                                        Kurum
                                                                    <asp:RequiredFieldValidator ControlToValidate="ddlKurum" ValidationGroup="form" ID="RequiredFieldValidator4" ForeColor="Red" Text="*" SetFocusOnError="true" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlKurum" ValidationGroup="form"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                            </div>
                            <div class="card-footer">
                                <button OnServerClick="btnKaydet_OnClick" runat="server" id="btnRun" class="btn btn-primary pull-right" title="Kurum İçin Sınavı Kopyala">
                                    <i class="far fa-object-ungroup"></i>  Kurum İçin Sınavı Kopyala
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

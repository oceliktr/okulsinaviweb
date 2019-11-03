<%@ Page Title="" Language="C#" MasterPageFile="~/SoruBank/MasterPage.master" AutoEventWireup="true" CodeFile="Bilgilerim.aspx.cs" Inherits="SoruBankBilgilerim" %>


<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Bilgilerim</h1>
            <ol class="breadcrumb">
                <li><a href="Giris.aspx"><i class="fa fa-home"></i>Giriş</a></li>
                <li class="active">Bilgilerim</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                   
                                <div class="col-md-4">
                                    <div class="box box-comments">
                                        <div class="form-horizontal">
                                            <div class="box-body">
                                                <div class="nav-tabs-custom">

                                                    <div class="tab-content">
                                                        <div class="tab-pane active" id="tab_1">
                                                            <div class="box-header">
                                                                <h3 class="box-title">Bilgilerim</h3>
                                                            </div>
                                                            <asp:HiddenField ID="hfId" Value="0" runat="server" />
                                                            
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">İlçe</label>
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlIlce" Enabled="False" OnSelectedIndexChanged="ddlIlce_OnSelectedIndexChanged"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">Kurum</label>
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlKurum" Enabled="False"></asp:DropDownList>
                                                                </div>
                                                            </div><div class="form-group">
                                                                <label class="col-sm-4 control-label">Tc Kimlik</label>
                                                                <div class="col-sm-8">***********</div>
                                                            </div>
                                                           <div class="form-group">
                                                                <label class="col-sm-4 control-label">Adı Soyadı</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtAdiSoyadi" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">Branşı</label>
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBrans" Enabled="False"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">
                                                                    E-mail Adresi
                                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEpostaAdresi" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red"
                                                                       SetFocusOnError="True" ValidationGroup="form">*</asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEpostaAdresi" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                                                        ForeColor="Red" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="form">*</asp:RegularExpressionValidator></label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtEpostaAdresi" CssClass="form-control" runat="server" placeholder="E-posta Adresi" ValidationGroup="form"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                           <div class="form-group">
                                                                <label class="col-sm-4 control-label">Cep Telefonu<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEpostaAdresi" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red"
                                                                       SetFocusOnError="True" ValidationGroup="form">*</asp:RequiredFieldValidator></label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtGsm" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">Şifre</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtSifre" CssClass="form-control" TextMode="Password" runat="server" placeholder="Şifre"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="box-footer">
                                                                <asp:Button ID="btnKaydet" CssClass="btn btn-primary pull-right" runat="server" ValidationGroup="form" Text="Kaydet" OnClick="btnKaydet_Click" />
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
        </section>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Bilgilerim.aspx.cs" Inherits="OkulSinaviBilgilerim" %>


<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Bilgilerim</h1>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">

            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">

                        <div class="col-md-6 offset-3">
                        <asp:PlaceHolder ID="phUyari" runat="server"></asp:PlaceHolder>
                            <div class="card card-primary card-outline">
                                <div class="card-header">
                                    <h3 class="card-title">Bilgilerim</h3>
                                </div>
                                <div class="card-body">

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
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Giriş Bilgisi</label>
                                        <div class="col-sm-8">
                                            <asp:Literal ID="ltrGiris" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Adı Soyadı</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtAdiSoyadi" CssClass="form-control" runat="server"></asp:TextBox>
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
                                        <label class="col-sm-4 control-label">
                                            Cep Telefonu<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEpostaAdresi" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red"
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

                                </div>

                            <div class="card-footer">
                                <asp:Button ID="btnKaydet" CssClass="btn btn-primary pull-right" runat="server" ValidationGroup="form" Text="Kaydet" OnClick="btnKaydet_Click" />
                            </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


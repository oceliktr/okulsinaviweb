<%@ page language="C#" autoeventwireup="true" inherits="Sinav_SinavGetir, okulsinavi" enableEventValidation="false" %>

<form id="form1" runat="server">
    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-56545817-27"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-56545817-27');

    </script>
    <div class="card card-primary card-outline">
        <input type="hidden" value="<%=Session["SoruNo"]%>" id="hfSoruNo" name="hfSoruNo" />
        <div class="card-header">
            <div class="col-12 makam">
                <div class="row">
                    <div class="col-md-1 col-sm-3 col-xs-2 col-lg-1 float-left">
                        <a href="#" class="btn btn-primary btn-sm float-left <%=Session["SoruNo"].ToInt32()==1?"invisible":""%>" onclick="SoruGetir(<%=(Session["SoruNo"].ToInt32()-1)%>)"><i class="fa fa-arrow-left mr-1"></i></a>
                    </div>
                    <div class="col-md-10 col-sm-6 col-xs-8 col-lg-10 text-center vertical">
                        <h5 class="card-text m-0 text-center">
                            <asp:Literal ID="ltrSoruNo" runat="server"></asp:Literal></h5>
                    </div>

                    <div class="col-md-1 col-sm-3 col-xs-2 col-lg-1 float-right">
                        <a href="#" class="btn btn-primary btn-sm float-right" onclick="SoruGetir(<%=(Session["SoruNo"].ToInt32()+1)%>)"><i class="fa fa-arrow-right mr-1"></i></a>
                    </div>
                </div>
            </div>
            <div class="col-12 makam2">
                <div class="row">
                    <div class="float-left w-25">
                        <a href="#" class="btn btn-primary btn-sm float-left <%=Session["SoruNo"].ToInt32()==1?"invisible":""%>" onclick="SoruGetir(<%=(Session["SoruNo"].ToInt32()-1)%>)"><i class="fa fa-arrow-left mr-1"></i></a>
                    </div>
                    <div class="text-center vertical w-50">
                        <h5 class="card-text m-0 text-center">
                            <asp:Literal ID="ltrSoruNo2" runat="server"></asp:Literal></h5>
                    </div>

                    <div class="w-25 float-right">
                        <a href="#" class="btn btn-primary btn-sm float-right" onclick="SoruGetir(<%=(Session["SoruNo"].ToInt32()+1)%>)"><i class="fa fa-arrow-right mr-1"></i></a>
                    </div>
                </div>
            </div>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-12 float-right">
                    <div class="float-left">
                        <asp:DropDownList runat="server" ID="ddlBranslar" CssClass="form-control dersler mb-2" onchange="DersDegis(this);"></asp:DropDownList>
                    </div>
                    <div class="float-right">
                        <asp:Repeater ID="rptSoruSayisi" runat="server">
                            <itemtemplate>
                            <a href="#" id="soru_<%#Eval("SoruNoValue") %>" onclick="SoruGetir(<%#Eval("SoruNoValue") %>)" class="badge <%#Eval("Bos").ToBoolean()?"bg-gray disabled":"badge-success" %> float-left mr-1 mb-1" <%#Eval("BuSoru").ToBoolean()?"style='border-bottom: 3px solid #ff0000;'":"" %>><%#Eval("SoruNoText") %></a>
                        </itemtemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="row">
                <p class="card-text rounded mx-auto d-block text-center mb-2">
                
                        <asp:Image ID="imgSoru" CssClass="img-fluid pad" Width="70%" runat="server"></asp:Image>
                    <asp:Literal ID="ltrSoru" runat="server"></asp:Literal>
                </p>
            </div>
            <div class="row">
                <div class="col-11">
                    <div class="form-group mb-4" id="cevap">
                        <div class="col-md-3 col-sm-6 col-xs-2 col-lg-3 float-left">
                            <label id="lblA" onclick="Isaretle('A')" class="custom-control custom-radio big <%=Session["Cevap"].ToString()=="A"?"bg-indigo":"bg-gray disabled" %>">
                                <input class="custom-control-input" type="radio" id="csA" name="customRadio" value="A" <%=Session["Cevap"].ToString()=="A"?"checked":"" %> />
                                <label for="csA" class="custom-control-label">A</label>
                            </label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-xs-2 col-lg-3 float-left">
                            <label id="lblB" onclick="Isaretle('B')" class="custom-control custom-radio big <%=Session["Cevap"].ToString()=="B"?"bg-indigo":"bg-gray disabled" %>">
                                <input class="custom-control-input" type="radio" id="csB" name="customRadio" value="B" <%=Session["Cevap"].ToString()=="B"?"checked":"" %> />
                                <label for="csB" class="custom-control-label">B</label>
                            </label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-xs-2 col-lg-3 float-left">
                            <label id="lblC" onclick="Isaretle('C')" class="custom-control custom-radio big <%=Session["Cevap"].ToString()=="C"?"bg-indigo":"bg-gray disabled" %>">
                                <input class="custom-control-input" type="radio" id="csC" name="customRadio" value="C" <%=Session["Cevap"].ToString()=="C"?"checked":"" %> />
                                <label for="csC" class="custom-control-label">C</label>
                            </label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-xs-2 col-lg-3 float-left">
                            <label id="lblD" onclick="Isaretle('D')" class="custom-control custom-radio big <%=Session["Cevap"].ToString()=="D"?"bg-indigo":"bg-gray disabled" %>">
                                <input class="custom-control-input" type="radio" id="csD" name="customRadio" value="D" <%=Session["Cevap"].ToString()=="D"?"checked":"" %> />
                                <label for="csD" class="custom-control-label">D</label>
                            </label>
                        </div>
                    </div>
                </div>
                <div class="col-1">
                    <button type="button" onclick="Temizle(<%=(Session["SoruNo"].ToInt32())%>)" data-tooltip="tooltip" title="Cevabımı Sil" class="btn btn-danger btn-sm float-right"><i class="fa fa-trash-alt"></i></button>

                </div>
            </div>
        </div>
        <div class="card-footer clearfix">
            <button type="button" class="btn btn-primary float-left <%=Session["SoruNo"].ToInt32()==1?"invisible":""%>" onclick="SoruGetir(<%=(Session["SoruNo"].ToInt32()-1)%>)"><i class="fa fa-arrow-left mr-1"></i>Önceki</button>
            
            <asp:PlaceHolder ID="phSonraki" runat="server" Visible="False">
                <button type="button" class="btn btn-primary float-right" onclick="SoruGetir(<%=(Session["SoruNo"].ToInt32()+1)%>)"><i class="fa fa-arrow-right mr-1"></i>Sonraki</button>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phBitti" runat="server" Visible="False">
                <button type="button" onclick="HizliBitir()" data-tooltip="tooltip" title="Sınavı Bitir" class="btn btn-danger btn-sm float-right"><i class="fa fa-power-off mr-1"></i> Sınavı Bitir</button>
           </asp:PlaceHolder>
        </div>
    </div>
    <script>
        //image full screen
        $('#imgSoru').addClass('img-enlargable').click(function () {
            var src = $(this).attr('src');
            var modal = $('<div>').css({
                background: 'RGBA(0,0,0,.5) url(' + src + ') no-repeat center',
                backgroundSize: 'contain',
                width: '100%', height: '100%',
                position: 'fixed',
                zIndex: '10000',
                top: '0', left: '0',
                cursor: 'zoom-out'
            }).click(function () {
                removeModal();
            }).appendTo('body');
            //handling ESC
            $('body').on('keyup.modal-close', function (e) {
                if (e.key === 'Escape') { removeModal(); }
            });
            function removeModal() {
                modal.remove();
                $('body').off('keyup.modal-close');
            }
        });
        $(document).bind("contextmenu", function (e) { return false; });
    </script>
</form>

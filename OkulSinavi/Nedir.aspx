<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Nedir.aspx.cs" Inherits="Nedir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="page_breadcrumbs template_breadcrumbs ds parallax">
                <div class="container-fluid">
                    <div class="row">
                        <div class="breadcrumbs_wrap col-lg-5 col-md-7 col-sm-8 text-right to_animate"
                            data-animation="fadeInLeftLong">
                            <div class="to_animate" data-animation="fadeInLeft" data-delay="500">
                                <h2>Okul Sınavı Nedir?</h2>
                            </div>

                            <ol class="breadcrumb greylinks to_animate" data-animation="fadeInLeft" data-delay="400">
                                <li>
                                    <a href="/">
                                        Anasayfa
                                    </a>
                                </li>
                                <li>
                                    <a href="#">Kurumsal</a>
                                </li>
                                <li class="active">Okul Sınavı Nedir?</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </section>

            <section class="ls section_padding_top_100">
                <div class="container">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="framed-heading">
                                <h2 class="section_header">
                                    Okul Sınavı
                                    <span class="highlight2">Nedir?</span>
                                                                </h2>
                            </div>
                            <p class="bottommargin_40">
                                Okul Sınavı hazırlamış olduğunuz sınavları çevrimiçi ortamda öğrencilerinize uygulayan pratik bir sınav sistemidir. 
                            </p>
                            <h3 class="entry-title">3 Aşamada Okul Sınavı Nedir?</h3>
                            <div class="panel-group" id="accordion1">
								<div class="panel panel-default">
									<div class="panel-heading">
										<h4 class="panel-title">
											<a data-toggle="collapse" data-parent="#accordion1" href="#collapse1" class="collapsed" aria-expanded="false">
                                                ÖĞRENCİLERİNİZİ KAYDEDİN
											</a>
										</h4>
									</div>
									<div id="collapse1" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">
										<div class="panel-body">
                                            Hızlı araçlarımızla öğrencilerinizi kolay bir şekilde sisteme tanımlayabilirsiniz.</div>
									</div>
								</div>
								<div class="panel panel-default">
									<div class="panel-heading">
										<h4 class="panel-title">
											<a data-toggle="collapse" data-parent="#accordion1" href="#collapse2" class="collapsed" aria-expanded="false">
												ÖĞRETMENLERİNİZİ TANIMLAYIN
											</a>
										</h4>
									</div>
									<div id="collapse2" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">
										<div class="panel-body">
											Öğretmenleri kaydederek ders düzeyinde sınav yapmalarını sağlayabilirsiniz.
										</div>
									</div>
								</div>
								<div class="panel panel-default">
									<div class="panel-heading">
										<h4 class="panel-title">
											<a data-toggle="collapse" data-parent="#accordion1" href="#collapse3" class="" aria-expanded="true">
												SINAVINIZI OLUŞTURUN
											</a>
										</h4>
									</div>
									<div id="collapse3" class="panel-collapse collapse in" aria-expanded="true" style="">
										<div class="panel-body">
											Öğretmenleriniz veya okul idaresi hazırlamış olduğu sorularla sınavlar oluşturabilirsiniz. Dilerseniz bir quiz, dilerseniz bir deneme sınavı hazırlayarak öğrenci başarısını ölçebilirsiniz.
										</div>
									</div>
								</div>

							</div>
                        </div>
                        <div class="col-md-6 text-center bottommargin_0">
                            <img class="top-overlap-small" src="Content/images/serdizayn.png" alt="Serdizayn" />
                        </div>
                    </div>
                </div>
            </section>

            
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foter" Runat="Server">
</asp:Content>


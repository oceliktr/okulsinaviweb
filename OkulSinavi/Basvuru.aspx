<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Basvuru.aspx.cs" Inherits="Basvuru" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<section class="page_breadcrumbs template_breadcrumbs ds parallax">
		<div class="container-fluid">
			<div class="row">
				<div class="breadcrumbs_wrap col-lg-5 col-md-7 col-sm-8 text-right to_animate" data-animation="fadeInLeftLong">
					<div class="to_animate" data-animation="fadeInLeft" data-delay="500">
						<h2>BAŞVURU</h2>
					</div>

					<ol class="breadcrumb greylinks to_animate" data-animation="fadeInLeft" data-delay="400">
						<li>
							<a href="./">Anasayfa
							</a>
						</li>
						<li class="active">BAŞVURU</li>
					</ol>
				</div>
			</div>
		</div>
	</section>

	<section class="ls section_padding_top_75 section_padding_bottom_100">
				<div class="container">
					<div class="row">
						<div class="col-sm-12 text-center">
							<div class="framed-heading">
								<h2 class="section_header">
									ONLİNE SINAV İÇİN
								</h2>
								<p>Size uygun çözümlerimizle hemen şimdi başvurun.</p>
							</div>
							<div class="prices-row topmargin_50">
								<div class="price-table-wrap">
									<ul class="price-table style2">
										<li class="plan-name">
											<h4>BAŞLANGIÇ</h4>
											<p>1 AY</p>
										</li>
										<li class="plan-price">0 TL</li>
										<li class="features-list">
											<ul>
												<li class="enabled">70 Öğrenci kayıt</li>
												<li class="enabled">3 sınav</li>
											</ul>
										</li>
										<li class="call-to-action">
											<a href="Basvuru_Kayit.aspx?p=1" class="theme_button inverse">BAŞVUR</a>
										</li>
									</ul>
								</div>

								<div class="price-table-wrap">
									<ul class="price-table style2">
										<li class="plan-name">
											<h4>STANDART</h4>
											<p>YILLIK ÖDEME</p>
										</li>
										<li class="plan-price">30 TL</li>
										<li class="features-list">
											<ul>
												<li class="enabled">300 Öğrenci kayıt</li>
												<li class="enabled">Aylık 4 sınav</li>
											</ul>
										</li>
										<li class="call-to-action">
											<a href="Basvuru_Kayit.aspx?p=2" class="theme_button inverse">BAŞVUR</a>
										</li>
									</ul>
								</div>


								<div class="price-table-wrap highlighted">
									<ul class="price-table style2">
										<li class="plan-name">
											<h3>Premium</h3>
											<p>YILLIK ÖDEME</p>
										</li>
										<li class="plan-price">80 TL</li>
										<li class="features-list">
											<ul>
												<li class="enabled">1000 Öğrenci kayıt</li>
												<li class="enabled">Aylık 20 sınav</li>
											</ul>
										</li>
										<li class="call-to-action">
											<a href="Basvuru_Kayit.aspx?p=3" class="theme_button inverse">BAŞVUR</a>
										</li>
									</ul>
								</div>

								<div class="price-table-wrap">
									<ul class="price-table style2">
										<li class="plan-name">
											<h3>ÖZEL</h3>
											<p>KURUMSAL</p>
										</li>
										<li class="plan-price">&nbsp;</li>
										<li class="features-list">
											<ul>
												<li class="enabled">Özel teklifler için bizi arayın</li>
											</ul>
										</li>
										<li class="call-to-action">
											<a href="tel:+905528002501" class="theme_button inverse">ŞİMDİ ARA</a>
										</li>
									</ul>
								</div>
							</div>
						</div>
					</div>
				</div>
			</section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foter" Runat="Server">
</asp:Content>


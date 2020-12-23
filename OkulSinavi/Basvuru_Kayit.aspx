<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Basvuru_Kayit.aspx.cs" Inherits="Basvuru_Kayit" %>

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
	
	<section class="ls section_padding_top_100 section_padding_bottom_75 columns_margin_bottom_30">
				<div class="container">
					<div class="row">
						<div class="col-sm-12 text-center">
							<div class="framed-heading">
								<h2 class="section_header">
									BAŞVURU KAYIT FORMU
								</h2>
							</div>
							<p class="columns_margin_bottom_30">
								Aşağıdaki formu doldurarak başvurunuzu tamamlayınız. <asp:Literal ID="ltrBasvuruAciklama" runat="server"></asp:Literal>
							</p>

							<form class="quote-form row topmargin_40" method="post" action="./">

								<div class="col-sm-6">

									<h3 class="entry-title">BAŞVURU BİLGİLERİNİZ</h3>

									<div class="bottommargin_10">
										<label for="quote-first-name" class="sr-only">Adınız Soyadınız
											<span class="required">*</span>
										</label>
										<div class="input-group">
											<i class="flaticon-avatar highlight"></i>
											<input type="text" aria-required="true" size="30" value="" name="quote-first-name" id="quote-first-name" class="form-control" placeholder="Adınız Soyadınız">
										</div>
									</div>

									<div class="bottommargin_10">
										<label for="quote-email" class="sr-only">E-mail Adresiniz
											<span class="required">*</span>
										</label>
										<div class="input-group">
											<i class="flaticon-envelope highlight"></i>
											<input type="text" aria-required="true" size="30" value="" name="quote-email" id="quote-email" class="form-control" placeholder="E-mail Adresiniz">
										</div>
									</div>

									<div class="bottommargin_10">
										<label for="quote-home-phone" class="sr-only">Telefon Numaranız</label>
										<div class="input-group">
											<i class="flaticon-phone-call highlight"></i>
											<input type="text" size="30" value="" name="quote-home-phone" id="quote-home-phone" class="form-control" placeholder="Telefon Numaranız">
										</div>
									</div>

									<div class="bottommargin_10">
										<label for="iliniz" class="sr-only">İliniz</label>
										<div class="input-group">
											<i class="flaticon-next highlight"></i>
											<select class="form-control" id="iliniz"><option value="">İlinizi Seçiniz</option></select>
										</div>
									</div>

									<div class="bottommargin_10">
										<label for="iliniz" class="sr-only">İliniz</label>
										<div class="input-group">
											<i class="flaticon-next highlight"></i>
											<select class="form-control" id="ilceniz"><option value="">İlçenizi Seçiniz</option></select>
										</div>
									</div>

									<div class="bottommargin_10">
										<label for="quote-fax" class="sr-only">Okulunuz</label>
										<div class="input-group">
											<i class="flaticon-house highlight"></i>
											<input type="text" size="30" value="" name="quote-fax" id="quote-fax" class="form-control" placeholder="Okulunuz">
										</div>
									</div>

									<div class="bottommargin_10">
										<label class="sr-only">Ödeme Tercihiniz</label>
										<p>
											<button type="button" class="theme_button mr-5"><i class="fa fa-check-circle"></i> Havale/EFT ile ödeme</button>
											<button type="button" class="theme_button color2 mr-5"><i class="fa fa-circle"></i> Kredi Kartı ile ödeme</button>
										</p>
									</div>

								</div>

								<div class="col-sm-6">

									<h3 class="entry-title">More info</h3>

									<div class="bottommargin_10">
										<label for="quote-sender-place" class="sr-only">From: (Sender place)</label>
										<div class="input-group">
											<i class="flaticon-truck highlight"></i>
											<input type="text" size="30" value="" name="quote-sender-place" id="quote-sender-place" class="form-control" placeholder="From: (Sender place)">
										</div>
									</div>

									<div class="bottommargin_10">
										<label for="quote-receiver-place" class="sr-only">From: (Sender place)</label>
										<div class="input-group">
											<i class="flaticon-truck highlight"></i>
											<input type="text" size="30" value="" name="quote-receiver-place" id="quote-receiver-place" class="form-control" placeholder="From: (Receiver place)">
										</div>
									</div>

									<div class="bottommargin_10">
										<label for="quote-receiver-address" class="sr-only">Receiver Address</label>
										<div class="input-group">
											<i class="flaticon-truck-1 highlight"></i>
											<input type="text" size="30" value="" name="quote-receiver-address" id="quote-receiver-address" class="form-control" placeholder="Receiver Address">
										</div>
									</div>

									<div class="bottommargin_10">
										<label for="quote-town" class="sr-only">Town / City</label>
										<div class="input-group">
											<i class="flaticon-house highlight"></i>
											<input type="text" size="30" value="" name="quote-town" id="quote-town" class="form-control" placeholder="Town / City">
										</div>
									</div>

									<div class="bottommargin_10">
										<label for="quote-country" class="sr-only">State / Country</label>
										<div class="input-group">
											<i class="flaticon-placeholder highlight"></i>
											<input type="text" size="30" value="" name="quote-country" id="quote-country" class="form-control" placeholder="State / Country">
										</div>
									</div>

									<div class="bottommargin_10">
										<label for="quote-zipcode" class="sr-only">Postal / Zipcode</label>
										<div class="input-group">
											<i class="flaticon-edit highlight"></i>
											<input type="text" size="30" value="" name="quote-zipcode" id="quote-zipcode" class="form-control" placeholder="Postal / Zipcode">
										</div>
									</div>

									<div class="bottommargin_10">
										<label for="quote-cargo" class="sr-only">Cargo Type</label>
										<div class="input-group select-group">
											<i class="flaticon-settings highlight"></i>
											<select aria-required="true" id="quote-cargo" name="quote-cargo" class="choice form-control">
												<option value="" selected data-default>Cargo Type</option>
												<option value="type1">Type 1</option>
												<option value="type2">Type 2</option>
												<option value="type3">Type 3</option>
												<option value="type4">Type 4</option>
											</select>
											<i class="rt-icon2-chevron-thin-down"></i>
										</div>
									</div>

									<div class="bottommargin_10">
										<label for="quote-shippment" class="sr-only">Shippment Type (Regular, Express)</label>
										<div class="input-group select-group">
											<i class="flaticon-glasses highlight"></i>
											<select aria-required="true" id="quote-shippment" name="quote-shippment" class="choice form-control">
												<option value="" selected data-default>Shippment Type (Regular, Express)</option>
												<option value="type1">Regular</option>
												<option value="type2">Express</option>
											</select>
											<i class="rt-icon2-chevron-thin-down"></i>
										</div>
									</div>
								</div>


								<div class="col-sm-12 text-center">
									<button type="submit" id="quote_submit" name="quote_submit" class="theme_button color2 topmargin_10">Get a quote</button>
								</div>
							</form>

						</div>
					</div>
				</div>
			</section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foter" Runat="Server">
</asp:Content>


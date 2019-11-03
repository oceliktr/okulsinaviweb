using System;
using System.Web.UI.WebControls;
using DAL;

namespace ODM
{
    public partial class OdmSinavlar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!Master.Yetki().Contains("Admin")) 
                    Response.Redirect("Giris.aspx");

                SinavVerileriniSil();

                KayitlariListele();

                SinavleriListele();
            }
        }

        private void SinavleriListele()
        {
            SinavlarDb veriDb = new SinavlarDb();
            ddlAktifSinav.DataSource = veriDb.KayitlariGetir();
            ddlAktifSinav.DataValueField = "Id";
            ddlAktifSinav.DataTextField = "SinavAdi";
            ddlAktifSinav.DataBind();
            ddlAktifSinav.Items.Insert(0, new ListItem("Sınav Seçiniz", ""));

            AyarlarDb aDb = new AyarlarDb();
            AyarlarInfo info = aDb.KayitBilgiGetir(1);
            ddlAktifSinav.SelectedValue = info.SinavId.ToString();
            hfAktifSinav.Value = info.SinavId.ToString();
            if (info.VeriGirisi == 1)
                cbVeriGirisi.Checked = true;
        }

        private void SinavVerileriniSil()
        {
            if (Request.QueryString["del"] != null)
            {
                if (Request.QueryString["del"] == "ok")
                {
                    if (Request.QueryString["id"] != null)
                    {
                        if (Request.QueryString["id"].IsInteger())
                        {
                            int sinavId = Request.QueryString["id"].ToInt32();
                            SinavlarDb dnmDb = new SinavlarDb();
                        
                            dnmDb.KayitSil(sinavId);
                            Master.UyariIslemTamam("Döneme ait kayıtlar başarıyla silindi.", phUyari);
                        }
                    }
                }
            }
        }

        private void KayitlariListele()
        {
            SinavlarDb veriDb = new SinavlarDb();
            rptKayitlar.DataSource = veriDb.KayitlariGetir();
            rptKayitlar.DataBind();
        }
        protected void rptKayitlar_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = e.CommandArgument.ToInt32();
            hfId.Value = id.ToString();
            SinavlarDb veriDb = new SinavlarDb();

            if (e.CommandName.Equals("Sil"))
            {
                if (id.ToString() == hfAktifSinav.Value)
                {
                    Master.UyariTuruncu("Aktif dönem silinemez.", phUyari);
                }
                else
                {
                    CevaplarDb cvbDb = new CevaplarDb();
                    if (cvbDb.KayitKontrol(id))
                    {
                        Master.UyariKirmizi(string.Format("Bu döneme ait kayıt olduğu için silinmesi durumunda geriye dönüşü mümkün olmayacaktır. <a href=Sinavlar.aspx?del=ok&id={0}>Yinede silmek istiyor musunuz?</a>", id), phUyari);
                    }
                    else
                    {
                        veriDb.KayitSil(id);
                        KayitlariListele();
                        Master.UyariIslemTamam("Kayıt başarıyla silindi.", phUyari);
                        FormuTemizle();
                    }
                }
                hfId.Value = "0";
                SinavleriListele();
            }
            else if (e.CommandName.Equals("Duzenle"))
            {
                SinavlarInfo info = veriDb.KayitBilgiGetir(id);
                hfId.Value = info.Id.ToString();
                txtSinav.Text = info.SinavAdi;
                txtTarih.Text = info.Tarihi.TarihYaz();

                btnKaydet.Text = "Bilgileri Değiştir";
                ltrKayitBilgi.Text = "Sınav bilgisi düzenleme formu";
            }
        }
        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            string sinavAdi = txtSinav.Text;
            DateTime tarih = txtTarih.Text.ToDateTime();
            int id = hfId.Value.ToInt32();

            SinavlarDb veriDb = new SinavlarDb();
            SinavlarInfo info = new SinavlarInfo
            {
                SinavAdi = sinavAdi,
                Tarihi = tarih
            };

            // Yeni bir kayıt ise.
            if (id == 0)
            {
                veriDb.KayitEkle(info);
                Master.UyariIslemTamam("Yeni dönem eklendi.", phUyari);
                FormuTemizle();
            }
            else
            {
                info.Id = id;
                veriDb.KayitGuncelle(info);
                Master.UyariIslemTamam("Dönem tanım bilgisi güncellendi.", phUyari);
                FormuTemizle();
            }
            KayitlariListele();
            SinavleriListele();

        }
        protected void btnVazgec_Click(object sender, EventArgs e)
        {
            FormuTemizle();
        }

        private void FormuTemizle()
        {
            hfId.Value = "0";
            txtSinav.Text = "";
            txtTarih.Text = "";
            ltrKayitBilgi.Text = "Yeni Sınav Kayıt Formu";
        }

        protected void btnAktifSinav_OnClick(object sender, EventArgs e)
        {
            if (ddlAktifSinav.SelectedValue == "")
            {
                Master.UyariTuruncu("Bir sınav seçiniz.", phUyari);
                AyarlarDb aDb = new AyarlarDb();
                AyarlarInfo info = aDb.KayitBilgiGetir(1);
                ddlAktifSinav.SelectedValue = info.SinavId.ToString();
            }
            else
            {
                int aktifSinav = ddlAktifSinav.SelectedValue.ToInt32();
                int veriGirisi = cbVeriGirisi.Checked ? 1 : 0;

                AyarlarDb veriDb = new AyarlarDb();
                veriDb.KayitGuncelle(aktifSinav, veriGirisi);
           
                Master.UyariIslemTamam("Değişiklikler kaydedildi.", phUyari);

                ddlAktifSinav.SelectedValue = aktifSinav.ToString();
                hfAktifSinav.Value = aktifSinav.ToString();
            }
        }
    }
}
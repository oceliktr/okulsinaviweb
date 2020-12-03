using Newtonsoft.Json;
using System;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Okul_SinaviYonetim_SinavYonetim : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("Default.aspx");
            }


            DonemVerileriniSil();

            KayitlariListele();

            DonemleriListele();
        }
    }

    private static bool YetkiKontrol()
    {
        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        bool yetkili = !kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin");
        return yetkili;
    }

    [WebMethod]
    public static string SinavSil(string SinavId)
    {
        int id = SinavId.ToInt32();
        JsonMesaj soList;
        if (YetkiKontrol())
        {
            soList = new JsonMesaj
            {
                Sonuc = "no",
                Mesaj = "Bunun için yetkiniz yoktur.",
            };
            return JsonConvert.SerializeObject(soList);
        }
        TestSinavlarDb sinavDb = new TestSinavlarDb();

        TestSinavlarInfo sonuc = sinavDb.KayitBilgiGetir(id);
        if (sonuc.Id == 0)
        {
            soList = new JsonMesaj
            {
                Sonuc = "no",
                Mesaj = "Kayıt bulunamadı.",
            };
            return JsonConvert.SerializeObject(soList);
        }

        int res = sinavDb.KayitSil(id);
        if (res > 0)
        {
            soList = new JsonMesaj
            {
                Sonuc = "ok",
                Mesaj = "Kayıt silindi.",
            };
            return JsonConvert.SerializeObject(soList);
        }

        soList = new JsonMesaj
        {
            Sonuc = "no",
            Mesaj = "Kayıt silinemedi.",
        };
        return JsonConvert.SerializeObject(soList);
    }




    private void DonemleriListele()
    {
        TestDonemDb veriDb = new TestDonemDb();
        ddlAktifDonem.DataSource = veriDb.KayitlariGetir();
        ddlAktifDonem.DataValueField = "Id";
        ddlAktifDonem.DataTextField = "Donem";
        ddlAktifDonem.DataBind();
        ddlAktifDonem.Items.Insert(0, new ListItem("--- Seçiniz ---", ""));

        ddlAktifDonem.SelectedValue = veriDb.AktifDonem().Id.ToString();
    }

    private void DonemVerileriniSil()
    {
        if (Request.QueryString["del"] != null)
        {
            if (Request.QueryString["del"] == "ok")
            {
                if (Request.QueryString["id"] != null)
                {
                    if (Request.QueryString["id"].IsInteger())
                    {
                        int donemId = Request.QueryString["id"].ToInt32();
                        TestDonemDb dnmDb = new TestDonemDb();
                        dnmDb.KayitSil(donemId);

                        Master.UyariIslemTamam("Döneme ait kayıtlar başarıyla silindi.", phUyari);
                    }
                }
            }
        }
    }

    private void KayitlariListele()
    {
        TestDonemDb veriDb = new TestDonemDb();
        rptKayitlar.DataSource = veriDb.KayitlariGetir();
        rptKayitlar.DataBind();
    }
    protected void rptKayitlar_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = e.CommandArgument.ToInt32();
        hfId.Value = id.ToString();
        TestDonemDb veriDb = new TestDonemDb();

        if (e.CommandName.Equals("Sil"))
        {
            if (id.ToString() == hfAktifdonem.Value)
            {
                Master.UyariTuruncu("Aktif dönem silinemez.", phUyari);
            }
            else
            {
                int kayitSayisi = 0;
                TestKutukDb kutukDb = new TestKutukDb();
                TestSinavlarDb sinavDb = new TestSinavlarDb();


                kayitSayisi += kutukDb.KayitSayisi(id);
                kayitSayisi += sinavDb.KayitSayisi(id);

                if (kayitSayisi > 0)
                {
                    Master.UyariKirmizi(string.Format("Bu döneme ait kayıt olduğu için silinmesi durumunda geriye dönüşü mümkün olmayacaktır. <a href=Donemler.aspx?del=ok&id={0}>Yinede silmek istiyor musunuz?</a>", id), phUyari);
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
            DonemleriListele();
        }
        else if (e.CommandName.Equals("Duzenle"))
        {
            TestDonemInfo info = veriDb.KayitBilgiGetir(id);
            hfId.Value = info.Id.ToString();
            txtDonem.Text = info.Donem;


            btnKaydet.Text = "Bilgileri Değiştir";
            ltrKayitBilgi.Text = "Dönem tanımı düzenleme formu";
        }
    }
    protected void btnKaydet_Click(object sender, EventArgs e)
    {
        string donem = txtDonem.Text;
        int id = hfId.Value.ToInt32();

        TestDonemDb veriDb = new TestDonemDb();
        TestDonemInfo info = new TestDonemInfo
        {
            Donem = donem
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
        DonemleriListele();

    }
    protected void btnVazgec_Click(object sender, EventArgs e)
    {
        FormuTemizle();
    }

    private void FormuTemizle()
    {
        hfId.Value = "0";
        txtDonem.Text = "";
        ltrKayitBilgi.Text = "Yeni Dönem Kayıt Formu";
        btnKaydet.Text = "Kaydet";
    }

    protected void btnAktifDonem_OnClick(object sender, EventArgs e)
    {
        TestDonemDb veriDb = new TestDonemDb();
        if (ddlAktifDonem.SelectedValue == "")
        {
            Master.UyariTuruncu("Bir dönem seçiniz.", phUyari);
            TestDonemInfo info = veriDb.KayitBilgiGetir(1);
            ddlAktifDonem.SelectedValue = info.Id.ToString();
        }
        else
        {
            int aktifDonem = ddlAktifDonem.SelectedValue.ToInt32();
            int veriGirisi = cbVeriGirisi.Checked?1:0;
            veriDb.AktifDonem(aktifDonem,veriGirisi);

            Master.UyariIslemTamam("Değişiklikler kaydedildi.", phUyari);
            KayitlariListele();
            ddlAktifDonem.SelectedValue = aktifDonem.ToString();
            hfAktifdonem.Value = aktifDonem.ToString();
        }
    }

}
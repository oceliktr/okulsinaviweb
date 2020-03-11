using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LGSSoruBank_Sorular : System.Web.UI.Page
{
    private const int sayfaKayitSayisi = 50;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Master.Yetki().Contains("Root")&& !Master.Yetki().Contains("Admin"))
                Response.Redirect("/ODM/Cikis.aspx");

            ddlSoruYazarlari.Items.Insert(0, new ListItem("Önce Branş Seçiniz", ""));

            BranslarDb brnsDb = new BranslarDb();
            ddlBrans.DataSource = brnsDb.KayitlariGetir();
            ddlBrans.DataValueField = "Id";
            ddlBrans.DataTextField = "BransAdi";
            ddlBrans.DataBind();
            ddlBrans.Items.Insert(0, new ListItem("Branş Seçiniz", ""));

            SinavlarDb sinav = new SinavlarDb();
            ddlSinavlar.DataSource = sinav.KayitlariGetir();
            ddlSinavlar.DataValueField = "Id";
            ddlSinavlar.DataTextField = "SinavAdi";
            ddlSinavlar.DataBind();
            ddlSinavlar.Items.Insert(0, new ListItem("Sınav Seçiniz", ""));

        }
    }

    private void KayitlariListele()
    {
        int sinavId = ddlSinavlar.SelectedValue.ToInt32();
        int kullaniciId = ddlSoruYazarlari.SelectedValue.ToInt32();
        int bransId = ddlBrans.SelectedValue.ToInt32();
        int sinif = ddlSinif.SelectedValue.ToInt32();
        int durum = ddlDurum.SelectedValue.ToInt32();

        LgsSorularDB veriDb = new LgsSorularDB();
        DataTable kayit = veriDb.KayitlariGetir(sinavId, kullaniciId, bransId, sinif, durum);
        PagedDataSource sayfaNo = new PagedDataSource();
        DataView dv = new DataView(kayit);
        sayfaNo.DataSource = dv;
        sayfaNo.AllowPaging = true;
        sayfaNo.PageSize = sayfaKayitSayisi;
        sayfaNo.CurrentPageIndex = SayfaNumaralama;
        if (sayfaNo.PageCount > 1)
        {
            rptSayfalar.Visible = true;
            ArrayList list = new ArrayList();
            var pageCount = sayfaNo.PageCount > 15 ? 15 : sayfaNo.PageCount;
            for (int i = 0; i < pageCount; i++)
            {
                list.Add((i + 1).ToString());
            }

            if (pageCount >= 15)
            {
                ddlEkSayfalar.Visible = true;
                ddlEkSayfalar.DataSource = EkSayfalarOlustur(sayfaNo.PageCount);
                ddlEkSayfalar.DataTextField = "SayfaTextField";
                ddlEkSayfalar.DataValueField = "SayfaValueField";
                ddlEkSayfalar.DataBind();
            }

            rptSayfalar.DataSource = list;
            rptSayfalar.DataBind();
        }
        else
        {
            rptSayfalar.Visible = false;
        }

        rptKayitlar.DataSource = sayfaNo;
        rptKayitlar.DataBind();
    }

    static DataRow YeniRow(string text, string value, DataTable dt)
    {
        DataRow row = dt.NewRow();
        row[0] = text;
        row[1] = value;
        return row;
    }

    static ICollection EkSayfalarOlustur(int sayi)
    {
        DataTable dt = new DataTable();

        dt.Columns.Add(new DataColumn("SayfaTextField", typeof(String)));
        dt.Columns.Add(new DataColumn("SayfaValueField", typeof(String)));

        dt.Rows.Add(YeniRow("Sayfa", "0", dt));
        for (int i = 16; i < sayi; i++)
        {
            dt.Rows.Add(YeniRow(i.ToString(), i.ToString(), dt));
        }

        DataView dv = new DataView(dt);
        return dv;
    }

    public int SayfaNumaralama
    {
        get
        {
            if (ViewState["SayfaNumaralama"] != null)
                return Convert.ToInt32(ViewState["SayfaNumaralama"]);
            else
                return 0;
        }
        set { ViewState["SayfaNumaralama"] = value; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        rptSayfalar.ItemCommand += rptSayfalar_ItemCommand;

    }

    protected void rptSayfalar_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        SayfaNumaralama = Convert.ToInt32(e.CommandArgument) - 1;
        KayitlariListele();
    }

    protected void ddlEkSayfalar_SelectedIndexChanged(object sender, EventArgs e)
    {
        SayfaNumaralama = ddlEkSayfalar.SelectedValue.ToInt32();
        ddlEkSayfalar.SelectedValue = ddlEkSayfalar.SelectedValue;

        KayitlariListele();
    }

    protected void rptKayitlar_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int soruId = e.CommandArgument.ToInt32();

        LgsSorularDB mkDb = new LgsSorularDB();
        LgsSorularInfo info = mkDb.KayitBilgiGetir(soruId);

        if (e.CommandName.Equals("Sil"))
        {
            DizinIslemleri.DosyaSil(Server.MapPath(info.SoruUrl));

            mkDb.KayitSil(soruId);

            Master.UyariIslemTamam("Soru başarıyla silindi.", phUyari);
            KayitlariListele();
        }

        if (e.CommandName.Equals("Download"))
        {
            LgsKazanimlarDb kznmDb = new LgsKazanimlarDb();
            LgsKazanimlarInfo kznmInfo = kznmDb.KayitBilgiGetir(info.KazanimId);

            //File to be downloaded.
            string filePath = Server.MapPath(info.SoruUrl);

            //Set the New File name.
            string newFileName =string.Format("{0}-SoruNo-{1}.docx", kznmInfo.KazanimNo,info.Id);

            //Setting the Content Type, Header and the new File name.
            Response.ContentType = "application/msword";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + newFileName);

            //Writing the File to Response Stream.
            Response.WriteFile(filePath);
            Response.Flush();
            Response.End();
        }
    }

    protected void btnListele_OnClick(object sender, EventArgs e)
    {
        KayitlariListele();
    }

    protected void rptKayitlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", SayfaNumaralama, sayfaKayitSayisi);
            int onay = DataBinder.Eval(e.Item.DataItem, "Onay").ToInt32();

            CheckBox cbOnay = (CheckBox)e.Item.FindControl("cbOnay");
            cbOnay.Checked = onay == 1;
        }
    }

    protected void ddlBrans_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int bransId = ddlBrans.SelectedValue.ToInt32();

        KullanicilarDb kDb = new KullanicilarDb();

        ddlSoruYazarlari.DataSource = kDb.OgretmenleriGetir("LgsYazari", bransId);
        ddlSoruYazarlari.DataValueField = "Id";
        ddlSoruYazarlari.DataTextField = "AdiSoyadi";
        ddlSoruYazarlari.DataBind();
        ddlSoruYazarlari.Items.Insert(0, new ListItem("Soru Yazarı Seçiniz", ""));
    }



    protected void cbOnay_OnCheckedChanged(object sender, EventArgs e)
    {

        CheckBox chk = (CheckBox)sender;
        RepeaterItem item = (RepeaterItem)chk.NamingContainer;
        HiddenField hfId = (HiddenField)item.FindControl("hfId");
        Literal ltrOnay = (Literal)item.FindControl("ltrOnay");
        LgsSorularDB mkDb = new LgsSorularDB();
        int id = hfId.Value.ToInt32();

        if (chk.Checked)
        {
            mkDb.KayitOnay(id, 1);
            ltrOnay.Text = 1.LgsDurumBul();
        }
        else
        {
            mkDb.KayitOnay(id, 0);
            ltrOnay.Text = 0.LgsDurumBul();
        }
    }
}
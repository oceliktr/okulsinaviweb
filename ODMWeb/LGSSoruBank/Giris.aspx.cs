﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class LGSSoruBank_Giris : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

       if (Master.UyeId() == 0) Response.Redirect("~/ODM/Cikis.aspx");
        int uyeId = Master.UyeId();

        KullanicilarDb kDb = new KullanicilarDb();
        KullanicilarInfo kInfo = kDb.KayitBilgiGetir(uyeId);
        if (string.IsNullOrEmpty(kInfo.AdiSoyadi))
            ltrKullaniciAdi.Text = "DİKKAT:Bilgilerim bölümünden isminizi giriniz";
        else
            ltrKullaniciAdi.Text = kInfo.AdiSoyadi;

        SinavlarDb sDb = new SinavlarDb();
        SinavlarInfo sinf = sDb.AktifSinavAdi();
        ltrDonemAdi.Text = sinf.SinavAdi;
        int sinavId = sinf.SinavId;
        if (sinf.VeriGirisi == 0)
        {
            Master.UyariTuruncu(string.Format("<b>{0}</b> için veri girişleri kapatıldı.", sinf.SinavAdi), phUyari);
        }

        string[] yetkiler = Master.Yetki().Split(Convert.ToChar("|"));
        foreach (var yt in yetkiler)
        {
            if (yt == "Ogretmen")
                ltrYetkiler.Text += "<li>Puanlayıcı Öğretmen</li>";
            if (yt == "UstDegerlendirici")
                ltrYetkiler.Text += "<li>Üst Puanlayıcı Öğretmen</li>";
            if (yt == "OkulYetkilisi")
                ltrYetkiler.Text += "<li>Okul Modül yöneticisi</li>";
            if (yt == "IlceMEMYetkilisi")
                ltrYetkiler.Text += "<li>İlçe MEM Modül yöneticisi</li>";
            if (yt == "Admin")
                ltrYetkiler.Text += "<li>Modül yöneticisi</li>";
        }

        if (Master.Yetki().Contains("Admin") && Master.Yetki().Contains("UstDegerlendirici") && Master.Yetki().Contains("Ogretmen"))
        {
            divPuanlamaGrafik.Visible = true;
        }
        SonucAuDB cvpDb = new SonucAuDB();
        int toplamSoruSayisi = cvpDb.CevaplanacakCkSayisi(sinavId);
        int toplamSoruSayisiNet = toplamSoruSayisi == 0 ? 1 : toplamSoruSayisi;

        int cevaplananA = cvpDb.CevaplananCkSayisi(sinavId, "A");
        int cevaplananB = cvpDb.CevaplananCkSayisi(sinavId, "B");


        int ustPckSayisi = cvpDb.UstPCKSayisi(sinavId);
        int ustPckSayisiNet = ustPckSayisi == 0 ? 1 : ustPckSayisi;

        int ustPCevaplananCkSayisi = cvpDb.UstPCevaplananCKSayisi(sinavId);

        int oranA = (cevaplananA * 100) / toplamSoruSayisiNet;
        int oranB = (cevaplananB * 100) / toplamSoruSayisiNet;
        int oranUp = (ustPCevaplananCkSayisi * 100) / ustPckSayisiNet;
        txtAGrubu.Text = oranA.ToString();
        ltrAGrup.Text = string.Format("A Grubu Kalan : {0}", toplamSoruSayisi - cevaplananA);
        txtBGrubu.Text = oranB.ToString();
        ltrBGrup.Text = string.Format("B Grubu Kalan : {0}", toplamSoruSayisi - cevaplananB);
        txtUstP.Text = oranUp.ToString();
        ltrUstP.Text = string.Format("Üst Puanlayıcı Kalan : {0}", ustPckSayisi - ustPCevaplananCkSayisi);

    }

}
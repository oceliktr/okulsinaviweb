using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ErzurumOdmMvc.Areas.ODM.Model;
using ErzurumOdmMvc.Business.Abstract;

namespace ErzurumOdmMvc.Business.ViewManager
{
    public class KullaniciBilgileriViewManager : ManagerBase<KullaniciBilgileriViewModel>
    {
        public Task<KullaniciBilgileriViewModel> Bilgilerim(string tcKimlik)
        {
            string sql = @"SELECT i.IlceAdi,k.KurumAdi,b.BransAdi,u.* from kullanicilar AS u
                        INNER JOIN kurumlar AS k ON u.KurumKodu=k.KurumKodu
                        INNER JOIN ilceler AS i ON u.IlceId=i.Id
                        LEFT JOIN branslar AS b ON u.Bransi=b.Id
                        where u.TcKimlik=@TcKimlik";
            Task<KullaniciBilgileriViewModel> res = FindAsync(sql, new { TcKimlik = tcKimlik });

            return res;
        }
        public Task<IEnumerable<KullaniciBilgileriViewModel>> KurumunKullanicilari(int kurumKodu)
        {
            string sql = @"SELECT i.IlceAdi,k.KurumAdi,b.BransAdi,u.* from kullanicilar AS u
                        INNER JOIN kurumlar AS k ON u.KurumKodu=k.KurumKodu
                        INNER JOIN ilceler AS i ON u.IlceId=i.Id
                        LEFT JOIN branslar AS b ON u.Bransi=b.Id
                        where u.KurumKodu=@KurumKodu ORDER BY u.AdiSoyadi";
            Task<IEnumerable<KullaniciBilgileriViewModel>> list = QueryAsync(sql, new { KurumKodu = kurumKodu });

            return list;
        }
        public Task<IEnumerable<KullaniciBilgileriViewModel>> Kullanicilar(int ilceId, int kurumKodu, int bransId, string yetki)
        {
            string sql = @"SELECT i.IlceAdi,k.KurumAdi,b.BransAdi,u.* from kullanicilar AS u
                        INNER JOIN kurumlar AS k ON u.KurumKodu=k.KurumKodu
                        INNER JOIN ilceler AS i ON u.IlceId=i.Id
                        LEFT JOIN branslar AS b ON u.Bransi=b.Id
                        where u.Id<>0";
            if (ilceId != 0)
                sql += " and u.IlceId=@IlceId";
            if (kurumKodu != 0)
                sql += " and u.KurumKodu=@KurumKodu";
            if (bransId != 0)
                sql += " and u.Bransi=@Bransi";
            if (!string.IsNullOrEmpty(yetki))
                sql += " and u.Yetki LIKE @Yetki";

            Task<IEnumerable<KullaniciBilgileriViewModel>> list = QueryAsync(sql, new { IlceId = ilceId, KurumKodu = kurumKodu, Bransi = bransId, Yetki = "%" + yetki + "%" });

            return list;
        }
    }
}
using ErzurumOdmMvc.Business.Abstract;
using ErzurumOdmMvc.Common.Library;
using ErzurumOdmMvc.Entities;
using ErzurumOdmMvcDAL.Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErzurumOdmMvc.Business
{
    public class KullaniciManager : ManagerBase<Kullanici>
    {
        private readonly Repository<Kullanici> repo = new Repository<Kullanici>();

        public async Task<BusinessResult<Kullanici>> Giris(string kurumKodu, string sifre,string guid)
        {

            BusinessResult<Kullanici> res = new BusinessResult<Kullanici>();

            string kurumKoduStr = kurumKodu.ToTemizMetin();
            string password = sifre.ToTemizMetin().MD5Sifrele();

            string sql = "Select * from kullanicilar where TcKimlik=@TcKimlik and Sifre=@Sifre";

            res.Result = await repo.QueryFirstOrDefaultAsync(sql, new { TcKimlik = kurumKoduStr, Sifre = password });

            if (res.Result == null)
            {
                res.Ekle("Giriş bilgisi ve şifre uyuşmuyor.");
            }
            else
            {
                res.Result.OncekiGiris = res.Result.SonGiris;
                res.Result.SonGiris = TarihIslemleri.YerelTarih();
                res.Result.GirisSayisi += 1;
                res.Result.GirisKodu = guid; 
                await UpdateAsync(res.Result);

            }

            return res;
        }
        public Kullanici KullaniciBilgisi(string girisKodu)
        {
            string sql = "Select * from kullanicilar where GirisKodu=@GirisKodu"; 
            Kullanici res = repo.QueryFirstOrDefault(sql, new { GirisKodu = girisKodu });
            return res;
        }
        //
    
        public Task<IEnumerable<Kullanici>> Kullanicilar(int ilceId, int kurumKodu, int bransId, string yetki)
        {
            string sql = @"SELECT * from kullanicilar AS u where u.Id<>0";
            if (ilceId != 0)
                sql += " and u.IlceId=@IlceId";
            if (kurumKodu != 0)
                sql += " and u.KurumKodu=@KurumKodu";
            if (bransId != 0)
                sql += " and u.Bransi=@Bransi";
            if (!string.IsNullOrEmpty(yetki))
                sql += " and u.Yetki LIKE @Yetki";

            Task<IEnumerable<Kullanici>> list = QueryAsync(sql, new { IlceId = ilceId, KurumKodu = kurumKodu, Bransi = bransId, Yetki = "%" + yetki + "%" });

            return list;
        }
        //Şifremi unuttum alanında kurum.Id=Kurumkodu ve tc kimlik kontrolü
        public int KullaniciKontrol(int kurumId, int kurumKodu, string tcKimlik)
        {
            string sql = @"SELECT u.Id from kullanicilar AS u
                            INNER JOIN kurumlar on kurumlar.Id=@KurumId 
                            where (u.KurumKodu=@KurumKodu and u.TcKimlik=@TcKimlik)";
            var resultList = FindAsync(sql, new { KurumId = kurumId, KurumKodu = kurumKodu, TcKimlik=tcKimlik });
            if (resultList.Result==null)
            {
                return 0;
            }
            return resultList.Result.Id;

        }
        //Kurumda kullanıcı var mı kontrolü
        public bool KullaniciKontrol(int kurumKodu)
        {
            var resultList = Scalar("SELECT Id FROM kullanicilar WHERE KurumKodu=@KurumKodu", new { KurumKodu = kurumKodu });

            return resultList.ToInt32() != 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Yeni kayıt kontrolü için 0 girilmeli</param>
        /// <param name="tcKimlik"></param>
        /// <returns></returns>
        public bool KullaniciKontrol(int id, string tcKimlik)
        {
            string sql = "Select * from kullanicilar where TcKimlik=@TcKimlik and Id<>@Id";
            var res = repo.Scalar(sql, new { TcKimlik = tcKimlik, Id = id });
            return res.ToInt32() != 0;
        }
        public bool SifreDegistir(Kullanici kullanici, string sifre)
        {
            string password = sifre.ToTemizMetin().MD5Sifrele();
            kullanici.Sifre = password;

            return Update(kullanici);
        }

        public IEnumerable<Kullanici> OkulKullanicilari(int kurumKodu)
        {
            string sql = "SELECT * FROM kullanicilar WHERE KurumKodu=@KurumKodu ORDER BY AdiSoyadi";
            IEnumerable<Kullanici> list = Query(sql, new {KurumKodu = kurumKodu});

            return list;
        }
    }
}

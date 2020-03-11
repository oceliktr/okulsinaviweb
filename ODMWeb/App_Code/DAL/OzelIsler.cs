using System;
using MySql.Data.MySqlClient;


    public static class OzelIsler
    {
        public static string GetMyMetin(this MySqlDataReader dr, string alanAdi)
        {
            string gonder = String.Empty;

            try
            {
                gonder = dr.GetString(dr.GetOrdinal(alanAdi));
            }
            catch (Exception)
            {
                //throw new Exception("Bir hata oldu"+ ex.Message); 
            }
            return gonder;
        }

        public static int GetMySayi(this MySqlDataReader dr, string alanAdi)
        {
            int gonder = 0;

            try
            {
                gonder = dr.GetInt32(dr.GetOrdinal(alanAdi));
            }
            catch (Exception)
            {
            }
            return gonder;
        }
        public static DateTime GetMyTarih(this MySqlDataReader dr, string alanAdi)
        {
            DateTime gonder = DateTime.Now;

            try
            {
                gonder = dr.GetDateTime(dr.GetOrdinal(alanAdi));
            }
            catch (Exception)
            {
            }
            return gonder;
        }
        public static bool GetMyBool(this MySqlDataReader dr, string alanAdi)
        {
            bool gonder = false;

            try
            {
                gonder = dr.GetBoolean(dr.GetOrdinal(alanAdi));
            }
            catch (Exception)
            {
            }
            return gonder;
        }
        public static double GetMyDouble(this MySqlDataReader dr, string alanAdi)
        {
            double gonder = 0;

            try
            {
                gonder = dr.GetDouble(dr.GetOrdinal(alanAdi));
            }
            catch (Exception)
            {//
            }
            return gonder;
        }

        public static decimal GetMyDecimal(this MySqlDataReader dr, string alanAdi)
        {
            decimal gonder = 0;

            try
            {
                gonder = dr.GetDecimal(dr.GetOrdinal(alanAdi));
            }
            catch (Exception)
            {
            }
            return gonder;
        }

        public static long GetMyLong(this MySqlDataReader dr, string alanAdi)
        {
            long gonder = 0;

            try
            {
                gonder = dr.GetInt64(dr.GetOrdinal(alanAdi));
            }
            catch (Exception)
            {
            }
            return gonder;
        }
    }



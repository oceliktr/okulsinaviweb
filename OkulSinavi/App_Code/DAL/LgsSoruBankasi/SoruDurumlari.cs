
    /// <summary>
    /// Summary description for SoruDurumlari
    /// </summary>
    public static class SoruDurumlari
    {
        public enum LgsDurum
        {
            Yeni = 0,
            Incelendi = 1
        }
        public static string LgsDurumBul(this int d)
        {
            string s = "";
            switch (d)
            {
                case 0:
                    s = "<span class=\"label label-info pull-right\">Henüz İncelenmedi</span>";
                    break;
                case 1:
                    s = "<span class=\"label label-success pull-right\">İncelendi</span>";
                    break;
            }

            return s;
        }

    }

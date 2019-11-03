using System;
using System.Net.Sockets;

namespace ODM.Kutuphanem
{
    class AgIslemleri
    {
        public static bool InternetControl()
        {
            try
            {
                TcpClient tcp = new TcpClient("www.google.com.tr", 80);
                tcp.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool InternetControl(string webAdresi)
        {
            try
            {
                TcpClient tcp = new TcpClient(webAdresi, 80);
                tcp.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

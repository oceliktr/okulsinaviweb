using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb
{
    static class RandomExtensions
    {
        /// <summary>
        ///Listeden rastgele bir öğe veya liste boşsa null değerini döndürür.
        /// </summary>
        /// <typeparam name="T">Numaralanan nesnenin türü</typeparam>
        /// <param name="rand">Rasgele sayı üreteci örneği</param>
        /// <returns>Bir listeden rastgele öğe veya liste boşsa null döndürür</returns>
        public static T Random<T>(this IEnumerable<T> list, Random rand)
        {
            if (list != null && list.Count() > 0)
                return list.ElementAt(rand.Next(list.Count()));
            return default(T);
        }

        /// <summary>
        /// IEnumerable tipinde karışık olarak döndürür.
        /// </summary>
        /// <typeparam name="T">Numaralanan nesnenin türü</typeparam>
        /// <returns>Kaynak öğelerin karıştırılmış bir kopyası</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(new Random());
        }

        /// <summary>
        /// Karışık karıştırılmış bir IEnumerable döndürür.
        /// </summary>
        /// <typeparam name="T">Numaralanan nesnenin türü</typeparam>
        /// <param name="rand">Rasgele sayı üreteci örneği</param>
        /// <returns>Kaynak öğelerin karıştırılmış bir kopyası</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rand)
        {
            var list = source.ToList();
            list.Shuffle(rand);
            return list;
        }

        /// <summary>
        /// Bir IList'i yerinde karıştırır.
        /// </summary>
        /// <typeparam name="T">Listedeki öğelerin türü</typeparam>
        public static void Shuffle<T>(this IList<T> list)
        {
            list.Shuffle(new Random());
        }

        /// <summary>
        /// Bir IList'i yerinde karıştırır.
        /// </summary>
        /// <typeparam name="T">Listedeki öğelerin türü</typeparam>
        /// <param name="rand">Rasgele sayı üreteci örneği</param>
        public static void Shuffle<T>(this IList<T> list, Random rand)
        {
            int count = list.Count;
            while (count > 1)
            {
                int i = rand.Next(count--);
                T temp = list[count];
                list[count] = list[i];
                list[i] = temp;
            }
        }
    }
}

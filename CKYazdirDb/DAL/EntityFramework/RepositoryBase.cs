
namespace ODM.CKYazdirDb.DAL
{
    public class RepositoryBase
    {
        //Singleton patern örneği DatabaseContext clası birden fazla new lenmemesi için önlem
        protected static DatabaseContext context;
        private static readonly object _lockSync = new object();
        public RepositoryBase()
        {
            CreateContext();
        }
        private static void CreateContext()
        {
            if (context == null)
            {
                //multi threat uygulamalar için ekstra önlem.
                //bu sayede multi threat aynı anda girip new leme yapamazlar
                lock (_lockSync)
                {
                    if (context == null)
                    {
                        context = new DatabaseContext();
                    }
                }
            }
        }
    }
}

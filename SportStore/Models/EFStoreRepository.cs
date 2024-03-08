namespace SportStore.Models
{
    public class EFStoreRepository:IStoreRepositiry
    {
        private StoreDbContext context;

        public EFStoreRepository(StoreDbContext ctx) 
        {
            context = ctx;
        }

        public IQueryable<Product> Products=>context.Products;
    }
}

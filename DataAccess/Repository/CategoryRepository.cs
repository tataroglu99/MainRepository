using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(dbShoppingEntities dataContextFactory) : base(dataContextFactory)
        {
        }

        public List<Category> listActiveProducts()
        {
            List<Category> products = GetAllWithExpression(x => x.IsDeleted, true);
            return products;
        }
    }

    
}

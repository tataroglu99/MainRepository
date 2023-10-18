using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CartRepository : GenericRepository<Cart>
    {
        public CartRepository(dbShoppingEntities dataContextFactory) : base(dataContextFactory)
        {
        }

        public List<Cart> listCarts()
        {
            List<Cart> tempData = All(true).OrderBy(x => x.Product.Name).ToList();
            return tempData;
        }
    }
}

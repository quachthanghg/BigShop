using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Data.Infrastructure
{
    public interface IDbFactory
    {
        BigShopDbContext Init();
    }
}

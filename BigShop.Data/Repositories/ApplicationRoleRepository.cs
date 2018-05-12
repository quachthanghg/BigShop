using BigShop.Data.Infrastructure;
using BigShop.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace BigShop.Data.Repositories
{
    public interface IApplicationRoleRepository : IRepository<ApplicationRole>
    {
        IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupID);
    }

    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupID)
        {
            var query = from g in DbContext.ApplicationRoles
                        join ug in DbContext.ApplicationRoleGroups
                        on g.Id equals ug.RoleID
                        where ug.GroupID == groupID
                        select g;

            return query;
        }
    }
}
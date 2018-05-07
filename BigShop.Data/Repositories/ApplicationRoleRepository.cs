using BigShop.Data.Infrastructure;
using BigShop.Model.Models;
using System.Collections.Generic;

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
            //var query = from g in DbContext.ApplicationRoles
            //            join ug in DbContext.ApplicationRoleGroups
            //            on g.Id equals ug.RoleId
            //            where ug.GroupId == groupId
            //            select g;
            //return query;
            throw new System.NotImplementedException();
        }
    }
}
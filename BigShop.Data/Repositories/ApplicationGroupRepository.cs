using BigShop.Data.Infrastructure;
using BigShop.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace BigShop.Data.Repositories
{
    public interface IApplicationGroupRepository : IRepository<ApplicationGroup>
    {
        IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId);
        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);
    }

    public class ApplicationGroupRepository : RepositoryBase<ApplicationGroup>, IApplicationGroupRepository
    {
        public ApplicationGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ApplicationGroup> GetListGroupByUserId(string userID)
        {
            var query = from g in DbContext.ApplicationGroups
                        join ug in DbContext.ApplicationUserGroups
                        on g.ID equals ug.GroupID
                        where ug.UserID == userID
                        select g;
            return query;
            throw new System.NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(int groupID)
        {
            var query = from g in DbContext.ApplicationGroups
                        join ug in DbContext.ApplicationUserGroups
                        on g.ID equals ug.GroupID
                        join u in DbContext.Users
                        on ug.UserID equals u.Id
                        where ug.GroupID == groupID
                        select u;
            return query;
        }
    }
}
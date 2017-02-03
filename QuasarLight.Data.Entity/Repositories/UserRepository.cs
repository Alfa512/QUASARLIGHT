using QuasarLight.Common.Contracts.Repositories;
using QuasarLight.Data.Model.DataModel;

namespace QuasarLight.Data.Entity.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
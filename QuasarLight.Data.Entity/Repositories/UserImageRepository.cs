using QuasarLight.Common.Contracts.Repositories;
using QuasarLight.Data.Model.DataModel;

namespace QuasarLight.Data.Entity.Repositories
{
    public class UserImageRepository : GenericRepository<UserImage>, IUserImageRepository
    {
        public UserImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
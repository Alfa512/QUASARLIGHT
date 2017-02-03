using QuasarLight.Common.Contracts.Repositories;
using QuasarLight.Data.Model.DataModel;

namespace QuasarLight.Data.Entity.Repositories
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
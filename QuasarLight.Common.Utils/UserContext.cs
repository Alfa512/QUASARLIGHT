using Microsoft.Owin;
using QuasarLight.Common.Contracts.Application;

namespace QuasarLight.Common.Utils
{
    public class UserContext : IUserContext
    {
        public UserContext(IOwinContext ctx)
        {
            _context = ctx;
        }

        public string UserId
        {
            get
            {
                return _context.Get<string>("Id");
            }
        }

        private readonly IOwinContext _context;

    }
}
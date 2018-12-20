using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models
{
    public interface IWebUserRepository
    {
        IEnumerable<WebUser> GetAllWebUsers();

        WebUser GetWebUserById(int uid);
    }

    public class WebUserRepository : IWebUserRepository
    {
        private AppDbContext _appDbContext;

        public WebUserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<WebUser> GetAllWebUsers() {
            return _appDbContext.WebUsers;
        }

        public WebUser GetWebUserById(int uid)
        {
            return _appDbContext.WebUsers.FirstOrDefault(p => p.Id == uid);
        }
    }
}

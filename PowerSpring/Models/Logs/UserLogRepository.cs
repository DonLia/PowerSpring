using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models.Logs
{
    public class UserLogRepository : IUserLogRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserLogRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void CreateLog(UserLog userLog)
        {
            _appDbContext.Logs.Add(userLog);
            _appDbContext.SaveChanges();
        }
        public IEnumerable<UserLog> SelectLogById(int id)
        {
            return _appDbContext.Logs.Where(p => p.UserId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerSpring.Models.Logs
{
    public interface IUserLogRepository
    {
        void CreateLog(UserLog userLog);
        IEnumerable<UserLog> SelectLogById(int id);
    }
}

using System.Collections.Generic;
using PowerSpring.Models;

namespace PowerSpring.Helper
{
    public interface IUserManager
    {
        WebUser Authenticate(string UserName, string password);
        WebUser Create(string username, string password);
        void Delete(int id);
        IEnumerable<WebUser> GetAll();
        WebUser GetById(int id);
        void Update(WebUser userParam, string password = null);
    }
}
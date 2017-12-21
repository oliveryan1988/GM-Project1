using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.Model.Models;
using LincolnBrandChampion.DL.Repositories;


namespace LincolnBrandChampion.BL.Persisters
{
    public class UsuarioBL
    {
        public UserModel GetUserBy(string wslxId)
        {
            UserRepository _user = new UserRepository();
            return _user.GetUserBy(wslxId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LincolnBrandChampion.Model.Models;
using LincolnBrandChampion.BL.Persisters;

namespace LincolnBrandChampion.Web.Models
{
    public class ModelUser:UserModel
    {
        public ModelUser()
        {
            
        }

        public UserModel GetUserBy(string wslxId)
        {
            UsuarioBL _usuarioBl = new UsuarioBL();
            return _usuarioBl.GetUserBy(wslxId);
        }
    }
}
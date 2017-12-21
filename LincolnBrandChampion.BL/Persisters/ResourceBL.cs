using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.DL.Repositories;
using LincolnBrandChampion.DL.Properties;
using LincolnBrandChampion.Model.Models;

namespace LincolnBrandChampion.BL.Persisters
{
    public class ResourceBL
    {
        ResourceRepository _resource = new ResourceRepository();

        public void SaveResourceTracking(ResourceTracking model)
        {
             _resource.SaveResourceTracking(model);
        }
    }
}

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
    public class AttendanceInfoBL
    {
        public AttendanceReportModel GetAttendanceInfoByStarsId(string starsId)
        {
            AttendanceInfoRepository _attendance = new AttendanceInfoRepository();

            return _attendance.GetAttendanceInfoByStarsId(starsId);

        }
        public bool UpdateAttendanceInfo(AttendanceReportModel model)
        {
            AttendanceInfoRepository _attend = new AttendanceInfoRepository();
            return _attend.UpdateAttendanceInfo(model);
        }
    }
}

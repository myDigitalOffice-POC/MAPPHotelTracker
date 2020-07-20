using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAPPOnBoardingStats;

namespace MAPPHotelTracker.Models
{
    public class HotelAuditInfo
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string OrganizationMDOEmail { get; set; }
        public string ReportSubmissionMethod { get; set; }
        public string HotelMDOEmail { get; set; }
        public int HotelId { get; set; }
        public string HotelCode { get; set; }
        public string HotelName { get; set; }
        public string PMS { get; set; }
        public List<EmailAuditInfo> EmailAuditInfoList { get; set; }
        
    }
}

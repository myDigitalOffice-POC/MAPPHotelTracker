using System.Collections.Generic;

namespace MAPPHotelTracker.Models
{
    public class EmailAuditInfo
    {
        public string DateReceived { get; set; }
        public string FromEmailAddress { get; set; }
        public string ToEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ContainerFolder { get; set; }
        internal List<string> Attachments { get; set; }
    }
}

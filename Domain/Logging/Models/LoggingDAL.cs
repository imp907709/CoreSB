using System;
using CoreSB.Universal;

namespace CoreSB.Domain.Logging.Models
{
    public class LoggingDAL : EntityIntIdDAL
    {
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }
}

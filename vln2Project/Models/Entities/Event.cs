using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h37.Models.Entities
{
    public class Event
    {
        public enum eventType { created = 0, modified = 1 }

        public int userID { get; set; }
        public DateTime timestamp { get; set; }
        public eventType type { get; set; }
    }
}
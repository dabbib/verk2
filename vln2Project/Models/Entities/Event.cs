using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace h37.Models.Entities
{
    public class Event
    {
        public Event(string userID, int fileID, DateTime timestamp, eventType type)
        {
            this.userID = userID;
            this.fileID = fileID;
            this.timestamp = timestamp;
            this.type = type;
        }
        public enum eventType { created = 0, modified = 1 }

        public int eventID { get; set; }
        public string userID { get; set; }
        public DateTime timestamp { get; set; }
        public eventType type { get; set; }
        public int fileID { get; set; }
    }
}
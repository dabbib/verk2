using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static h37.Models.Entities.Event;

namespace h37.Models.ViewModels
{
    public class EventLogViewModel
    {
        public EventLogViewModel()
        {

        }
        public DateTime timestamp { get; set; }
        public string userName { get; set; }
        public eventType type { get; set; }
        public string fileName { get; set; }

    }
}
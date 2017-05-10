using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h37.Models.ViewModels
{
    public class FileCreateViewModel
    {
        public int projectID { get; set; }
        public string userID { get; set; }
        public string fileName { get; set; }
    }
}
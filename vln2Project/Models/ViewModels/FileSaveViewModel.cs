using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h37.Models.ViewModels
{
    public class FileSaveViewModel
    {
        public string content { get; set; }
        public int fileID { get; set; }
        public int projectID { get; set; }
    }
}
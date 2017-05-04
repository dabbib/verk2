using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h37.Models.Entities
{
    public class File
    {
        /// <summary>
        /// Entity class for Files
        /// </summary>
        public int fileID { get; set; }
        public string fileName { get; set; }
        public string fileType { get; set; }
       
    }
}
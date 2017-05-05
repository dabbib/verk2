using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h37.Models.Entities
{
    public class File
    {
        public File(string fileName, string fileType, int projectID)
        {
            this.fileName = fileName;
            this.fileType = fileType;
            this.projectID = projectID;
        }
        /// <summary>
        /// Entity class for Files
        /// </summary>
        public int fileID { get; set; }
        public string fileName { get; set; }
        public string fileType { get; set; }
        public int projectID { get; set; }
        /* Todo create field for the content of the file ! */
       
    }
}
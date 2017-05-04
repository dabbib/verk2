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
        public int fileid { get; set; }
        public string filename { get; set; }
        public string filetype { get; set; }
       
    }
}
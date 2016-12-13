using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class PuppyMark
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GeoLong { get; set; } // for google maps
        public string GeoLat { get; set; } // for google maps
    }
}
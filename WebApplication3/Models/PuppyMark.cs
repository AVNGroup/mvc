using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class PuppyMark
    {
        private int Id { get; set; }
        private string Name { get; set; }
        private string GeoLong { get; set; } // for google maps
        private string GeoLat { get; set; } // for google maps
    }
}